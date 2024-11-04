using Candidate_BusinessObjects;
using Candidate_DAO;
using Candidate_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public class JobPostingService : IJobPostingService
    {   private readonly IJobPostingRepo _jobPostingService;
        public JobPostingService()
        {
            _jobPostingService = new JobPostingRepo();   
        }
        public JobPosting GetJobPostingById(string jobId)
        {
           return _jobPostingService.GetJobPostingById(jobId);
        }

        public ArrayList GetJobPostings()
        {
            return _jobPostingService.GetJobPostings();
        }
        public bool AddJobPosting(JobPosting jobPosting)=>_jobPostingService.AddJobPosting(jobPosting);
        public bool UpdateJobPosting(JobPosting jobPosting) => _jobPostingService.UpdateJobPosting(jobPosting);
        public bool DeleteJobPosting(string jobId) => _jobPostingService.DeleteJobPosting(jobId);
        public ArrayList GetJobPostingByTitle(string title)=> _jobPostingService.GetJobPostingByTitle(title);
    }
}
