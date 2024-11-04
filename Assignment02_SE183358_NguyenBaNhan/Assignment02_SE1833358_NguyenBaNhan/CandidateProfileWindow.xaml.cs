using Candidate_BusinessObjects;
using Candidate_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Interaction logic for CandidateProfileWindow.xaml
    /// </summary>
    public partial class CandidateProfileWindow : Window
    {
        private string? email;
        private Hraccount account;
        private IHRAccountService hracountService;
        private ICandidateProfileService profileService;
        private IJobPostingService jobPostingService;
        public CandidateProfileWindow()
        {
            InitializeComponent();
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
            hracountService = new HRAccountService();
        }

        public CandidateProfileWindow(string? _email)
        {
            InitializeComponent();
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
            hracountService = new HRAccountService();
            this.email = _email;
            account = hracountService.GetHraccountByEmail(email);
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

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow(email);
            menuWindow.Show();
            this.Close();
       
        }

       

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            string candidateID = txtCandidateID.Text;


            if (candidateID != null &&
  MessageBox.Show("Do you really want to delete this row data?",
                      "Delete Data ",
                      MessageBoxButton.YesNo,
                      MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (candidateID.Length > 0 && profileService.DeleteCandidateProfile(candidateID))
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           loadDataInit();
        }

        

        private void dtgCandidateProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            
                
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
            if (row !=null)
            {
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;
                CandidateProfile candidateProfile = profileService.GetCandidateProfileByID(id);
                if (candidateProfile != null)
                {
                    txtCandidateID.Text = candidateProfile.CandidateId;
                    txtFullName.Text = candidateProfile.Fullname;
                    txtDescription.Text = candidateProfile.ProfileShortDescription;
                    txtImageURL.Text = candidateProfile.ProfileUrl;
                    CBJobPostingID.SelectedValue = candidateProfile.PostingId;
                    DatePickerBD.SelectedDate = candidateProfile.Birthday;
                }
            }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
           CandidateProfile candidate = new CandidateProfile();
           
            if (string.IsNullOrWhiteSpace(txtCandidateID.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text)||
                string.IsNullOrWhiteSpace(txtImageURL.Text)||
                string.IsNullOrWhiteSpace(DatePickerBD.Text)||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(CBJobPostingID.Text))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text;
            candidate.ProfileUrl = txtImageURL.Text;
            candidate.Birthday = DateTime.Parse(DatePickerBD.Text);
            candidate.ProfileShortDescription = txtDescription.Text;
            candidate.PostingId = CBJobPostingID.SelectedValue.ToString();
            var _existCandidate = profileService.GetCandidateProfileByID(candidate.CandidateId);
            if (_existCandidate != null)
            {

                MessageBox.Show("Has already this Profile");
                return;
            }
            
            if (profileService.AddCandidateProfile(candidate))
            {
                MessageBox.Show("Add successfull");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Add unsuccessfull");

            }

        }

        private void loadDataInit()
        {
            this.dtgCandidateProfile.ItemsSource = profileService.GetCandidateProfiles();
            this.CBJobPostingID.ItemsSource = jobPostingService.GetJobPostings();
            this.CBSearchJobposting.ItemsSource = jobPostingService.GetJobPostings();
            this.CBJobPostingID.DisplayMemberPath = "JobPostingTitle";
            this.CBJobPostingID.SelectedValuePath = "PostingId";
            this.CBSearchJobposting.DisplayMemberPath = "JobPostingTitle";
            this.CBSearchJobposting.SelectedValuePath = "PostingId";


            txtCandidateID.Text ="";
            txtFullName.Text = "";
            txtDescription.Text = "";
            txtImageURL.Text = "";
            CBJobPostingID.SelectedValue = "";
            DatePickerBD.Text = "";
            CBSearchJobposting.SelectedValue = "";
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {

            CandidateProfile candidate = new CandidateProfile();
            if (string.IsNullOrWhiteSpace(txtCandidateID.Text) ||
               string.IsNullOrWhiteSpace(txtFullName.Text) ||
               string.IsNullOrWhiteSpace(txtImageURL.Text) ||
               string.IsNullOrWhiteSpace(DatePickerBD.Text) ||
               string.IsNullOrWhiteSpace(txtDescription.Text) ||
               string.IsNullOrWhiteSpace(CBJobPostingID.Text))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text;
            candidate.ProfileUrl = txtImageURL.Text;
            candidate.Birthday = DateTime.Parse(DatePickerBD.Text);
            candidate.ProfileShortDescription = txtDescription.Text;
            candidate.PostingId = CBJobPostingID.SelectedValue.ToString();
            var _existCandidate = profileService.GetCandidateProfileByID(candidate.CandidateId);
            if (_existCandidate == null)
            {

                MessageBox.Show("Not have already this profile");
                return;
            }
            if (profileService.UpdateCandidateProfile(candidate))
            {
                MessageBox.Show("Update Successful");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Update Unsuccessful");
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
           
            
                dtgCandidateProfile.ItemsSource = profileService.GetCandidateProfileByNameJob(txtSeacrhName.Text,CBSearchJobposting.SelectedValue?.ToString());
                      CBSearchJobposting.SelectedValue = "";
            txtSeacrhName.Text = "";
        }


    }
}
