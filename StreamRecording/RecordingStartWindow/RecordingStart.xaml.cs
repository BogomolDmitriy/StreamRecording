using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<CaseRecordingStart> ListRecording;
        public RecordingStart(ObservableCollection<CaseRecordingStart> listRecording)
        {
            InitializeComponent();
            ListRecording = listRecording;
            AddList();
        }

        public void AddList ()
        {
            ListRecording.Add(new CaseRecordingStart(this, $"Case {ListRecording.Count}"));
        }
    }
}
