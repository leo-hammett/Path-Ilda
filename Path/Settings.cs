using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Path
{
    public partial class Settings : Form
    {
        public class LaserSettings
        {
            public int kpps = 4000;       //The maximum number of points we should be sending down the dac. Mine was rated at 40KPPS but that could be a false rating at this point.
            public int maxVelocity = 15;
            public int maxAcceleration = 2;
            public int bufferLength = 10;
            public int dwellPoints = 10;
            public bool project = false;
            public LaserSettings()
            {

            }
        }
        public class TimelineSettings
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
        public LaserSettings currentLaserSettings;
        public TimelineSettings currentTimelineSettings;
        public Settings()
        {
            InitializeComponent();
            try
            {
                currentLaserSettings = JsonSerialization.ReadFromJsonFile<LaserSettings>(@"//CurrentLaserSettings.Config");
                currentTimelineSettings = JsonSerialization.ReadFromJsonFile<TimelineSettings>(@"//CurrentTimelineSettings.Config");
            }
            catch
            {

            }
        }
        public void updateTimeline()
        {
            try
            {
                JsonSerialization.WriteToJsonFile<LaserSettings>(@"//CurrentLaserSettings.Config", currentLaserSettings);
                JsonSerialization.WriteToJsonFile<TimelineSettings>(@"//CurrentTimelineSettings.Config", currentTimelineSettings);
            }
            catch
            { }
        }
        public void updateProperties()
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
