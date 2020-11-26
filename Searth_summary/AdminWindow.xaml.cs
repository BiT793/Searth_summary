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

namespace Searth_summary
{
    
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(int access_level)
        {
            InitializeComponent();
            this.access_level = access_level;
            Check();
        }
        int access_level;
        string Id;
        List<string> JobSeekerId = new List<string>(); 
        ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();

        public void Check()
        {
            if (access_level==0)//админ
            {
                Btn_choose.Visibility = Visibility.Hidden;
                Btn_chooseJob.Visibility = Visibility.Hidden;
                CheckAllert.Visibility = Visibility.Visible;

            }
            if (access_level == 1)//работадатель
            {
                Btn_Users.Visibility = Visibility.Hidden;
                Btn_report_vacancy.Visibility= Visibility.Hidden;
                Btn_report.Visibility = Visibility.Hidden;
                vacanciID.Visibility= Visibility.Hidden;
                vacanciUserID.Visibility = Visibility.Hidden;
                jodseekerID.Visibility = Visibility.Hidden;
                jodseekerUserID.Visibility = Visibility.Hidden;
                Btn_choose.Visibility = Visibility.Hidden;
                Btn_chooseJob.Visibility = Visibility.Hidden;
            }
            if (access_level == 2)//рекрутер
            {
                Btn_report_vacancy.Visibility = Visibility.Hidden;
                Btn_report.Visibility = Visibility.Hidden;
                vacanciID.Visibility = Visibility.Hidden;
                vacanciUserID.Visibility = Visibility.Hidden;
                jodseekerID.Visibility = Visibility.Hidden;
                jodseekerUserID.Visibility = Visibility.Hidden;
                Btn_Users.Visibility = Visibility.Hidden;
            }
            if (access_level == 3)//соискатель
            {
                Btn_summary.Visibility = Visibility.Hidden;
                Btn_choose.Visibility = Visibility.Hidden;
                Btn_chooseJob.Visibility = Visibility.Hidden;
                listUsers.Visibility = Visibility.Hidden;
                listJodseeker.Visibility = Visibility.Hidden;
                Btn_report_vacancy.Visibility = Visibility.Hidden;
                Btn_report.Visibility = Visibility.Hidden;
                Btn_Users.Visibility = Visibility.Hidden;
                vacanciID.Visibility = Visibility.Hidden;
                vacanciUserID.Visibility = Visibility.Hidden;
                jodseekerID.Visibility = Visibility.Hidden;
                jodseekerUserID.Visibility = Visibility.Hidden;
            }
            if (access_level == 4) 
            {
                Btn_summary.Visibility = Visibility.Hidden;
                Btn_choose.Visibility = Visibility.Hidden;
                Btn_chooseJob.Visibility = Visibility.Hidden;
                listUsers.Visibility = Visibility.Hidden;
                listJodseeker.Visibility = Visibility.Hidden;
                Btn_report_vacancy.Visibility = Visibility.Hidden;
                Btn_report.Visibility = Visibility.Hidden;
                Btn_Users.Visibility = Visibility.Hidden;
                vacanciID.Visibility = Visibility.Hidden;
                vacanciUserID.Visibility = Visibility.Hidden;
                jodseekerID.Visibility = Visibility.Hidden;
                jodseekerUserID.Visibility = Visibility.Hidden;
                Btn_vacancy.Visibility = Visibility.Hidden;

            }

        }
        private async void Btn_vacancy_Click(object sender, RoutedEventArgs e)
        {
            
            if (await service.GetVacanciAsync(access_level) != null)
            {
                listJodseeker.Visibility = Visibility.Hidden;
                listUsers.Visibility = Visibility.Hidden;
                CrashTable.Visibility = Visibility.Hidden;
                listVacanci.Visibility = Visibility.Visible;
                listVacanci.ItemsSource = await service.GetVacanciAsync(access_level);
            }       
            else
                MessageBox.Show("The database is not available", "Error");
        }

        private async void Btn_summary_Click(object sender, RoutedEventArgs e)
        {

            if (await service.GetJobAsync(access_level) != null)
            {
                listUsers.Visibility = Visibility.Hidden;
                listVacanci.Visibility = Visibility.Hidden;
                CrashTable.Visibility = Visibility.Hidden;
                listJodseeker.Visibility = Visibility.Visible;
                listJodseeker.ItemsSource = await service.GetJobAsync(access_level);
            }
            else
                MessageBox.Show("The database is not available", "Error");
        }

