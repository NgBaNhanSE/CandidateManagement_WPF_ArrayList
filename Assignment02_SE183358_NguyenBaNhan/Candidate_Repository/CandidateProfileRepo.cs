using Candidate_BusinessObjects;
using Candidate_DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
    public class CandidateProfileRepo : ICandidateProfileRepo
    {
        private readonly CandidateProfileDAO _candidateProfileDAO;
        public CandidateProfileRepo()
        {
            _candidateProfileDAO = new CandidateProfileDAO();
        }

        public bool AddCandidateProfile(CandidateProfile candidateProfile)=> _candidateProfileDAO.AddCandidateProfile(candidateProfile);

        public bool DeleteCandidateProfile(string profileID)=> _candidateProfileDAO.DeleteCandidateProfile(profileID);

        public CandidateProfile GetCandidateProfileByID(string id)=> _candidateProfileDAO.GetCandidateProfileByID(id);
        public ArrayList GetCandidateProfiles()=> _candidateProfileDAO.GetCandidateProfiles();

        public ArrayList GetCandidateProfileByNameJob(string? Name, string? jobposting)=> _candidateProfileDAO.GetCandidateProfileByNameJob(Name,jobposting);
        public bool UpdateCandidateProfile(CandidateProfile profileID)=> _candidateProfileDAO.UpdateCandidateProfile(profileID);
    }
}
