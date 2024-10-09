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
        public int _min { get; set; }
        public int _hour { get; set; }
        public ApplicationSetup(int min, int hour)
        {
            _min = min;
            _hour = hour;
            InitializeComponent();
            integerUpDown.Value = _min;
        }

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) //Temp
        {
            if (integerUpDown.Value != null)
            {
                _min = (int)integerUpDown.Value;
                resultTextBlock.Text = $"Значение: {_min}";
            }
        }
    }
}
