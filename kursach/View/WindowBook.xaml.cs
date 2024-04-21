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
using kursach.Model;

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowBook.xaml
    /// </summary>
    public partial class WindowBook : Window
    {
        public WindowBook()
        {
            InitializeComponent();
            nameUser.Content = App.currentUser.FName;
            DatabaseContext db = new DatabaseContext();
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

    }
}

