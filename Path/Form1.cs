using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using HeliosLaserDAC;
using System.Drawing;
using System.Linq.Expressions;
//project.dynamicPath = the file & whole animation
//framePath = the PathLineFrame array for the individual frame

//REMEMBER THAT ALL SHAPES SHOULD BE STORED IN A 4095 CO-OORDINATE RESOLUTION NOT THE RESOLUTION OF THE SCREEN.

namespace Path
{
    public partial class Form1 : Form
    {
        PathProject project = new PathProject();
        List<PathLineFrame> framePath;      //What individual frames should look like. Should hopefully be generateable from a for loop and get line at project.dynamicPath[i]
        int framePathTime = -1;                  //Gives the time the framePath is generated for

        int mainTime = 0;       //The time in frames that the system is at

        Point mouseLastLocation;   //If set to -1,-1 no line preview should be made - Saving where the mouse went down
        Point timelineMouseLastLocation;

        List<LinePoint> closestPoints = new List<LinePoint>();
        List<LinePoint> closestPointsFrozen = new List<LinePoint>();

        Point closestPoint;

        int selectedLineDynamicIndex = 0;
        PathLineFrame selectedFrameReadOnly;
        int selectedPointDynamicIndex = -1;

        Queue<PathLineFrame> laserFrame;    //This is a step on the way for making laser points.
        List<HeliosPoint> laserPoints = new List<HeliosPoint>();

        bool showCircles = false;
        bool showLines = false;
        bool mouseDown = false;

        bool showLaser = false; //THIS IS ACTUALLY A SAFETY THINGYMIGGY SO DONT SET THIS TO TRUE UNLESS YOU ACC MEAN IT. CHILDRENS EYES ARE AT STEAK.

        //Timeline GUI variables
        TimelineSettings currentTimelineSettings;
        LinePoint timelineClosestPoint;
        List<LinePoint> TimelineDots = new List<LinePoint>();

        //Helios Laser management variables
        public HeliosDac helios = new HeliosDac();
        List<HeliosPoint> heliosLaserPoints = new List<HeliosPoint>();
        LaserSettings currentLaserSettings = new LaserSettings();


        public Form1()
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
            public List<PathLine> dynamicPath = new List<PathLine>();         //The highest level

            bool showLaser = false; //THIS IS ACTUALLY A SAFETY THINGYMIGGY SO DONT SET THIS TO TRUE UNLESS YOU ACC MEAN IT. CHILDRENS EYES ARE AT STEAK.

            public int fps = 30;        //The number of frames per second (to get time in seconds divide time by fps)
        }
        class LaserSettings
        {
            public int kpps = 4000;       //The maximum number of points we should be sending down the dac. Mine was rated at 40KPPS but that could be a false rating at this point.
            public int maxVelocity = 15;
            public int maxAcceleration = 2;
            public int bufferLength = 10;
            public bool project = false;
        }
        List<HeliosPoint> projectToHelios(List<PathLine> dynamicPath)
        {
            List<HeliosPoint> heliosLaserPoints = dynamicPath[0].GenFrameAt(mainTime).GenLaserPoints(currentLaserSettings);

            return heliosLaserPoints;
        }

