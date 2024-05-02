using kursach.Model;
using kursach.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kursach.View
{

    /// <summary>
    /// Логика взаимодействия для WindowEditBook.xaml
    /// </summary>
    public partial class WindowEditBook : Window
    {
        private List<int?> selectedThemeIds = new List<int?>();

        DatabaseContext db = new DatabaseContext();
        private string selectedImagePath;
        private Book b;
        private bool isCoverChanged;


        public WindowEditBook(Book selectedBook)
        {
            InitializeComponent();
            b = selectedBook;
            var viewModel = new WindowAddBookViewModel();

            imageControl.SetBinding(Image.SourceProperty, new Binding("Cover") { Converter = new CoverImageConverter() });
            imageControl.DataContext = b;

            var authors = db.author
                .Where(a => a.ID_User == App.currentUser.ID_User)
                .Select(a => new { FullName = a.Id + ". " + a.FName + " " + a.LName, Author = a })
                .ToList();

            CbAuthor.ItemsSource = authors;
            CbAuthor.DisplayMemberPath = "FullName";
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CbAuthor.ItemsSource);
            view.Refresh();

            // Загрузка автора книги
            var selectedBookAuthor = db.author
                .Where(a => a.Id == selectedBook.ID_Author)
                .Select(a => new { FullName = a.Id + ". " + a.FName + " " + a.LName, Author = a })
                .FirstOrDefault();

            if (selectedBookAuthor != null)
            {
                CbAuthor.SelectedItem = selectedBookAuthor;
            }

            // Загрузить связи между книгой и темами в словарь
            Dictionary<int?, bool> bookThemeLinks = db.book_theme
                .Where(bt => bt.ID_Book == b.Id)
                .ToDictionary(bt => bt.ID_Theme, bt => true);


            // Получить список тем из базы данных
            // ... (код получения b и bookThemeLinks)

            selectedThemeIds = bookThemeLinks.Keys.ToList();


            List<Theme> themes = db.theme.ToList();

            // Получить список идентификаторов связанных тем
            var linkedThemeIds = bookThemeLinks.Keys.ToList();

            // Отфильтровать список тем
            var filteredThemes = themes.Where(t => linkedThemeIds.Contains(t.Id)).ToList();

            // Создать список Model.ListBoxItem с отфильтрованными темами
            List<Model.ListBoxItem> listBoxItems = new List<Model.ListBoxItem>();
            foreach (var theme in filteredThemes)
            {
                listBoxItems.Add(new Model.ListBoxItem
                {
                    ThemeId = theme.Id,
                    ThemeName = theme.Name,
                    IsSelected = true // Темы уже связаны
                });
            }

            // Создать список имен добавленных тем
            var addedThemeNames = listBoxItems.Select(item => item.ThemeName).ToList();

            // Добавить не связанные темы
            foreach (var theme in themes)
            {
                if (!addedThemeNames.Contains(theme.Name))
                {
                    listBoxItems.Add(new Model.ListBoxItem
                    {
                        ThemeId = theme.Id,
                        ThemeName = theme.Name,
                        IsSelected = selectedThemeIds.Contains(theme.Id) // Темы не связаны
                    });
                    addedThemeNames.Add(theme.Name); // Добавить имя темы в список добавленных
                }
            }

            LbThemes.ItemsSource = listBoxItems;


            //var linkedThemeIds = new List<int?>();
            //if (b.Book_Theme != null)
            //{
            //    linkedThemeIds = b.Book_Theme.Select(bt => bt.ID_Theme).ToList();
            //}



            label1.Content = "Название*";
            label2.Content = "Путь к файлу*";
            label3.Content = "Автор";
            label4.Content = "Количество печатных страниц";
            label5.Content = "Год написания";
            label6.Content = "Год издания";
            label7.Content = "ISBN";
            label8.Content = "Время на чтение";
            label9.Content = "Возрастной рейтинг";
            label10.Content = "О книге";
            nameUser.Content = App.currentUser.FName;

            textBox1.Text = b.Name;
            textBox2.Text = b.The_Path_To_The_File;

            Author author = db.author.Find(b.ID_Author);

            // Находим соответствующий элемент в выпадающем списке
            //ComboBoxItem comboBoxItem = (ComboBoxItem)CbAuthor.Items.Cast<ComboBoxItem>().FirstOrDefault(item =>
            //{
            //    string authorName = item.Content.ToString();
            //    string id = authorName.Split('.')[0];
            //    return id == author.Id.ToString();
            //});

            // Устанавливаем выбранный элемент в выпадающем списке
            //if (comboBoxItem != null)
            //{
            //    CbAuthor.SelectedItem = comboBoxItem;
            //}
            //string selectedAuthorName = CbAuthor.SelectedItem.ToString();

            //int dotIndex = selectedAuthorName.IndexOf('.');
            //string id_AuthorString = selectedAuthorName.Substring(0, dotIndex).Trim();
            //id_AuthorString = Regex.Match(id_AuthorString, @"\d+").Value;
            //b.ID_Author = int.Parse(id_AuthorString);

            //if (!string.IsNullOrEmpty(b.Number_Of_Printed_Pages) && int.TryParse(b.Number_Of_Printed_Pages, out int temp))
            //{
            //    number_Of_Printed_Pages = temp;
            //}
            textBox4.Text = b.Number_Of_Printed_Pages.ToString();
            textBox5.Text = b.Date_Of_Writing.ToString();
            textBox6.Text = b.The_Year_Of_Publishing.ToString();
            textBox7.Text = b.ISBN;
            textBox8.Text = b.Time_To_Read;
            textBox9.Text = b.About_The_Book;
            textBox10.Text = b.Age_Rating;


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
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImagePath));
                imageControl.Source = bitmap;

                // Установить флаг изменения обложки
                isCoverChanged = true;
            }
        }


        public class CoverImageConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string cover = (string)value;
                return string.IsNullOrEmpty(cover) ? "D:\\3_курс\\рпм\\курсовой\\add_image.jpg" : cover;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }


        private void save_Click(object sender, RoutedEventArgs e)
        {
            b.Name = textBox1.Text.Trim();
            b.The_Path_To_The_File = textBox2.Text.Trim();
            string selectedAuthorName = CbAuthor.SelectedItem.ToString();

            int dotIndex = selectedAuthorName.IndexOf('.');
            string id_AuthorString = selectedAuthorName.Substring(0, dotIndex).Trim();
            id_AuthorString = Regex.Match(id_AuthorString, @"\d+").Value;
            b.ID_Author = int.Parse(id_AuthorString);

            b.ISBN = textBox7.Text.Trim();
            b.Time_To_Read = textBox8.Text.Trim();
            b.About_The_Book = textBox9.Text.Trim();
            b.Age_Rating = textBox10.Text.Trim();
            if (isCoverChanged)
            {
                b.Cover = selectedImagePath;
            }
            int? number_Of_Printed_Pages = null;
            int? date_Of_Writing = null;
            int? the_Year_Of_Publishing = null;

            if (!string.IsNullOrEmpty(textBox4.Text.Trim()) && int.TryParse(textBox4.Text.Trim(), out int temp))
            {
                number_Of_Printed_Pages = temp;
            }

            if (!string.IsNullOrEmpty(textBox5.Text.Trim()) && int.TryParse(textBox5.Text.Trim(), out temp))
            {
                date_Of_Writing = temp;
            }

            if (!string.IsNullOrEmpty(textBox6.Text.Trim()) && int.TryParse(textBox6.Text.Trim(), out temp))
            {
                the_Year_Of_Publishing = temp;
            }

            b.Number_Of_Printed_Pages = number_Of_Printed_Pages ??= 0;
            b.Date_Of_Writing = date_Of_Writing ??= 0;
            b.The_Year_Of_Publishing = the_Year_Of_Publishing ??= 0;

            db.book.Update(b);
            db.SaveChanges();



            // Удаляем существующие связи темы с книгой
            //var existingThemeLinks = db.book_theme.Where(bt => bt.ID_Book == b.Id);
            //db.book_theme.RemoveRange(existingThemeLinks);

            //// Добавляем новые связи для выбранных тем
            //foreach (var item in LbThemes.Items.Cast<Model.ListBoxItem>().Where(i => i.IsSelected))
            //{
            //    var themeId = db.theme.First(t => t.Name == item.ThemeName).Id;
            //    db.book_theme.Add(new Book_Theme { ID_Book = b.Id, ID_Theme = themeId });
            //}

            //// Сохраняем изменения
            //db.SaveChanges();


            //// 1. Get selected themes and existing book themes
            //var selectedThemes = LbThemes.Items.Cast<Model.ListBoxItem>().Where(item => item.IsSelected).ToList();
            //var existingBookThemes = db.book_theme.Where(bt => bt.ID_Book == b.Id).ToList();

            //// 2. Identify themes to add and remove
            //var themesToAdd = selectedThemes.Where(theme => !existingBookThemes.Any(bt => bt.ID_Theme == theme.ThemeId)).ToList();
            //var themesToRemove = existingBookThemes.Where(bt => !selectedThemes.Any(theme => theme.ThemeId == bt.ID_Theme)).ToList();

            //// 3. Add new theme associations
            //foreach (var theme in themesToAdd)
            //{
            //    db.book_theme.Add(new Book_Theme { ID_Book = b.Id, ID_Theme = theme.ThemeId });
            //}

            //// 4. Remove theme associations that are no longer selected
            //db.book_theme.RemoveRange(themesToRemove);

            //// 5. Save changes to the database
            //db.SaveChanges();


            // Удаляем существующие связи темы с книгой
            var existingThemeLinks = db.book_theme.Where(bt => bt.ID_Book == b.Id);
            db.book_theme.RemoveRange(existingThemeLinks);

            // Добавляем новые связи для выбранных тем
            foreach (var themeId in selectedThemeIds)
            {
                db.book_theme.Add(new Book_Theme { ID_Book = b.Id, ID_Theme = themeId });
            }

            // Сохраняем изменения
            db.SaveChanges();


            MessageBox.Show("Данные книги обновлены.");

            WindowBook wBook = new WindowBook();
            wBook.Show();
            Close();

        }
        private void LbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var addedItem in e.AddedItems)
            {
                var listBoxItem = addedItem as Model.ListBoxItem;
                if (listBoxItem != null)
                {
                    listBoxItem.IsSelected = true;
                }
            }

            foreach (var removedItem in e.RemovedItems)
            {
                var listBoxItem = removedItem as Model.ListBoxItem;
                if (listBoxItem != null)
                {
                    listBoxItem.IsSelected = false;
                }
            }
        }

        private void LbThemes_ItemClicked(object sender, RoutedEventArgs e)
        {
            var listBoxItem = e.OriginalSource as System.Windows.Controls.ListBoxItem;
            if (listBoxItem != null)
            {
                var themeId = db.theme.First(t => t.Name == listBoxItem.Content.ToString()).Id;
                if (listBoxItem.IsSelected)
                {
                    selectedThemeIds.Add(themeId);
                }
                else
                {
                    selectedThemeIds.Remove(themeId);
                }
            }
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var listBoxItem = toggleButton.DataContext as Model.ListBoxItem;
            if (listBoxItem != null)
            {
                listBoxItem.IsSelected = false;
                // Удаление ID темы из списка selectedThemeIds
                selectedThemeIds.Remove(listBoxItem.ThemeId);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var listBoxItem = toggleButton.DataContext as Model.ListBoxItem;
            if (listBoxItem != null)
            {
                listBoxItem.IsSelected = true;
                // Добавление ID темы в список selectedThemeIds (как в предыдущем примере)
                selectedThemeIds.Add(listBoxItem.ThemeId);
            }
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowBook wBook = new WindowBook();
            wBook.Show();
            Close();
        }
    }
}

