using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamRecording.RecordingStartWindow.Record
{
    public abstract class Record
    {
        protected string UrlAddress;
        protected string FilePath;

        public Record(string urlAddress, string filePath)
        {
            UrlAddress = urlAddress;
            FilePath = filePath;
        }

        public abstract void StreamRecordingURL();
    }
}
