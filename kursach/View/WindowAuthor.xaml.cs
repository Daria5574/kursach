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
    /// Логика взаимодействия для WindowAuthor.xaml
    /// </summary>
    public partial class WindowAuthor : Window
    {
        public WindowAuthor()
        {
            InitializeComponent();
            nameUser.Content = App.currentUser.FName;
            DatabaseContext db = new DatabaseContext();
            List<Author> authors = db.author
                .Where(b => b.ID_User == App.currentUser.ID_User)
                .Select(b => new Author { FName = b.FName + " "+ b.LName })
                .ToList();
            lvMyBook.ItemsSource = authors;
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
            WindowAddAuthor wAdd = new WindowAddAuthor();
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
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                Author currentAuthor = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;

                    string fname = selectedItem.FName + " " + selectedItem.LName ; // Получаем название книги из анонимного типа

                    // Проверяем, есть ли в базе данных книга с таким же названием
                   currentAuthor = db.author.FirstOrDefault(a => a.FName + " " + a.LName == fname);

                  WindowAuthorDetails wAuthDetails = new WindowAuthorDetails(currentAuthor);
                    wAuthDetails.Show();
                    Close();
                }
            }
        }

    }
}
