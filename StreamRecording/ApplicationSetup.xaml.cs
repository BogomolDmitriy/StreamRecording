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
using System.Windows.Shapes;

namespace StreamRecording
{
    /// <summary>
    /// Interaction logic for ApplicationSetup.xaml
    /// </summary>
    public partial class ApplicationSetup : Window
    {
        private int tempRecordMin;
        private int tempRecordHour;

        private int tempEmergencySeconds;
        private int tempEmergencyMinutes;

        public int _recordMin { get; set; }
        public int _recordHour { get; set; }

        public int _emergencySeconds { get; set; }
        public int _emergencyMinutes { get; set; }
        public ApplicationSetup(int recordMin, int recordHour, int emergencySeconds, int emergencyMinutes)
        {
            _recordMin = recordMin;
            _recordHour = recordHour;
            _emergencySeconds = emergencySeconds;
            _emergencyMinutes = emergencyMinutes;
            InitializeComponent();
            AssignElements();
        }

        private void IntegerUpDown_hoursLimitChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (hoursLimit.Value != null)
            {
                tempRecordHour = (int)hoursLimit.Value;
            }

            SaveButtonActivityCheck();
        }

        private void IntegerUpDown_minLimitChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (minLimit.Value != null)
            {
                tempRecordMin = (int)minLimit.Value;
            }

            SaveButtonActivityCheck();
        }

        private void EmergencyMinUpDown_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (minEmergencyTimer.Value != null)
            {
                tempEmergencyMinutes = (int)minEmergencyTimer.Value;
            }

            SaveButtonActivityCheck();
        }

        private void EmergencySecondsUpDown_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (secondsEmergencyTimer.Value != null)
            {
                tempEmergencySeconds = (int)secondsEmergencyTimer.Value;
            }

            SaveButtonActivityCheck();
        }

        private void CloseButton_Click (object sender, RoutedEventArgs e)
        {
            AssignElements();
            this.Hide();
        }

        private void SaveButton_Click (object sender, RoutedEventArgs e)
        {
            _recordMin = tempRecordMin;
            _recordHour = tempRecordHour;
            _emergencySeconds = tempEmergencySeconds;
            _emergencyMinutes = tempEmergencyMinutes;
            buttonSave.IsEnabled = false;
            this.Hide();
        }

        private void SaveButtonActivityCheck ()
        {
            if (tempEmergencySeconds == 0 && tempEmergencyMinutes == 0)
            { 
                buttonSave.IsEnabled = false;
            }

            else if (tempRecordMin == 0 && tempRecordHour == 0)
            {
                buttonSave.IsEnabled = false;
            }

            else if (tempEmergencySeconds == _emergencySeconds && tempEmergencyMinutes == _emergencyMinutes && 
                tempRecordMin == _recordMin && tempRecordHour == _recordHour)
            {
                buttonSave.IsEnabled = false;
            }

            else { buttonSave.IsEnabled = true; }
        }

        private void AssignElements ()
        {
            //_recordMin = tempRecordMin;
            //_recordHour = tempRecordHour;
            //_emergencySeconds = tempEmergencySeconds;
            //_emergencyMinutes = tempEmergencyMinutes;

            hoursLimit.Value = _recordHour;
            minLimit.Value = _recordMin;
            minEmergencyTimer.Value = _emergencyMinutes;
            secondsEmergencyTimer.Value = _emergencySeconds;
        }
    }
}
