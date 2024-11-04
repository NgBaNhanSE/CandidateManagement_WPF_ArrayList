using Candidate_BusinessObjects;
using Candidate_Services;
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

namespace CandidateManagement_NguyenBaNhan
{
    /// <summary>
    /// Interaction logic for HrAccountWindow.xaml
    /// </summary>
    public partial class HrAccountWindow : Window
    {
        private string? email;
        private Hraccount account;
        private IHRAccountService hRAccountService;
        public HrAccountWindow()
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }
        public HrAccountWindow(string? _email)
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();

            this.email = _email;
            account = hRAccountService.GetHraccountByEmail(email);
            switch (account.MemberRole)
            {

                case 1:

                    break;
                case 2:
                    btn_Add.Visibility = Visibility.Collapsed;
                    btn_Delete.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    btn_Add.Visibility = Visibility.Collapsed;
                    btn_Update.Visibility = Visibility.Collapsed;
                    btn_Delete.Visibility = Visibility.Collapsed; ;
                    break;
                default:
                    MessageBox.Show("You don't have a role to access");
                    Application.Current.Shutdown();
                    break;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
        private void loadDataInit()
        {
            this.dtgAccount.ItemsSource = hRAccountService.GetHraccounts();
            account = hRAccountService.GetHraccountByEmail(email);
            switch (account.MemberRole)
            {

                case 1:

                    break;
                case 2:
                    btn_Add.Visibility = Visibility.Collapsed;
                    btn_Delete.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    btn_Add.Visibility = Visibility.Collapsed;
                    btn_Update.Visibility = Visibility.Collapsed;
                    btn_Delete.Visibility = Visibility.Collapsed; ;
                    break;
                default:
                    MessageBox.Show("You don't have a role to access");
                    Application.Current.Shutdown();
                    break;
            }

            var roleList = new List<object>()
      {
          new { MemberRole = 1, RoleName = "Admin" },
          new { MemberRole = 2, RoleName = "Manager" },
          new { MemberRole = 3, RoleName = "Staff" }
      };

            this.cbRole.ItemsSource = roleList;
            this.cbRole.DisplayMemberPath = "RoleName";
            this.cbRole.SelectedValuePath = "MemberRole";
            this.cbSearchRole.ItemsSource = roleList;
            this.cbSearchRole.DisplayMemberPath = "RoleName";
            this.cbSearchRole.SelectedValuePath = "MemberRole";


            txtEmail.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtSeacrhName.Text = "";
            cbRole.SelectedValue = null;
            cbSearchRole.SelectedValue = null;
        }
    

    private void dtgJobPosting_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        DataGrid dataGrid = sender as DataGrid;


        DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
        if (row != null)
        {
            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string email = ((TextBlock)RowColumn.Content).Text;
            Hraccount hraccount = hRAccountService.GetHraccountByEmail(email);
            if (hraccount != null)
            {
                txtEmail.Text = hraccount.Email;
                txtFullName.Text = hraccount.FullName;
                txtPassword.Text = hraccount.Password;
                
                    cbRole.SelectedValue = hraccount.MemberRole;
            }
        }
    }

    private void btn_Add_Click(object sender, RoutedEventArgs e)
    {
        Hraccount hraccount = new Hraccount();

        if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtFullName.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
              string.IsNullOrWhiteSpace(cbRole.SelectedValue.ToString())||
            !txtEmail.Text.Contains("@"))
        {
            MessageBox.Show("All fields are required and email must contain '@'");
            return;
        }
      
        

        hraccount.Email = txtEmail.Text;
        hraccount.FullName = txtFullName.Text;
        hraccount.Password = txtPassword.Text;
        hraccount.MemberRole = (int)cbRole.SelectedValue;

        var _existAccount = hRAccountService.GetHraccountByEmail(hraccount.Email);
        if (_existAccount != null)
        {

            MessageBox.Show("Has already this Profile");
            return;
        }

        if (hRAccountService.AddHrAccount(hraccount))
        {
            MessageBox.Show("Add successfull");
            loadDataInit();
        }
        else
        {
            MessageBox.Show("Add unsuccessfull");

        }
    }

    private void btn_Update_Click(object sender, RoutedEventArgs e)
    {

        Hraccount hraccount = new Hraccount();

        if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtFullName.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
             string.IsNullOrWhiteSpace(cbRole.SelectedValue.ToString()))
        {
            MessageBox.Show("All fields are require ! ");
            return;
        }
       

        hraccount.Email = txtEmail.Text;
        hraccount.FullName = txtFullName.Text;
        hraccount.Password = txtPassword.Text;
        hraccount.MemberRole = (int)cbRole.SelectedValue;
        if (account.MemberRole != 1 && hraccount.MemberRole == 1)
        {
            MessageBox.Show("You can't update to admin role with this account ! ");
            return;
        }
        var _existAccount = hRAccountService.GetHraccountByEmail(hraccount.Email);
        if (_existAccount == null)
        {

            MessageBox.Show("Not have this Account ");
            return;
        }

        if (
  MessageBox.Show("If you update role (The Role will be apply mediately !!)",
                  "Update Data ",
                  MessageBoxButton.YesNo,
                  MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            if (hRAccountService.UpdateHrAccount(hraccount))
            {
                MessageBox.Show("Update Successful");
                    loadDataInit();
                }
            else
            {
                MessageBox.Show("Update Unsuccessful");
            }
        }
    }

    private void btn_Delete_Click(object sender, RoutedEventArgs e)
    {
        string email = txtEmail.Text;


        if (email != null &&
  MessageBox.Show("Do you really want to delete this row data?",
                  "Delete Data ",
                  MessageBoxButton.YesNo,
                  MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            if (email.Length > 0 && hRAccountService.DeleteHrAccount(email))
            {
                MessageBox.Show("Delete Successful");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Delete Unsuccessful");

            }
        }
    }

    private void btn_Back_Click(object sender, RoutedEventArgs e)
    {
        MenuWindow menuWindow = new MenuWindow(email);
        menuWindow.Show();
        this.Close();

    }

    private void btn_Search_Click(object sender, RoutedEventArgs e)
    {
            dtgAccount.ItemsSource = hRAccountService.GetHraccountByNameOrRole(
         txtSeacrhName.Text,
         cbSearchRole.SelectedValue?.ToString() 
     );
            cbSearchRole.SelectedValue = null;
            txtSeacrhName.Text = "";
        }
}
}
