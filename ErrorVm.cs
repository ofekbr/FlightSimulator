using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class ErrorVm: FlightControlViewModel
    {
        public ErrorVm(ISimulatorModel model) : base(model) { }

        public String VM_ErrorMessage
        {
            get { return model.ErrorMessage; }
        }
    }
}
