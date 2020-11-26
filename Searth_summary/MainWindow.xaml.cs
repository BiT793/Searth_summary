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
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace Searth_summary
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

        string Id;
        ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

        private void button_reg_Click(object sender, RoutedEventArgs e)
        {
            border.Visibility = Visibility.Hidden;
            table.Visibility = Visibility.Hidden;
            button_reg.Visibility = Visibility.Hidden;
            button_log.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Hidden;
            textBoxLogon.Visibility = Visibility.Hidden;

            textBoxLoginReg.Visibility = Visibility.Visible;
            textBoxCompany.Visibility = Visibility.Visible;
            textBoxEmail.Visibility = Visibility.Visible;
            passwordBoxReg.Visibility = Visibility.Visible;
            passwordBoxRegCmf.Visibility = Visibility.Visible;
            groupBox.Visibility = Visibility.Visible;
            Btn_reg.Visibility = Visibility.Visible;
            butn_back.Visibility = Visibility.Visible;
        }

        private void butn_back_Click(object sender, RoutedEventArgs e)
        {
            border.Visibility = Visibility.Visible;
            table.Visibility = Visibility.Visible;
            button_reg.Visibility = Visibility.Visible;
            button_log.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Visible;
            textBoxLogon.Visibility = Visibility.Visible;

            textBoxLoginReg.Visibility = Visibility.Hidden;
            textBoxCompany.Visibility = Visibility.Hidden;
            textBoxEmail.Visibility = Visibility.Hidden;
            passwordBoxReg.Visibility = Visibility.Hidden;
            passwordBoxRegCmf.Visibility = Visibility.Hidden;
            groupBox.Visibility = Visibility.Hidden;
            Btn_reg.Visibility = Visibility.Hidden;
            butn_back.Visibility = Visibility.Hidden;
        }

        private void button_log_Click(object sender, RoutedEventArgs e)
        {
            var access_level = service.Authentication(textBoxLogon.Text, passwordBox.Password);
            if (access_level == 4)
            {
                MessageBox.Show("The database is not available", "Error");
                return;
            }
            else if (access_level !=-1)
            {
                AdminWindow window = new AdminWindow(access_level);
                window.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Error login","Error");
            }
             
        }

        private void Btn_reg_Click(object sender, RoutedEventArgs e)
        {
            int accessLvl=1;
            string password = passwordBoxReg.Password.GetHashCode().ToString();
            string passwordCmf = passwordBoxRegCmf.Password.GetHashCode().ToString();
            
            if (password == passwordCmf)
            {
                if (radioButton_summary.IsChecked == true)
                    accessLvl = 1;
                if (radioButton_emploer.IsChecked == true)
                    accessLvl = 2;
                if (radioButton_recruiter.IsChecked == true)
                    accessLvl = 3;
                service.AddAccount(textBoxLoginReg.Text, textBoxEmail.Text, accessLvl, textBoxCompany.Text, password);
            }
           
        }
    }
}
