using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;
using System.Windows;

namespace StreamRecording.RecordingStartWindow.Record
{
    public class RecordUrlWav : RecordURL
    {
        public RecordUrlWav(string urlAddress, string filePath, ApplicationSetup setup) : base(urlAddress, filePath, setup)
        {
        }

        //public void RestartRecording()
        //{
        //    throw new NotImplementedException();
        //}

        //public void StopRecording()
        //{
        //    BoolRecordProcess = false;
        //}

        public override async void StartRecording()
        {
            await Task.Run(() => StreamRecordingURL());
        }

        private void StreamRecordingURL()
        {
            //    BoolRepeatRecordingProcess = true;
            //    BoolRecordProcess = true;
            //    while (BoolRepeatRecordingProcess) 
            //    {
            //        if (BoolRecordProcess == false)
            //        {
            //            BoolRecordProcess = true;
            //        }
            //        var webRequest = WebRequest.Create(UrlAddress);
            //        using (var response = webRequest.GetResponse())
            //        using (var stream = response.GetResponseStream())
            //        using (var waveOut = new WaveFileWriter($"{FilePath}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.wav", new WaveFormat(44100, 16, 2)))
            //        {
            //            byte[] buffer = new byte[65536]; // Буфер для чтения аудиопотока
            //            int bytesRead;

            //            while (BoolRecordProcess && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            //            {
            //                waveOut.Write(buffer, 0, bytesRead);
            //                waveOut.Flush();
            //            }
            //        }
            //    }
            //}



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
                    using (var waveOut = new WaveFileWriter($"{FilePath}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.wav", new WaveFormat(44100, 16, 2)))
                    {
                        byte[] buffer = new byte[65536]; // Буфер для чтения аудиопотока
                        int bytesRead;

                        while (BoolRecordProcess && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveOut.Write(buffer, 0, bytesRead);
                            waveOut.Flush();
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
