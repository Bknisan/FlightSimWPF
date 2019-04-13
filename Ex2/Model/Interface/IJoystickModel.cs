using Ex2.Model.EventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Models.Interface
{
    interface IJoystickModel
    {
        double Throttle { get; set; }
        double Elevator { get; set; }
        double Aileron { get; set; }
        double Rudder { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
