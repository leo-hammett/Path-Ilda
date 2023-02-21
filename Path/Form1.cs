using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using HeliosLaserDAC;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Linq.Expressions;
//project.dynamicPath = the file & whole animation
//framePath = the PathLineFrame array for the individual frame

//REMEMBER THAT ALL SHAPES SHOULD BE STORED IN A 4095 CO-OORDINATE RESOLUTION NOT THE RESOLUTION OF THE SCREEN.

namespace Path
{
    public partial class PathMainWindow : Form
    {
        PathProject project = new PathProject();
        List<PathLineFrame> framePath;  //What individual frames should look like. Should hopefully be generateable from a for loop and get line at project.dynamicPath[i]
        int framePathTime = -1;  //Gives the time the framePath is generated for

        int mainTime = 0;  //The time in frames that the system is at

        Point mouseLastLocation;  //If set to -1,-1 no line preview should be made - Saving where the mouse went down
        Point timelineMouseLastLocation;

        List<KeyLinePoint> closestPoints = new List<KeyLinePoint>();
        List<KeyLinePoint> closestPointsFrozen = new List<KeyLinePoint>();

        Point closestPoint;

        bool currentlyPlaying = false;

        Point tempIntermediateFramePoint = new Point(-1, -1);

        int selectedLineDynamicIndex = 0;
        PathLineFrame selectedFrameReadOnly;
        int selectedPointDynamicIndex = -1;

        Queue<PathLineFrame> laserTraversalPath;  //Used for converting frames into a path we can make projections for.
        List<HeliosPoint> laserPoints = new List<HeliosPoint>();

        bool showCircles = false;
        bool showLines = false;
        bool mouseDown = false;

        //Timeline GUI variables
        public TimelineSettings currentTimelineSettings;
        TimeLinePoint timelineClosestPoint;
        List<TimeLinePoint> TimelineDots = new List<TimeLinePoint>();

        //Helios Laser management variables
        public HeliosDac helios = new HeliosDac();
        List<HeliosPoint> heliosLaserPoints = new List<HeliosPoint>();
        public LaserSettings currentLaserSettings = new LaserSettings();


        public PathMainWindow()
        {
            InitializeComponent();
            //Initialising Variables
            project.dynamicPath = new List<PathLine>();
            HeliosDac helios = new HeliosDac();
            framePath = new List<PathLineFrame>();
            mainTime = 0;
            DrawerColorDialog.Color = Color.White;
            OptionsDrawLineMode.Checked = true;
            currentTimelineSettings = new TimelineSettings();
            InformationPreviewModeData.Text = helios.openDevices().ToString();
            backgroundWorker1.RunWorkerAsync();
        }
        class PathProject
        {
            public List<PathLine> dynamicPath { get; set; }  = new List<PathLine>(); //The whole animation, all of the shapes etc.

            public float maxtimeSeconds { get; set; }  = 25;

            public int fps { get; set; } = 30; //The number of frames per second (to obtain time in seconds divide time by fps)
            public string fileHash { get; set; }    //Used to see if file has been modified outside of the software.
        }
        public class LaserSettings
        {
            public int Kpps { get; set; }  //The amount of points per second the laser can travel across.
            public int MaxVelocity { get; set; }  //The fastest speed the laser can travel.
            public int MaxAcceleration { get; set; }   //The fastest rate of change the laser can travel.
            public int DwellPoints { get; set; } //The amount of time the laser waits at the end of a line. This is a quality improver.
            public bool ShowLaser  { get; set; }  //Important variable for safety measures. When 'true' the laser is on which is a danger to eyes. 
            public LaserSettings()  //JSON constructor.
            {
                Kpps = 30000;  //The amount of points per second the laser can travel across.
                MaxVelocity = 15;  //The fastest speed the laser can travel.
                MaxAcceleration = 2;  //The fastest rate of change the laser can travel.
                DwellPoints = 10;  //The amount of time the laser waits at the end of a line. This is a quality improver.
                ShowLaser = false; //Important variable for safety measures. When 'true' the laser is on which is a danger to eyes. 
            }
        }
        Point ConvertToHeliosCoords(Point Original, bool backwards = false)
        {
            Point returnConverted;  //This translates all the laser coordinates onto a single map to ensure consistency when resizing images.
            float Scale = 4095f / (float)(PreviewGraphics.Size.Width);  //This assumes preview graphics is square, I intend to make this bit such that the projection range is resizeable.
            if (!backwards)
            {
                returnConverted = new Point((int)(Original.X * Scale), (int)(Original.Y * Scale));
                if (returnConverted.X > 4095)  //Checks to make sure no points are outside the laser area.
                {
                    returnConverted.X = 4095;
                }
                else if (returnConverted.X < 0)
                {
                    returnConverted.X = 0;
                }
                if (returnConverted.Y > 4095)
                {
                    returnConverted.Y = 4095;
                }
                else if (returnConverted.Y < 0)
                {
                    returnConverted.Y = 0;
                }
            }
            else
            {
                returnConverted = new Point((int)(Original.X / Scale), (int)(Original.Y / Scale));
            }
            return returnConverted;
        }
        static double getDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        //not working vvv
        List<HeliosPoint> travelBetweenPoints(PointF zeroPoint, PointF lastPoint, PointF currentVelocity, PointF endVelocity, int MaxVelocity, int MaxAcceleration)
        {
            HeliosPoint currentPoint;
            List<HeliosPoint> returnPoints = new List<HeliosPoint>();

            double timeToReachFrameX = (lastPoint.X - zeroPoint.X) / (((3 / 2) * currentVelocity.X) - ((1 / 2) * endVelocity.X));
            double timeToReachFrameY = (lastPoint.Y - zeroPoint.Y) / (((3 / 2) * currentVelocity.Y) - ((1 / 2) * endVelocity.Y));
            double timeScalingX;
            double timeScalingY;
            double totalTime;

            if ((endVelocity.X - currentVelocity.X) > (endVelocity.X - currentVelocity.X))
            {
                timeScalingX = (endVelocity.X - currentVelocity.X) / (MaxAcceleration * timeToReachFrameX);
                timeScalingY = timeToReachFrameX / timeToReachFrameY;
                totalTime = timeToReachFrameX * timeScalingX;
            }
            else
            {
                timeScalingY = (endVelocity.Y - currentVelocity.Y) / (MaxAcceleration * timeToReachFrameY);
                timeScalingX = timeToReachFrameY / timeToReachFrameX;
                totalTime = timeToReachFrameX * timeScalingX;
            }

            for (float time = 0; time < totalTime; time++)
            {
                currentPoint.x = (ushort)(1.5 * currentVelocity.X * time - 0.5 * endVelocity.X * time + zeroPoint.X);
                currentPoint.y = (ushort)(1.5 * currentVelocity.Y * time - 0.5 * endVelocity.Y * time + zeroPoint.Y);
                currentPoint.r = (Byte)0;
                currentPoint.g = (Byte)0;
                currentPoint.b = (Byte)0;
                currentPoint.i = (Byte)0;
                returnPoints.Add(currentPoint);
            }

            return returnPoints;
        }         //Beginning of my optimising pathalgorithm
        abstract class LinePoint
        {
            public int ShapeListIndex { get; set; }
            public Point Location { get; set; }

