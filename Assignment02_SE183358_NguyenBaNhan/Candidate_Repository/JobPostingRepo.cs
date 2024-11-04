using Candidate_BusinessObjects;
using Candidate_DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
    public class JobPostingRepo : IJobPostingRepo
    {
        private readonly JobPostingDAO _JobPostingDAO;
        public JobPostingRepo()
        {
            _JobPostingDAO = new JobPostingDAO();
        }
        public JobPosting GetJobPostingById(string jobId)=> _JobPostingDAO.GetJobPostingById(jobId);    
       

        public ArrayList GetJobPostings()=> _JobPostingDAO.GetJobPostings();
        public bool AddJobPosting(JobPosting jobPosting)=> _JobPostingDAO.AddJobPosting(jobPosting);
        public bool UpdateJobPosting(JobPosting jobPosting)=> _JobPostingDAO.UpdateJobPosting(jobPosting);
        public bool DeleteJobPosting(string jobId) => _JobPostingDAO.DeleteJobPosting(jobId);
        public ArrayList GetJobPostingByTitle(string title)=> _JobPostingDAO.GetJobPostingByTitle(title);
    }
}
