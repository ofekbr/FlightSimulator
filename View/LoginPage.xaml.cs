using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        MapVM mapVM;
        ControlsVM controlsVM;
        DashBoardVM dashBoardVM;
        ErrorVm errorVm;
        ControlsPage cp;
        public LoginPage()
        {
            InitializeComponent();
            controlsVM = (Application.Current as App).ControlsVM;
            dashBoardVM = (Application.Current as App).DashVM;
            mapVM = (Application.Current as App).MapVM;
            errorVm = (Application.Current as App).ErrorVm;
        }
        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(IP.Text))
            {
                IP.BorderBrush = Brushes.Red;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(port.Text))
                {
                    port.BorderBrush = Brushes.Red;
                }
                else
                {
                    connect_button.Content = "Connecting...";
                    try
                    {
                    controlsVM.connect(IP.Text, Int32.Parse(port.Text));
                    controlsVM.start();
                    cp = new ControlsPage(mapVM, controlsVM, dashBoardVM, errorVm);
                    this.NavigationService.Navigate(cp);
                    }
                    catch (Exception)
                    {
                        Error.Visibility = Visibility.Visible;
                    }
                    connect_button.Content = "Connect";
                }
            }                        
        }

        private void _default_Click(object sender, RoutedEventArgs e)
        {
            IP.Text = ConfigurationManager.AppSettings["ip"];
            port.Text = ConfigurationManager.AppSettings["port"];
        }
    }
}