            public LinePoint(int shapeListIndex, Point location)
            {
                ShapeListIndex = shapeListIndex;
                Location = location;
            }
        }
        class TraversalLinePoint : LinePoint
        {
            public bool IsStart { get; set; }
            public TraversalLinePoint(int shapeListIndex, Point location, bool isStart) : base(shapeListIndex, location)
            {
                ShapeListIndex = shapeListIndex;
                Location = location;
                IsStart = isStart;
            }
        }
        class KeyLinePoint : LinePoint
        {
            public bool IsMiddle { get; set; }
            public int PathPointsListIndex { get; set; }
            public KeyLinePoint(int shapeListIndex, int pathPointsListIndex, Point location, bool isMiddle) : base(shapeListIndex, location)
            {
                IsMiddle = isMiddle;
                PathPointsListIndex = pathPointsListIndex;
            }
        }
        class TimeLinePoint : LinePoint
        {
            public int FrameTime { get; set; }
            public TimeLinePoint(int shapeListIndex, int frameTime, Point location) : base(shapeListIndex, location)
            {
                FrameTime = frameTime;
            }
        }

        Queue<PathLineFrame> genShapeLaserPath(List<PathLineFrame> framePath)
        {
            List<TraversalLinePoint> pointsToTraverse = new List<TraversalLinePoint>();

            // Gets all the points required in order to traverse.
            foreach (PathLineFrame line in framePath)
            {
                pointsToTraverse.Add(new TraversalLinePoint(framePath.IndexOf(line), line.PathPoints[0], true));
                pointsToTraverse.Add(new TraversalLinePoint(framePath.IndexOf(line), line.PathPoints[^1], false));
            }

            // Setting up variables.
            Queue<PathLineFrame> linesToGenPointsFor = new Queue<PathLineFrame>();
            TraversalLinePoint currentPoint;  // Where we are.
            TraversalLinePoint nextPoint;  // Where we want to go.

            // If we are in a position where there is a need to project.
            if (pointsToTraverse.Count > 1 && (pointsToTraverse.Count % 2 == 0))
            {
                currentPoint = pointsToTraverse[0];

                // Travels to the next line.
                linesToGenPointsFor.Enqueue(framePath[currentPoint.ShapeListIndex]);
                int nextIndex = pointsToTraverse.IndexOf(currentPoint);  // Not add one because current point is removed.
                pointsToTraverse.Remove(currentPoint);
                currentPoint = pointsToTraverse[nextIndex];

                // This if statement basically travels the line either forwards or backwards.
                pointsToTraverse.Remove(currentPoint);  // Removed because it's already been traversed.

                // Start the while loop after drawing the first line.
                while (pointsToTraverse.Count > 0)
                {
                    nextPoint = new TraversalLinePoint(-1, new Point(0x7FFFFFFF, 0x7FFFFFFF), false);  // Reset next point to be infinitely far away. Here we are setting the ideal closest point.

                    foreach (TraversalLinePoint point in pointsToTraverse)
                    {
                        // This point closer to the goal than the other point.
                        if (getDistance(point.Location, currentPoint.Location) < getDistance(nextPoint.Location, currentPoint.Location))
                        {
                            nextPoint = point;  // Closest point = the closer point.
                        }
                    }

                    // Find where to travel to (yes, it's the most basic path traversal heuristic, but it works quite well when things are simple).
                    // I would do a more complicated one, but the points need to be met in certain ways.
                    linesToGenPointsFor.Enqueue(new PathLineFrame(true, currentPoint.Location, nextPoint.Location));
                    nextIndex = pointsToTraverse.IndexOf(nextPoint);  // Needs it to remove the other part of the line. Could have done a for loop for each line to be honest, might be a piece of improvement for the future.
                    pointsToTraverse.Remove(nextPoint);  // And remove the point you've just run away from. You need to remove two points per line/while loop. It's next point that gets removed as this has just been traversed.
                    currentPoint = nextPoint;

                    // Travel between two lines (the laser has to do this it cannot simply teleport).
                    if (pointsToTraverse.Count > 0)
                    {
                        if (currentPoint.IsStart)  // If yes, travel the line then remove the points.
                        {
                            linesToGenPointsFor.Enqueue(framePath[currentPoint.ShapeListIndex]);  // Index already set above.
                        }
                        else
                        {
                            PathLineFrame frame = new PathLineFrame(framePath[currentPoint.ShapeListIndex]);
                            frame.Reverse();
                            linesToGenPointsFor.Enqueue(frame);
                            nextIndex = nextIndex - 1;  //Subtract one because the index below isnt affected. Like because the line is reversed yk
                        }
                        currentPoint = pointsToTraverse[nextIndex];
                        pointsToTraverse.Remove(pointsToTraverse[nextIndex]);
                    }
                }
                linesToGenPointsFor.Enqueue(new PathLineFrame(true, currentPoint.Location, framePath[0].PathPoints[0]));
            }
            return linesToGenPointsFor;
        }
        class PathLineFrame //SOMETIMES IS A KEYFRAME SOMETIMES ISNT
        {
            public int Time { get; set; }//Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public bool Hidden { get; set; }
            public Color PathColor { get; set; }
            public List<Point> PathPoints { get; set; }
            public void AddPoint(Point newPoint)
            {
                PathPoints.Add(newPoint);
            }
            public int ListIndex { get; set; }                      //THE INDEX OF THE LIST VARIES DEPENDING ON IF THE OBJECT IS A KEYFRAME OR NOT
            public List<HeliosPoint> GenLaserPoints(LaserSettings currentLaserSettings)
            {
                List<HeliosPoint> points = new List<HeliosPoint>();
                #region higher order maths that i swear is very close to working but isnt quite working. Think it might have made curves anyway.
                /* DOESNT WORK AS I SO VERY WISHED IT WOULD
                for(int i = 0; i < PathPoints.Count-1; i++)
                {
                    double lineFrameLength = getDistance(PathPoints[i], PathPoints[i+1]);
                    double lineFrameProcessed = (double)(currentLaserSettings.MaxAcceleration - currentLaserSettings.bufferLength);
                    HeliosPoint currentPoint;
                    double deltaY = PathPoints[i+1].Y - PathPoints[i].Y;
                    double deltaX = PathPoints[i + 1].X - PathPoints[i].X;
                    int velocity = currentLaserSettings.MaxAcceleration;
                    while (lineFrameProcessed < (lineFrameLength / 2))
                    {
                        currentPoint.x = (ushort) (PathPoints[i].X + (lineFrameProcessed * deltaX));
                        currentPoint.y = (ushort) (PathPoints[i].Y + (lineFrameProcessed * deltaY));
                        currentPoint.r = PathColor.R;
                        currentPoint.g = PathColor.G;
                        currentPoint.b = PathColor.B;
                        currentPoint.i = PathColor.A;
                        points.Add(currentPoint);
                        if (velocity + currentLaserSettings.MaxAcceleration < currentLaserSettings.MaxVelocity)
                        {
                            velocity += currentLaserSettings.MaxAcceleration;
                        }
                        lineFrameProcessed += velocity;
                    }
                    lineFrameProcessed -= velocity;
                    lineFrameProcessed = lineFrameLength - lineFrameProcessed;
                    while (lineFrameProcessed < (lineFrameLength + currentLaserSettings.bufferLength))
                    {
                        currentPoint.x = (ushort)(PathPoints[i].X + (lineFrameProcessed * deltaX));
                        currentPoint.y = (ushort)(PathPoints[i].Y + (lineFrameProcessed * deltaY));
                        currentPoint.r = PathColor.R;
                        currentPoint.g = PathColor.G;
                        currentPoint.b = PathColor.B;
                        currentPoint.i = PathColor.A;
                        points.Add(currentPoint);
                        if (velocity + currentLaserSettings.MaxAcceleration < currentLaserSettings.MaxVelocity)
                        {
                            velocity += currentLaserSettings.MaxAcceleration;
                        }
                        lineFrameProcessed += velocity;
                    }
                }*/
                #endregion
                //If we have constant acceleration life is hella easy thanks to SUVAT equations
                //S = ut + 1/2 a t^2 (hold on one sec we got to the good bit in my playlist I needa jam for a sec)
                if (PathPoints.Count > 0)
                {
                    Queue<double> beginningDisplacements = new Queue<double>();
                    Stack<double> endingDisplacements = new Stack<double>();
                    for (int i = 0; i < currentLaserSettings.DwellPoints; i++)
                    {
                        beginningDisplacements.Enqueue(0);
                    }
                    int acceleratingTime = currentLaserSettings.MaxVelocity / currentLaserSettings.MaxAcceleration;
                    double currentDisplacement = 0;
                    double halfDisplacement = getDistance(this.PathPoints[0], this.PathPoints[1]) / 2;//I realise that I havent added multi point compatibility this is a relatively easy fix for before summer :)
                    for (double t = 0; t < acceleratingTime && (0.5 * t * t * currentLaserSettings.MaxAcceleration) < halfDisplacement; t++)
                    {
                        beginningDisplacements.Enqueue(0.5 * t * t * currentLaserSettings.MaxAcceleration);
                        endingDisplacements.Push(0.5 * t * t * currentLaserSettings.MaxAcceleration);
                        currentDisplacement = 0.5 * t * t * currentLaserSettings.MaxAcceleration;
                    }
                    if (currentDisplacement < halfDisplacement + (currentLaserSettings.MaxVelocity / 2))//If distance left to cover will take more than a point.
                    {
                        for (int i = 0; currentDisplacement + currentLaserSettings.MaxVelocity < halfDisplacement; i++)
                        {
                            currentDisplacement += currentLaserSettings.MaxVelocity;
                            beginningDisplacements.Enqueue(currentDisplacement + currentLaserSettings.MaxVelocity);
                            endingDisplacements.Push(currentDisplacement + currentLaserSettings.MaxVelocity); //Should make it so the middle points are evenly spaced.0

                        }

                        // This bit fills in the middle so the velocity isnt bigger than the max velocity. The rest uses the max acceleration.
                    }
                    HeliosPoint currentPoint = new HeliosPoint();
                    currentDisplacement = 0;
                    double horizontaleScaleValue = (PathPoints[1].X - PathPoints[0].X) / getDistance(this.PathPoints[0], this.PathPoints[1]);
                    double verticalScaleValue = (PathPoints[1].Y - PathPoints[0].Y) / getDistance(this.PathPoints[0], this.PathPoints[1]);
                    while (beginningDisplacements.Count > 0)
                    {
                        currentDisplacement = beginningDisplacements.Dequeue();
                        currentPoint.x = (ushort)(PathPoints[0].X + currentDisplacement * horizontaleScaleValue);
                        currentPoint.y = (ushort)(PathPoints[0].Y + currentDisplacement * verticalScaleValue);
                        currentPoint.r = (byte)((this.PathColor.R * this.PathColor.A) / 0xFF);
                        currentPoint.g = (byte)((this.PathColor.G * this.PathColor.A) / 0xFF);
                        currentPoint.b = (byte)((this.PathColor.B * this.PathColor.A) / 0xFF);
                        currentPoint.i = (byte)(this.PathColor.A);
                        points.Add(currentPoint);
                    }
                    while (endingDisplacements.Count > 0)
                    {
                        currentDisplacement = endingDisplacements.Pop();
                        currentPoint.x = (ushort)(PathPoints[1].X - currentDisplacement * horizontaleScaleValue);
                        currentPoint.y = (ushort)(PathPoints[1].Y - currentDisplacement * verticalScaleValue);
                        currentPoint.r = (byte)((this.PathColor.R * this.PathColor.A) / 0xFF);
                        currentPoint.g = (byte)((this.PathColor.G * this.PathColor.A) / 0xFF);
                        currentPoint.b = (byte)((this.PathColor.B * this.PathColor.A) / 0xFF);
                        currentPoint.i = (byte)(this.PathColor.A);
                        points.Add(currentPoint);
                    }
                }
                else
                {
                    points = new List<HeliosPoint>();
                }
                return points;
            }
            public void Reverse()
            {
                PathPoints.Reverse();
            }
            public List<KeyLinePoint> GenKeyPoints(bool middle = false)
            {
                List<KeyLinePoint> KeyPoints = new List<KeyLinePoint>();

                for (int i = 0; i < PathPoints.Count - 1; i++)
                {
                    KeyPoints.Add(new KeyLinePoint(ListIndex, i, PathPoints[i], false));
                    if (middle)
                    {
                        KeyPoints.Add(new KeyLinePoint(ListIndex, i, new Point(
                                Convert.ToInt32((PathPoints[i].X + PathPoints[i + 1].X) / 2),
                                Convert.ToInt32((PathPoints[i].Y + PathPoints[i + 1].Y) / 2))
                                , true));
                    }
                }

                KeyPoints.Add(new KeyLinePoint(ListIndex, PathPoints.Count - 1, PathPoints.Last(), false));
                return KeyPoints;
            }
            /*So basically this function generates keypoints, keypoints are any points with details that we might want to adjust
             * This selection tool should work well until there are a lot of points to select*/
            public int getValueXWayBetweenTwoPoints(int value1, int value2, float multiplier)
            {
                return Convert.ToInt32(value1 + (value2 - value1) * multiplier);
            } //This is a function to work out the properties of a nonkeyframe. Iz guud.
            public PathLineFrame(int time, Color pathColor, List<Point> pathPoints, int listIndex)
            {
                this.Time = time;
                this.PathColor = pathColor;
                this.PathPoints = pathPoints;
                this.ListIndex = listIndex;
            }   //Basic constructor
            public PathLineFrame(int time, Color pathColor, Point point1, Point point2, int listIndex)
            {
                Time = time;
                PathColor = pathColor;
                PathPoints = new List<Point>();
                PathPoints.Add(point1);
                PathPoints.Add(point2);
                ListIndex = listIndex;
            }   //Mid Constructor
            public PathLineFrame()  //Json Constructor
            {
                //Just dont use this
            }
            public PathLineFrame(bool hidden, Point point1, Point point2)
            {
                if (hidden)
                {
                    this.Time = -1;
                    this.PathColor = Color.Black;
                    this.PathPoints = new List<Point>();
                    this.PathPoints.Add(point1);
                    this.PathPoints.Add(point2);
                    this.Hidden = hidden;
                    this.ListIndex = -1;
                }
            }
            public PathLineFrame(float animationProgress, PathLineFrame frameBefore, PathLineFrame frameAfter)
            {
                Time = getValueXWayBetweenTwoPoints(frameBefore.Time, frameAfter.Time, animationProgress);
                PathColor = Color.FromArgb(
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.A, frameAfter.PathColor.A, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.R, frameAfter.PathColor.R, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.G, frameAfter.PathColor.G, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.B, frameAfter.PathColor.B, animationProgress)); //I turned it into a function because I was bound to make a mistake. And because I need to use this function a LOT.
                while (frameBefore.PathPoints.Count != frameAfter.PathPoints.Count)
                {
                    if (frameBefore.PathPoints.Count < frameAfter.PathPoints.Count)
                    {
                        frameAfter.PathPoints.Add(frameBefore.PathPoints.Last());
                    }
                    else
                    {
                        frameBefore.PathPoints.Add(frameAfter.PathPoints.Last());
                    }
                }   //Makes all extra detail grow out the end of the line --im proud of myself for the attention to detail, might be buggy tho
                for (int i = 0; i < frameAfter.PathPoints.Count; i++)
                {
                    PathPoints.Add(new Point(
                        getValueXWayBetweenTwoPoints(frameBefore.PathPoints[i].X, frameAfter.PathPoints[i].X, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathPoints[i].Y, frameAfter.PathPoints[i].Y, animationProgress)
                        ));
                }
            }
            /*public PathLineFrame(int newTime, PathLineFrame frame)
            {
                time = newTime;
                PathColor = frame.PathColor;
                PathPoints = frame.PathPoints;

            }*/
            public PathLineFrame(PathLineFrame frame)
            {
                Time = frame.Time;
                ListIndex = frame.ListIndex;
                PathColor = frame.PathColor;
                PathPoints = new List<Point>();
                foreach (Point point in frame.PathPoints)
                {
                    PathPoints.Add(point);
                }
            }
        }
        class PathLine
        {
            public string Name { get; set; }
            public List<PathLineFrame> KeyFrames { get; set; } //A list of all keyframes sorted by time
            public int DynamicPathIndex { get; set; }
            public bool IsHidden { get; set; }
            public PathLineFrame GetFrameAt(int FrameTime)  //Literally a recursive algorithm. Didnt even make it on purpose.
            {
                List<PathLineFrame> touchingFrames = findTouchingFrames(FrameTime, KeyFrames);
                if (touchingFrames.Count == 1)
                {
                    return touchingFrames[0];
                }
                KeyFrames.Add(new PathLineFrame(GenFrameAt(FrameTime)));
                this.SortKeyFramesByTime();
                return GetFrameAt(FrameTime);
            }
            public List<PathLineFrame> findTouchingFrames(int timeOfFrame, List<PathLineFrame> touchingPoints, bool startingOccurance = true)
            {
                if (startingOccurance)
                {
                    touchingPoints = new List<PathLineFrame> { new PathLineFrame(-1, Color.Black, new List<Point>(), -1) };
                    //Added fake beginning abnd ending frames so theres always a frame above and below or the code would j break
                    touchingPoints.AddRange(KeyFrames);
                    touchingPoints.Add(new PathLineFrame((int)0x7FFFFFFF, Color.Black, new List<Point>(), -1));
                }
                int midFrameIndex = touchingPoints.Count() / 2;
                if (touchingPoints[midFrameIndex].Time == timeOfFrame)  //If the mid frame matches the time
                {
                    return new List<PathLineFrame> { touchingPoints[midFrameIndex] };
                }
                else if (timeOfFrame > touchingPoints[midFrameIndex].Time)
                {
                    if (timeOfFrame < touchingPoints[midFrameIndex + 1].Time)
                    {
                        return new List<PathLineFrame> { touchingPoints[midFrameIndex], touchingPoints[midFrameIndex + 1] };
                    }
                    return findTouchingFrames(timeOfFrame, touchingPoints.GetRange(midFrameIndex - 1, 1 + touchingPoints.Count() - midFrameIndex), false);
                }
                else if (timeOfFrame < touchingPoints[midFrameIndex].Time)
                {
                    return findTouchingFrames(timeOfFrame, touchingPoints.GetRange(0, midFrameIndex + 1), false);
                }
                else
                {
                    return new List<PathLineFrame>();
                }

            }
            public PathLineFrame GenFrameAt(int FrameTime)
            {
                PathLineFrame newFrame;

                List<PathLineFrame> touchingFrames = findTouchingFrames(FrameTime, new List<PathLineFrame>());  //This algorithm should be much more efficient :0

                if (touchingFrames.Count == 1)
                {
                    return touchingFrames[0];
                }
                else if (touchingFrames[0].Time == -1)                  //If before all frames then newframe is the same as the frame after or first frame
                {
                    newFrame = new PathLineFrame(touchingFrames[1]);    //Sets it to the not fake frame
                    newFrame.Time = FrameTime;
                    return newFrame;
                }
                else if (touchingFrames[1].Time == (int)0x7FFFFFFF)                   //Literally the same situation as the above if
                {
                    newFrame = new PathLineFrame(touchingFrames[0]);        //Sets it to the not fake frame
                    newFrame.Time = FrameTime;
                    return newFrame;
                }
                else
                {
                    //SET ALL PROPERTIES TO PROPERTIES IN BETWEEN THEM BOTH
                    float animationProgress = ((float)(FrameTime - touchingFrames[0].Time) / (float)(touchingFrames[1].Time - touchingFrames[0].Time)); //If you set each property to beforeFrame value plus (afterFrame - beforeFrame) * difference  -- This assumes a linear animation hence the constant progress as time moves forward.
                    return new PathLineFrame(animationProgress, touchingFrames[0], touchingFrames[1]);
                }
            }   //The majourity of the animation code IF THIS PROJECT DOESNT WORK IMMA CRY
            public PathLine(string name, PathLineFrame keyFrame, int dynamicPathIndex)
            { //For ListIndex just do Path
                Name = name;
                KeyFrames = new List<PathLineFrame>();
                KeyFrames.Add(keyFrame);
                DynamicPathIndex = dynamicPathIndex;
                IsHidden = false;
            }
            public PathLine()
            {
                //Needed for JSON Conversion IDK why its just how the extension works
            }
            public void SortKeyFramesByTime()
            {
                //Here I impliment quicksort partially explained here: https://www.youtube.com/watch?v=SLauY6PpjW4&t=15s
                //I used a recursive routine
                QuicksortByTime(KeyFrames);
            }
            public List<PathLineFrame> QuicksortByTime(List<PathLineFrame> list)
            {
                //Here I impliment quicksort partially explained here: https://www.youtube.com/watch?v=SLauY6PpjW4&t=15s
                //I used a recursive routine
                if (list.Count() == 0)
                {
                    return new List<PathLineFrame>();       //Gotta have something end the recursive routine
                }

                List<PathLineFrame> left = new List<PathLineFrame>();
                List<PathLineFrame> right = new List<PathLineFrame>();
                PathLineFrame pivot = list[0];
                //Basically the way pivot works is you keep making lists of numbers greater than the pivot and less than the pivot until the whole thing is sorted
                //This makes it O(nlogn) because you keep splitting the array in half each time
                //You learn something new everyday!
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].Time < pivot.Time)
                    {
                        left.Add(list[i]);
                    }
                    else if (list[i].Time > pivot.Time)
                    {
                        right.Add(list[i]);
                    }
                }
                List<PathLineFrame> leftSorted = QuicksortByTime(left);
                List<PathLineFrame> rightSorted = QuicksortByTime(right);
                list.Clear();
                for (int i = 0; i < leftSorted.Count; i++)
                {
                    list.Add(leftSorted[i]);
                }
                list.Add(pivot);
                for (int i = 0; i < rightSorted.Count; i++)
                {
                    list.Add(rightSorted[i]);
                }
                return list;
            }
        }

        //THE GRAPHICS PANEL
        private void PreviewGraphics_Paint(object sender, PaintEventArgs e)
        {
            //Gen framePath (useful for lazer as well.)
            if (framePathTime != mainTime)
            {
                framePathTime = mainTime;
                framePath = new List<PathLineFrame>();
                for (int i = 0; i < project.dynamicPath.Count(); i++)
                {
                    PathLineFrame newFrame = project.dynamicPath[i].GenFrameAt(framePathTime);
                    newFrame.ListIndex = framePath.Count;
                    framePath.Add(newFrame);
                }
            }
            InformationFrameListCountInfo.Text = framePath.Count().ToString();

            laserTraversalPath = genShapeLaserPath(framePath);  //Literally generates the laser Path
            laserPoints.Clear();
            foreach (PathLineFrame laserLine in laserTraversalPath)
            {
                if (laserPathToolStripMenuItem.Checked)
                {
                    if (laserLine.Hidden)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Blue),
                        ConvertToHeliosCoords(laserLine.PathPoints[0], true), ConvertToHeliosCoords(laserLine.PathPoints[1], true));
                    }
                    else
                    {
                        e.Graphics.DrawLine(new Pen(Color.Green, 2),
                            ConvertToHeliosCoords(laserLine.PathPoints[0], true), ConvertToHeliosCoords(laserLine.PathPoints[1], true));
                    }
                }

                laserPoints.AddRange(laserLine.GenLaserPoints(currentLaserSettings));

            }
            if (laserPointsToolStripMenuItem.Checked)
            {
                foreach (HeliosPoint point in laserPoints)
                {
                    e.Graphics.FillCircle(new SolidBrush(Color.Pink), ConvertToHeliosCoords(new Point(point.x, point.y), true), 2);
                }
            }
            //Draw lines, dots and selection bits.
            Pen linePen;
            double closestPointDistance = 1000000;

            for (int i = 0; i < framePath.Count(); i++) //Goes through all the lines
            {
                //This bit draws all the points out, and the lines if needed, and also shows the points..
                linePen = new Pen(framePath[i].PathColor);
                for (int j = 0; j < framePath[i].PathPoints.Count() - 1; j++)    //REMEMBER TO UNCOMMENT THIS
                {
                    if (previewShapesToolStripMenuItem.Checked)
                    {
                        e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(framePath[i].PathPoints[j], true), ConvertToHeliosCoords(framePath[i].PathPoints[j + 1], true));
                    }
                }

                //Im pretty sure this is the function that provides closest points.
                List<KeyLinePoint> keyPoints = new List<KeyLinePoint>();
                keyPoints = framePath[i].GenKeyPoints(OptionsSelectModeButton.Checked);
                for (int j = 0; j < keyPoints.Count(); j++)
                {
                    Point pointLocation = keyPoints[j].Location;
                    Point pointLocationGraphics = ConvertToHeliosCoords(pointLocation, true);
                    if (showCircles)     //Also finds closest point to mouse
                    {
                        e.Graphics.FillCircle(new SolidBrush(Color.LightGray), pointLocationGraphics.X, pointLocationGraphics.Y, 4);
                        e.Graphics.FillCircle(new SolidBrush(Color.DarkGray), pointLocationGraphics.X, pointLocationGraphics.Y, 3);
                    }
                    if (getDistance(pointLocation, mouseLastLocation) < closestPointDistance) //Finds closest point
                    {
                        closestPoint = keyPoints[j].Location;
                        closestPoints.Clear();
                        closestPoints.Add(keyPoints[j]);
                        InformationClosestPointData.Text = closestPoint.ToString();
                        closestPointDistance = getDistance(pointLocation, mouseLastLocation);
                    }
                    else if (getDistance(pointLocation, mouseLastLocation) == closestPointDistance)  //Adds all lines to the closest point, pretty nifty if you ask me :)
                    {
                        closestPoints.Add(keyPoints[j]);
                    }
                }
            }
            if (showLines)
            {
                try
                {
                    if (closestPoints[0].IsMiddle)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red, 2), ConvertToHeliosCoords(project.dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[0], true),
                            ConvertToHeliosCoords(project.dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[1], true));
                        e.Graphics.DrawLine(new Pen(project.dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathColor), ConvertToHeliosCoords(project.dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[0], true),
                            ConvertToHeliosCoords(project.dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[1], true));
                    }
                    else
                    {
                        e.Graphics.FillCircle(new SolidBrush(Color.Red), ConvertToHeliosCoords(closestPoint, true), 3);
                    }
                }
                catch
                {

                }
            }
        }
        private void PreviewGraphics_MouseDown(object sender, MouseEventArgs e)                         //MOUSE MOVEMENT
        {
            //If mouse down, create a new line or frame.
            if (OptionsDrawLineMode.Checked)
            {
                project.dynamicPath.Add(new PathLine("(" + ConvertToHeliosCoords(e.Location).X + "," + ConvertToHeliosCoords(e.Location).Y + ")", new PathLineFrame(mainTime, DrawerColorDialog.Color, new List<Point>(), project.dynamicPath.Count), project.dynamicPath.Count));
                selectedLineDynamicIndex = project.dynamicPath.Count - 1;
                if (OptionsSnapToPoint.Checked)
                {
                    project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(closestPoints[0].Location));
                }
                else
                {
                    project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(e.Location));
                }
                if (OptionsSnapToPoint.Checked)
                {
                    project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(closestPoints[0].Location));
                }
                else
                {
                    project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(e.Location));
                }
            }
            else if (OptionsSelectModeButton.Checked)
            {
                if (closestPoints[0].IsMiddle)
                {
                    selectedLineDynamicIndex = closestPoints[0].ShapeListIndex;
                }
                else
                {
                    selectedLineDynamicIndex = closestPoints[0].ShapeListIndex;
                    closestPointsFrozen = new List<KeyLinePoint>(closestPoints);
                }

            }
            mouseDown = true;
            framePathTime = -1;
            PreviewGraphics.Invalidate();
            UpdateLineProperties();
        }
        private void PreviewGraphics_MouseMove(object sender, MouseEventArgs e)                             //MOUSE ACTIVITY
        {
            if (mouseDown)
            {
                if (OptionsDrawLineMode.Checked)
                {
                    if (OptionsSnapToPoint.Checked)
                    {
                        GetSelectedFrameWrite().PathPoints[1] = ConvertToHeliosCoords(closestPoints[0].Location);
                    }
                    else
                    {
                        GetSelectedFrameWrite().PathPoints[1] = ConvertToHeliosCoords(e.Location);
                    }
                }
                else if (OptionsSelectModeButton.Checked)
                {
                    foreach (var point in closestPointsFrozen)
                    {
                        if (OptionsSnapToPoint.Checked)
                        {
                            GetSelectedFrameWrite().PathPoints[point.PathPointsListIndex] = ConvertToHeliosCoords(closestPoints[0].Location);
                        }
                        else
                        {
                            GetSelectedFrameWrite().PathPoints[point.PathPointsListIndex] = ConvertToHeliosCoords(e.Location);
                        }
                    }
                }
            }
            mouseLastLocation = ConvertToHeliosCoords(e.Location);
            PreviewGraphics.Invalidate();
            framePathTime = -1;
            UpdateLineProperties();
        }
        private void PreviewGraphics_MouseUp(object sender, MouseEventArgs e)                           //MOUSE SHENINAGINS
        {
            mouseDown = false;
            if (project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).PathPoints[0] == e.Location)
            {
                project.dynamicPath[selectedLineDynamicIndex].KeyFrames.Remove(project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime));
                if (project.dynamicPath[selectedLineDynamicIndex].KeyFrames.Count == 0)
                {
                    project.dynamicPath.RemoveAt(selectedLineDynamicIndex);
                }
            }
            closestPointsFrozen.Clear();
            framePathTime = -1;
            PreviewGraphics.Invalidate();
            UpdateLineProperties();
            mouseLastLocation = ConvertToHeliosCoords(e.Location);
        }
        private void OptionsColorSelecterOpener_Click(object sender, EventArgs e)
        {
            DrawerColorDialog.ShowDialog();
        }

        private void PreviewGraphics_Resize(object sender, EventArgs e)
        {
            int maxHorizontalSpace = splitContainer1.Panel1.Width - PreviewGraphics.Margin.Horizontal;  //300 for the rhs panels
            int maxVerticalSpace = splitContainer1.Panel1.Height - (splitContainer1.Panel1.Height / 5) - PreviewGraphics.Margin.Vertical - TimeLinePanel.Margin.Vertical;   //150 for the bottom panel
            if (maxHorizontalSpace < maxVerticalSpace)
            {
                maxVerticalSpace = maxHorizontalSpace;
            }
            else
            {
                maxHorizontalSpace = maxVerticalSpace;  //The actual max size is the smallest of the two.
            }
            PreviewGraphics.Size = new Size(maxHorizontalSpace - PreviewGraphics.Margin.Horizontal, maxVerticalSpace - 2 * PreviewGraphics.Margin.Vertical);
            TimeLinePanel.Size = new Size(splitContainer1.Panel1.Width - TimeLinePanel.Margin.Horizontal, splitContainer1.Panel1.Height - maxVerticalSpace);
            TimeLinePanel.Location = new Point(TimeLinePanel.Margin.Left, splitContainer1.Panel1.Height - (TimeLinePanel.Height + TimeLinePanel.Margin.Bottom));
        }

        private void OptionsDrawLineMode_CheckedChanged(object sender, EventArgs e)
        {
            OptionsSelectModeButton.Checked = !OptionsDrawLineMode.Checked;
            if (OptionsDrawLineMode.Checked)
            {
                OptionsDrawLineMode.BackColor = Color.Green;
            }
            else
            {
                OptionsDrawLineMode.BackColor = Color.Red;
            }
        }

        private void OptionsSelectModeButton_CheckedChanged(object sender, EventArgs e)
        {
            OptionsDrawLineMode.Checked = !OptionsSelectModeButton.Checked;
            showCircles = OptionsSelectModeButton.Checked;
            showLines = OptionsSelectModeButton.Checked;
            if (OptionsSelectModeButton.Checked)
            {
                OptionsSelectModeButton.BackColor = Color.Green;
            }
            else
            {
                OptionsSelectModeButton.BackColor = Color.Red;
            }
        }

        private void OptionsSnapToPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (OptionsSnapToPoint.Checked)
            {
                OptionsSnapToPoint.BackColor = Color.Green;
            }
            else
            {
                OptionsSnapToPoint.BackColor = Color.Red;
            }
        }

        private void TimeLineSecondsInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TimeLineFramesInput.Text != "")
                {
                    mainTime = Convert.ToInt32(TimeLineFramesInput.Text) * project.fps;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: cannot convert text to number: " + TimeLineFramesInput.Text);
            }
        }
        private void TimeLineFramesInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TimeLineFramesInput.Text != "")
                {
                    ChangeTime(Convert.ToInt32(TimeLineFramesInput.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: cannot convert text to number: " + TimeLineFramesInput.Text);
            }
            UpdateLineProperties();
            PreviewGraphics.Invalidate();
        }

        void UpdateLineProperties()
        {
            // If currently playing, dont bother to update line properties
            if (currentlyPlaying)
            {
                return;
            }

            // Invalidate timeline GUI
            timelineGUI.Invalidate();

            // If dynamic path has no points dont run this code.
            if (project.dynamicPath.Count == 0)
            {
                return;
            }

            // Ensure selected dynamic line index is within bounds
            if (selectedLineDynamicIndex > project.dynamicPath.Count)
            {
                // If out of bounds, reset to 0
                selectedLineDynamicIndex = 0;
                selectedPointDynamicIndex = 0;
            }

            // Set selected frame to frame generated by selected dynamic line at current mainTime
            // This frame is self-updating
            selectedFrameReadOnly = project.dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime);

            //Ensures the item is within the selected index by changing it to the closest allowed value.
            selectedPointDynamicIndex = Math.Clamp(selectedPointDynamicIndex, 0, selectedFrameReadOnly.PathPoints.Count - 1);

            // Clear items in PathLinePointsListBox and add each point in selected frame
            PathLinePointsListBox.BeginUpdate();    //Ag
            PathLinePointsListBox.Items.Clear();
            foreach (var pathPoint in selectedFrameReadOnly.PathPoints)
            {
                PathLinePointsListBox.Items.Add(pathPoint.ToString());
            }
            PathLinePointsListBox.EndUpdate();

            // Set LinePropertiesTitle label to display name of selected dynamic line
            LinePropertiesTitle.Text = "Line Properties: " + project.dynamicPath[selectedLineDynamicIndex].Name;

            // Set LinePropertiesPathIndexData label to display index of selected dynamic line
            LinePropertiesPathIndexData.Text = selectedLineDynamicIndex.ToString();

            // If there is a selected point, set X and Y value of LinePropertiesXCoordinate and LinePropertiesYCoordinate
            if (selectedPointDynamicIndex != -1)
            {
                Point tempPoint = selectedFrameReadOnly.PathPoints[selectedPointDynamicIndex];
                tempIntermediateFramePoint.X = tempPoint.X; //I don't know if there is a better way to do this it feels a bit tedious.
                LinePropertiesXCoordinate.Value = tempPoint.X;
                tempIntermediateFramePoint = tempPoint;
                LinePropertiesYCoordinate.Value = tempPoint.Y;
            }

            // Clear items in LinePropertiesKeyFramesTextBox and add each keyframe time in selected dynamic line
            LinePropertiesKeyFramesTextBox.BeginUpdate();   //This line and endupdate allow the list to update without flickering.
            LinePropertiesKeyFramesTextBox.Items.Clear();
            foreach (var keyFrame in project.dynamicPath[selectedLineDynamicIndex].KeyFrames)
            {
                LinePropertiesKeyFramesTextBox.Items.Add(keyFrame.Time);
            }
            LinePropertiesKeyFramesTextBox.EndUpdate();

            // Set color of LinePropertiesChangeColor label to color of selected dynamic line at current mainTime
            LinePropertiesChangeColor.ForeColor = selectedFrameReadOnly.PathColor;
        }

        PathLineFrame GetSelectedFrameWrite()
        {
            return project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime);
        }
        void ChangeTime(int newTime)
        {
            selectedLineDynamicIndex = 0;
            selectedPointDynamicIndex = -1;
            mainTime = newTime;
            UpdateLineProperties();
            timelineGUI.Invalidate();
            PreviewGraphics.Invalidate();
        }
        private void LinePropertiesKeyFramesTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LinePropertiesKeyFramesTextBox.SelectedIndex != -1)
            {
                mainTime = project.dynamicPath[selectedLineDynamicIndex].KeyFrames[LinePropertiesKeyFramesTextBox.SelectedIndex].Time;
                UpdateLineProperties();
            }
        }

        private void PathLinePointsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PathLinePointsListBox.SelectedIndex != -1)
            {
                selectedPointDynamicIndex = PathLinePointsListBox.SelectedIndex;
                UpdateLineProperties();
            }
        }
        public class TimelineSettings
        {
            public float PixelsPerSecond { get; set; } = 20;
            public float LeftMargin { get; set; }  = 10;
            public float TopMargin { get; set; }  = 10;
            public float PixelsPerShape { get; set; } = 20;
            public float TimelineDotSize { get; set; }  = 5;
            public Font SecondsFont { get; set; } = new Font("Arial", 8, FontStyle.Bold);
            public TimelineSettings(int pixelsPerSecond = 20, int pixelsPerShape = 20, Font secondsFont = null)
            {
                this.PixelsPerSecond = pixelsPerSecond;
                this.PixelsPerShape = pixelsPerShape;
                if (secondsFont != null)
                {
                    this.SecondsFont = secondsFont;
                }
                this.LeftMargin = pixelsPerSecond / 2;
                this.TopMargin = pixelsPerShape / 2;
            }
            public float getFloatXOfFrameTime(int frameTime, int fps)
            {
                return this.LeftMargin + (frameTime) * (this.PixelsPerSecond / fps) + this.PixelsPerSecond;
            }
            public TimelineSettings()
            {
                //JSON fies
            }
        }

        private void timeline_GUI_updater(object sender, PaintEventArgs e)
        {
            timelineGUI.Size = new Size(Convert.ToInt32((float)project.maxtimeSeconds * currentTimelineSettings.PixelsPerSecond + 2 * currentTimelineSettings.LeftMargin),
                Convert.ToInt32(currentTimelineSettings.TopMargin * 2 + currentTimelineSettings.PixelsPerShape * (project.dynamicPath.Count)) + 10);
            int seconds = 0;
            Pen thinWhitePen = new Pen(Color.White);
            float currentTimeX = currentTimelineSettings.LeftMargin + (mainTime + project.fps) * (currentTimelineSettings.PixelsPerSecond / project.fps);
            e.Graphics.DrawLine(thinWhitePen, new PointF(currentTimeX, currentTimelineSettings.PixelsPerShape), new PointF(currentTimeX, timelineGUI.Size.Height));

            // Draw seconds markers
            for (float horizontalPixelsUsed = currentTimelineSettings.LeftMargin; horizontalPixelsUsed < timelineGUI.Size.Width; horizontalPixelsUsed += currentTimelineSettings.PixelsPerSecond)
            {
                e.Graphics.DrawString((seconds - 1).ToString(), currentTimelineSettings.SecondsFont, new SolidBrush(Color.White),
                    new PointF(horizontalPixelsUsed - ((int)((currentTimelineSettings.SecondsFont.Size * seconds.ToString().Count()) / 2)),
                        currentTimelineSettings.TopMargin + timelineGUIHugger.VerticalScroll.Value));
                seconds++;
            }

            // Draw shape number markers
            int shapeNumber = 1;
            for (float verticalPixelsUsed = currentTimelineSettings.TopMargin + currentTimelineSettings.PixelsPerShape; verticalPixelsUsed < timelineGUI.Size.Height; verticalPixelsUsed += currentTimelineSettings.PixelsPerShape)
            {
                e.Graphics.DrawString(shapeNumber.ToString(), currentTimelineSettings.SecondsFont, new SolidBrush(Color.White),
                    new PointF(currentTimelineSettings.LeftMargin + timelineGUIHugger.HorizontalScroll.Value - ((int)((currentTimelineSettings.SecondsFont.Size * shapeNumber.ToString().Count()) / 2)),
                        verticalPixelsUsed));
                shapeNumber++;
            }

            // Draw keyframes for each dynamic path
            int dynamicPathTempIndex = 0;
            TimelineDots.Clear();
            for (float verticalSpaceUsed = currentTimelineSettings.TopMargin; verticalSpaceUsed < Convert.ToInt32(currentTimelineSettings.TopMargin * 2 + currentTimelineSettings.PixelsPerShape * (project.dynamicPath.Count)); verticalSpaceUsed += currentTimelineSettings.PixelsPerShape)
            {
                if (project.dynamicPath.Count != 0 && project.dynamicPath.Count > dynamicPathTempIndex)
                {
                    foreach (PathLineFrame frame in project.dynamicPath[dynamicPathTempIndex].KeyFrames)
                    {
                        TimelineDots.Add(new TimeLinePoint(dynamicPathTempIndex, frame.Time, new Point((int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape))));

                        e.Graphics.FillCircle(new SolidBrush(Color.LightGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape), 4);

                        e.Graphics.FillCircle(new SolidBrush(Color.DarkGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape), 3);
                    }
                    dynamicPathTempIndex++;
                }
            }

            // Draw closest point marker if it's actually close
            if (timelineClosestPoint != null)
            {
                if (getDistance(timelineClosestPoint.Location, timelineMouseLastLocation) < 5)
                {
                    e.Graphics.FillCircle(new SolidBrush(Color.Red), timelineClosestPoint.Location, 3);
                }
            }
        }

        private void timelineGUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int newTimeFrame = (int)((e.Location.X - currentTimelineSettings.LeftMargin - currentTimelineSettings.PixelsPerSecond
                    ) / (currentTimelineSettings.PixelsPerSecond / project.fps));
                if (newTimeFrame > 0)
                {
                    ChangeTime(newTimeFrame);
                }
            }
            double closestPointDistance = 100000;
            for (int j = 0; j < TimelineDots.Count(); j++)
            {
                Point pointLocation = TimelineDots[j].Location;
                if (getDistance(pointLocation, e.Location) < closestPointDistance) //Finds closest point
                {
                    timelineClosestPoint = TimelineDots[j];
                    closestPointDistance = getDistance(timelineClosestPoint.Location, e.Location);
                }
            }
            timelineMouseLastLocation = e.Location;
            timelineGUI.Invalidate();
        }

        private void timelineGUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (timelineClosestPoint != null)
            {
                if (getDistance(timelineClosestPoint.Location, timelineMouseLastLocation) < 5)
                {
                    selectedLineDynamicIndex = timelineClosestPoint.ShapeListIndex;
                    ChangeTime(timelineClosestPoint.FrameTime);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            project.fileHash = "";
            // Calculate the MD5 hash of the project object
            project.fileHash = GetHash(project);
            JsonSerialization.WriteToJsonFile<PathProject>(saveFileDialog1.FileName, project);
        }

        public string GetHash<T>(T currentObject)
        {
            // Calculate the MD5 hash of the project object MADE THIS MYSELF THE STACK OVERFLOW DIDNT WORK SO PROUD
            // Convert the object to a byte array
            byte[] data = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(currentObject)); // Convert object to a byte array

            // Generate the MD5 hash
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(data);
            string hashString = BitConverter.ToString(hash);
            return Convert.ToBase64String(hash);
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            project = JsonSerialization.ReadFromJsonFile<PathProject>(openFileDialog1.FileName);
            String projectsFileHash = project.fileHash;
            project.fileHash = "";
            if (GetHash(project) != projectsFileHash)
            {
                MessageBox.Show("File modified outside of software or corrupted." +
                    " Be careful when using the file as it may be dangerous or damage device. ", "Warning");
            }
            selectedLineDynamicIndex = 0;
            selectedPointDynamicIndex = 0;
            ChangeTime(0);
        }

        private void connectToDACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationPreviewModeData.Text = helios.openDevices().ToString();
        }

        private void disconnectDACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationPreviewModeData.Text = "Disconnected";
            helios.closeDevices();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                if (currentLaserSettings.ShowLaser)
                {
                    while (helios.getStatus(0) == 0)
                    {
                        Thread.Sleep(1);
                    }
                    helios.writeFrame(0, currentLaserSettings.Kpps, 0, laserPoints.ToArray(), laserPoints.Count());
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linePropertiesXOrYCoordinate_ValueChanged(object sender, EventArgs e)
        {
            if (tempIntermediateFramePoint.X == -1 || tempIntermediateFramePoint.Y == -1)
            {
                return;
            }
            if (tempIntermediateFramePoint.X == LinePropertiesXCoordinate.Value && tempIntermediateFramePoint.Y == LinePropertiesYCoordinate.Value)
            {
                return; //If this was set to show the value of a midway frame, dont generate a new keypoint, only if user wanted new keyframe.
            }
            if (GetSelectedFrameWrite().PathPoints.Count > (selectedPointDynamicIndex))
            {
                GetSelectedFrameWrite().PathPoints[selectedPointDynamicIndex] = new Point(Convert.ToInt32(LinePropertiesXCoordinate.Value), Convert.ToInt32(LinePropertiesYCoordinate.Value));
                this.PreviewGraphics.Invalidate();
                UpdateLineProperties();
            }
        }

        private void deleteShape_Click(object sender, EventArgs e)
        {
            if (selectedLineDynamicIndex != -1)
            {
                project.dynamicPath.RemoveAt(selectedLineDynamicIndex);
                ChangeTime(mainTime);
                PreviewGraphics.Invalidate();
                UpdateLineProperties();
                selectedLineDynamicIndex = 0;
                selectedPointDynamicIndex = 0;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {

                currentLaserSettings.ShowLaser = false;
                HeliosPoint[] blackFrame = { new HeliosPoint() };
                blackFrame[0].x = (ushort)(0x000);
                blackFrame[0].y = (ushort)(0x000);
                blackFrame[0].r = (byte)(0x00);
                blackFrame[0].g = (byte)(0x00);
                blackFrame[0].b = (byte)(0x00);
                blackFrame[0].i = (byte)(0x00);
                helios.writeFrame(0, currentLaserSettings.Kpps, 1, blackFrame, 1);
                OptionsToggleProject.Checked = false;
            }
            if (e.KeyChar == 'p')
            {
                PreviewGraphics.Invalidate();
                currentLaserSettings.ShowLaser = true;
            }
        }

        private void OptionsToggleProject_CheckedChanged(object sender, EventArgs e)
        {
            if (OptionsToggleProject.Checked)
            {
                PreviewGraphics.Invalidate();
                currentLaserSettings.ShowLaser = true;
            }
            else
            {
                currentLaserSettings.ShowLaser = false;
                HeliosPoint[] blackFrame = { new HeliosPoint() };
                blackFrame[0].x = (ushort)(0x000);
                blackFrame[0].y = (ushort)(0x000);
                blackFrame[0].r = (byte)(0x00);
                blackFrame[0].g = (byte)(0x00);
                blackFrame[0].b = (byte)(0x00);
                blackFrame[0].i = (byte)(0x00);
                helios.writeFrame(0, currentLaserSettings.Kpps, 1, blackFrame, 1);
            }
        }

        private void LinePropertiesChangeColor_Click(object sender, EventArgs e)
        {
            LinePropertiesColorDialog.ShowDialog(this);
            GetSelectedFrameWrite().PathColor = LinePropertiesColorDialog.Color;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.Show();
            try
            {
                currentLaserSettings = JsonSerialization.ReadFromJsonFile<LaserSettings>(@"//CurrentLaserSettings.Config");
                currentTimelineSettings = JsonSerialization.ReadFromJsonFile<TimelineSettings>(@"//CurrentTimelineSettings.Config");
            }
            catch
            {

            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int sleepTime = 10;
            float timeIncrement = (sleepTime / 1000f) * project.fps;
            float preciseTime = mainTime;
            while (currentlyPlaying)
            {
                Thread.Sleep(sleepTime);
                preciseTime += timeIncrement;
                //Change time method without updating line properties
                selectedPointDynamicIndex = -1;
                mainTime = Convert.ToInt32(preciseTime);
                timelineGUI.Invalidate();
                PreviewGraphics.Invalidate();
                if (mainTime > project.maxtimeSeconds * project.fps)
                {
                    selectedPointDynamicIndex = -1;
                    mainTime = Convert.ToInt32(0);
                    preciseTime = 0;    //This gave me hella headaches
                    timelineGUI.Invalidate();
                    PreviewGraphics.Invalidate();
                }
            }
            if (!currentlyPlaying)
            {
                backgroundWorker2.Dispose();
            }
            return;
        }

        private void projectMaxTimeSelector_ValueChanged(object sender, EventArgs e)
        {
            project.maxtimeSeconds = (float)projectMaxTimeSelector.Value;
        }

        private void TimeLinePlay_CheckedChanged(object sender, EventArgs e)
        {
            currentlyPlaying = TimeLinePlay.Checked;
            if (!currentlyPlaying)
            {
                backgroundWorker2.Dispose();
            }
            else
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }


    }
    #region NOT MY CODE USED FOR SAVING FILES IN A HUMAN READABLE FORMAT & serialising files. I do understand it but because I had so much help I wont take credit.
    /// <summary>
    /// Functions for performing common Json Serialization operations.
    /// <para>Requires the Newtonsoft.Json assembly (Json.Net package in NuGet Gallery) to be referenced in your project.</para>
    /// <para>Only public properties and variables will be serialized.</para>
    /// <para>Use the [JsonIgnore] attribute to ignore specific public properties or variables.</para>
    /// <para>Object to be serialized must have a parameterless constructor.</para>
    /// </summary>
    public static class JsonSerialization
    {
        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
                try
                {
                    writer = new StreamWriter(filePath, append);
                    writer.Write(contentsToWriteToFile);
                }
                catch
                {

                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }

    #endregion
    public static class GraphicsExtensions      //This code (the graphics extensions) was someoene elses but worked really well so i am keeping it
    {
        public static void DrawCircle(this Graphics g, Pen pen,
                                      float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }//End of someone elses code.
        public static void FillCircle(this Graphics g, Brush brush,
                                      Point center, float radius)
        {
            g.FillEllipse(brush, center.X - radius, center.Y - radius,
                          radius + radius, radius + radius);
        }
    }
}