using System.Collections.Generic;
//Point.distance is your friend.

namespace Path
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class PathLine
        {
            string name;
            public string Name  { get; set; }
            List<PathLineFrame> keyFrames; //A list of all keyframes sorted by time
            public List<PathLineFrame> KeyFrames    { get; set; }
            int listIndex;
            public int ListIndex { get; set; }
            public int getValueXWayBetweenTwoPoints(int value1, int value2, float multiplier)
            {
                return Convert.ToInt32(value1 + (value2 - value1) * multiplier);
            }

            bool isHidden = false;
            public bool IsHidden { get; set; }
            public PathLineFrame GenFrameAt(int time)
            {
                PathLineFrame frameBefore;
                PathLineFrame frameAfter;
                frameAfter = new PathLineFrame();
                frameAfter.Time = -1;                       //Set the time to -1 so we can tell if no frames before or after were found
                frameBefore = new PathLineFrame();
                frameBefore.Time = -1;                      //^^^ (I am sure theres an easier way to do this.
                for (int i = 0; i < keyFrames.Count; i++)
                {
                    if (keyFrames[i].Time == time)          //Checks to see if the time lands on a keyframe - This is merely a performance thing
                    {
                        return keyFrames[i];
                    }
                    else if (keyFrames[i].Time < time)      //Finds the latest KeyFrame before the time
                    {
                        frameBefore = keyFrames[i];
                    }
                    else if (frameAfter.Time == -1)         //Finds the first keyframe after the time. Gonna be real theres bound to be a logic error here.
                    {
                        frameAfter = keyFrames[i];
                    }
                }
                if (frameBefore.Time == -1)                  //If before all frames then newframe is the same as the frame after or first frame
                {
                    return frameAfter;
                }
                else if (frameAfter.Time == -1)                   //Literally the same situation as the above if
                {
                    return frameBefore;
                }
                else
                {
                    PathLineFrame newFrame = new PathLineFrame();
                    //SET ALL PROPERTIES TO PROPERTIES IN BETWEEN THEM BOTH
                    float animationProgress = (time - frameBefore.Time) / (frameAfter.Time-frameBefore.Time); //If you set each property to beforeFrame value plus (afterFrame - beforeFrame) * difference  -- This assumes a linear animation hence the constant progress as time moves forward.
                    newFrame.PathColor = Color.FromArgb(
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.A, frameAfter.PathColor.A, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.R, frameAfter.PathColor.R, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.G, frameAfter.PathColor.G, animationProgress),
                        getValueXWayBetweenTwoPoints(frameBefore.PathColor.B, frameAfter.PathColor.B, animationProgress)); //I turned it into a function because I was bound to make a mistake. And because I need to use this function a LOT.
                    while (frameBefore.PathPoints.Count != frameAfter.PathPoints.Count)
                    {
                        if (frameBefore.PathPoints.Count < frameAfter.PathPoints.Count)
                        {
                            frameBefore.PathPoints.Add(frameBefore.PathPoints.Last());
                        }
                        else
                        {
                            frameAfter.PathPoints.Add(frameAfter.PathPoints.Last());
                        }
                    }   //Makes all extra detail grow out the end of the line --im proud of myself for the attention to detail, might be buggy tho
                    for(int i = 0; i < frameBefore.PathPoints.Count;i++)
                    {
                        newFrame.PathPoints.Add(new Point(
                            getValueXWayBetweenTwoPoints(frameBefore.PathPoints[i].X, frameAfter.PathPoints[i].X,animationProgress),
                            getValueXWayBetweenTwoPoints(frameBefore.PathPoints[i].Y, frameAfter.PathPoints[i].Y,animationProgress)
                            ));
                    }
                    return newFrame;
                }
            }   //The majourity of the animation code IF THIS PROJECT DOESNT WORK IMMA CRY
        }
        class PathLineFrame
        {
            int time; //Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public int Time { get; set; }

            Color pathColor;
            public Color PathColor { get; set; }

            List<Point> pathPoints;
            public List<Point> PathPoints { get; set; }
            int listIndex;
            public int ListIndex { get; set; };
            public List<LinePoint> GenKeyPoints(bool middle = false)
            {
                List<LinePoint> KeyPoints= new List<LinePoint>();
                for(int i = 0; i < PathPoints.Count; i++)
                {
                    KeyPoints.Add(new LinePoint(ListIndex, pathPoints[i], false));
                    if(middle && (i+1) != pathPoints.Count)
                    {
                        KeyPoints.Add(new LinePoint(ListIndex, 
                            Convert.ToInt32((pathPoints[i].X + pathPoints[i+1].X) / 2),
                            Convert.ToInt32((pathPoints[i].Y + pathPoints[i + 1].Y) / 2)
                            ,true));
                    }
                }
                return KeyPoints;
            }   
            /*So basically this function generates keypoints, keypoints are any points with details that we might want to adjust
             * This selection tool should work well until there are a lot of points to select*/
            
        }
        class LinePoint
        {
            int shapeListIndex;
            Point location;
            bool isMiddle;
            public LinePoint(int ShapeListIndex, Point Location, bool IsMiddle)
            {
                this.shapeListIndex = ShapeListIndex;
                this.location = Location;
                this.isMiddle = IsMiddle;
            }
            public LinePoint(int ShapeListIndex, int X, int Y, bool IsMiddle)
            {
                this.shapeListIndex = ShapeListIndex;
                this.location = new Point(X,Y);
                this.isMiddle = IsMiddle;
            }
        }
    }
}