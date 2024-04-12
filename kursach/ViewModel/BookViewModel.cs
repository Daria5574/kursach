using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kursach.Helper;
using kursach.Model;
using kursach.View;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using JetBrains.Annotations;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
namespace kursach.ViewModel
{
    public class BookViewModel : INotifyPropertyChanged
    {
        readonly string path = @"C:\Users\Acer\source\repos\kursach\kursach\DataModels\BookData.json";

        private BookDPO _selectedBookDpo;

        public BookDPO SelectedBookDpo
        {
            get { return _selectedBookDpo; }
            set
            {
                _selectedBookDpo = value;
                OnPropertyChanged("SelectedBookDpo");
            }
        }

        public ObservableCollection<Book> ListBook { get; set; }
        AuthorViewModel vmAuthor = new AuthorViewModel();
        ObservableCollection<Author> ListAuthor = new ObservableCollection<Author>();
        public ObservableCollection<BookDPO> ListBookDPO
        {
            get;
            set;
        }
        string _jsonBooks = String.Empty;
        public string Error { get; set; }
        public string Message { get; set; }
        public BookViewModel()
        {
            ListBook = new ObservableCollection<Book>();
            ListBookDPO = new ObservableCollection<BookDPO>();
            ListBook = LoadBook();
            ListBookDPO = GetListBookDPO();
            foreach (Author a in vmAuthor.ListAuthor)
            {
                ListAuthor.Add(a);
            }

        }

        #region AddBook

        private RelayCommand _addBook;
        public RelayCommand AddBook
        {
            get
            {
                return _addBook ??
                (_addBook = new RelayCommand(obj =>
                {
                    WindowAddBook wnBook = new WindowAddBook
                    {
                        Title = "Новая книга"
                    };
                    // формирование кода новой книги
                    int maxIdBook = MaxId() + 1;
                    BookDPO book = new BookDPO
                    {
                        Id = maxIdBook
                    };

                    wnBook.DataContext = book;
                    wnBook.CbAuthor.ItemsSource = ListAuthor;
                    wnBook.CbAuthor.DisplayMemberPath = "LName";

                    if (wnBook.ShowDialog() == true)
                    {
                        Author a = (Author)wnBook.CbAuthor.SelectedValue;

                        if (a != null)
                        {
                            book.ID_Author = a.LName;
                            ListBookDPO.Add(book);
                            // добавление нового сотрудника в коллекцию ListChapters<Chapter>
                            Book b = new Book();
                            b = b.CopyFromBookDPO(book);
                            ListBook.Add(b);
                            try
                            {
                                // сохранение изменений в файле json
                                SaveChanges(ListBook);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка добавления данных в json файл\n" + e.Message;
                            }
                        }
                    }

                },
                (obj) => true));
            }
        }

