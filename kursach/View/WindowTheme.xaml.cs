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
    /// Логика взаимодействия для WindowTheme.xaml
    /// </summary>
    public partial class WindowTheme : Window
    {
        public WindowTheme()
        {
            InitializeComponent();
            nameUser.Content = App.currentUser.FName;

            DatabaseContext db = new DatabaseContext();

            var themes = from theme in db.theme
                         where theme.ID_User == App.currentUser.ID_User
                         select new
                         {
                             ThemeName = theme.Name,
                         };

            lvMyTheme.ItemsSource = themes.ToList();

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
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowAddTheme wAdd = new WindowAddTheme();
            wAdd.Show();
            Close();
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                Theme currentTheme = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;

                    string themeName = selectedItem.ThemeName; // Получаем название книги из анонимного типа

                    // Проверяем, есть ли в базе данных книга с таким же названием
                    currentTheme = db.theme.FirstOrDefault(b => b.Name == themeName);

                    WindowThemeBooks wThBook = new WindowThemeBooks(currentTheme);
                    wThBook.Show();
                    Close();
                }
            }
        }
    }
}

