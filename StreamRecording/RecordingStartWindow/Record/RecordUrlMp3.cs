using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StreamRecording.RecordingStartWindow.Record
{
    public class RecordUrlMp3 : Record
    {
        public RecordUrlMp3(string urlAddress, string filePath) : base(urlAddress, filePath)
        {
        }

        public override void StreamRecordingURL()
        {
            var webRequest = WebRequest.Create(UrlAddress);
            using (var response = webRequest.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[65536]; // Буфер для чтения аудиопотока
                int bytesRead;

                // Чтение потока и сохранение в файл
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
