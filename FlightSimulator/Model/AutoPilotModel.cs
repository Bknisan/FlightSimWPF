using FlightSimulator.Connection;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    class AutoPilotModel
    {
        // send commands to the simulator
        public void SendCommands(string input)
        {
            if (Commands.Instance.Connected)
            {
                new Task(delegate ()
                {
                    Commands.Instance.ChangeValues(input);
                }).Start();
            }

        }

    }
}

