using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kursach.Helper;
using kursach.Model;
using kursach.View;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace kursach.ViewModel
{
    public class AuthorViewModel : INotifyPropertyChanged
    {
        readonly string path = @"C:\Users\Acer\source\repos\kursach\kursach\DataModels\AuthorData.json";

        public ObservableCollection<Author> ListAuthor { get; set; } = new ObservableCollection<Author>();
        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get
            {
                return _selectedAuthor;
            }
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
                EditAuthor.CanExecute(true);
            }
        }
        public string Error { get; set; }

        string _jsonAuthors = String.Empty;
        public AuthorViewModel()
        {
            ListAuthor = LoadAuthor();
        }

        #region command AddAuthor
        /// команда добавления автора
        private RelayCommand _addAuthor;
        public RelayCommand AddAuthor
        {
            get
            {
                return _addAuthor ??
                (_addAuthor = new RelayCommand(obj =>
                {
                    WindowAddAuthor wnAuthor = new WindowAddAuthor
                    {
                        Title = "Новый автор",
                    };
                    // формирование кода новой книги
                    int maxIdAuthor = MaxId() + 1;
                    Author author = new Author
                    {
                        Id = maxIdAuthor,
                    };
                    wnAuthor.DataContext = author;
                    if (wnAuthor.ShowDialog() == true)
                    {
                        ListAuthor.Add(author);
                        SaveChanges(ListAuthor);
                    }
                    SelectedAuthor = author;
                },
                (obj) => true));
            }
        }

        #endregion
        #region Command EditBook 
        /// команда редактирования книги
        private RelayCommand _editAuthor;
        public RelayCommand EditAuthor
        {
            get
            {
                return _editAuthor ??
                (_editAuthor = new RelayCommand(obj =>
                {
                    WindowAddAuthor wnAuthor = new WindowAddAuthor
                    {
                        Title = "Редактирование автора",
                    };
                    Author author = SelectedAuthor;
                    var tempAuthor = author.ShallowCopy();
                    wnAuthor.DataContext = tempAuthor;
                    if (wnAuthor.ShowDialog() == true)
                    {
                        // сохранение данных в оперативной памяти
                        author.FName = tempAuthor.FName;
                        author.LName = tempAuthor.LName;
                        author.Description = tempAuthor.Description;
                        SaveChanges(ListAuthor);
                    }
                }, (obj) => SelectedAuthor != null && ListAuthor.Count >
               0));
            }
        }

        #endregion
        #region DeleteAuthor
        /// команда удаления книги
        private RelayCommand _deleteAuthor;
        public RelayCommand DeleteAuthor
        {
            get
            {
                return _deleteAuthor ??
                (_deleteAuthor = new RelayCommand(obj =>
                {
                    Author author = SelectedAuthor;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по автору: " + author.FName + author.LName, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListAuthor.Remove(author);
                        SaveChanges(ListAuthor);
                    }
                }, (obj) => SelectedAuthor != null && ListAuthor.Count > 0));
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// загрузка json файла и дессериализация данных для коллекции должностей ListAuthor
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Author> LoadAuthor()
        {
            _jsonAuthors = System.IO.File.ReadAllText(path);
            if (_jsonAuthors != null)
            {
                ListAuthor = JsonConvert.DeserializeObject<ObservableCollection<Author>>(_jsonAuthors);
                return ListAuthor;
            }
            else
            {
                return null;
            }
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListAuthor)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }
        /// <summary>
        /// Сохранение json-строки с данными по должностям в файл 
        /// </summary>
        /// <param name="listAuthor"></param>
        private void SaveChanges(ObservableCollection<Author> listAuthor)
        {
            var jsonAuthor = JsonConvert.SerializeObject(listAuthor);
            try
            {
                using (StreamWriter writer = System.IO.File.CreateText(path))
                {
                    writer.Write(jsonAuthor);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
