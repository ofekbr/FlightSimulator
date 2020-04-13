using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for ControlsPage.xaml
    /// </summary>
    public partial class ControlsPage : Page
    {
        MapVM mapVM;
        ControlsVM controlsVM;
        DashBoardVM dashBoardVM;
        ErrorVm errorVm;
        //Error errorComponent;
        public ControlsPage(MapVM map, ControlsVM controls, DashBoardVM dashBoard, ErrorVm error)
        {
            errorVm = error;
            mapVM = map;
            controlsVM = controls;
            dashBoardVM = dashBoard;
            InitializeComponent();
            Map.DataContext = mapVM;
            DashBoard.DataContext = dashBoardVM;
            Controls.DataContext = controlsVM;
            Error.DataContext = errorVm;
           // errorComponent = Error as Error;
           // errorComponent.init();
        }
    }
}
