using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StreamRecording.RecordingStartWindow.Record
{
    public class RecordUrlMp3 : RecordURL
    {
        public RecordUrlMp3(string urlAddress, string filePath, ApplicationSetup setup) : base(urlAddress, filePath, setup)
        {
        }

        public override async void StartRecording()
        {
            TaskRun = Task.Run(() => StreamRecordingURL());
        }

        private void StreamRecordingURL()
        {
            BoolRepeatRecordingProcess = true;
            BoolRecordProcess = true;
            try
            {
                while (BoolRepeatRecordingProcess)
                {
                    if (BoolRecordProcess == false)
                    {
                        BoolRecordProcess = true;
                    }

                    Timer.Start();

                    var webRequest = WebRequest.Create(UrlAddress);
                    using (var response = webRequest.GetResponse())
                    using (var stream = response.GetResponseStream())
                    using (var fileStream = new FileStream($"{FilePath}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.mp3", FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[65536]; // Буфер для чтения аудиопотока
                        int bytesRead;

                        // Чтение потока и сохранение в файл
                        while (BoolRecordProcess && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);
                            fileStream.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи {ex.Message}");
                Task.Delay(1000);// Задержка
                StartRecording();
            }
        }
    }
}
