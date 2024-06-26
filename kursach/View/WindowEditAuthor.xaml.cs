﻿using kursach.Model;
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
    /// Логика взаимодействия для WindowEditAuthor.xaml
    /// </summary>
    public partial class WindowEditAuthor : Window
    {
        DatabaseContext db;
        private Author auth;
        public WindowEditAuthor(Author selectedAuthor)
        {
            InitializeComponent();

            db = new DatabaseContext();
            auth = selectedAuthor;

            label1.Content = "Имя автора";
            label2.Content = "Фамилия автора*";
            label3.Content = "Об авторе";
            nameUser.Content = App.currentUser.FName;

            textBox1.Text = auth.FName;
            textBox2.Text = auth.LName;
            textBox3.Text = auth.Description;

        }
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            auth.FName = textBox1.Text.Trim();
            auth.LName = textBox2.Text.Trim();
            auth.Description = textBox3.Text.Trim();

            db.author.Update(auth);
            db.SaveChanges();

            MessageBox.Show("Данные автора обновлены.");

            WindowAuthor wAuth = new();
            wAuth.Show();
            Close();
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor wAuth = new();
            wAuth.Show();
            Close();
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

    }
}
