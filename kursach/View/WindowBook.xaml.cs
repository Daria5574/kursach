using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using kursach.Model;

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowBook.xaml
    /// </summary>
    public partial class WindowBook : Window
    {
        DatabaseContext db = new DatabaseContext();
        public WindowBook()
        {
            InitializeComponent();
            UpdateBooks();
            nameUser.Content = App.currentUser.FName;

        }
        public void UpdateBooks()
        {
            var listViewData = from book in db.book
                               join author in db.author on book.ID_Author equals author.Id
                               where book.ID_User == App.currentUser.ID_User
                               select new
                               {
                                   BookName = book.Name,
                                   AuthorLastName = author.FName + " " + author.LName,
                               };

            lvMyBook.ItemsSource = listViewData.ToList();
        }
        private void theme_Click(object sender, RoutedEventArgs e)
        {
            WindowTheme wTheme = new WindowTheme();
            wTheme.Show();
            Close();
        }
        private void NavigateToMainPage(object sender, MouseButtonEventArgs e)
        {
            WindowBook wMainPage = new WindowBook();
            wMainPage.Show();
            Close();
        }

        private void favorite_Click(object sender, RoutedEventArgs e)
        {
            WindowFavorite wFav = new WindowFavorite();
            wFav.Show();
            Close();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBook wAdd = new WindowAddBook();
            wAdd.Show();
            Close();
        }

        private void author_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor wAuth = new WindowAuthor();
            wAuth.Show();
            Close();
        }

        private void NavigateToUser(object sender, MouseButtonEventArgs e)
        {
            WindowUser wUser = new WindowUser();
            wUser.Show();
            Close();
        }
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                Book currentBook = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;

                    string bookTitle = selectedItem.BookName; // Получаем название книги из анонимного типа

                    // Проверяем, есть ли в базе данных книга с таким же названием
                    currentBook = db.book.FirstOrDefault(b => b.Name == bookTitle);

                    WindowBookDetails wBookDetails = new WindowBookDetails(currentBook);
                    wBookDetails.Show();
                    Close();
                }
            }
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyBook.SelectedItem != null)
            {
                //Book selectedBookListItem = lvMyBook.SelectedItem as Book;

                //// Получаем фамилию и имя выбранного элемента списка
                //string name = selectedBookListItem.Name;

                string selectedItem = lvMyBook.SelectedItem.ToString();

                int equalIndex = selectedItem.IndexOf('=');
                int commaIndex = selectedItem.IndexOf(',');

                if (equalIndex != -1 && commaIndex != -1 && equalIndex < commaIndex)
                {
                    string bookName = selectedItem.Substring(equalIndex + 1, commaIndex - equalIndex - 1).Trim();




                    ////Theme selectedThemeListItem = lvMyTheme.SelectedItem as Theme;
                    //string[] strArray = selectedItem.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                    //string bookName = "";
                    //string authorName = "";
                    //if (strArray.Length > 0)
                    //{
                    //    string[] data = strArray[0].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    //    foreach (string pair in data)
                    //    {
                    //        string[] keyValue = pair.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    //        if (keyValue.Length >= 2)
                    //        {
                    //            switch (keyValue[1].Trim())
                    //            {
                    //                case "BookName":
                    //                    bookName = keyValue[keyValue.Length - 1].Trim('"', '"');
                    //                    Console.WriteLine("Название книги: " + bookName);
                    //                    break;
                    //                case "AuthorLastName":
                    //                    authorName = keyValue[keyValue.Length - 1].Trim('"', '"');
                    //                    Console.WriteLine("Фамилия автора: " + authorName);
                    //                    break;
                    //            }
                    //        }
                    //    }
                    //}

                    // Находим автора в БД по фамилии и имени
                    Book selectedBook = db.book
                            .Where(b => b.Name == bookName)
                            .FirstOrDefault();
                    WindowEditBook wEditBook = new WindowEditBook(selectedBook);
                    wEditBook.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для редактирования");
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyBook.SelectedItem != null)
            {
                string selectedItem = lvMyBook.SelectedItem.ToString();
                // Извлекаем название книги (учитываем возможность отсутствия кавычек)
                int bookStartIndex = selectedItem.IndexOf('=') + 1;
                while (selectedItem[bookStartIndex] == ' ') bookStartIndex++; // Пропускаем пробелы
                int bookEndIndex = selectedItem.IndexOf(',', bookStartIndex);
                string bookName = selectedItem.Substring(bookStartIndex, bookEndIndex - bookStartIndex).Trim();

                // Извлекаем имя фамилию (учитываем возможность отсутствия кавычек)
                int authorStartIndex = selectedItem.IndexOf('=', bookEndIndex) + 1;
                while (selectedItem[authorStartIndex] == ' ') authorStartIndex++; // Пропускаем пробелы
                int authorEndIndex = selectedItem.IndexOf('}', authorStartIndex);
                string authorLastName = selectedItem.Substring(authorStartIndex, authorEndIndex - authorStartIndex).Trim();
                    
                // Соединяем название книги и имя фамилию
                string name = bookName + " " + authorLastName;

                Book selectedBook = db.book
    .Join(db.author, book => book.ID_Author, author => author.Id, (book, author) => new { book, author })
    .Where(x => x.book.Name + " " + x.author.FName + " " + x.author.LName == name)
    .Select(x => x.book)
    .FirstOrDefault();

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить книгу {name}?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var bookThemesToDelete = db.book_theme.Where(bt => bt.ID_Book == selectedBook.Id).ToList();

                    // Удаляем связанные записи
                    db.book_theme.RemoveRange(bookThemesToDelete);

                    //db.Entry(selectedBook).Reload();
                    db.book.Remove(selectedBook);
                    db.SaveChanges();
                    UpdateBooks();
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления");
            }

            //Book selectedBook = db.book
            //    .Join author on on book.ID_Author equals author.Id
            //    .Where(book => book.Name + " " + author.FName + " " + author.LName == name );
            //var listViewData = from book in db.book
            //                   join author in db.author on book.ID_Author equals author.Id
            //                   where book.ID_User == App.currentUser.ID_User
            //                   select new
            //                   {
            //                       BookName = book.Name,
            //                       AuthorLastName = author.FName + " " + author.LName,
            //                   };

            //lvMyBook.ItemsSource = listViewData.ToList();
        }
        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lvMyAuthor != null)
        //    {
        //        Author selectedAthor = lvMyAuthor.SelectedItem as Author;
        //        string fullName = selectedAthor.FName;

        //        Author dAuthor = db.author
        //            .Where(a => a.FName + " " + a.LName == fullName)
        //            .FirstOrDefault();

        //        MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить автора {fullName}?", "Подтверждение удаления", MessageBoxButton.YesNo);

        //        if (result == MessageBoxResult.Yes)
        //        {
        //            db.Entry(dAuthor).Reload();
        //            db.author.Remove(dAuthor);
        //            db.SaveChanges();
        //            UpdateAuthors();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите автора для удаления");
        //    }
        //}
    }
}