        Point ConvertToHeliosCoords(Point Original, bool backwards = false)
        {
            float Scale = 4095 / PreviewGraphics.Size.Width;
            if (!backwards)
            {
                return new Point((int)(Original.X * Scale),(int)(Original.Y * Scale));
            }
            else
            {
                return new Point((int)(Original.X / Scale), (int)(Original.Y / Scale));
            }
        }
        static double getDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y,2));
        }
        //not workin vvv
        List<HeliosPoint> travelBetweenPoints(PointF zeroPoint, PointF lastPoint, PointF currentVelocity, PointF endVelocity, int maxVelocity, int maxAcceleration)
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
                timeScalingX = (endVelocity.X - currentVelocity.X) / (maxAcceleration * timeToReachFrameX);
                timeScalingY = timeToReachFrameX / timeToReachFrameY;
                totalTime = timeToReachFrameX * timeScalingX;
            }
            else
            {
                timeScalingY = (endVelocity.Y - currentVelocity.Y) / (maxAcceleration * timeToReachFrameY);
                timeScalingX = timeToReachFrameY / timeToReachFrameX;
                totalTime = timeToReachFrameX * timeScalingX;
            }

            for(float time = 0; time < totalTime; time++)
            {
                currentPoint.x = (ushort) (1.5 * currentVelocity.X * time - 0.5 * endVelocity.X * time + zeroPoint.X);
                currentPoint.y = (ushort) (1.5 * currentVelocity.Y * time - 0.5 * endVelocity.Y * time + zeroPoint.Y);
                currentPoint.r = (Byte) 0;
                currentPoint.g = (Byte) 0;
                currentPoint.b = (Byte) 0;
                currentPoint.i = (Byte) 0;
                returnPoints.Add(currentPoint);
            }

            return returnPoints;
        }         //Beginning of my optimising pathalgorithm
        void updateKeyPointInfo()
        {
            for(int i = 0; i < project.dynamicPath.Count; i++)
            {
                for(int j = 0; j < project.dynamicPath[i].KeyFrames.Count; j++)
                {
                    MessageBox.Show(project.dynamicPath[i].KeyFrames[j].Time.ToString());
                }
            }
        }
        Queue<PathLineFrame> genShapeLaserPath(List<PathLineFrame> framePath)
        {
            List<LinePoint> pointsToTraverse = new List<LinePoint>();
            foreach(PathLineFrame line in framePath)
            {
                pointsToTraverse.Add(new LinePoint(framePath.IndexOf(line), -1, line.PathPoints[0], false, true));
                pointsToTraverse.Add(new LinePoint(framePath.IndexOf(line), -1, line.PathPoints[^1], false, false));
            }
            Queue<PathLineFrame> linesToGenPointsFor = new Queue<PathLineFrame>();
            LinePoint currentPoint;
            LinePoint nextPoint;
            if (pointsToTraverse.Count > 1 && (pointsToTraverse.Count % 2 == 0))
            {
                currentPoint = pointsToTraverse[0];
                //Travels to the next line
                linesToGenPointsFor.Enqueue(framePath[currentPoint.ShapeListIndex]);
                int nextIndex = pointsToTraverse.IndexOf(currentPoint);//Not add one because current point is removed
                pointsToTraverse.Remove(currentPoint);
                currentPoint = pointsToTraverse[nextIndex];
                //This if statement basically travels the line either forewards or backwards.
                pointsToTraverse.Remove(currentPoint);  //Removed because its already been traversed.

                while (pointsToTraverse.Count > 0)  //Start the while loop after drawing the first line.
                {
                    nextPoint = new LinePoint(-1, -1, new Point(100000, 100000), false);    //Reset next point to be hella far away //Here we are settign the ideal closest point.
                    foreach (LinePoint point in pointsToTraverse)
                    {
                        if (getDistance(point.Location, currentPoint.Location) < getDistance(nextPoint.Location, currentPoint.Location))   //This point closer to the goal than the other point
                        {
                            nextPoint = point;
                        }
                    }   //Find where to travel to (yes its the most basic path traversal heuristic but it works quite well when things are simple,
                        //I would do a more complicated one but the points need to be met in certain ways.
                    linesToGenPointsFor.Enqueue(new PathLineFrame(true, currentPoint.Location, nextPoint.Location));
                    nextIndex = pointsToTraverse.IndexOf(nextPoint);    //Needs it to remove the other part of the line. Could have done a for loop for each line to be honest, might be a piece of improvement for the future.
                    pointsToTraverse.Remove(nextPoint);  //And remove the point youve just run away from, you needa remove two points per line/while loop.
                                                         //Its next point that gets removed as this has just been traverrsed
                    currentPoint = nextPoint;   //Travel between two lines (the laser acc has to do this)
                    if (pointsToTraverse.Count > 0)
                    {
                        if (currentPoint.IsStart)   //If yes travel the line then remove the points
                        {
                            linesToGenPointsFor.Enqueue(framePath[currentPoint.ShapeListIndex]);
                            //Index already set above
                        }//This if statement basically travels the line either forewards or backwards.
                        else
                        {
                            PathLineFrame frame = new PathLineFrame(framePath[currentPoint.ShapeListIndex]);
                            frame.Reverse();
                            linesToGenPointsFor.Enqueue(frame);
                            nextIndex = nextIndex - 1;    //Subtract one because the index below isnt affected. Like because the line is reversed yk
                        }
                        currentPoint = new LinePoint(pointsToTraverse[nextIndex]);
                        pointsToTraverse.Remove(pointsToTraverse[nextIndex]);
                    }
                }
                linesToGenPointsFor.Enqueue(new PathLineFrame(true, currentPoint.Location, framePath[0].PathPoints[0]));
            }
            return linesToGenPointsFor;
        }
        class PathLineFrame //SOMETIMES IS A KEYFRAME SOMETIMES ISNT
        {
            private int time; //Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public int Time
            {
                get { return time; }
                set { time = value; }
            }
            private bool hidden;
            public bool Hidden
            {
                get { return hidden; }
                set { hidden = value; }
            }
            private Color pathColor;
            public Color PathColor
            {
                get { return pathColor; }
                set { pathColor = value; }
            }

            private List<Point> pathPoints = new List<Point>();
            public List<Point> PathPoints
            {
                get { return pathPoints; }
                set { pathPoints = value; }
            }
            public void AddPoint(Point NewPoint) 
            { 
                pathPoints.Add(NewPoint); 
            }
            private int listIndex;                      //THE INDEX OF THE LIST VARIES DEPENDING ON IF THE OBJECT IS A KEYFRAME OR NOT
            public int ListIndex
            {
                get { return listIndex; }
                set { listIndex = value; }
            }
            public List<HeliosPoint> GenLaserPoints(LaserSettings currentLaserSettings)
            {
                List<HeliosPoint> points = new List<HeliosPoint>();
                #region higher order maths that i swear is hella close to working but isnt quite working. Think it might have made curves anyway.
                /* DOESNT WORK AS I SO VERY WISHED IT WOULD
                for(int i = 0; i < pathPoints.Count-1; i++)
                {
                    double lineFrameLength = getDistance(pathPoints[i], pathPoints[i+1]);
                    double lineFrameProcessed = (double)(currentLaserSettings.maxAcceleration - currentLaserSettings.bufferLength);
                    HeliosPoint currentPoint;
                    double deltaY = pathPoints[i+1].Y - pathPoints[i].Y;
                    double deltaX = pathPoints[i + 1].X - pathPoints[i].X;
                    int velocity = currentLaserSettings.maxAcceleration;
                    while (lineFrameProcessed < (lineFrameLength / 2))
                    {
                        currentPoint.x = (ushort) (pathPoints[i].X + (lineFrameProcessed * deltaX));
                        currentPoint.y = (ushort) (pathPoints[i].Y + (lineFrameProcessed * deltaY));
                        currentPoint.r = pathColor.R;
                        currentPoint.g = pathColor.G;
                        currentPoint.b = pathColor.B;
                        currentPoint.i = pathColor.A;
                        points.Add(currentPoint);
                        if (velocity + currentLaserSettings.maxAcceleration < currentLaserSettings.maxVelocity)
                        {
                            velocity += currentLaserSettings.maxAcceleration;
                        }
                        lineFrameProcessed += velocity;
                    }
                    lineFrameProcessed -= velocity;
                    lineFrameProcessed = lineFrameLength - lineFrameProcessed;
                    while (lineFrameProcessed < (lineFrameLength + currentLaserSettings.bufferLength))
                    {
                        currentPoint.x = (ushort)(pathPoints[i].X + (lineFrameProcessed * deltaX));
                        currentPoint.y = (ushort)(pathPoints[i].Y + (lineFrameProcessed * deltaY));
                        currentPoint.r = pathColor.R;
                        currentPoint.g = pathColor.G;
                        currentPoint.b = pathColor.B;
                        currentPoint.i = pathColor.A;
                        points.Add(currentPoint);
                        if (velocity + currentLaserSettings.maxAcceleration < currentLaserSettings.maxVelocity)
                        {
                            velocity += currentLaserSettings.maxAcceleration;
                        }
                        lineFrameProcessed += velocity;
                    }
                }*/
                #endregion
                //If we have constant acceleration life is hella easy thanks to a person named Mrs SUVAT (idk if Suvat equations were named after a man or a woman) 
                //Also because this is a legal exam I needa say i'm joking I know Suvat is the acronym for displacement, intial velocity, etc.
                //S = ut + 1/2 a t^2 (hold on one sec we got to the good bit in my playlist I needa jam for a sec)
                if (pathPoints.Count > 0)
                {
                    Queue<double> beginningDisplacements = new Queue<double>();
                    Stack<double> endingDisplacements = new Stack<double>();
                    int acceleratingTime = currentLaserSettings.maxVelocity / currentLaserSettings.maxAcceleration;
                    double currentDisplacement = 0;
                    double halfDisplacement = getDistance(this.pathPoints[0], this.pathPoints[1]) / 2;//I realise that I havent added multi point compatibility this is a relatively easy fix for before summer :)
                    for (double t = 0; t < acceleratingTime && (0.5 * t * t * currentLaserSettings.maxAcceleration) < halfDisplacement; t++)
                    {
                        beginningDisplacements.Enqueue(0.5 * t * t * currentLaserSettings.maxAcceleration);
                        endingDisplacements.Push(0.5 * t * t * currentLaserSettings.maxAcceleration);
                        currentDisplacement = 0.5 * t * t * currentLaserSettings.maxAcceleration;
                    }
                    if (currentDisplacement < halfDisplacement + (currentLaserSettings.maxVelocity / 2))//If distance left to cover will take more than a point.
                    {
                        for (int i = 0;currentDisplacement + currentLaserSettings.maxVelocity< halfDisplacement; i++)
                        {
                            currentDisplacement += currentLaserSettings.maxVelocity;
                            beginningDisplacements.Enqueue(currentDisplacement + currentLaserSettings.maxVelocity);
                            endingDisplacements.Push(currentDisplacement + currentLaserSettings.maxVelocity); //Should make it so the middle points are evenly spaced.0

                        }   // SHCHC A GOOOD MIXXXX I CANNOT EXPLAIN HOW GOOD IT IS ITS FIREREEEEEE  https://youtu.be/WKuaujIHBT4?t=3914

                        // This bit fills in the middle so the velocity isnt bigger than the max velocity. The rest uses the max acceleration.
                    }
                    HeliosPoint currentPoint = new HeliosPoint();
                    currentDisplacement = 0;
                    double horizontaleScaleValue = (pathPoints[1].X - pathPoints[0].X) / getDistance(this.pathPoints[0], this.pathPoints[1]);
                    double verticalScaleValue = (pathPoints[1].Y - pathPoints[0].Y) / getDistance(this.pathPoints[0], this.pathPoints[1]);
                    while(beginningDisplacements.Count > 0)
                    {
                        currentDisplacement = beginningDisplacements.Dequeue();
                        currentPoint.x = (ushort)(pathPoints[0].X + currentDisplacement * horizontaleScaleValue);
                        currentPoint.y = (ushort)(pathPoints[0].Y + currentDisplacement * verticalScaleValue);
                        currentPoint.r = (byte)((this.pathColor.R * this.pathColor.A) / 0xFF);
                        currentPoint.g = (byte)((this.pathColor.G * this.pathColor.A) / 0xFF);
                        currentPoint.b = (byte)((this.pathColor.B * this.pathColor.A) / 0xFF);
                        currentPoint.i = (byte)(this.pathColor.A);
                        points.Add(currentPoint);
                    }
                    while(endingDisplacements.Count > 0)
                    {
                        currentDisplacement = endingDisplacements.Pop();
                        currentPoint.x = (ushort)(pathPoints[1].X - currentDisplacement * horizontaleScaleValue);
                        currentPoint.y = (ushort)(pathPoints[1].Y - currentDisplacement * verticalScaleValue);
                        currentPoint.r = (byte)((this.pathColor.R * this.pathColor.A) / 0xFF);
                        currentPoint.g = (byte)((this.pathColor.G * this.pathColor.A) / 0xFF);
                        currentPoint.b = (byte)((this.pathColor.B * this.pathColor.A) / 0xFF);
                        currentPoint.i = (byte)(this.pathColor.A);
                        points.Add(currentPoint);
                    }
                }
                return points;
            }
            public void Reverse()
            {
                pathPoints.Reverse();
            }
            public List<LinePoint> GenKeyPoints(bool middle = false)
            {
                List<LinePoint> KeyPoints = new List<LinePoint>();

                for (int i = 0; i < PathPoints.Count - 1; i++)
                {
                    KeyPoints.Add(new LinePoint(ListIndex, i, pathPoints[i], false));
                    if (middle)
                    {
                        KeyPoints.Add(new LinePoint(ListIndex, i, new Point(
                                Convert.ToInt32((pathPoints[i].X + pathPoints[i+1].X) / 2),
                                Convert.ToInt32((pathPoints[i].Y + pathPoints[i+1].Y) / 2))
                                , true));
                    }
                }
                
                KeyPoints.Add(new LinePoint(ListIndex, PathPoints.Count-1, pathPoints.Last(), false));
                return KeyPoints;
            }   
            /*So basically this function generates keypoints, keypoints are any points with details that we might want to adjust
             * This selection tool should work well until there are a lot of points to select*/
            public int getValueXWayBetweenTwoPoints(int value1, int value2, float multiplier)
            {
                return Convert.ToInt32(value1 + (value2 - value1) * multiplier);
            } //This is a function to work out the properties of a nonkeyframe. Iz guud.
            public PathLineFrame(int Time, Color PathColor, List<Point> PathPoints, int ListIndex)
            {
                this.time = Time;
                this.pathColor = PathColor;
                this.pathPoints = PathPoints;
                this.listIndex = ListIndex;
            }   //Basic constructor
            public PathLineFrame(int Time, Color PathColor, Point Point1, Point Point2, int ListIndex)
            {
                time = Time;
                pathColor = PathColor;
                pathPoints = new List<Point>();
                pathPoints.Add(Point1);
                pathPoints.Add(Point2);
                listIndex = ListIndex;
            }   //Mid Constructor
            public PathLineFrame(int Time, Color PathColor, int Point1x, int Point1y, int Point2x, int Point2y, int ListIndex)
            {
                time = Time;
                pathColor = PathColor;
                pathPoints = new List<Point>();
                pathPoints.Add(new Point(Point1x,Point1y));
                pathPoints.Add(new Point(Point2x,Point2y));
                listIndex = ListIndex;
            }   //Easy Constructor
            public PathLineFrame()  //Json Constructor
            {
                //Just dont use this
            }
            public PathLineFrame(bool hidden, Point Point1, Point Point2)
            {
                if (hidden)
                {
                    this.time = -1;
                    this.pathColor = Color.Black;
                    this.pathPoints = new List<Point>();
                    this.pathPoints.Add(Point1);
                    this.pathPoints.Add(Point2);
                    this.hidden = hidden;
                    this.listIndex = -1;
                }
            }
            public PathLineFrame(float AnimationProgress, PathLineFrame FrameBefore, PathLineFrame FrameAfter)
            {
                time = getValueXWayBetweenTwoPoints(FrameBefore.time, FrameAfter.time, AnimationProgress);
                pathColor = Color.FromArgb(
                        getValueXWayBetweenTwoPoints(FrameBefore.PathColor.A, FrameAfter.PathColor.A, AnimationProgress),
                        getValueXWayBetweenTwoPoints(FrameBefore.PathColor.R, FrameAfter.PathColor.R, AnimationProgress),
                        getValueXWayBetweenTwoPoints(FrameBefore.PathColor.G, FrameAfter.PathColor.G, AnimationProgress),
                        getValueXWayBetweenTwoPoints(FrameBefore.PathColor.B, FrameAfter.PathColor.B, AnimationProgress)); //I turned it into a function because I was bound to make a mistake. And because I need to use this function a LOT.
                while (FrameBefore.pathPoints.Count != FrameAfter.PathPoints.Count)
                {
                    if (FrameBefore.pathPoints.Count < FrameAfter.PathPoints.Count)
                    {
                        FrameAfter.pathPoints.Add(FrameBefore.PathPoints.Last());
                    }
                    else
                    {
                        FrameBefore.pathPoints.Add(FrameAfter.PathPoints.Last());
                    }
                }   //Makes all extra detail grow out the end of the line --im proud of myself for the attention to detail, might be buggy tho
                for (int i = 0; i < FrameAfter.PathPoints.Count; i++)
                {
                    pathPoints.Add(new Point(
                        getValueXWayBetweenTwoPoints(FrameBefore.pathPoints[i].X, FrameAfter.pathPoints[i].X, AnimationProgress),
                        getValueXWayBetweenTwoPoints(FrameBefore.pathPoints[i].Y, FrameAfter.pathPoints[i].Y, AnimationProgress)
                        ));
                }
            }
            public PathLineFrame(int newTime, PathLineFrame frame)
            {
                time = newTime;
                pathColor = frame.pathColor;
                pathPoints = frame.pathPoints;

            }
            public PathLineFrame(PathLineFrame frame)
            {
                time = frame.Time;
                listIndex = frame.ListIndex;
                pathColor = frame.PathColor;
                pathPoints = new List<Point>();
                foreach(Point point in frame.PathPoints)
                {
                    pathPoints.Add(point);
                }
            }
        }
        class PathLine
        {
            string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            List<PathLineFrame> keyFrames; //A list of all keyframes sorted by time
            public List<PathLineFrame> KeyFrames
            {
                get { return keyFrames; }
                set { keyFrames = value; }
            }

            int dynamicPathIndex;
            public int DynamicPathIndex
            {
                get { return dynamicPathIndex;}
            }

            bool isHidden = false;
            public bool IsHidden
            {
                get { return isHidden;}
                set { isHidden = value;}
            }
            public PathLineFrame GetFrameAt(int FrameTime)  //Literally a recursive algorithm. Didnt even make it on purpose.
            {
                foreach(PathLineFrame frame in keyFrames)
                {
                    if(frame.Time == FrameTime)
                    {
                        return frame;
                    }
                }
                KeyFrames.Add(new PathLineFrame(GenFrameAt(FrameTime)));
                this.SortKeyFramesByTime();
                return GetFrameAt(FrameTime);
            }
            public PathLineFrame GenFrameAt(int frameTime)
            {
                int frameBeforeIndex = -1;
                int frameAfterIndex = -1;
                PathLineFrame newFrame;
                for (int i = 0; i < keyFrames.Count; i++)       //DO A BINARY SEARCH HERE
                {
                    if (keyFrames[i].Time == frameTime)          //Checks to see if the time lands on a keyframe - This is merely a performance thing
                    {
                        newFrame = new PathLineFrame(keyFrames[i]);
                        return newFrame;
                    }
                    else if (keyFrames[i].Time < frameTime)      //Finds the latest KeyFrame before the time
                    {
                        frameBeforeIndex = i;
                    }
                    else if (frameAfterIndex == -1)         //Finds the first keyframe after the time. Gonna be real theres bound to be a logic error here.
                    {
                        frameAfterIndex = i;
                    }
                }
                if (frameBeforeIndex == -1)                  //If before all frames then newframe is the same as the frame after or first frame
                {
                    newFrame = new PathLineFrame(keyFrames[frameAfterIndex]);
                    newFrame.Time = frameTime;
                    return newFrame;
                }
                else if (frameAfterIndex == -1)                   //Literally the same situation as the above if
                {
                    newFrame = new PathLineFrame(keyFrames[frameBeforeIndex]);
                    newFrame.Time = frameTime;
                    return newFrame;
                }
                else
                {
                    PathLineFrame frameAfter = new PathLineFrame(keyFrames[frameAfterIndex]);
                    PathLineFrame frameBefore = new PathLineFrame(keyFrames[frameBeforeIndex]);
                    
                    //SET ALL PROPERTIES TO PROPERTIES IN BETWEEN THEM BOTH
                    float animationProgress = ((float) (frameTime - frameBefore.Time) / (float) (frameAfter.Time - frameBefore.Time)); //If you set each property to beforeFrame value plus (afterFrame - beforeFrame) * difference  -- This assumes a linear animation hence the constant progress as time moves forward.
                    return new PathLineFrame(animationProgress,frameBefore,frameAfter);
                }
            }   //The majourity of the animation code IF THIS PROJECT DOESNT WORK IMMA CRY
            public PathLine(string Name, PathLineFrame KeyFrame, int DynamicPathIndex)
            { //For ListIndex just do Path
                name = Name; 
                keyFrames = new List<PathLineFrame>();
                keyFrames.Add(KeyFrame);
                dynamicPathIndex = DynamicPathIndex;
            }
            public PathLine()
            {
                //Needed for JSON Conversion IDK why its just how the extension works
            }
            public void SortKeyFramesByTime()
            {
                //Here I impliment quicksort partially explained here: https://www.youtube.com/watch?v=SLauY6PpjW4&t=15s
                //I used a recursive routine
                QuicksortByTime(keyFrames);
            }
            public List<PathLineFrame> QuicksortByTime(List<PathLineFrame> list)
            {
                if(list.Count() == 0)
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
                    else if(list[i].Time > pivot.Time)
                    {
                        right.Add(list[i]);
                    }
                }
                List<PathLineFrame> leftSorted = QuicksortByTime(left);
                List<PathLineFrame> rightSorted = QuicksortByTime(right);
                list.Clear();
                for(int i = 0; i < leftSorted.Count; i++)
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
        class LinePoint
        {
            int shapeListIndex;
            public int ShapeListIndex
            {
                get { return shapeListIndex; }
                set { shapeListIndex = value; }
            }
            int pathPointsListIndex;
            public int PathPointsListIndex
            {
                get { return pathPointsListIndex; }
                set { pathPointsListIndex = value; }
            }
            Point location;
            public Point Location
            {
                get { return location; }
                set { location = value; }
            }
            bool isMiddle;
            public bool IsMiddle
            {
                get { return isMiddle; }
            }
            bool isStart;
            public bool IsStart
            {
                get { return isStart; }
            }
            public LinePoint(LinePoint newLinePoint)
            {
                this.shapeListIndex = newLinePoint.ShapeListIndex;
                this.location = new Point(newLinePoint.Location.X, newLinePoint.Location.Y);
                this.isMiddle = newLinePoint.IsMiddle;
                this.pathPointsListIndex = newLinePoint.PathPointsListIndex;
                this.isStart = newLinePoint.IsStart;
            }
            public LinePoint(int ShapeListIndex, int PathPointsListIndex, Point Location, bool IsMiddle, bool IsStart = false)
            {
                this.shapeListIndex = ShapeListIndex;
                this.location = Location;
                this.isMiddle = IsMiddle;
                this.pathPointsListIndex = PathPointsListIndex;
                this.isStart = IsStart;
            }

        }

        //THE GRAPHICS PANEL
        private void PreviewGraphics_Paint(object sender, PaintEventArgs e)
        {
            //Gen framePath (useful for lazer as well.)
            if(framePathTime != mainTime)
            {
                framePathTime = mainTime;
                framePath = new List<PathLineFrame>();
                for(int i = 0; i < project.dynamicPath.Count(); i++)
                {
                    PathLineFrame newFrame = project.dynamicPath[i].GenFrameAt(framePathTime);
                    newFrame.ListIndex = framePath.Count;
                    framePath.Add(newFrame);
                }
            }
            InformationFrameListCountInfo.Text = framePath.Count().ToString();

            laserFrame = genShapeLaserPath(framePath);  //Literally generates the laser Path
            laserPoints.Clear();
            foreach(PathLineFrame laserLine in laserFrame)
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
                    e.Graphics.FillCircle(new SolidBrush(Color.Pink), ConvertToHeliosCoords(new Point(point.x,point.y), true), 2);
                }
            }
            //Draw lines, dots and selection bits.
            Pen linePen;
            double closestPointDistance = 1000000;
            
            for (int i = 0; i < framePath.Count(); i++) //Goes through all the lines
            {
                //This bit draws all the points out, and the lines if needed, and also shows the points..
                linePen = new Pen(framePath[i].PathColor);
                for(int j = 0; j < framePath[i].PathPoints.Count() - 1; j++)    //REMEMBER TO UNCOMMENT THIS
                {
                    if (previewShapesToolStripMenuItem.Checked)
                    {
                        e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(framePath[i].PathPoints[j], true), ConvertToHeliosCoords(framePath[i].PathPoints[j + 1], true));
                    }
                }
                
                //Im pretty sure this is the function that provides closest points.
                List<LinePoint> keyPoints = new List<LinePoint>();
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
                    if(getDistance(pointLocation,mouseLastLocation) < closestPointDistance) //Finds closest point
                    {
                        closestPoint = keyPoints[j].Location;
                        closestPoints.Clear();
                        closestPoints.Add(keyPoints[j]);
                        InformationClosestPointData.Text = closestPoint.ToString();
                        closestPointDistance = getDistance(pointLocation, mouseLastLocation);
                    }
                    else if(getDistance(pointLocation, mouseLastLocation) == closestPointDistance)  //Adds all lines to the closest point, pretty nifty if you ask me :)
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
            if(currentLaserSettings.project)
            {
                //ADD IN THE PROJECTION CODE HERE
            }
        }
        private void PreviewGraphics_MouseDown(object sender, MouseEventArgs e)                         //MOUSE MOVEMENT
        {
            //If mouse down, create a new line or frame.
            if (OptionsDrawLineMode.Checked)
            {
                project.dynamicPath.Add(new PathLine("(" + ConvertToHeliosCoords(e.Location).X + "," + ConvertToHeliosCoords(e.Location).Y + ")", new PathLineFrame(mainTime, DrawerColorDialog.Color, new List<Point>(), project.dynamicPath.Count), project.dynamicPath.Count));
                selectedLineDynamicIndex = project.dynamicPath.Count-1;
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
                    closestPointsFrozen = new List<LinePoint>(closestPoints);
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
                else if(OptionsSelectModeButton.Checked)
                {
                    foreach(var point in closestPointsFrozen)
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
                if(project.dynamicPath[selectedLineDynamicIndex].KeyFrames.Count == 0)
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
            if(maxHorizontalSpace < maxVerticalSpace)
            {
                maxVerticalSpace = maxHorizontalSpace;
            }
            else
            {
                maxHorizontalSpace = maxVerticalSpace;  //The actual max size is the smallest of the two.
            }
            PreviewGraphics.Size = new Size(maxHorizontalSpace - PreviewGraphics.Margin.Horizontal, maxVerticalSpace - 2*PreviewGraphics.Margin.Vertical);
            TimeLinePanel.Size = new Size(splitContainer1.Panel1.Width - TimeLinePanel.Margin.Horizontal, splitContainer1.Panel1.Height - maxVerticalSpace);
            TimeLinePanel.Location = new Point(TimeLinePanel.Margin.Left,splitContainer1.Panel1.Height - (TimeLinePanel.Height + TimeLinePanel.Margin.Bottom)); 
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
                    mainTime = Convert.ToInt32(TimeLineFramesInput.Text)*project.fps;
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
            timelineGUI.Invalidate();
            //Select PathLineFrame
            if (project.dynamicPath.Count != 0)
            {
                //Make sure all indexes are correct
                if (selectedLineDynamicIndex > project.dynamicPath.Count)
                {
                    selectedLineDynamicIndex = 0;
                    selectedPointDynamicIndex = 0;
                }
                selectedFrameReadOnly = project.dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime); //Self updating
                if (selectedPointDynamicIndex > selectedFrameReadOnly.PathPoints.Count)
                {
                    selectedPointDynamicIndex = 0;
                }
                //selectedLineDynamicIndex = needs to be selected via the GUI or maybe a button at some point.
                selectedFrameReadOnly = project.dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime); //Self updating
                //selectedPointDynamicIndex = must be changed via a specific function
                //Add to the points textbox
                PathLinePointsListBox.Items.Clear();
                for (int i = 0; i < selectedFrameReadOnly.PathPoints.Count; i++)
                {
                    PathLinePointsListBox.Items.Add(selectedFrameReadOnly.PathPoints[i].ToString());
                }
                //PathLinePointsListBox.SelectedItem = selectedFrameReadOnly.PathPoints[selectedPointDynamicIndex].ToString();
                //Line properties title
                LinePropertiesTitle.Text = "Line Properties: " + project.dynamicPath[selectedLineDynamicIndex].Name;
                LinePropertiesPathIndexData.Text = selectedLineDynamicIndex.ToString();

                //Update the colors and coordinates
                if (selectedPointDynamicIndex != -1) 
                {
                    Point tempPoint = project.dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime).PathPoints[selectedPointDynamicIndex];
                    LinePropertiesXCoordinate.Value = tempPoint.X;
                    LinePropertiesYCoordinate.Value = tempPoint.Y;
                }

                //Add the keyframes to the keyframe textbox
                LinePropertiesKeyFramesTextBox.Items.Clear();
                for (int i = 0; i < project.dynamicPath[selectedLineDynamicIndex].KeyFrames.Count; i++)
                {
                    LinePropertiesKeyFramesTextBox.Items.Add(project.dynamicPath[selectedLineDynamicIndex].KeyFrames[i].Time);
                }
                //LinePropertiesKeyFramesTextBox.SelectedItem = mainTime;
                LinePropertiesChangeColor.BackColor = project.dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime).PathColor;
            }
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
        class TimelineSettings
        {
            public float PixelsPerSecond = 20;
            public float LeftMargin = 10;
            public float TopMargin = 10;
            public float PixelsPerShape = 20;
            public float TimelineDotSize = 5;
            public Font SecondsFont = new Font("Arial", 8, FontStyle.Bold);
            public TimelineSettings(int pixelsPerSecond = 20, int pixelsPerShape = 20, Font secondsFont = null)
            {
                this.PixelsPerSecond = pixelsPerSecond;
                this.PixelsPerShape = pixelsPerShape;
                if(secondsFont != null)
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
        }

        private void timeline_GUI_updater(object sender, PaintEventArgs e)
        {
            timelineGUI.Size = new Size(Convert.ToInt32((float)projectMaxTimeSelector.Value*currentTimelineSettings.PixelsPerSecond + 2*currentTimelineSettings.LeftMargin)
                ,Convert.ToInt32(currentTimelineSettings.TopMargin*2 + currentTimelineSettings.PixelsPerShape*(project.dynamicPath.Count)) + 10);
            int seconds = 0;
            Pen thinWhitePen = new Pen(Color.White);
            float currentTimeX = currentTimelineSettings.LeftMargin + (mainTime + project.fps) * (currentTimelineSettings.PixelsPerSecond / project.fps);
            e.Graphics.DrawLine(thinWhitePen, new PointF(currentTimeX, currentTimelineSettings.PixelsPerShape), new PointF(currentTimeX, timelineGUI.Size.Height));
            for (float horizontalPixelsUsed = currentTimelineSettings.LeftMargin; horizontalPixelsUsed < timelineGUI.Size.Width; horizontalPixelsUsed += currentTimelineSettings.PixelsPerSecond)
            {
                e.Graphics.DrawString((seconds-1).ToString(), currentTimelineSettings.SecondsFont, new SolidBrush(Color.White), new PointF(horizontalPixelsUsed - ((int)((currentTimelineSettings.SecondsFont.Size * seconds.ToString().Count()) / 2)),currentTimelineSettings.TopMargin + timelineGUIHugger.VerticalScroll.Value));
                seconds++;
            }
            int shapeNumber = 1; //Skip the first zero for aesthetics.
            for (float verticalPixelsUsed = currentTimelineSettings.TopMargin + currentTimelineSettings.PixelsPerShape; verticalPixelsUsed < timelineGUI.Size.Height; verticalPixelsUsed += currentTimelineSettings.PixelsPerShape)
            {
                e.Graphics.DrawString(shapeNumber.ToString(), currentTimelineSettings.SecondsFont, new SolidBrush(Color.White), new PointF(currentTimelineSettings.LeftMargin + timelineGUIHugger.HorizontalScroll.Value - ((int)((currentTimelineSettings.SecondsFont.Size * shapeNumber.ToString().Count()) / 2)), verticalPixelsUsed));
                shapeNumber++;
            }
            int dynamicPathTempIndex = 0;
            TimelineDots.Clear();
            for(float verticalSpaceUsed = currentTimelineSettings.TopMargin; verticalSpaceUsed < Convert.ToInt32(currentTimelineSettings.TopMargin * 2 + currentTimelineSettings.PixelsPerShape * (project.dynamicPath.Count)); verticalSpaceUsed += currentTimelineSettings.PixelsPerShape)
            {
                if (project.dynamicPath.Count != 0 && project.dynamicPath.Count > dynamicPathTempIndex)
                {
                    foreach (PathLineFrame frame in project.dynamicPath[dynamicPathTempIndex].KeyFrames)
                    {
                        TimelineDots.Add(new LinePoint(dynamicPathTempIndex, frame.Time, new Point((int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape)), false));

                        e.Graphics.FillCircle(new SolidBrush(Color.LightGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time,project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape), 4);

                        e.Graphics.FillCircle(new SolidBrush(Color.DarkGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, project.fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape), 3);
                    }
                    dynamicPathTempIndex++;
                }
            }
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
            if(e.Button == MouseButtons.Left)
            {
                int newTimeFrame = (int)((e.Location.X - currentTimelineSettings.LeftMargin - currentTimelineSettings.PixelsPerSecond
                    ) / (currentTimelineSettings.PixelsPerSecond / project.fps));
                if (newTimeFrame > 0)
                {
                    ChangeTime(newTimeFrame);
                }
            }
            double closestPointDistance = 100000;
            for(int j = 0; j < TimelineDots.Count(); j++)
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
                    ChangeTime(timelineClosestPoint.PathPointsListIndex);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            JsonSerialization.WriteToJsonFile<PathProject>(saveFileDialog1.FileName, project);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            project = JsonSerialization.ReadFromJsonFile<PathProject>(openFileDialog1.FileName);
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
                if (currentLaserSettings.project)
                {
                    while (helios.getStatus(0) == 0)
                    {
                        Thread.Sleep(1);
                    }
                    helios.writeFrame(0, 40000, 0, laserPoints.ToArray(), laserPoints.Count());
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
            if(GetSelectedFrameWrite().PathPoints.Count > (selectedPointDynamicIndex))
            {
                GetSelectedFrameWrite().PathPoints[selectedPointDynamicIndex] = new Point(Convert.ToInt32(LinePropertiesXCoordinate.Value), Convert.ToInt32(LinePropertiesYCoordinate.Value));
                this.PreviewGraphics.Invalidate();
                UpdateLineProperties();
            }
        }

        private void deleteShape_Click(object sender, EventArgs e)
        {
            project.dynamicPath.RemoveAt(selectedLineDynamicIndex);
            ChangeTime(mainTime);
            PreviewGraphics.Invalidate();
            UpdateLineProperties();
            selectedLineDynamicIndex = 0;
            selectedPointDynamicIndex = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ' ')
            {

                currentLaserSettings.project = false;
                HeliosPoint[] blackFrame = { new HeliosPoint() };
                blackFrame[0].x = (ushort)(0x000);
                blackFrame[0].y = (ushort)(0x000);
                blackFrame[0].r = (byte)(0x00);
                blackFrame[0].g = (byte)(0x00);
                blackFrame[0].b = (byte)(0x00);
                blackFrame[0].i = (byte)(0x00);
                helios.writeFrame(0,currentLaserSettings.kpps,1,blackFrame, 1);
                OptionsToggleProject.Checked = false;
            }
            if (e.KeyChar == 'p')
            {
                PreviewGraphics.Invalidate();
                currentLaserSettings.project = true;
            }
        }

        private void OptionsToggleProject_CheckedChanged(object sender, EventArgs e)
        {
            if (OptionsToggleProject.Checked)
            {
                PreviewGraphics.Invalidate();
                currentLaserSettings.project = true;
            }
            else
            {
                currentLaserSettings.project = false;
                HeliosPoint[] blackFrame = { new HeliosPoint() };
                blackFrame[0].x = (ushort)(0x000);
                blackFrame[0].y = (ushort)(0x000);
                blackFrame[0].r = (byte)(0x00);
                blackFrame[0].g = (byte)(0x00);
                blackFrame[0].b = (byte)(0x00);
                blackFrame[0].i = (byte)(0x00);
                helios.writeFrame(0, currentLaserSettings.kpps, 1, blackFrame, 1);
            }
        }

        private void LinePropertiesChangeColor_Click(object sender, EventArgs e)
        {
            LinePropertiesColorDialog.ShowDialog(this);
            project.dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).PathColor = LinePropertiesColorDialog.Color;
        }
    }
    #region NOT MY CODE USED FOR SAVING FILES IN A HUMAN READABLE FORMAT
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