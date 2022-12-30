using System.Collections.Generic;
using System.Linq;
//dynamicPath = the file & whole animation
//framePath = the PathLineFrame array for the individual frame

//REMEMBER THAT ALL SHAPES SHOULD BE STORED IN A 4095 CO-OORDINATE RESOLUTION NOT THE RESOLUTION OF THE SCREEN.

namespace Path
{
    public partial class Form1 : Form
    {
        List<PathLine> dynamicPath;         //The highest level
        List<PathLineFrame> framePath;      //What individual frames should look like. Should hopefully be generateable from a for loop and get line at dynamicPath[i]
        int framePathTime = -1;                  //Gives the time the framePath is generated for

        int mainTime;       //The time in frames that the system is at
        int fps;        //The number of frames per second (to get time in seconds divide time by fps)
        int kpps;       //The maximum number of points we should be sending down the dac. Mine was rated at 40KPPS but that could be a false rating at this point.

        Point newPoint1;   //If set to -1,-1 no line preview should be made - Saving where the mouse went down
        Point newPoint2;   //Should ideally be wherever the mouse is (is -1,-1 when mouse is outside the box)

        List<LinePoint> closestPoints = new List<LinePoint>();
        Point closestPoint;

        int previewMode = 0;    //0 = no changes are about to happen, 1 = normal line will be created, 2 = snapped line will be created, 3 = line properties changed, 4 = line or point will be selected, 5 = just line will be selected
        int selectedLineDynamicIndex;
        public Form1()
        {
            InitializeComponent();
            //Initialising Variables
            dynamicPath = new List<PathLine>();
            framePath = new List<PathLineFrame>();
            newPoint1 = new Point(-1, -1);
            mainTime = 0;
            DrawerColorDialog.Color = Color.White;
            OptionsDrawLineMode.Checked = true;
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
            public PathLineFrame GenFrameAt(int frameTime)
            {
                int frameBeforeIndex = -1;
                int frameAfterIndex = -1;
                PathLineFrame newFrame;
                for (int i = 0; i < keyFrames.Count; i++)
                {
                    if (keyFrames[i].Time == frameTime)          //Checks to see if the time lands on a keyframe - This is merely a performance thing
                    {
                        newFrame = keyFrames[i];
                        newFrame.Time = frameTime;
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
                    newFrame = keyFrames[frameAfterIndex];
                    newFrame.Time = frameTime;
                    return newFrame;
                }
                else if (frameAfterIndex == -1)                   //Literally the same situation as the above if
                {
                    newFrame = keyFrames[frameBeforeIndex];
                    newFrame.Time = frameTime;
                    return newFrame;
                }
                else
                {
                    PathLineFrame frameAfter = keyFrames[frameAfterIndex];
                    PathLineFrame frameBefore = keyFrames[frameBeforeIndex];
                    
                    //SET ALL PROPERTIES TO PROPERTIES IN BETWEEN THEM BOTH
                    float animationProgress = (frameTime - frameBefore.Time) / (frameAfter.Time - frameBefore.Time); //If you set each property to beforeFrame value plus (afterFrame - beforeFrame) * difference  -- This assumes a linear animation hence the constant progress as time moves forward.
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
            public void SortKeyFramesByTime()
            {
                //Here I impliment quicksort partially explained here: https://www.youtube.com/watch?v=SLauY6PpjW4&t=15s
                //I used a recursive routine
                QuicksortByTime(keyFrames);
            }
            public List<PathLineFrame> QuicksortByTime(List<PathLineFrame> list)
            {
                MessageBox.Show(list.Count.ToString());
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
                MessageBox.Show(list.Count.ToString());
                return list;
            }
        }
        class PathLineFrame //SOMETIMES IS A KEYFRAME SOMETIMES ISNT
        {
            private int time; //Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public int Time
            {
                get { return time; }
                set { time = value; }
            }

            private Color pathColor;
            public Color PathColor
            {
                get { return pathColor; }
                set { pathColor = value; }
            }

            private List<Point> pathPoints;
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
                for (int i = 0; i < pathPoints.Count; i++)
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
        }
        class LinePoint
        {
            int shapeListIndex;
            public int ShapeListIndex
            {
                get { return shapeListIndex; }
                set { shapeListIndex = value; }
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
        public Point ConvertToHeliosCoords(Point Original, bool backwards = false)
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
        public double getDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y,2));
        }
        public void updateKeyPointInfo()
        {
            for(int i = 0; i < dynamicPath.Count, i++)
            {
                for(int j = 0; j < dynamicPath[i].KeyFrames.Count; j++)
                {
                    MessageBox.Show(dynamicPath[i].KeyFrames[j].Time.ToString());
                }
            }
        }
        //THE GRAPHICS PANEL
        private void PreviewGraphics_Paint(object sender, PaintEventArgs e)
        {
            InformationPreviewModeData.Text = Convert.ToString(previewMode);
            //Gen framePath
            if(framePathTime != mainTime)
            {
                framePathTime = mainTime;
                framePath = new List<PathLineFrame>();
                for(int i = 0; i < dynamicPath.Count(); i++)
                {
                    framePath.Add(dynamicPath[i].GenFrameAt(framePathTime));
                }
            }
            InformationFrameListCountInfo.Text = framePath.Count().ToString();

            //DrawFramePathTime
            Pen linePen;
            double closestPointDistance = 1000000;
            for (int i = 0; i < framePath.Count(); i++)
            {
                linePen = new Pen(framePath[i].PathColor);
                for(int j = 0; j < framePath[i].PathPoints.Count() - 1; j++)
                {
                    if(previewMode is 3 && newPoint1 == framePath[i].PathPoints[j])
                    {
                        e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(newPoint2, true), ConvertToHeliosCoords(framePath[i].PathPoints[j + 1], true));
                    }
                    else if(previewMode is 3 && newPoint1 == framePath[i].PathPoints[j + 1])
                    {
                        e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(framePath[i].PathPoints[j], true), ConvertToHeliosCoords(newPoint2, true));
                    }
                    else
                    {
                        e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(framePath[i].PathPoints[j], true), ConvertToHeliosCoords(framePath[i].PathPoints[j + 1], true));
                    }
                }
                
                List<LinePoint> keyPoints = new List<LinePoint>();
                keyPoints = framePath[i].GenKeyPoints(OptionsSelectModeButton.Checked);
                Pen bigCircle = new Pen(Color.Gray, 3);
                Pen smallCircle = new Pen(Color.DimGray, 2);
                for (int j = 0; j < keyPoints.Count(); j++)
                {
                    Point pointLocation = keyPoints[j].Location;
                    Point pointLocationGraphics = ConvertToHeliosCoords(pointLocation, true);
                    if (previewMode is 3 or 4 or 5)     //Also finds closest point to mouse
                    {
                        e.Graphics.FillCircle(new SolidBrush(Color.LightGray), pointLocationGraphics.X, pointLocationGraphics.Y, 4);
                        e.Graphics.FillCircle(new SolidBrush(Color.DarkGray), pointLocationGraphics.X, pointLocationGraphics.Y, 3);
                    }
                    if(getDistance(pointLocation,newPoint2) < closestPointDistance) //Finds closest point
                    {
                        closestPoint = keyPoints[j].Location;
                        closestPoints.Clear();
                        closestPoints.Add(keyPoints[j]);
                        InformationClosestPointData.Text = closestPoint.ToString();
                        closestPointDistance = getDistance(pointLocation, newPoint2);
                    }
                    else if(getDistance(pointLocation, newPoint2) == closestPointDistance)  //Adds all lines to the closest point, pretty nifty if you ask me :)
                    {
                        closestPoints.Add(keyPoints[j]);
                    }
                }
            }
            if(previewMode is 2 or 3 or 4 or 5)
            {
                try
                {
                    if (closestPoints[0].IsMiddle && closestPoints.Count == 1 && previewMode is 3)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red, 2), ConvertToHeliosCoords(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[0], true),
                            ConvertToHeliosCoords(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[1], true));
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
            if (previewMode is 2 && getDistance(closestPoint, newPoint2) < 100)
            {
                e.Graphics.DrawLine(new Pen(DrawerColorDialog.Color), ConvertToHeliosCoords(newPoint1, true), ConvertToHeliosCoords(closestPoint, true));
            }
            else if(previewMode is 1)
            {
                e.Graphics.DrawLine(new Pen(DrawerColorDialog.Color), ConvertToHeliosCoords(newPoint1, true), ConvertToHeliosCoords(newPoint2, true));
            }      //If pendown & within the preview panel show a preview line
        }
        private void PreviewGraphics_MouseDown(object sender, MouseEventArgs e)
        {
            //If mouse down, check which tool is selected
            if (OptionsDrawLineMode.Checked)
            {
                if (OptionsSnapToPoint.Checked && getDistance(closestPoint, newPoint2) < 100) //both conditions so the snap tool is useful
                {
                    newPoint1 = closestPoint;
                    previewMode = 2;
                }
                else
                {
                    previewMode = 1;
                    newPoint1 = ConvertToHeliosCoords(e.Location); //Closest point already a variable & is therefore converted.
                }
            }
            else if (closestPoints[0].IsMiddle && closestPoints.Count == 1)
            {
                selectedLineDynamicIndex = closestPoints[0].ShapeListIndex;
                LinePropertiesTitle.Text = "Line Properties: " + dynamicPath[closestPoints[0].ShapeListIndex].Name;
                LinePropertiesPathIndexData.Text = selectedLineDynamicIndex.ToString();
                LinePropertiesKeyFramesTextBox.Items.Clear();
                for(int i = 0; i < dynamicPath[closestPoints[0].ShapeListIndex].KeyFrames.Count; i++)
                {
                    LinePropertiesKeyFramesTextBox.Items.Add(dynamicPath[closestPoints[0].ShapeListIndex].KeyFrames[i].Time);
                }
                LinePropertiesTimeData.Text = mainTime.ToString();
                LinePropertiesChangeColor.BackColor = dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathColor;
                previewMode = 0;
            }
            else if (OptionsSelectModeButton.Checked)
            {
                previewMode = 3;
                newPoint1 = closestPoint;
            }
        }
        private void PreviewGraphics_MouseMove(object sender, MouseEventArgs e)
        {
            newPoint2 = ConvertToHeliosCoords(e.Location);
            this.PreviewGraphics.Invalidate();
            //updateInformation
            InformationPoint1Info.Text = newPoint1.ToString();
            InformationPoint2Info.Text = newPoint2.ToString();
            if(previewMode == 0)
            {
                if (OptionsSelectModeButton.Checked)
                {
                    previewMode = 4;
                }
                else if (OptionsSnapToPoint.Checked)
                {
                    previewMode = 5;
                }
            }
        }

