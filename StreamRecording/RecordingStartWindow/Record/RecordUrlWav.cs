using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace StreamRecording.RecordingStartWindow.Record
{
    public class RecordUrlWav : Record
    {
        public RecordUrlWav(string urlAddress, string filePath) : base(urlAddress, filePath)
        {
        }

        public override void StreamRecordingURL()
        {
            var webRequest = WebRequest.Create(UrlAddress);
            using (var response = webRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var waveOut = new WaveFileWriter(FilePath, new WaveFormat(44100, 16, 2)))
            {
                byte[] buffer = new byte[65536]; // Буфер для чтения аудиопотока
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveOut.Write(buffer, 0, bytesRead);
                    waveOut.Flush();
                }
            }
        }
    }
}
