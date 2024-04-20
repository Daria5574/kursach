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
using kursach.Model;

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public WindowAuthorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {

            string email = emailTextBox.Text.Trim().ToLower();
            string pass = passwordTextBox.Password.Trim();

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                emailTextBox.ToolTip = "Email введен неккоректно.";
                emailTextBox.Background = Brushes.LightCoral;
            }
            else if (pass.Length < 5)
            {
                passwordTextBox.ToolTip = "Пароль должен быть длиной 6 символов или больше!";
                passwordTextBox.Background = Brushes.LightCoral;
            }
            else
            {
                emailTextBox.ToolTip = "";
                emailTextBox.Background = Brushes.Transparent;

                passwordTextBox.ToolTip = "";
                passwordTextBox.Background = Brushes.Transparent;

                User authUser = null;
                using (DatabaseContext db = new DatabaseContext())
                {
                    authUser = db.user.Where(b => b.Email == email && b.Password == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    App.currentUser = authUser;
                    WindowBook wMyBooks = new WindowBook();
                    wMyBooks.Show();
                    Close();
                }
                else
                    MessageBox.Show("Пользователь с такой почтой и паролем не найден.");
            }
        }
    }
}