        private void PreviewGraphics_MouseUp(object sender, MouseEventArgs e)
        {
            if (OptionsDrawLineMode.Checked)
            {
                if (previewMode is 2 && getDistance(closestPoint,newPoint2) < 100)
                {
                    dynamicPath.Add(new PathLine(("(" + newPoint1.X.ToString() + "," + newPoint1.Y.ToString() + "),(" + closestPoint.X.ToString() + "," + closestPoint.Y.ToString() + ")"),
                    new PathLineFrame(mainTime, DrawerColorDialog.Color, newPoint1, closestPoint, dynamicPath.Count()),
                    dynamicPath.Count() + 1));
                }
                else
                {
                    dynamicPath.Add(new PathLine(("(" + newPoint1.X.ToString() + "," + newPoint1.Y.ToString() + "),(" + newPoint2.X.ToString() + "," + newPoint2.Y.ToString() + ")"),
                    new PathLineFrame(mainTime, DrawerColorDialog.Color, newPoint1, newPoint2, dynamicPath.Count()),
                    dynamicPath.Count() + 1));
                }
                newPoint1.X = -1;
                newPoint1.Y = -1;
                previewMode = 0;
                InformationDynamicListCountInfo.Text = dynamicPath.Count().ToString();
                framePathTime = -1;     //The equilivant of framePath.invalidate() <- Would be more efficient to just add this onto the framepath but am testing the whole thing right now
            }
            else if (OptionsSelectModeButton.Checked)
            {
                if(previewMode == 3)
                {
                    for(int i = 0; i < closestPoints.Count(); i++)
                    {
                        int replaceIndex = -1;
                        for (int j = 0; j < dynamicPath[closestPoints[i].ShapeListIndex].KeyFrames.Count(); j++)
                        {
                            if (dynamicPath[closestPoints[i].ShapeListIndex].KeyFrames[j].Time == mainTime)
                            {
                                replaceIndex = j;
                            }
                        }
                        PathLineFrame currentFrame = dynamicPath[closestPoints[i].ShapeListIndex].GenFrameAt(mainTime);
                        for(int j = 0; j < currentFrame.PathPoints.Count(); j++)
                        {
                            if (currentFrame.PathPoints[j] == newPoint1)
                            {
                                currentFrame.PathPoints[j] = newPoint2;
                            }
                        }
                        if(replaceIndex != -1)
                        {
                            dynamicPath[closestPoints[i].ShapeListIndex].KeyFrames[replaceIndex] = currentFrame;
                        }
                        else
                        {
                            dynamicPath[closestPoints[i].ShapeListIndex].KeyFrames.Add(currentFrame);
                            //dynamicPath[closestPoints[i].ShapeListIndex].SortKeyFramesByTime();
                        }
                    }
                    previewMode = 0;
                }
            }
        }
        private void OptionsColorSelecterOpener_Click(object sender, EventArgs e)
        {
            DrawerColorDialog.ShowDialog();
        }

        private void PreviewGraphics_Resize(object sender, EventArgs e)
        {
            if (PreviewGraphics.Width == PreviewGraphics.Height) return;

            if (PreviewGraphics.Width > PreviewGraphics.Height)
            {
                PreviewGraphics.Height = PreviewGraphics.Width;
            }
            else
            {
                PreviewGraphics.Width = PreviewGraphics.Height;
            }
        }

        private void TimeLineFramesInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(TimeLineFramesInput.Text != "")
                {
                    mainTime = Convert.ToInt32(TimeLineFramesInput.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: cannot convert text to number: " + TimeLineFramesInput.Text);
            }
        }
        private void LinePropertiesTimeData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TimeLineFramesInput.Text != "")
                {
                    mainTime = Convert.ToInt32(TimeLineFramesInput.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: cannot convert text to number: " + TimeLineFramesInput.Text);
            }
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

    }
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