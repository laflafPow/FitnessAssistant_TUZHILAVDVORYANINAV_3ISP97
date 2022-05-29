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
using FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97.Classes;

namespace FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97.Windows
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        string login;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        public WelcomeWindow(EF.User user)
        {
            InitializeComponent();

            tbWelcome.Text = $"Добро пожаловать, {user.FirstName}!";
            tbHeight.Text = user.Height.ToString();
            tbWeight.Text = user.Weight.ToString();
            tbAge.Text = user.Age.ToString();
            tbBMI.Text = ReturnBMI(user);
            tbBMR.Text = ReturnBMR(user);
            login = user.Login;
        }

        public string ReturnBMI(EF.User user)
        {
            string bmiResult;

            if (Classes.Validation.CalculatorBMI(user.Height, user.Weight) < 18.5)
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Ниже нормального веса";
            }

            else if (Classes.Validation.CalculatorBMI(user.Height, user.Weight) >= 18.5 &&
                     Classes.Validation.CalculatorBMI(user.Height, user.Weight) < 25)
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Нормальный вес";
            }

            else if (Classes.Validation.CalculatorBMI(user.Height, user.Weight) >= 25 &&
                     Classes.Validation.CalculatorBMI(user.Height, user.Weight) < 30)
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Избыточный вес";
            }

            else if (Classes.Validation.CalculatorBMI(user.Height, user.Weight) >= 30 &&
                     Classes.Validation.CalculatorBMI(user.Height, user.Weight) < 35)
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Ожирение I степени";
            }

            else if (Classes.Validation.CalculatorBMI(user.Height, user.Weight) >= 35 &&
                     Classes.Validation.CalculatorBMI(user.Height, user.Weight) < 40)
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Ожирение II степени";
            }

            else 
            {
                bmiResult = $"{Classes.Validation.CalculatorBMI(user.Height, user.Weight)} кг/м2 Ожирение III степени";
            }

            return bmiResult;
        }

        public string ReturnBMR(EF.User user)
        {
            string bmrResult = $"{Classes.Validation.CalculatorBMR(user.idGender, Convert.ToInt32(user.Age), user.Height, user.Weight)} ккал/день";

            return bmrResult;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();           
        }

        private void btnChangeSettings_Click(object sender, RoutedEventArgs e)
        {
            var editUser = ClassHelper.AppData.Context.User.ToList().
            Where(i => i.Login == login).FirstOrDefault();

            RegistrationWindow registrationWindow = new RegistrationWindow(editUser);
            registrationWindow.Show();
            this.Close();
        }
    }
}
