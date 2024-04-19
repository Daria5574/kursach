using kursach.Model;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для WindowBookDetails.xaml
    /// </summary>
    public partial class WindowBookDetails : Window
    {
        public WindowBookDetails(Book currentBook)
        {
            InitializeComponent();
            DataContext = currentBook;

            using (var db = new DatabaseContext())
            {
                var book = db.book.Include(b => b.Author).FirstOrDefault(b => b.Id == currentBook.Id);
                if (book != null)
                {
                    authorLabel.Content = book.Author.LName;
                }
            }
            nameUser.Content = currentBook.Name;
            //name.Content = currentBook.Name;

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
    }
}
