using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace igorGui
{
    public partial class SplashForm : Form
    {

        Timer SplashTimer;

        public SplashForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string VersionText = version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();

            TitleLabel.ForeColor = Color.Cyan;
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            TitleLabel.Text = "Igor v" + VersionText + Environment.NewLine +
                              "(c) 2020, Ryan L. Boyd, Ph.D.";

        }

        private void SplashForm_Load(object sender, EventArgs e)
        {

        }

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            SplashTimer = new Timer();
            SplashTimer.Interval = 3000;
            SplashTimer.Tick += new EventHandler(TimerEventProcessor);
            SplashTimer.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            this.Close();
        }


    }
}
