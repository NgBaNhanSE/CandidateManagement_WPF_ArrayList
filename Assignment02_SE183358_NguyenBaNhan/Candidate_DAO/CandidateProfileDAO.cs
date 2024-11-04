using Candidate_BusinessObjects;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class CandidateProfileDAO
    {
        private readonly string _filePath;
        public CandidateProfileDAO()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "DataContext");
        }
       
        public CandidateProfile GetCandidateProfileByID(string id)
        {
            try
            {
                var candidateList = this.GetCandidateProfiles();
                foreach (CandidateProfile profile in candidateList) 
                {
                    if (profile.CandidateId.Equals(id)) 
                    {
                        return profile; 
                    }
                }
                return null; 
            }
            catch
            {
                return null; 
            }
        }
        public ArrayList GetCandidateProfileByNameJob(string? Name, string? jobposting) 
        {
            ArrayList result = new ArrayList(); 
            var candidateProfiles = this.GetCandidateProfiles(); 

            foreach (CandidateProfile profile in candidateProfiles) 
            {
              
                if ((string.IsNullOrEmpty(Name) || profile.Fullname.Contains(Name)) &&
                    (string.IsNullOrEmpty(jobposting) || profile.PostingId.Contains(jobposting)))
                {
                    result.Add(profile);
                }
            }

            return result; 
        }
        public ArrayList GetCandidateProfiles()
        {
            ArrayList candidate = new ArrayList();
            string filePath = Path.Combine(_filePath, "CandidateProfile.txt");
            if (!File.Exists(filePath)) 
            {
                throw new FileNotFoundException($"Cannot find file: {filePath}"); 
            }
            using (StreamReader reader = new StreamReader(filePath)) {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var fields = line.Split('\t');
                    var profile = new CandidateProfile 
                    {
                        CandidateId = fields[0],
                        Fullname = fields[1], 
                        Birthday = DateTime.Parse(fields[2]), 
                        ProfileShortDescription = fields[3] == "NULL" ? null : fields[3],
                        ProfileUrl = fields[4], 
                        PostingId = fields[5] 
                    };
                    candidate.Add(profile);
                }
            }
            return candidate;
        }
        public bool AddCandidateProfile(CandidateProfile candidateProfile)
        {
            try
            {
                
                var candidate = this.GetCandidateProfiles();
                candidate.Add(candidateProfile);
                return this.SaveCandidate(candidate);
            }
            catch
            {
                return false;
            }
          
        }
        public bool DeleteCandidateProfile(string profileID)
        {
           
            try
            {
                var candidateList = this.GetCandidateProfiles();
                bool result = false;
                for (int i = candidateList.Count - 1; i >= 0; i--) 
                {
                    var candidate = candidateList[i] as CandidateProfile; 
                    if (candidate.CandidateId.Equals(profileID)) 
                    {
                        candidateList.RemoveAt(i);
                        result = true; 
                        break;
                    }
                }
                if (result)
                {
                    return this.SaveCandidate(candidateList);
                }
                return result;
            }
            catch 
            {
                return false;
            }
          
        }

        public bool UpdateCandidateProfile(CandidateProfile profile)
        {
           try
            {
                var candidateList = this.GetCandidateProfiles();
                bool result = false;
                for (int i = candidateList.Count - 1; i >= 0; i--)
                {
                    var candidate = candidateList[i] as CandidateProfile;
                    if (candidate.CandidateId.Equals(profile.CandidateId))
                    {
                        candidateList[i] = profile;
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    return this.SaveCandidate(candidateList);
                }
                return result;
            }
            catch 
            {
                return false;
            }
            
        }
        private bool SaveCandidate(ArrayList candidates) 
        {
            try
            {
                string filePath = Path.Combine(_filePath, "CandidateProFile.txt"); 
                using (StreamWriter writer = new StreamWriter(filePath)) 
                {
                    writer.WriteLine("CandidateID\tFullname\tBirthday\tProfileShortDescription\tProfileURL\tPostingID"); 
                    foreach (CandidateProfile candidate in candidates) 
                    {
                        writer.WriteLine($"{candidate.CandidateId}\t{candidate.Fullname}\t{candidate.Birthday:yyyy-MM-dd HH:mm:ss.fff}\t{candidate.ProfileShortDescription ?? "NULL"}\t{candidate.ProfileUrl}\t{candidate.PostingId}");
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

