using Candidate_BusinessObjects;
using Candidate_DAO;
using Candidate_Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public class HRAccountService : IHRAccountService
    {
        private IHRAccountRepo iAccountRepo;
        public HRAccountService()
        {
            iAccountRepo = new HRAccountRepo();
        }
        public Hraccount GetHraccountByEmail(string email)
        {
            return iAccountRepo.GetHraccountByEmail(email);
            }

        public ArrayList GetHraccounts()
        {
            return iAccountRepo.GetHraccounts();    
        }
        public bool AddHrAccount(Hraccount hraccount) => iAccountRepo.AddHrAccount(hraccount);
        public bool UpdateHrAccount(Hraccount hraccount) => iAccountRepo.UpdateHrAccount(hraccount);
        public bool DeleteHrAccount(string email) => iAccountRepo.DeleteHrAccount(email);
        public ArrayList GetHraccountByNameOrRole(string? Name, string? role) => iAccountRepo.GetHraccountByNameOrRole(Name, role);

    }
}
