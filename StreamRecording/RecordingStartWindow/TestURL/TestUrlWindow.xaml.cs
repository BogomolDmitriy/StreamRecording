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

namespace StreamRecording.RecordingStartWindow.TestURL
{
    /// <summary>
    /// Interaction logic for TestUrlWindow.xaml
    /// </summary>
    public partial class TestUrlWindow : Window
    {
        List<BlueAboutTheSite> _BlueAboutTheSite;
        public TestUrlWindow(List<BlueAboutTheSite> BlueAboutTheSite)
        {
            InitializeComponent();
            _BlueAboutTheSite= BlueAboutTheSite;
            TestURLDataGrid.ItemsSource = _BlueAboutTheSite;
        }
    }
}
