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
   
    public partial class JobPostingWindow : Window
    {
        private string? email;
        private Hraccount account;
        private IHRAccountService hracountService;
        private ICandidateProfileService profileService;
        private IJobPostingService jobPostingService;
        public JobPostingWindow()
        {
            InitializeComponent();
            
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
        }
        public JobPostingWindow(string? _email)
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
        private void loadDataInit()
        {
            this.dtgJobPosting.ItemsSource = jobPostingService.GetJobPostings();
           


            txtPosting.Text = "";
            txtJobPostingTitle.Text = "";
            txtJobDes.Text = "";
            dpPostedDate.Text = "";
          
        }

        private void dtgJobPosting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;


            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
            if (row != null)
            {
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;
                JobPosting jobPosting = jobPostingService.GetJobPostingById(id);
                if (jobPosting != null)
                {
                    txtPosting.Text = jobPosting.PostingId;
                    txtJobPostingTitle.Text = jobPosting.JobPostingTitle;
                    txtJobDes.Text = jobPosting.Description;
                    dpPostedDate.SelectedDate = jobPosting.PostedDate;
                }
            }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            JobPosting jobPosting = new JobPosting();

            if (string.IsNullOrWhiteSpace(txtPosting.Text) ||
                string.IsNullOrWhiteSpace(txtJobPostingTitle.Text) ||
                string.IsNullOrWhiteSpace(txtJobDes.Text) ||
                string.IsNullOrWhiteSpace(dpPostedDate.Text))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            jobPosting.PostingId = txtPosting.Text;
            jobPosting.JobPostingTitle = txtJobPostingTitle.Text;
            jobPosting.Description = txtJobDes.Text;
            jobPosting.PostedDate = DateTime.Parse(dpPostedDate.Text);
            
            var _existJobposting = jobPostingService.GetJobPostingById(jobPosting.PostingId);
                if (_existJobposting != null)
            {

                MessageBox.Show("Has already this Profile");
                return;
            }

            if (jobPostingService.AddJobPosting(jobPosting))
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

            JobPosting jobPosting = new JobPosting();

            if (string.IsNullOrWhiteSpace(txtPosting.Text) ||
                string.IsNullOrWhiteSpace(txtJobPostingTitle.Text) ||
                string.IsNullOrWhiteSpace(txtJobDes.Text) ||
                string.IsNullOrWhiteSpace(dpPostedDate.Text))
            {
                MessageBox.Show("All fields are required");
                return;
            }
            jobPosting.PostingId = txtPosting.Text;
            jobPosting.JobPostingTitle = txtJobPostingTitle.Text;
            jobPosting.Description = txtJobDes.Text;
            jobPosting.PostedDate = DateTime.Parse(dpPostedDate.Text);
            var _existJobposting = jobPostingService.GetJobPostingById(jobPosting.PostingId);
            if (_existJobposting == null)
            {

                MessageBox.Show("Not have already this profile");
                return;
            }
            if (jobPostingService.UpdateJobPosting(jobPosting))
            {
                MessageBox.Show("Update Successful");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Update Unsuccessful");
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            string jobId = txtPosting.Text;


            if (jobId != null &&
      MessageBox.Show("Do you really want to delete this row data? (It will delete all candidate profile relate to this jobposting !!)",
                      "Delete Data ",
                      MessageBoxButton.YesNo,
                      MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (jobId.Length > 0 && jobPostingService.DeleteJobPosting(jobId))
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
            dtgJobPosting.ItemsSource = jobPostingService.GetJobPostingByTitle(txtSeacrhName.Text);
        }

        
    }
}
