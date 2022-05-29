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

namespace FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        bool isEdit = false;
        EF.User editUser = new EF.User();

        public RegistrationWindow()
        {
            InitializeComponent();

            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "GenderName";
            cbGender.SelectedIndex = 0;      
        }

        public RegistrationWindow(EF.User user)
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "GenderName";
            cbGender.SelectedIndex = 0;

            txtLogin.Text = user.Login;
            txtPassword.Text = user.Password;
            txtLastName.Text = user.LastName;
            txtFirstName.Text = user.FirstName;
            txtHeight.Text = user.Height.ToString();
            txtWeight.Text = user.Weight.ToString();
            dpDateBirth.SelectedDate = user.DateBirst;

            cbGender.SelectedIndex = user.idGender - 1;

            tbWelcome.Text = "Изменение данных";
            btnEnter.Content = "Сохранить";

            isEdit = true;

            editUser = user;
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                if (editUser.Login != txtLogin.Text)
                {
                    if (Classes.Validation.ValidationLogin(txtLogin.Text) == false)
                    {
                        MessageBox.Show("Недопустимый логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            else
            {
                if (Classes.Validation.ValidationLogin(txtLogin.Text) == false)
                {
                    MessageBox.Show("Недопустимый логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }         

            if (Classes.Validation.ValidationPassword(txtPassword.Text) == false)
            {
                MessageBox.Show("Недопустимый пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Classes.Validation.ValidationName(txtLastName.Text) == false)
            {
                MessageBox.Show("Недопустимая фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Classes.Validation.ValidationName(txtFirstName.Text) == false)
            {
                MessageBox.Show("Недопустимое имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Classes.Validation.ValidationWeightAndHeight(txtHeight.Text) == false)
            {
                MessageBox.Show("Недопустимый рост", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Classes.Validation.ValidationWeightAndHeight(txtWeight.Text) == false)
            {
                MessageBox.Show("Недопустимый вес", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpDateBirth.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Ошибка в поле даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Classes.Validation.ValidationDateBirthday(dpDateBirth.SelectedDate.Value) == false)
            {
                MessageBox.Show("Недопустимая дата рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                var resClick = MessageBox.Show("Изменить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                try
                {
                    editUser.Login = txtLogin.Text;
                    editUser.Password = txtPassword.Text;
                    editUser.LastName = txtLastName.Text;
                    editUser.FirstName = txtFirstName.Text;
                    editUser.Height = Convert.ToInt32(txtHeight.Text);
                    editUser.Weight = Convert.ToInt32(txtWeight.Text);
                    editUser.DateBirst = dpDateBirth.SelectedDate.Value;
                    editUser.idGender = (cbGender.SelectedItem as EF.Gender).idGender;

                    ClassHelper.AppData.Context.SaveChanges();

                    MessageBox.Show("Пользователь изменен");

                    WelcomeWindow welcomeWindow = new WelcomeWindow(editUser);
                    welcomeWindow.Show();
                    this.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                var resClick = MessageBox.Show("Добавить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                try
                {
                    EF.User newUser = new EF.User();

                    newUser.Login = txtLogin.Text;
                    newUser.Password = txtPassword.Text;

                    newUser.LastName = txtLastName.Text;
                    newUser.FirstName = txtFirstName.Text;

                    newUser.Height = Convert.ToInt32(txtHeight.Text);
                    newUser.Weight = Convert.ToInt32(txtWeight.Text);

                    newUser.DateBirst = dpDateBirth.SelectedDate.Value;

                    newUser.idGender = (cbGender.SelectedItem as EF.Gender).idGender;


                    ClassHelper.AppData.Context.User.Add(newUser);
                    ClassHelper.AppData.Context.SaveChanges();

                    MessageBox.Show("Пользователь добавлен");

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();                   
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            
        }

        private void BtnCansel_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit == false)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                WelcomeWindow welcomeWindow = new WelcomeWindow(editUser);
                welcomeWindow.Show();
                this.Close();
            }
        }
    }
}
