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
            private List<PathLineFrame> keyFrames;
            public List<PathLineFrame> KeyFrames
            { get; set; }
        }
        class PathLineFrame
        {
            int time; //Note the time is in frames not seconds, id recommend 30 frames per second unless you have expensive gear

        }
    }
}