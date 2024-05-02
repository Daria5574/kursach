using kursach.Model;
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

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowThemeBooks.xaml
    /// </summary>
    public partial class WindowThemeBooks : Window
    {
        DatabaseContext db = new DatabaseContext();
        private Theme them;
        public WindowThemeBooks(Theme th)
        {
            InitializeComponent();
           
            them = th;
            nameUser.Content = App.currentUser.FName;
            nameTheme.Content = them.Name;

            var listViewData = from book in db.book
                               join bookTheme in db.book_theme on book.Id equals bookTheme.ID_Book
                               join theme in db.theme on bookTheme.ID_Theme equals theme.Id
                               join author in db.author on book.ID_Author equals author.Id
                               where book.ID_User == App.currentUser.ID_User && theme.Id == them.Id
                               select new
                               {
                                   BookName = book.Name,
                                   AuthorFullName = author.FName + " " + author.LName
                               };

            lvThemeBooks.ItemsSource = listViewData.ToList();

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
        private void theme_Click(object sender, RoutedEventArgs e)
        {
            WindowTheme wTheme = new WindowTheme();
            wTheme.Show();
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
