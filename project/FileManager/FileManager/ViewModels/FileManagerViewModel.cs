using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FileManager.ViewModels
{
    public class FileManagerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FileItem> LeftPanelFiles { get; set; } = new ObservableCollection<FileItem>();
        public ObservableCollection<FileItem> RightPanelFiles { get; set; } = new ObservableCollection<FileItem>();

        private string _leftPanelPath = @"C:\";
        public string LeftPanelPath
        {
            get { return _leftPanelPath; }
            set { _leftPanelPath = value; OnPropertyChanged(); LoadFiles(value, LeftPanelFiles); }
        }

        private string _rightPanelPath = @"D:\";
        public string RightPanelPath
        {
            get { return _rightPanelPath; }
            set { _rightPanelPath = value; OnPropertyChanged(); LoadFiles(value, RightPanelFiles); }
        }

        public ICommand OpenFileCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public FileManagerViewModel()
        {
            LoadFiles(LeftPanelPath, LeftPanelFiles);
            LoadFiles(RightPanelPath, RightPanelFiles);

            OpenFileCommand = new RelayCommand(OpenFile);
            CopyCommand = new RelayCommand(CopyFiles);
            MoveCommand = new RelayCommand(MoveFiles);
            DeleteCommand = new RelayCommand(DeleteFiles);
        }

        private void LoadFiles(string path, ObservableCollection<FileItem> panelFiles)
        {
            panelFiles.Clear();
            if (!Directory.Exists(path)) return;

            foreach (var dir in Directory.GetDirectories(path))
                panelFiles.Add(new FileItem { Name = Path.GetFileName(dir), Size = "<DIR>", Modified = Directory.GetLastWriteTime(dir).ToString() });

            foreach (var file in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(file);
                panelFiles.Add(new FileItem
                {
                    Name = fileInfo.Name,
                    Size = (fileInfo.Length / 1024).ToString() + " KB",
                    Modified = fileInfo.LastWriteTime.ToString()
                });
            }
        }

        private void OpenFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Виберіть файл",
                Filter = "Всі файли (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                string directoryPath = Path.GetDirectoryName(selectedFile);
                LeftPanelPath = directoryPath;  // Оновлюємо шлях панелі
            }
        }

        private void CopyFiles(object obj)
        {
            // Реалізація копіювання файлів
        }

        private void MoveFiles(object obj)
        {
            // Реалізація переміщення файлів
        }

        private void DeleteFiles(object obj)
        {
            // Реалізація видалення файлів
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FileItem
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Modified { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (param => true);  // Якщо canExecute не переданий, завжди дозволяємо виконання
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
