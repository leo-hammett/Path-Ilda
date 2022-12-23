using System.Collections.Generic;

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
            private string name;
            public string Name
            { get; set; }
            private List<PathLineFrame> keyFrames; //A list of all keyframes sorted by time
            public List<PathLineFrame> KeyFrames
            { get; set; }
            public int getValueXWayBetweenTwoPoints(int value1, int value2, float multiplier)
            {
                return Convert.ToInt32(value1 + (value2 - value1) * multiplier);
            }
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
                        getValueXWayBetweenTwoPoints(frameAfter.PathColor.A, frameBefore.PathColor.A, animationProgress),
                        getValueXWayBetweenTwoPoints(frameAfter.PathColor.R, frameBefore.PathColor.R, animationProgress),
                        getValueXWayBetweenTwoPoints(frameAfter.PathColor.G, frameBefore.PathColor.G, animationProgress),
                        getValueXWayBetweenTwoPoints(frameAfter.PathColor.B, frameBefore.PathColor.B, animationProgress)); //I turned it into a function because I was bound to make a mistake.
                    return newFrame;
                }
            }
        }
        class PathLineFrame
        {
            int time; //Note the time is in frames not seconds, i'd recommend 30 frames per second unless you have expensive gear
            public int Time { get; set; }

            Color pathColor;
            public Color PathColor { get; set; }

            List<Point> pathPoints;
        }
    }
}