using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Views.Windows;
using System.Threading;
using FlightSimulator.Connection;
using FlightSimulator.Models;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private Settings settingsWindow;
        private FlightBoardModel model;

        public FlightBoardViewModel()
        {
            settingsWindow = new Settings();
            model = new FlightBoardModel(new Info());
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Lat") Lat = model.Lat;
                else if (e.PropertyName == "Lon") Lon = model.Lon;
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public double Lon
        {
            get;
            set;
        }

        public double Lat
        {
            get;
            set;
        }

        #region SettingsCommand
        private ICommand settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() =>
                {
                    if (!settingsWindow.IsLoaded)
                    {
                        settingsWindow = new Settings();
                    }
                    settingsWindow.Show();

                }));
            }
        }
        #endregion

        #region
        private ICommand connectCommand;
        public ICommand ConnectsCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() =>
                {
                    new Thread(delegate ()
                    {
                        Commands.Instance.Connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
                    }).Start();
                    model.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
                }));
            }
        }
        #endregion
    }
}
