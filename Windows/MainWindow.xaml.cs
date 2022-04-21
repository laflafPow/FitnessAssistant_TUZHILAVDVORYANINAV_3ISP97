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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97.Windows;

namespace FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegBTN_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void Enterbtn_Click(object sender, RoutedEventArgs e)
        {
            EF.User authUser = ClassHelper.AppData.Context.User.ToList().
                Where(i => i.Login == tbLogin.Text && i.Password == tbPassword.Text).
                FirstOrDefault();

            if (authUser != null)
            {
                WelcomeWindow WelcomeWindow = new WelcomeWindow();
                WelcomeWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный пароль или логин!\n\nЕсли у вас еще нет аккаунта - зарегистрируйтесь по кнопке ниже!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

