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
using kursach.ViewModel;


namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {

        DatabaseContext db;

        public WindowRegistration()
        {
            InitializeComponent();

            db = new DatabaseContext();

            //List<User> users = db.Users.ToList();
            //string str = "";
            //foreach (User user in users)
            //{
            //    str += "Имя и фамилия: " + user.FName + " " + user.LName + user.Email;
            //}
            //example.Text = str;

            //List<Author> a = db.author.ToList();
            //string str2 = "";
            //foreach (Author author in a)
            //{
            //    str2 += "Имя и фамилия: " + author.FName + " " + author.LName;
            //}
            //example.Text = str2;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string fname = fnameTextBox.Text.Trim();
            string lname = lnameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim().ToLower();
            string pass = passwordTextBox.Password.Trim();
            string pass_2 = repeatPasswordTextBox.Password.Trim();

            if(pass.Length < 5)
            {
                passwordTextBox.ToolTip = "Пароль должен быть длиной 6 символов или больше!";
                passwordTextBox.Background = Brushes.LightCoral;
            } 
            
            else if (pass != pass_2)
            {
                repeatPasswordTextBox.ToolTip = "Пароли должны совпадать.";
                repeatPasswordTextBox.Background = Brushes.LightCoral;
            }

            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                emailTextBox.ToolTip = "Email введен неккоректно.";
                emailTextBox.Background = Brushes.LightCoral;
            }
            else
            {
                fnameTextBox.ToolTip = "";
                fnameTextBox.Background = Brushes.Transparent;

                lnameTextBox.ToolTip = "";
                lnameTextBox.Background = Brushes.Transparent;

                emailTextBox.ToolTip = "";
                emailTextBox.Background = Brushes.Transparent;

                passwordTextBox.ToolTip = "";
                passwordTextBox.Background = Brushes.Transparent;

                repeatPasswordTextBox.ToolTip = "";
                repeatPasswordTextBox.Background = Brushes.Transparent;

                MessageBox.Show("Регистрация успешно завершена!");

                User user = new User(fname, lname, email, pass);
                db.user.Add(user);
                db.SaveChanges();
            }
        }
    }
}
