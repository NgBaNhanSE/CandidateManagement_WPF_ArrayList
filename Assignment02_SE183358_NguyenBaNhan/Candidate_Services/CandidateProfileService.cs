using Candidate_BusinessObjects;
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
    public class CandidateProfileService : ICandidateProfileService
    {   private ICandidateProfileRepo _profileRepo;
        public CandidateProfileService()
        {
            _profileRepo = new CandidateProfileRepo();
        }
        public bool AddCandidateProfile(CandidateProfile candidateProfile)
        {
          return _profileRepo.AddCandidateProfile(candidateProfile);
        }

        public bool DeleteCandidateProfile(string profileID)
        {
          return  _profileRepo.DeleteCandidateProfile(profileID);
        }

        public CandidateProfile GetCandidateProfileByID(string id)
        {
          return _profileRepo.GetCandidateProfileByID(id);
        }

        public ArrayList GetCandidateProfiles()
        {
            return _profileRepo.GetCandidateProfiles();
        }

        public bool UpdateCandidateProfile(CandidateProfile profileID)
        {
           return _profileRepo.UpdateCandidateProfile(profileID);
        }
        public ArrayList GetCandidateProfileByNameJob(string? Name, string? jobposting)
        {
            return _profileRepo.GetCandidateProfileByNameJob(Name,jobposting);
        }
    }
}
