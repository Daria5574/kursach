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
        public WindowTheme()
        {
            InitializeComponent();
            nameUser.Content = App.currentUser.FName;

            DatabaseContext db = new DatabaseContext();

            var themes = from theme in db.theme
                         where theme.ID_User == App.currentUser.ID_User
                         select new
                         {
                             Name = theme.Name,
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

    //    Author selectedAuthorListItem = lvMyAuthor.SelectedItem as Author;

    //    // Получаем фамилию и имя выбранного элемента списка
    //    string firstName = selectedAuthorListItem.FName;

    //    // Находим автора в БД по фамилии и имени
    //    Author selectedAuthor = db.author
    //        .Where(a => a.FName + " " + a.LName == firstName)
    //        .FirstOrDefault();
    //    WindowEditAuthor wEditAuth = new WindowEditAuthor(selectedAuthor);
    //    wEditAuth.Show();
    //            Close();
    //}
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
        //public void UpdateAuthors()
        //{
        //    authors = db.author
        //    .Where(b => b.ID_User == App.currentUser.ID_User)
        //    .Select(b => new Author { FName = b.FName + " " + b.LName })
        //    .ToList();
        //    lvMyAuthor.ItemsSource = authors;
        //}
    }
}

