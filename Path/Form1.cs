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

        int mainTime = 0;       //The time in frames that the system is at
        int fps = 30;        //The number of frames per second (to get time in seconds divide time by fps)
        int kpps = 40000;       //The maximum number of points we should be sending down the dac. Mine was rated at 40KPPS but that could be a false rating at this point.

        Point mouseLastLocation;   //If set to -1,-1 no line preview should be made - Saving where the mouse went down
        Point timelineMouseLastLocation;

        List<LinePoint> closestPoints = new List<LinePoint>();
        List<LinePoint> closestPointsFrozen = new List<LinePoint>();

        Point closestPoint;

        int previewMode = 0;    //0 = no changes are about to happen, 1 = normal line will be created, 2 = snapped line will be created, 3 = line properties changed, 4 = line or point will be selected, 5 = just line will be selected
        int selectedLineDynamicIndex = 0;
        PathLineFrame selectedFrameReadOnly;
        int selectedPointDynamicIndex = -1;

        bool showCircles = false;
        bool showLines = false;
        bool mouseDown = false;

        //Timeline GUI variables
        TimelineSettings currentTimelineSettings;
        LinePoint timelineClosestPoint;
        List<LinePoint> TimelineDots = new List<LinePoint>();
        public Form1()
        {
            InitializeComponent();
            //Initialising Variables
            dynamicPath = new List<PathLine>();
            framePath = new List<PathLineFrame>();
            mainTime = 0;
            DrawerColorDialog.Color = Color.White;
            OptionsDrawLineMode.Checked = true;
            currentTimelineSettings = new TimelineSettings();
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
                for (int i = 0; i < keyFrames.Count; i++)
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
        public class PathLineFrame //SOMETIMES IS A KEYFRAME SOMETIMES ISNT
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
            public List<LinePoint> GenKeyPoints(bool middle = false)
            {
                List<LinePoint> KeyPoints= new List<LinePoint>();
                for(int i = 0; i < PathPoints.Count; i++)
                {
                    KeyPoints.Add(new LinePoint(ListIndex,i, pathPoints[i], false));
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
                pathPoints = new List<Point>(frame.PathPoints);
            }
        }
        public class LinePoint
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
            public PathLineFrame GetLineFrame()
            {
                return new PathLineFrame(0, Color.Black, 0, 0, 0, 0, 0);    //Make this return a line with the keypoint as point1 and the non keypoint as point2
            }
            public LinePoint(int ShapeListIndex, int PathPointsListIndex, Point Location, bool IsMiddle)
            {
                this.shapeListIndex = ShapeListIndex;
                this.location = Location;
                this.isMiddle = IsMiddle;
                this.pathPointsListIndex = PathPointsListIndex;
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
            for(int i = 0; i < dynamicPath.Count; i++)
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
            //Gen framePath (useful for lazer as well.)
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

            //Draw lines, dots and selection bits.
            Pen linePen;
            double closestPointDistance = 1000000;
            
            for (int i = 0; i < framePath.Count(); i++) //Goes through all the lines
            {
                //This bit draws all the points out, and the lines if needed, and also shows the points..
                linePen = new Pen(framePath[i].PathColor);
                for(int j = 0; j < framePath[i].PathPoints.Count() - 1; j++)
                {
                    e.Graphics.DrawLine(linePen, ConvertToHeliosCoords(framePath[i].PathPoints[j], true), ConvertToHeliosCoords(framePath[i].PathPoints[j + 1], true));
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
                        e.Graphics.DrawLine(new Pen(Color.Red, 2), ConvertToHeliosCoords(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[0], true),
                            ConvertToHeliosCoords(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[1], true));
                        e.Graphics.DrawLine(new Pen(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathColor), ConvertToHeliosCoords(dynamicPath[closestPoints[0].ShapeListIndex].GenFrameAt(mainTime).PathPoints[0], true),
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

        }
        private void PreviewGraphics_MouseDown(object sender, MouseEventArgs e)                         //MOUSE MOVEMENT
        {
            //If mouse down, create a new line or frame.
            if (OptionsDrawLineMode.Checked)
            {
                dynamicPath.Add(new PathLine("(" + ConvertToHeliosCoords(e.Location).X + "," + ConvertToHeliosCoords(e.Location).Y + ")", new PathLineFrame(mainTime, DrawerColorDialog.Color, new List<Point>(), dynamicPath.Count), dynamicPath.Count));
                selectedLineDynamicIndex = dynamicPath.Count-1;
                if (OptionsSnapToPoint.Checked)
                {
                    dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(closestPoints[0].Location));
                }
                else
                {
                    dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(e.Location));
                }
                if (OptionsSnapToPoint.Checked)
                {
                    dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(closestPoints[0].Location));
                }
                else
                {
                    dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).AddPoint(ConvertToHeliosCoords(e.Location));
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
            if (dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime).PathPoints[0] == e.Location)
            {
                dynamicPath[selectedLineDynamicIndex].KeyFrames.Remove(dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime));
                if(dynamicPath[selectedLineDynamicIndex].KeyFrames.Count == 0)
                {
                    dynamicPath.RemoveAt(selectedLineDynamicIndex);
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
                    mainTime = Convert.ToInt32(TimeLineFramesInput.Text)*fps;
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
        public void UpdateLineProperties()
        {
            timelineGUI.Invalidate();
            //Select PathLineFrame
            if (dynamicPath.Count != 0)
            {
                //Make sure all indexes are correct
                if (selectedLineDynamicIndex > dynamicPath.Count)
                {
                    selectedLineDynamicIndex = 0;
                    selectedPointDynamicIndex = 0;
                }
                selectedFrameReadOnly = dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime); //Self updating
                if (selectedPointDynamicIndex > selectedFrameReadOnly.PathPoints.Count)
                {
                    selectedPointDynamicIndex = 0;
                }
                //selectedLineDynamicIndex = needs to be selected via the GUI or maybe a button at some point.
                selectedFrameReadOnly = dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime); //Self updating
                //selectedPointDynamicIndex = must be changed via a specific function
                //Add to the points textbox
                PathLinePointsListBox.Items.Clear();
                for (int i = 0; i < selectedFrameReadOnly.PathPoints.Count; i++)
                {
                    PathLinePointsListBox.Items.Add(selectedFrameReadOnly.PathPoints[i].ToString());
                }
                //PathLinePointsListBox.SelectedItem = selectedFrameReadOnly.PathPoints[selectedPointDynamicIndex].ToString();
                //Line properties title
                LinePropertiesTitle.Text = "Line Properties: " + dynamicPath[selectedLineDynamicIndex].Name;
                LinePropertiesPathIndexData.Text = selectedLineDynamicIndex.ToString();

                //Update the colors and coordinates
                if (selectedPointDynamicIndex != -1) 
                {
                    LinePropertiesXCoordinate.Text = dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime).PathPoints[selectedPointDynamicIndex].X.ToString();
                    LinePropertiesYCoordinate.Text = dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime).PathPoints[selectedPointDynamicIndex].Y.ToString();
                }

                //Add the keyframes to the keyframe textbox
                LinePropertiesKeyFramesTextBox.Items.Clear();
                for (int i = 0; i < dynamicPath[selectedLineDynamicIndex].KeyFrames.Count; i++)
                {
                    LinePropertiesKeyFramesTextBox.Items.Add(dynamicPath[selectedLineDynamicIndex].KeyFrames[i].Time);
                }
                //LinePropertiesKeyFramesTextBox.SelectedItem = mainTime;
                LinePropertiesChangeColor.BackColor = dynamicPath[selectedLineDynamicIndex].GenFrameAt(mainTime).PathColor;
            }
        }
        public PathLineFrame GetSelectedFrameWrite()
        {
            return dynamicPath[selectedLineDynamicIndex].GetFrameAt(mainTime);
        }
        public void ChangeTime(int newTime)
        {
            mainTime = newTime;
            UpdateLineProperties();
            timelineGUI.Invalidate();
            PreviewGraphics.Invalidate();
        }
        private void LinePropertiesKeyFramesTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LinePropertiesKeyFramesTextBox.SelectedIndex != -1)
            {
                mainTime = dynamicPath[selectedLineDynamicIndex].KeyFrames[LinePropertiesKeyFramesTextBox.SelectedIndex].Time;
                UpdateLineProperties();
            }
        }

        private void LinePropertiesXCoordinate_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    GetSelectedFrameWrite().PathPoints[selectedPointDynamicIndex] = new Point(Convert.ToInt32(LinePropertiesXCoordinate.Text), Convert.ToInt32(LinePropertiesYCoordinate.Text));
            //    this.PreviewGraphics.Invalidate();
            //}
            //catch
            //{
            //
            //}
            //UpdateLineProperties();
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
            public Font SecondsFont = new Font("Arial", 4, FontStyle.Bold);
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
                return this.LeftMargin + frameTime * (this.PixelsPerSecond / fps);
            }
        }

        private void timeline_GUI_updater(object sender, PaintEventArgs e)
        {
            int seconds = 0;
            Pen thinWhitePen = new Pen(Color.White);
            float currentTimeX = currentTimelineSettings.LeftMargin + mainTime * (currentTimelineSettings.PixelsPerSecond / fps);
            e.Graphics.DrawLine(thinWhitePen, new PointF(currentTimeX, currentTimelineSettings.PixelsPerShape), new PointF(currentTimeX, timelineGUI.Size.Height));
            for (float horizontalPixelsUsed = currentTimelineSettings.LeftMargin; horizontalPixelsUsed < timelineGUI.Size.Width; horizontalPixelsUsed += currentTimelineSettings.PixelsPerSecond)
            {
                e.Graphics.DrawString(seconds.ToString(), currentTimelineSettings.SecondsFont, new SolidBrush(Color.White), new PointF(horizontalPixelsUsed - ((int)(currentTimelineSettings.SecondsFont.Size / 2)),currentTimelineSettings.LeftMargin));
                seconds++;
            }
            int dynamicPathTempIndex = 0;
            TimelineDots.Clear();
            for(float verticalSpaceUsed = currentTimelineSettings.TopMargin; verticalSpaceUsed < timelineGUI.Size.Height;verticalSpaceUsed += currentTimelineSettings.PixelsPerShape)
            {
                if (dynamicPath.Count != 0 && dynamicPath.Count > dynamicPathTempIndex)
                {
                    foreach (PathLineFrame frame in dynamicPath[dynamicPathTempIndex].KeyFrames)
                    {
                        TimelineDots.Add(new LinePoint(dynamicPathTempIndex, frame.Time, new Point((int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape)), false));

                        e.Graphics.FillCircle(new SolidBrush(Color.LightGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time,fps), (int)(
                            currentTimelineSettings.TopMargin + (dynamicPathTempIndex + 1) * currentTimelineSettings.PixelsPerShape), 4);

                        e.Graphics.FillCircle(new SolidBrush(Color.DarkGray), (int)currentTimelineSettings.getFloatXOfFrameTime(frame.Time, fps), (int)(
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
                int newTimeFrame = (int)((e.Location.X - currentTimelineSettings.LeftMargin) / (currentTimelineSettings.PixelsPerSecond / fps));
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