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


        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                Book currentBook = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;
                    string bookTitle = selectedItem.BookName;

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
                string selectedItem = lvMyBook.SelectedItem.ToString();
                int equalIndex = selectedItem.IndexOf('=');
                int commaIndex = selectedItem.IndexOf(',');

                if (equalIndex != -1 && commaIndex != -1 && equalIndex < commaIndex)
                {
                    string bookName = selectedItem.Substring(equalIndex + 1, commaIndex - equalIndex - 1).Trim();

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

                int bookStartIndex = selectedItem.IndexOf('=') + 1;
                while (selectedItem[bookStartIndex] == ' ') bookStartIndex++;
                int bookEndIndex = selectedItem.IndexOf(',', bookStartIndex);
                string bookName = selectedItem.Substring(bookStartIndex, bookEndIndex - bookStartIndex).Trim();

                int authorStartIndex = selectedItem.IndexOf('=', bookEndIndex) + 1;
                while (selectedItem[authorStartIndex] == ' ') authorStartIndex++;
                int authorEndIndex = selectedItem.IndexOf('}', authorStartIndex);
                string authorLastName = selectedItem.Substring(authorStartIndex, authorEndIndex - authorStartIndex).Trim();

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

                    db.book_theme.RemoveRange(bookThemesToDelete);
                    db.book.Remove(selectedBook);
                    db.SaveChanges();
                    UpdateBooks();
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления");
            }

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
    }
}

