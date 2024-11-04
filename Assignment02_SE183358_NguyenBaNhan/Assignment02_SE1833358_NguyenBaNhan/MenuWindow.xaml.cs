using Candidate_BusinessObjects;
using Candidate_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace CandidateManagement_NguyenBaNhan
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private string? email;
        private Hraccount account;
        private IHRAccountService hracountService;
        public MenuWindow()
        {
            InitializeComponent();
        }
        public MenuWindow(string? _email)
        {
            InitializeComponent();
  
            this.email = _email;
            hracountService = new HRAccountService();
            account = hracountService.GetHraccountByEmail(email);
            if (account.MemberRole != 1 && account.MemberRole != 2 && account.MemberRole != 3)
            {
               MessageBox.Show("Your role has change. You don't have a role to access !");
                Application.Current.Shutdown();
            }
           

        }

        private void btn_HrAccount_Click(object sender, RoutedEventArgs e)
        {
            HrAccountWindow hrAccountWindow = new HrAccountWindow(email);
            hrAccountWindow.Show();
            this.Close();
           
        }

    

        private void btn_Candidate_Click(object sender, RoutedEventArgs e)
        {
            CandidateProfileWindow candidateProfileWindow = new CandidateProfileWindow(email);
            candidateProfileWindow.Show();
            this.Close();
           
        }

        private void btn_JobPosting_Click(object sender, RoutedEventArgs e)
        {
            JobPostingWindow jobPostingWindow = new JobPostingWindow(email);
            jobPostingWindow.Show();
            this.Close();
               
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
           

        }
    }
}
