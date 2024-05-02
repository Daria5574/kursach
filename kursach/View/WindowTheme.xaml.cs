using kursach.Model;
using Microsoft.EntityFrameworkCore;
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

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowTheme.xaml
    /// </summary>
    public partial class WindowTheme : Window
    {
        DatabaseContext db = new DatabaseContext();
        List<Theme> themes;
        public WindowTheme()
        {
            InitializeComponent();
            UpdateThemes();
            nameUser.Content = App.currentUser.FName;

            DatabaseContext db = new DatabaseContext();

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
               // Theme currentTheme = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    string selectedItem = lvMyTheme.SelectedItem.ToString();
                    //Theme selectedThemeListItem = lvMyTheme.SelectedItem as Theme;
                    int startIndex = selectedItem.IndexOf('=') + 2; // Находим индекс после знака равенства и открывающей фигурной скобки
                    int endIndex = selectedItem.Length - 3; // Находим индекс перед закрывающей фигурной скобкой и пробелом перед ней
                    string name = selectedItem.Substring(startIndex, endIndex - startIndex + 1); // Извлекаем подстроку между индексами
                    Theme th = db.theme
                        .Where(t => t.Name == name)
                        .FirstOrDefault();

                    WindowThemeBooks wThBook = new WindowThemeBooks(th);
                    wThBook.Show();
                    Close();
                }
            }
        }
    private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyTheme.SelectedItem != null)
            {
                string selectedItem = lvMyTheme.SelectedItem.ToString();
                //Theme selectedThemeListItem = lvMyTheme.SelectedItem as Theme;
                int startIndex = selectedItem.IndexOf('=') + 2; // Находим индекс после знака равенства и открывающей фигурной скобки
                int endIndex = selectedItem.Length - 3; // Находим индекс перед закрывающей фигурной скобкой и пробелом перед ней
                string name = selectedItem.Substring(startIndex, endIndex - startIndex + 1); // Извлекаем подстроку между индексами
                Theme selectedTheme = db.theme
                    .Where(t => t.Name == name)
                    .FirstOrDefault();
                WindowEditTheme wEditTheme = new WindowEditTheme(selectedTheme);
                wEditTheme.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите тему для редактирования");
            }
        }

        public void UpdateThemes()
        {
            var themes = from theme in db.theme
                         where theme.ID_User == App.currentUser.ID_User
                         select new
                         {
                             Name = theme.Name,
                         };

            lvMyTheme.ItemsSource = themes.ToList();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyTheme != null)
            {
                string selectedItem = lvMyTheme.SelectedItem.ToString();
                //Theme selectedThemeListItem = lvMyTheme.SelectedItem as Theme;
                int startIndex = selectedItem.IndexOf('=') + 2; // Находим индекс после знака равенства и открывающей фигурной скобки
                int endIndex = selectedItem.Length - 3; // Находим индекс перед закрывающей фигурной скобкой и пробелом перед ней
                string name = selectedItem.Substring(startIndex, endIndex - startIndex + 1); // Извлекаем подстроку между индексами
                Theme selectedTheme = db.theme
                    .Where(t => t.Name == name)
                    .FirstOrDefault();

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить тему {name}?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    db.Entry(selectedTheme).Reload();
                    db.theme.Remove(selectedTheme);
                    db.SaveChanges();
                    UpdateThemes();
                }
            }
            else
            {
                MessageBox.Show("Выберите тему для удаления");
            }
        }
    }
}

