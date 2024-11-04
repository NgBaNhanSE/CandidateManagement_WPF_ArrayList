using Candidate_BusinessObjects;
using System;
using System.Collections;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Candidate_DAO
{
    public class JobPostingDAO
    {
        private readonly string _filePath;

        public JobPostingDAO()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), @"DataContext");
        }

        public JobPosting GetJobPostingById(string jobId)
        {
            var postings = this.GetJobPostings();
            foreach (JobPosting posting in postings)
            {
                if (posting.PostingId.Equals(jobId))
                {
                    return posting;
                }
            }
            return null;
        }

        public ArrayList GetJobPostingByTitle(string title)
        {
            ArrayList result = new ArrayList();
            var postings = this.GetJobPostings();

            foreach (JobPosting posting in postings)
            {
                if (posting.JobPostingTitle.Contains(title))
                {
                    result.Add(posting);
                }
            }
            return result;
        }

        public ArrayList GetJobPostings()
        {
            ArrayList result = new ArrayList();
            string filePath = Path.Combine(_filePath, "JobPosting.txt");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Cannot find file: {filePath}");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var fields = line.Split('\t');
                    var jobPosting = new JobPosting
                    {
                        PostingId = fields[0],
                        JobPostingTitle = fields[1],
                        Description = fields[2],
                        PostedDate = DateTime.Parse(fields[3])
                    };
                    result.Add(jobPosting);
                }
            }
            return result;
        }

        public bool AddJobPosting(JobPosting jobPosting)
        {
            try
            {
                var postings =  this.GetJobPostings();
                postings.Add(jobPosting);
                return this.SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateJobPosting(JobPosting jobPosting)
        {
            try
            {
                var postings = this.GetJobPostings();
                bool result = false;

                for (int i = 0; i < postings.Count; i++)
                {
                    var existing = postings[i] as JobPosting;
                    if (existing.PostingId == jobPosting.PostingId)
                    {
                        postings[i] = jobPosting;
                        result = true;
                        break;
                    }
                }

                return result && this.SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteJobPosting(string jobId)
        {
            try
            {
                var postings = this.GetJobPostings();
                bool result = false;

                for (int i = postings.Count - 1; i >= 0; i--)
                {
                    var posting = postings[i] as JobPosting;
                    if (posting.PostingId == jobId)
                    {
                        postings.RemoveAt(i);
                        result = true;
                        break;
                    }
                }

                return result && this.SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }

        private bool SaveJobPostings(ArrayList jobPostings)
        {
            try
            {
                string filePath = Path.Combine(_filePath, "JobPosting.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("PostingID\tJobPostingTitle\tDescription\tPostedDate");
                    foreach (JobPosting posting in jobPostings)
                    {
                        writer.WriteLine($"{posting.PostingId}\t{posting.JobPostingTitle}\t{posting.Description}\t{posting.PostedDate:yyyy-MM-dd HH:mm:ss.fff}");
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}