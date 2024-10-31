using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamRecording.RecordingStartWindow.Record
{
    public interface IRecord
    {
        //void StreamRecordingURL();
        void StartRecording();
        void StopRecording();
    }
}
