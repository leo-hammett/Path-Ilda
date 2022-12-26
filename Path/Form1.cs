using System.Collections.Generic;
//dynamicPath = the file & whole animation
//framePath = the PathLineFrame array for the individual frame

//REMEMBER THAT ALL SHAPES SHOULD BE STORED IN A 4095 CO-OORDINATE RESOLUTION NOT THE RESOLUTION OF THE SCREEN.

namespace Path
{
    public partial class Form1 : Form
    {
        List<PathLine> dynamicPath;         //The highest level
        List<PathLineFrame> framePath;      //What individual frames should look like. Should hopefully be generateable from a for loop and get line at dynamicPath[i]
        int framePathTime;                  //Gives the time the framePath is generated for

        int time;       //The time in frames that the system is at
        int fps;        //The number of frames per second (to get time in seconds divide time by fps)
        int kpps;       //The maximum number of points we should be sending down the dac. Mine was rated at 40KPPS but that could be a false rating at this point.

        Point newPoint1;   //If set to -1,-1 no line preview should be made - Saving where the mouse went down
        Point newPoint2;   //Should ideally be wherever the mouse is (is -1,-1 when mouse is outside the box)
        public Form1()
        {
            InitializeComponent();
            dynamicPath = new List<PathLine>();
            framePath = new List<PathLineFrame>();
            newPoint1 = new Point(-1, -1);
            time = 0;
        }
        class PathLine
        {
            string name;
            public string Name  { get; set; }

            List<PathLineFrame> keyFrames; //A list of all keyframes sorted by time
            public List<PathLineFrame> KeyFrames    { get; set; }

            int dynamicPathIndex;
            public int DynamicPathIndex { get; set; }

            bool isHidden = false;
            public bool IsHidden { get; set; }

            public PathLineFrame GenFrameAt(int time)
            {
                int frameBeforeIndex = -1;
                int frameAfterIndex = -1;
                for (int i = 0; i < keyFrames.Count; i++)
                {
                    if (keyFrames[i].Time == time)          //Checks to see if the time lands on a keyframe - This is merely a performance thing
                    {
                        return keyFrames[i];
                    }
                    else if (keyFrames[i].Time < time)      //Finds the latest KeyFrame before the time
                    {
                        PathLineFrame frameBefore = keyFrames[i];
                    }
                    else if (frameAfterIndex == -1)         //Finds the first keyframe after the time. Gonna be real theres bound to be a logic error here.
                    {
                        PathLineFrame frameAfter = keyFrames[i];
                    }
                }
                if (frameBeforeIndex == -1)                  //If before all frames then newframe is the same as the frame after or first frame
                {
                    return KeyFrames[frameAfterIndex];
                }
                else if (frameAfterIndex == -1)                   //Literally the same situation as the above if
                {
                    return KeyFrames[frameBeforeIndex];
                }
                else
                {
                    PathLineFrame frameAfter = keyFrames[frameAfterIndex];
                    PathLineFrame frameBefore = keyFrames[frameBeforeIndex];
                    
                    //SET ALL PROPERTIES TO PROPERTIES IN BETWEEN THEM BOTH
                    float animationProgress = (time - frameBefore.Time) / (frameAfter.Time-frameBefore.Time); //If you set each property to beforeFrame value plus (afterFrame - beforeFrame) * difference  -- This assumes a linear animation hence the constant progress as time moves forward.
                    
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

        }
        class PathLineFrame //SOMETIMES IS A KEYFRAME SOMETIMES ISNT
        {
            int time; //Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public int Time { get; set; }

            Color pathColor;
            public Color PathColor { get; set; }

            List<Point> pathPoints;
            public List<Point> PathPoints { get; set; }
            public void AddPoint(Point NewPoint) { pathPoints.Add(NewPoint); }
            int listIndex;                      //THE INDEX OF THE LIST VARIES DEPENDING ON IF THE OBJECT IS A KEYFRAME OR NOT
            public int ListIndex { get; set; }
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
            public int getValueXWayBetweenTwoPoints(int value1, int value2, float multiplier)
            {
                return Convert.ToInt32(value1 + (value2 - value1) * multiplier);
            } //This is a function to work out the properties of a nonkeyframe. Iz guud.
            public PathLineFrame(int Time, Color PathColor, List<Point> PathPoints, int ListIndex)
            {
                time = Time;
                pathColor = PathColor;
                pathPoints = PathPoints;
                listIndex = ListIndex;
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
            public PathLineFrame(float AnimationProgress, PathLineFrame FrameBefore, PathLineFrame FrameAfter)
            {
                time = getValueXWayBetweenTwoPoints(FrameBefore.Time, FrameAfter.Time, AnimationProgress);
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
                for (int i = 0; i < pathPoints.Count; i++)
                {
                    pathPoints.Add(new Point(
                        getValueXWayBetweenTwoPoints(FrameBefore.pathPoints[i].X, FrameAfter.pathPoints[i].X, AnimationProgress),
                        getValueXWayBetweenTwoPoints(FrameBefore.pathPoints[i].Y, FrameAfter.pathPoints[i].Y, AnimationProgress)
                        ));
                }
            }
        }
        class LinePoint
        {
            int shapeListIndex;
            Point location;
            bool isMiddle;
            public PathLineFrame GetLineFrame()
            {
                return new PathLineFrame(0, Color.Black, 0, 0, 0, 0, 0);    //Make this return a line with the keypoint as point1 and the non keypoint as point2
            }
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

        //THE GRAPHICS PANEL
        private void PreviewGraphics_Paint(object sender, PaintEventArgs e)
        {
            if(newPoint1 != (new Point(-1, -1)))
            {
                e.Graphics.DrawLine(new Pen(DrawerColorDialog.Color), newPoint1, newPoint2);
            }       //If pendown & within the preview panel show a preview line

            //Gen framePath
            if(framePathTime != time)
            {
                framePathTime = time;
                framePath = new List<PathLineFrame>();
                for(int i = 0; i < dynamicPath.Count(); i++)
                {
                    framePath.Add(dynamicPath[i].GenFrameAt(framePathTime));
                }
            }

            //DrawFramePathTime
            Pen linePen;
            for(int i = 0; i < framePath.Count(); i++)
            {
                linePen = new Pen(framePath[i].PathColor);
                for(int j = 0; j < framePath[i].PathPoints.Count() - 1; j++)
                {
                    e.Graphics.DrawLine(linePen, framePath[i].PathPoints[j], framePath[i].PathPoints[j + 1]);
                }
            }
        }

        private void PreviewGraphics_MouseDown(object sender, MouseEventArgs e)
        {
            //If mouse down, check which tool is selected
            if (OptionsDrawLineMode.Checked)
            {
                newPoint1 = new Point(e.X, e.Y);
            }
        }
        private void PreviewGraphics_MouseMove(object sender, MouseEventArgs e)
        {
            newPoint2 = e.Location;
            this.PreviewGraphics.Invalidate();
        }

        private void PreviewGraphics_MouseUp(object sender, MouseEventArgs e)
        {
            if (OptionsDrawLineMode.Checked)
            {
                dynamicPath.Add(new PathLine((newPoint1.ToString() + "," + newPoint2.ToString()),
                    new PathLineFrame(time, DrawerColorDialog.Color, newPoint1, newPoint2, dynamicPath.Count()),
                    dynamicPath.Count() + 1));
                newPoint1.X = -1;
                newPoint1.Y = -1;
            }
        }

        private void OptionsColorSelecterOpener_Click(object sender, EventArgs e)
        {
            DrawerColorDialog.ShowDialog();
        }
    }
}