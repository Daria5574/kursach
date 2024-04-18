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
    /// Логика взаимодействия для WindowFavorite.xaml
    /// </summary>
    public partial class WindowFavorite : Window
    {
        public WindowFavorite()
        {
            InitializeComponent();
            nameUser.Content = App.currentUser.FName;

            DatabaseContext db = new DatabaseContext();
            var listViewData = from book in db.book
                               join author in db.author on book.ID_Author equals author.Id
                               where book.ID_User == App.currentUser.ID_User && book.Is_Favorite == 1
                               select new
                               {
                                   BookName = book.Name,
                                   AuthorLastName = author.FName + " " + author.LName,
                               };

            lvMyBook.ItemsSource = listViewData.ToList();

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
    }
}
