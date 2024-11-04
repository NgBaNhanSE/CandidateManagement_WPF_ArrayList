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
    public class HRAccountRepo : IHRAccountRepo
    {
        private readonly HRAccountDAO _HRAccountDAO;
        public HRAccountRepo()
        {
            _HRAccountDAO = new HRAccountDAO();
        }
        public Hraccount GetHraccountByEmail(string email)=> _HRAccountDAO.getHraccountByEmail(email);
        

        public ArrayList GetHraccounts()=> _HRAccountDAO.GetHraccounts();
        public bool AddHrAccount(Hraccount hraccount) => _HRAccountDAO.AddHrAccount(hraccount);
        public bool UpdateHrAccount(Hraccount hraccount) => _HRAccountDAO.UpdateHrAccount(hraccount);
        public bool DeleteHrAccount(string email) => _HRAccountDAO.DeleteHrAccount(email);
        public ArrayList GetHraccountByNameOrRole(string? Name, string? role) => _HRAccountDAO.GetHraccountByNameOrRole(Name,role);
    }
}
