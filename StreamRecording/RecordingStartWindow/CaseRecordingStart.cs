using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamRecording.RecordingStartWindow
{
    public class CaseRecordingStart
    {
        public string _Name { get; set; }
        public RecordingStart _RecordingStart { get; set; }

        public CaseRecordingStart(RecordingStart RecordingStart, string Name)
        {
            _Name = Name;
            _RecordingStart = RecordingStart;
        }
    }
}