        public RelayCommand AdBook
        {
            get
            {
                return _addBook ??
                (_addBook = new RelayCommand(obj =>
                {
                    WindowAddBook wnBook = new WindowAddBook
                    {
                        Title = "Новая книга"
                    };

                    int maxIdBook = MaxId() + 1;

                    BookDPO book = new BookDPO
                    {
                        Id = maxIdBook
                    };

                    wnBook.DataContext = book;
                    wnBook.CbAuthor.ItemsSource = ListAuthor;
                    wnBook.CbAuthor.DisplayMemberPath = "FName";

                    if (wnBook.ShowDialog() == true)
                    {
                        var a = (Author)wnBook.CbAuthor.SelectedValue;

                        if (a != null)
                        {
                            book.ID_Author = a.LName;

                            ListBookDPO.Add(book);
                            Book b = new Book();

                            b = b.CopyFromBookDPO(book);
                            b.ID_Author = a.Id;
                            ListBookDPO.Add(book);

                            try
                            {
                                SaveChanges(ListBook);
                            }

                            catch (Exception e)
                            {
                                Error = "Ошибка добавления данных в json файл\n" + e.Message;
                            }
                        }
                    }

                },
                (obj) => true));
            }
        }
        #endregion
        #region EditBook
        /// команда редактирования данных по сотруднику
        private RelayCommand _editBook;
        public RelayCommand EditBook
        {
            get
            {
                return _editBook ??
                (_editBook = new RelayCommand(obj =>
                {
                    WindowAddBook wnBook = new WindowAddBook()
                    {
                        Title = "Редактирование данных книги",
                    };
                    BookDPO bookDPO = SelectedBookDpo;
                    var tempBook = bookDPO.ShallowCopy();
                    wnBook.DataContext = tempBook;
                    wnBook.CbAuthor.ItemsSource = ListAuthor;

                    var creator = ListAuthor.FirstOrDefault(p => p.LName == bookDPO.ID_Author);
                    if (creator != null)
                    {
                        int creatorIndex = ListBook.IndexOf(creator);
                        wnBook.CbAuthor.SelectedIndex = creatorIndex;
                    }

                    if (wnBook.ShowDialog() == true)
                    {
                        // сохранение данных в оперативной памяти
                        // перенос данных из временного класса в класс отображения данных 
                        Author a = (Author)wnBook.CbAuthor.SelectedValue;
                        if (a != null)
                        {
                            bookDPO.Name = tempBook.Name;
                            bookDPO.ID_Author = a.LName;
                            bookDPO.The_Path_To_The_File = tempBook.The_Path_To_The_File;
                            bookDPO.Cover = tempBook.Cover;
                            bookDPO.Number_Of_Printed_Pages = tempBook.Number_Of_Printed_Pages;
                            bookDPO.Date_Of_Writing = tempBook.Date_Of_Writing;
                            bookDPO.The_Year_Of_Publishing = tempBook.The_Year_Of_Publishing;
                            bookDPO.ISBN = tempBook.ISBN;
                            bookDPO.Time_To_Read = tempBook.Time_To_Read;
                            bookDPO.About_The_Book = tempBook.About_The_Book;
                            bookDPO.Age_Rating = tempBook.Age_Rating;
                            bookDPO.Is_Favorite = tempBook.Is_Favorite;
                            bookDPO.ID_User = tempBook.ID_User;

                            // перенос данных из класса отображения данных в класс chapterDPO

                            var bb = ListBook.FirstOrDefault(p => p.Id == bookDPO.Id);
                            if (bb != null)
                            {
                                bb = bb.CopyFromBookDPO(bookDPO);
                            }
                            try
                            {
                                // сохраненее данных в файле json
                                SaveChanges(ListBook);
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка редактирования данных в json файл\n" + e.Message;
                            }
                        }
                        else
                        {
                            Message = "Необходимо выбрать книг.";
                        }
                    }
                }, (obj) => SelectedBookDpo != null && ListBookDPO.Count > 0));
            }
        }

        #endregion
        #region DeleteBook
        /// команда удаления данных по сотруднику
        private RelayCommand _deleteBook;
        public RelayCommand DeleteBook
        {
            get
            {
                return _deleteBook ??
                (_deleteBook = new RelayCommand(obj =>
                {
                    BookDPO book = SelectedBookDpo;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по книге: \n" + book.Name, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            // удаление данных в списке отображения данных
                            ListBookDPO.Remove(book);
                            // поиск удаляемого класса в коллекции ListRoles
                            var bb = ListBook.FirstOrDefault(b => b.Id == book.Id);
                            if (bb != null)
                            {
                                ListBook.Remove(bb);
                                // сохраненее данных в файле json
                                SaveChanges(ListBook);
                            }
                        }
                        catch (Exception e)
                        {
                            Error = "Ошибка удаления данных\n" + e.Message;
                        }
                    }

                }, (obj) => SelectedBookDpo != null && ListBookDPO.Count > 0));
            }
        }
        #endregion
        #region Method

        public ObservableCollection<Book> LoadBook()
        {
            _jsonBooks = System.IO.File.ReadAllText(path);
            if (_jsonBooks != null)
            {
                ListBook = JsonConvert.DeserializeObject<ObservableCollection<Book>>(_jsonBooks);
                return ListBook;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<BookDPO> GetListBookDPO()
        {
            foreach (var book in ListBook)
            {
                BookDPO b = new BookDPO();
                b = b.CopyFromBook(book);
                ListBookDPO.Add(b);
            }
            return ListBookDPO;
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListBook)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }

        private void SaveChanges(ObservableCollection<Book> listBooks)
        {
            var jsonBook = JsonConvert.SerializeObject(listBooks);
            try
            {
                using (StreamWriter writer = System.IO.File.CreateText(path)) // Явное указание на пространство имен System.IO
                {
                    writer.Write(jsonBook);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }


        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]
string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
