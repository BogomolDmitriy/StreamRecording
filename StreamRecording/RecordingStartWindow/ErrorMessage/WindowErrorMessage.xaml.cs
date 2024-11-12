using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace StreamRecording.RecordingStartWindow.ErrorMessage
{
    /// <summary>
    /// Interaction logic for WindowErrorMessage.xaml
    /// </summary>
    public partial class WindowErrorMessage : Window
    {
        private ApplicationSetup Setup;
        private RecordingStart Recording;

        private DispatcherTimer timer;
        private TimeSpan countdownTime;
        public WindowErrorMessage(ApplicationSetup setup, RecordingStart recording)
        {
            InitializeComponent();
            Setup = setup;
            Recording = recording;
            this.Title = $"{Recording.Name} error!";
            StartCountdown(TimeSpan.FromMinutes(setup._emergencyMinutes).Add(TimeSpan.FromSeconds(setup._emergencySeconds))); // 1 минута 20 секунд
        }

        private void StartCountdown(TimeSpan time)
        {
            countdownTime = time;

            // Инициализация таймера
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Устанавливаем интервал в 1 секунду
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновляем оставшееся время
            countdownTime = countdownTime.Subtract(TimeSpan.FromSeconds(1));

            // Обновляем текст на экране
            CountdownTextBlock.Text = countdownTime.ToString("c");

            // Изменяем цвет текста, если осталось меньше 10 секунд
            if (countdownTime.TotalSeconds <= 10)
            {
                CountdownTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }

            // Если время истекло, останавливаем таймер
            if (countdownTime <= TimeSpan.Zero)
            {
                timer.Stop();
                CountdownTextBlock.Text = "Time's up!";
                Recording.Start();
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Recording.Stop();
            this.Close();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Recording.Start();
            this.Close();
        }
    }
}
