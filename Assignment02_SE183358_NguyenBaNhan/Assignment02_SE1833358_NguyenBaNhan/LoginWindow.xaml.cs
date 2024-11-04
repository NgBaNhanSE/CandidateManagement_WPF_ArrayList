using Assignment01_SE183358_NguyenBaNhan;
using Candidate_BusinessObjects;
using Candidate_Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CandidateManagement_NguyenBaNhan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private IHRAccountService hRAccountService;
        public LoginWindow()
        {
            InitializeComponent();
           
            hRAccountService = new HRAccountService();
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Hraccount hraccount = hRAccountService.GetHraccountByEmail(TXTEmail.Text);
            if (hraccount != null && TXTPassword.Text.Equals(hraccount.Password))
            {
                if (hraccount.MemberRole == 1 || hraccount.MemberRole == 2 || hraccount.MemberRole == 3)
                {
                    var email = hraccount.Email;
                    MenuWindow menuWindow = new MenuWindow(email);
                    menuWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You don't have a role to access !");
                }

            }
            else
            {
                MessageBox.Show("Wrong password or email !");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}