using Candidate_BusinessObjects;
using Candidate_DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public interface IHRAccountService
    {
        public Hraccount GetHraccountByEmail(string email);
        public ArrayList GetHraccounts();
        public bool AddHrAccount(Hraccount hraccount);
        public bool UpdateHrAccount(Hraccount hraccount);
        public bool DeleteHrAccount(string email) ;
        public ArrayList GetHraccountByNameOrRole(string? Name, string? role);
    }
}
