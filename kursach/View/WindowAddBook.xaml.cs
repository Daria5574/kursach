using kursach.Model;
using kursach.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.Xml.Linq;

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowAddBook.xaml
    /// </summary>
    public partial class WindowAddBook : Window
    {
        DatabaseContext db;
        private string selectedImagePath;
        public WindowAddBook()
        {
            InitializeComponent();

            var viewModel = new WindowAddBookViewModel();
            db = new DatabaseContext();

            CbAuthor.ItemsSource = db.author.Where(a => a.ID_User == App.currentUser.ID_User).ToList();
            CbAuthor.DisplayMemberPath = "FullName";

            label1.Content = "Название*";
            label2.Content = "Путь к файлу*";
            label3.Content = "Автор";
            label4.Content = "Количество печатных страниц";
            label5.Content = "Год написания";
            label6.Content = "Год издания";
            label7.Content = "ISBN";
            label8.Content = "Время на чтение";
            label9.Content = "О книге";
            label10.Content = "Возрастной рейтинг";
            nameUser.Content = App.currentUser.FName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = textBox1.Text.Trim();
            string the_Path_To_The_File = textBox2.Text.Trim();

            int id_Author = (CbAuthor.SelectedItem as Author)?.Id ?? 0;

            int number_Of_Printed_Pages;
            int date_Of_Writing;
            int the_Year_Of_Publishing;
            string isbn = textBox7.Text.Trim();
            string time_To_Read = textBox8.Text.Trim();
            string about_The_Book = textBox9.Text.Trim();
            string age_Rating = textBox10.Text.Trim();

            if (int.TryParse(textBox4.Text.Trim(), out number_Of_Printed_Pages) &&
                int.TryParse(textBox5.Text.Trim(), out date_Of_Writing) &&
                int.TryParse(textBox6.Text.Trim(), out the_Year_Of_Publishing))
            {
                Book book = new Book(name, id_Author, the_Path_To_The_File, selectedImagePath, number_Of_Printed_Pages, date_Of_Writing, the_Year_Of_Publishing, isbn, time_To_Read, about_The_Book, age_Rating, 0, App.currentUser.ID_User);
                db.book.Add(book);
                db.SaveChanges();

                MessageBox.Show("Книга успешно добавлена");

                WindowBook wBook = new WindowBook();
                wBook.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения.");
            }
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
            }
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

    }
}
