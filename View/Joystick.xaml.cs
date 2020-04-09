using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace FlightSimulatorApp.View
{
    public partial class Joystick : UserControl
    {
        const int size = 75;
        private Storyboard sb;
        private Point firstPoint = new Point();
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(Joystick), new PropertyMetadata(0.0));


        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(Joystick), new PropertyMetadata(0.0));


        public Joystick()
        {
            InitializeComponent();
            sb = Knob.FindResource("CenterKnob") as Storyboard;
        }
                
        private void centerKnob_Completed(object sender, EventArgs e)
        {
            sb.Stop();
            knobPosition.X = 0;
            X = 0;
            knobPosition.Y = 0;
            Y = 0;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement senderKnob = sender as UIElement;
            senderKnob.CaptureMouse();

            if (e.ChangedButton == MouseButton.Left)
            {
                firstPoint = e.GetPosition(this);
                X = normalize(firstPoint.X);
                Y = -normalize(firstPoint.Y);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x1 = e.GetPosition(this).X - firstPoint.X;
                double y1 = e.GetPosition(this).Y - firstPoint.Y;

                if (x1 > size)
                {
                    knobPosition.X = size;
                }
                else if (x1 < -size)
                {
                    knobPosition.X = -size;
                }
                else
                {                    
                    knobPosition.X = x1;
                }

                if (y1 > size)
                {
                    knobPosition.Y = size;
                }
                else if (y1 < -size)
                {
                    knobPosition.Y = -size;
                }
                else
                {
                    knobPosition.Y = y1;
                }

                X = normalize(knobPosition.X);
                Y = - normalize(knobPosition.Y);            
            }
        }
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UIElement senderKnob = sender as UIElement;
            senderKnob.ReleaseMouseCapture();
            sb.Begin();
        }

        private double normalize(double x)
        {
            return (2 * (x - (-size)) / (size - (-size)) - 1);
        }
    }
}
