using System;
using System.Collections.Generic;
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
        FlightControlViewModel vm = new FlightControlViewModel(new SimulatorModel(new MyTelnetClient()));
        ControlsPage cp;
        public LoginPage()
        {
            InitializeComponent();
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
                    //vm.connect(IP.Text, Int32.Parse(port.Text));
                    //vm.start();
                    cp = new ControlsPage(vm);
                    this.NavigationService.Navigate(cp);
                }
            }                        
        }
    }
}
