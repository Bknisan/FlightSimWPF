using FlightSimulator.Model;
using FlightSimulator.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AutoPilotModel model = new AutoPilotModel();

        private Brush background = Brushes.White; 

        private string commands; 
        public string Commands
        {
            get { return commands; }

            set
            {
                commands = value;
                // command isnt sent yet.
                if (!string.IsNullOrEmpty(Commands) && Background == Brushes.White) Background = Brushes.PaleVioletRed;
                else if (string.IsNullOrEmpty(Commands)) Background = Brushes.White; 
            }
        }
        public Brush Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                NotifyPropertyChanged("Background");
            }

        }

        #region okCommand
        private ICommand okCommand; // Ok command for clear button
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() =>
                {
                    string toBeSent = Commands;
                    Commands = ""; // remove text
                    NotifyPropertyChanged(Commands); // Notify view!
                    Background = Brushes.White; // make background white again
                    model.SendCommands(toBeSent);

                }));
            }
        }

        #endregion


        #region clearCommand
        private ICommand clearCommand; // Clear command for clear button
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() =>
                {
                    Commands = "";
                    Background = Brushes.White;
                    NotifyPropertyChanged(Commands); // Notify view!
                }));
            }
        }

        #endregion

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
