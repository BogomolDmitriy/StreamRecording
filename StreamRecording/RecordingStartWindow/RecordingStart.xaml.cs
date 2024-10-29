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

        public ObservableCollection<CaseRecordingStart> ListRecording;
        public RecordingStart(ObservableCollection<CaseRecordingStart> listRecording)
        {
            InitializeComponent();
            ListRecording = listRecording;
            AddList();
            EnumComboBoxRecordingTools.ItemsSource = Enum.GetValues(typeof(RecordingTools));
            //EnumComboBoxRecordingTools.SelectedIndex = 0;
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

            string[] parts = TempSelectedFilePath.Split("\\");
            Name = parts[parts.Length-1];
            this.Title = Name;

            FilePathTextBox.Text = TempSelectedFilePath;
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

                throw;
            }
        }

        private void EnumComboBoxRecordingTools_SelectedIndexChanged (object sender, SelectionChangedEventArgs e)
        {
            RecordingTools recordingTools = (RecordingTools)EnumComboBoxRecordingTools.SelectedItem;
            switch(recordingTools)
            {
                case RecordingTools.RecordUrlMp3:
                    record = new RecordUrlMp3((string)UrlTextBox.Content, FilePath);
                    break;
                case RecordingTools.RecordUrlWav:
                    record = new RecordUrlWav((string)UrlTextBox.Content, FilePath);
                    break;
            }    
        }

        private void ButtonStartRecording_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
