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
    /// Логика взаимодействия для WindowAddAuthor.xaml
    /// </summary>
    public partial class WindowAddAuthor : Window
    {
        DatabaseContext db;
        public WindowAddAuthor()
        {
            InitializeComponent();
            db = new DatabaseContext();

            label1.Content = "Имя автора";
            label2.Content = "Фамилия автора*";
            label3.Content = "Об авторе";
            nameUser.Content = App.currentUser.FName;
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

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string fname = textBox1.Text.Trim();
            string lname = textBox2.Text.Trim();
            string description = textBox3.Text.Trim();

            Author author = new Author(fname, lname, description, App.currentUser.ID_User);
            db.author.Add(author);
            db.SaveChanges();

            MessageBox.Show("Запись добавлена успешно");

            WindowAuthor wAuth = new WindowAuthor();
            wAuth.Show();
            Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor wAuth = new WindowAuthor();
            wAuth.Show();
            Close();
        }
    }
}
