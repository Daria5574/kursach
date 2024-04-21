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
            nameUser.Content = App.currentUser.FName; ;
            nameBook.Content = currentBook.Name;

            if (currentBook.Cover != null)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(currentBook.Cover));
                coverBook.Source = bitmap;
            }
            pagesBook.Content = currentBook.Number_Of_Printed_Pages + " печатных страниц";
            timeBook.Content = "Время чтения " + currentBook.Time_To_Read;
            yearBook.Content = currentBook.The_Year_Of_Publishing + " год";
            ratingBook.Content = currentBook.Age_Rating;
            aboutBook.Text = currentBook.About_The_Book;
            using (var db = new DatabaseContext())
            {
                var book = db.book.Include(b => b.Author).FirstOrDefault(b => b.Id == currentBook.Id);
                if (book != null)
                {
                    authorLabel.Content = book.Author.FName + " " + book.Author.LName;
                }

                var bookWithThemes = db.book_theme
                    .Include(bt => bt.Theme) // Включаем связанные записи Theme
                    .Where(bt => bt.ID_Book == currentBook.Id)
                    .Select(bt => new
                    {
                        BookId = bt.ID_Book,
                        Theme = bt.Theme
                    })
                    .ToList();

                if (bookWithThemes != null && bookWithThemes.Count > 0) // Проверяем наличие данных
                {
                    WrapPanel wrapPanel = new WrapPanel() // Используем WrapPanel для расположения в ряд
                    {
                        Orientation = Orientation.Horizontal, // Горизонтальное расположение элементов
                        Margin = new Thickness(10)
                    };

                    foreach (var item in bookWithThemes)
                    {
                        var theme = item.Theme;

                        Button button = new Button
                        {
                            Content = theme.Name,
                            Height = 30,
                            HorizontalContentAlignment = HorizontalAlignment.Stretch, // Растягиваем содержимое по горизонтали
                            Margin = new Thickness(5), // Добавляем отступы между кнопками
                            Style = (Style)FindResource("themes")
                        };

                        // Обработчик события нажатия (добавьте свою логику перехода на страницу)
                        button.Click += (sender, e) =>
                        {
                            // ... (логика перехода на страницу с книгами по теме)
                        };

                        wrapPanel.Children.Add(button);
                    }

                    // Добавляем WrapPanel в ItemsControl
                    itemsControl.Items.Add(wrapPanel);
                }
            }
            if (currentBook.Is_Favorite == 1)
            {
                FavoriteButton.Content = "★ :)";
                FavoriteButton.Style = (Style)FindResource("yellow");
            }
            else
            {
                FavoriteButton.Content = "☆";
                FavoriteButton.Style = (Style)FindResource("gray");
            }

            FavoriteButton.Click += async (sender, e) =>
            {
                // Изменяем значение Is_Favorite (переключение между 0 и 1)
                currentBook.Is_Favorite = currentBook.Is_Favorite == 0 ? 1 : 0;

                // Сохраняем изменения в базе данных асинхронно
                using (var db = new DatabaseContext())
                {
                    db.book.Attach(currentBook); // Присоединяем currentBook к контексту
                    db.Entry(currentBook).State = EntityState.Modified; // Указываем, что объект был изменен
                    await db.SaveChangesAsync();
                }

                // Обновляем стиль и содержимое кнопки в зависимости от Is_Favorite
                if (currentBook.Is_Favorite == 1)
                {
                    FavoriteButton.Content = "★;";
                    FavoriteButton.Style = (Style)FindResource("yellow");
                }
                else
                {
                    FavoriteButton.Content = "☆";
                    FavoriteButton.Style = (Style)FindResource("gray");
                }
            };
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

        private void NavigateToAuthor(object sender, MouseButtonEventArgs e)
        {
            Author currentAuthor = null;

            using (DatabaseContext db = new DatabaseContext())
            {
                var authorh = authorLabel.Content as dynamic;

                string fname = authorh; // Получаем название книги из анонимного типа

                // Проверяем, есть ли в базе данных книга с таким же названием
                currentAuthor = db.author.FirstOrDefault(a => a.FName + " " + a.LName == fname);
                WindowAuthorDetails wAuthor = new WindowAuthorDetails(currentAuthor);
                wAuthor.Show();
                Close();
            }
        }

    }
}
