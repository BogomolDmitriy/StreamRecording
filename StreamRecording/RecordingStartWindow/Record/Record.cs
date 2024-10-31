using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamRecording.RecordingStartWindow.Record
{
    public class Record
    {
        protected string UrlAddress;
        protected string FilePath;
        protected bool RecordProcess;

        public Record(string urlAddress, string filePath)
        {
            UrlAddress = urlAddress;
            FilePath = filePath;
            RecordProcess = false;
        }

        public void StopRecord()
        {
            RecordProcess = false;
        }
    }
}
