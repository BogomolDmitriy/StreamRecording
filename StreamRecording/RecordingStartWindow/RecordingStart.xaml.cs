using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.Win32;

namespace StreamRecording.RecordingStartWindow
{
    /// <summary>
    /// Interaction logic for RecordingStart.xaml
    /// </summary>
    public partial class RecordingStart : Window
    {
        public string Name;
        private string FilePath;
        private string UrlAddress;

        string TempSelectedFilePath;

        public ObservableCollection<CaseRecordingStart> ListRecording;
        public RecordingStart(ObservableCollection<CaseRecordingStart> listRecording)
        {
            InitializeComponent();
            ListRecording = listRecording;
            AddList();
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

            FilePathTextBox.Text = TempSelectedFilePath;
            

        }
    }
}
