using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlightSimulatorApp.View
{
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e) { }
        private Point firstPoint = new Point();

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Mouse.Capture(this);
            if (e.ChangedButton == MouseButton.Left)
            {
                firstPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ReleaseMouseCapture();
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - firstPoint.X;
                double y = e.GetPosition(this).Y - firstPoint.Y;
                if (Math.Sqrt(x*x + y*y) < Base.Width / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
        }
    }
}
