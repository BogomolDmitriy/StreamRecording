using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StreamRecording.RecordingStartWindow.Record
{
    public abstract class RecordURL
    {
        protected string UrlAddress;
        protected string FilePath;
        protected bool BoolRecordProcess;
        protected bool BoolRepeatRecordingProcess;
        //protected DateTime Date = new DateTime();
        protected Task TaskRun;
        protected ApplicationSetup Setup;

        protected DispatcherTimer Timer;

        public RecordURL(string urlAddress, string filePath, ApplicationSetup setup)
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(setup._recordHour, setup._recordMin, 0); // 15 минут и 42 секунды
            Timer.Tick += Timer_Tick; // Привязка обработчика события

            UrlAddress = urlAddress;
            FilePath = filePath;
            Setup = setup;
            BoolRecordProcess = false;
            BoolRepeatRecordingProcess = false;
        }

        public void StopRecord()
        {
            BoolRepeatRecordingProcess = false;
            BoolRecordProcess = false;
            Timer.Stop();
        }

        public void PauseRecord()
        {

        }

        public void RestartRecord()
        {
            BoolRecordProcess = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RestartRecord();
        }

        public abstract void StartRecording();
    }
}
