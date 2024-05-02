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
    /// Логика взаимодействия для WindowAuthor.xaml
    /// </summary>
    public partial class WindowAuthor : Window
    {
        DatabaseContext db = new DatabaseContext();
        List<Author> authors;
        public WindowAuthor()
        {
            InitializeComponent();
            UpdateAuthors();

            nameUser.Content = App.currentUser.FName;
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                Author currentAuthor = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    var selectedItem = listViewItem.Content as dynamic;
                    string fname = selectedItem.FName + " " + selectedItem.LName;
                    currentAuthor = db.author.FirstOrDefault(a => a.FName + " " + a.LName == fname);

                    WindowAuthorDetails wAuthDetails = new WindowAuthorDetails(currentAuthor);
                    wAuthDetails.Show();
                    Close();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyAuthor.SelectedItem != null)
            {
                Author selectedAuthorListItem = lvMyAuthor.SelectedItem as Author;

                string firstName = selectedAuthorListItem.FName;

                Author selectedAuthor = db.author
                    .Where(a => a.FName + " " + a.LName == firstName)
                    .FirstOrDefault();
                WindowEditAuthor wEditAuth = new WindowEditAuthor(selectedAuthor);
                wEditAuth.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите автора для редактирования");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMyAuthor != null)
            {
                Author selectedAthor = lvMyAuthor.SelectedItem as Author;
                string fullName = selectedAthor.FName;

                Author dAuthor = db.author
                    .Where(a => a.FName + " " + a.LName == fullName)
                    .FirstOrDefault();

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить автора {fullName}?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    db.Entry(dAuthor).Reload();
                    db.author.Remove(dAuthor);
                    db.SaveChanges();
                    UpdateAuthors();
                }
            }
            else
            {
                MessageBox.Show("Выберите автора для удаления");
            }
        }
        public void UpdateAuthors()
        {
                authors = db.author
                .Where(b => b.ID_User == App.currentUser.ID_User)
                .Select(b => new Author { FName = b.FName + " " + b.LName })
                .ToList();
            lvMyAuthor.ItemsSource = authors;
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