        private async void Btn_Users_Click(object sender, RoutedEventArgs e)
        {

            listJodseeker.Visibility = Visibility.Hidden;
            listVacanci.Visibility = Visibility.Hidden;
            CrashTable.Visibility = Visibility.Hidden;
            listUsers.Visibility = Visibility.Visible;
            listUsers.ItemsSource = await service.GetAccountAsync(access_level);
        }
      
        private void Btn_report_vacancy_Click(object sender, RoutedEventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void Btn_report_Click(object sender, RoutedEventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private async void Btn_choose_click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Vacanci vacanci = (ServiceReference1.Vacanci)listVacanci.SelectedItem;
            Id = vacanci.Id.ToString();
            Btn_Ok.Visibility = Visibility.Visible;
            Btn_vacancy.Visibility = Visibility.Hidden;
            Btn_summary.Visibility = Visibility.Visible;
            listVacanci.Visibility = Visibility.Hidden;
            listJodseeker.Visibility = Visibility.Visible;
            listJodseeker.ItemsSource = await service.GetJobAsync(access_level);
        }

        
        private void Btn_chooseJob_click(object sender, RoutedEventArgs e)
        {
            
            ServiceReference1.JobSeeker jobSeeker = (ServiceReference1.JobSeeker)listJodseeker.SelectedItem;
            JobSeekerId.Add( jobSeeker.Id.ToString());
            
        }

        private async void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {

            foreach (var idSeeker in JobSeekerId)
            {
                await service.UpdateVacanciAsync(Id, idSeeker, access_level);
            }
            Btn_Ok.Visibility = Visibility.Hidden;
            Btn_vacancy.Visibility = Visibility.Visible;
            listVacanci.Visibility = Visibility.Visible;
            listJodseeker.Visibility = Visibility.Hidden;
            listVacanci.ItemsSource = await service.GetVacanciAsync(access_level);


        }

        private void listUsers_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter && access_level==0)
            {
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                e.Handled = true;
                ServiceReference1.Account user = (ServiceReference1.Account)listUsers.SelectedItem;
                service.updateUser(user, access_level);
            }
        }

        private void listUsers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
   
            var u = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter && access_level == 0)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

            }

        }

        private async void BtnCrashTable_Click(object sender, RoutedEventArgs e)
        {
            listJodseeker.Visibility = Visibility.Hidden;
            listUsers.Visibility = Visibility.Hidden;
            CrashTable.Visibility = Visibility.Visible;
            listVacanci.Visibility = Visibility.Hidden;
            CrashTable.ItemsSource = await service.GetReservsAsync();
        }

        private void CrashTable_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter && access_level == 0)
            {
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                e.Handled = true;
                ServiceReference1.ReservAccount user = (ServiceReference1.ReservAccount)CrashTable.SelectedItem;
                service.updateAlertUsers(user, access_level);
            }
        }

        private void CrashTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter && access_level == 0)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

            }
        }

        private void listJodseeker_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter && access_level == 0)
            {
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                e.Handled = true;
                ServiceReference1.JobSeeker job = (ServiceReference1.JobSeeker)listJodseeker.SelectedItem;
                service.updateJobSeekerAsync(job, access_level);
            }
        }

        private void listJodseeker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter && access_level == 0)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

            }
        }

        private void listVacanci_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter && access_level == 0)
            {
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                e.Handled = true;
                ServiceReference1.Vacanci vacanci = (ServiceReference1.Vacanci)listVacanci.SelectedItem;
                service.updateVacanciItemsAsync(vacanci, access_level);
            }
        }

        private void listVacanci_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var u = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter && access_level == 0)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

            }
        }

        private async void CheckAllert_Checked(object sender, RoutedEventArgs e)
        {
            service.CrashTable();
            BtnCrashTable.Visibility = Visibility.Visible;
            CrashTable.Visibility = Visibility.Visible;
            CrashTable.ItemsSource = await service.GetReservsAsync();
        }

        private void CheckAllert_Unchecked(object sender, RoutedEventArgs e)
        {
            service.CrashTable();
            CrashTable.Visibility = Visibility.Hidden;
            BtnCrashTable.Visibility = Visibility.Hidden;
        }
    }
}
