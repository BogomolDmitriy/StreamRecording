﻿using Microsoft.Win32;
using StreamRecording.RecordingStartWindow;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StreamRecording
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CaseRecordingStart> caseRecordingStarts;
        ApplicationSetup windowSetup;
        public MainWindow()
        {
            caseRecordingStarts = new List<CaseRecordingStart>(); //Список начатых записей
            windowSetup = new ApplicationSetup(5,1,0,1);
            InitializeComponent();
            ComboBoxRecordingStreams.ItemsSource = caseRecordingStarts;
        }

        string selectedFilePath;

        private void New_Storage(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Настраиваем фильтр для типов файлов
            saveFileDialog.Filter = "All files (*.*)|*.*";

            // Показываем диалоговое окно и проверяем результат
            if (saveFileDialog.ShowDialog() == true)
            {
                // Если пользователь выбрал место и имя файла, выводим путь
                selectedFilePath = saveFileDialog.FileName;
                MessageBox.Show($"File will be saved at: {selectedFilePath}");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Завершить приложение
            Application.Current.Shutdown();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            windowSetup.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About clicked");
        }

        private void AddReccording_Click(object sender, RoutedEventArgs e)
        {
            new RecordingStart(caseRecordingStarts);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный элемент
            CaseRecordingStart selectedCase = (CaseRecordingStart)ComboBoxRecordingStreams.SelectedItem;
            if (selectedCase != null)
            {
                // Открываем выбранное окно
                //Window window = (Window)Activator.CreateInstance(selectedCase._RecordingStart);
                selectedCase._RecordingStart.Show();
            }
        }
    }
}