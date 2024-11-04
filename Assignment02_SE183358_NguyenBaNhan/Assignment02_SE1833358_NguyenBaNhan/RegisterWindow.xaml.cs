using Candidate_BusinessObjects;
using Candidate_Services;
using CandidateManagement_NguyenBaNhan;
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

namespace Assignment01_SE183358_NguyenBaNhan
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private IHRAccountService hRAccountService;
        public RegisterWindow()
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();    
            loginWindow.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Hraccount hraccount = new Hraccount();

            if (string.IsNullOrWhiteSpace(TXTEmail.Text) ||
                string.IsNullOrWhiteSpace(TXTFullName.Text) ||
                string.IsNullOrWhiteSpace(TXTPassword.Text) ||
                !TXTEmail.Text.Contains("@"))
            {
                MessageBox.Show("All fields are required and email must contain '@'");
                return;
            }
            int role;
          

            hraccount.Email = TXTEmail.Text;
            hraccount.FullName = TXTFullName.Text;
            hraccount.Password = TXTPassword.Text;
            hraccount.MemberRole = 3;

            var _existAccount = hRAccountService.GetHraccountByEmail(hraccount.Email);
            if (_existAccount != null)
            {

                MessageBox.Show("Has already this Profile");
                return;
            }

            if (hRAccountService.AddHrAccount(hraccount))
            {
                MessageBox.Show("Register successfull");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Register unsuccessfull");

            }

        }


        private void loadDataInit()
        { 
        TXTEmail.Text = "";
            TXTFullName.Text = "";
            TXTPassword.Text = "";
    
        }
    }
}
