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

namespace StreamRecording.RecordingStartWindow
{
    /// <summary>
    /// Interaction logic for RecordingStart.xaml
    /// </summary>
    public partial class RecordingStart : Window
    {
        public List<CaseRecordingStart> ListRecording;
        public RecordingStart(List<CaseRecordingStart> listRecording)
        {
            InitializeComponent();
            ListRecording = listRecording;
        }

        public void AddList ()
        {
            ListRecording.Add(new CaseRecordingStart(this, $"Case {ListRecording.Count}"));
        }
    }
}
