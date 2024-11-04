using Candidate_BusinessObjects;

using Candidate_DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public interface IJobPostingService
    {
        public JobPosting GetJobPostingById(string jobId);

        public ArrayList GetJobPostings();
        public bool AddJobPosting(JobPosting jobPosting);
        public bool UpdateJobPosting(JobPosting jobPosting);
        public bool DeleteJobPosting(string jobId);
        public ArrayList GetJobPostingByTitle(string title);
    }
}
