using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using Microsoft.Win32;
using StreamRecording.RecordingStartWindow.Record;
using StreamRecording.RecordingStartWindow.TestURL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StreamRecording.RecordingStartWindow
{
    /// <summary>
    /// Interaction logic for RecordingStart.xaml
    /// </summary>
    public partial class RecordingStart : Window
    {
        TestUrlWindow TestUrlWindow;

        public string Name;
        private string FilePath;
        //private string UrlAddress;
        private IRecord record;

        string TempSelectedFilePath;

        bool BoolStartRecording_Button; // Тригер смены функции кнопки StartRecording_Button
        bool BoolComboBoxRecordingTools = false; //тригер активации BoxRecording

        public ObservableCollection<CaseRecordingStart> ListRecording;
        public RecordingStart(ObservableCollection<CaseRecordingStart> listRecording)
        {
            InitializeComponent();
            ListRecording = listRecording;
            AddList();
            EnumComboBoxRecordingTools.ItemsSource = Enum.GetValues(typeof(RecordingTools));
            //EnumComboBoxRecordingTools.SelectedIndex = 0;

            URLPathTextBox.IsEnabled = false;
            UrlTestButton.IsEnabled = false;

            EnumComboBoxRecordingTools.IsEnabled = false;

            StartRecording_Button.IsEnabled = false;
            BoolStartRecording_Button = true;

            SaveRecording_Button.IsEnabled = false;
        }

        public void AddList () //добавляем RecordingStart в лист открития, на главной странице
        {
            ListRecording.Add(new CaseRecordingStart(this, $"Case {ListRecording.Count}"));
        }

        private void AddFilePath_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Настраиваем фильтр для типов файлов
            saveFileDialog.Filter = "All files (*.*)|*.*";

            // Показываем диалоговое окно и проверяем результат
            if (saveFileDialog.ShowDialog() == true)
            {
                // Если пользователь выбрал место и имя файла, выводим путь
                TempSelectedFilePath = saveFileDialog.FileName;
                MessageBox.Show($"File will be saved at: {TempSelectedFilePath}");
            }

            if (TempSelectedFilePath != null) // проверка наличия адреса
            {
                string[] parts = TempSelectedFilePath.Split("\\");
                Name = parts[parts.Length - 1];
                this.Title = Name;
                FilePath = TempSelectedFilePath;
                FilePathTextBox.Text = TempSelectedFilePath;

                URLPathTextBox.IsEnabled = true;
            }
        }

        private void UrlPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonActivator();
        }

        private void ButtonActivator ()
        {
            if (URLPathTextBox.Text.Length > 1)
            {
                EnumComboBoxRecordingTools.IsEnabled = true;
                if (BoolComboBoxRecordingTools)
                {
                    StartRecording_Button.IsEnabled = true;
                }

                UrlTestButton.IsEnabled = true;
            }

            else
            {
                EnumComboBoxRecordingTools.IsEnabled = false;
                StartRecording_Button.IsEnabled = false;
                UrlTestButton.IsEnabled = false;
            }
        }

        private void TestURL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BlueAboutTheSite> UrlList = new List<BlueAboutTheSite>();
                string radioUrl = (string)URLPathTextBox.Text; //URL интернет-радио
                                                       // Создаем WebRequest
                var webRequest = WebRequest.Create(radioUrl);
                using (var response = webRequest.GetResponse())
                {
                    // Получаем заголовки ответа
                    UrlList.Add(new BlueAboutTheSite("Content - Type", response.Headers["Content-Type"]));

                    // Выводим все заголовки для анализа
                    foreach (var header in response.Headers.AllKeys)
                    {
                        UrlList.Add(new BlueAboutTheSite(header, response.Headers[header]));
                    }
                }

                TestUrlWindow = new TestUrlWindow(UrlList);
                TestUrlWindow.Show();
            }
            catch (Exception)
            {

                MessageBox.Show("Source not found", "Error");
            }
        }

        private void EnumComboBoxRecordingTools_SelectedIndexChanged (object sender, SelectionChangedEventArgs e)
        {
            string url = URLPathTextBox.Text;
            RecordingTools recordingTools = (RecordingTools)EnumComboBoxRecordingTools.SelectedItem;
            switch(recordingTools)
            {
                case RecordingTools.RecordUrlMp3:
                    //record = new RecordUrlMp3(url, FilePath);
                    record = new RecordUrlMp3(url, $"{FilePath}.mp3");
                    break;
                case RecordingTools.RecordUrlWav:
                    record = new RecordUrlWav(url, FilePath);
                    break;
            }

            BoolComboBoxRecordingTools = true;
            ButtonActivator();
        }

        private void ButtonStartRecording_Click(object sender, RoutedEventArgs e)
        {
            if (BoolStartRecording_Button)
            {
                record.StartRecording();
                BoolStartRecording_Button = false;
                StartRecording_Button.Content = "Stop";

                // Отключение кнопок
                FilePathTextBox.IsEnabled = false;
                FilePathTextButton.IsEnabled = false;

                URLPathTextBox.IsEnabled = false;
                UrlTestButton.IsEnabled = false;

                EnumComboBoxRecordingTools.IsEnabled = false;
            }

            else
            {
                record.StopRecording();
                BoolStartRecording_Button = true;
                StartRecording_Button.Content = "Start";

                // Включение кнопок
                FilePathTextBox.IsEnabled = true;
                FilePathTextButton.IsEnabled = true;

                URLPathTextBox.IsEnabled = true;
                UrlTestButton.IsEnabled = true;

                EnumComboBoxRecordingTools.IsEnabled = true;
            }

        }
    }
}
