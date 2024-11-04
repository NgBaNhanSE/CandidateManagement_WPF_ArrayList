using Candidate_BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
    public interface IHRAccountRepo
    {
        public Hraccount GetHraccountByEmail(string email);
        public ArrayList GetHraccounts();
        public bool AddHrAccount(Hraccount hraccount);
        public bool UpdateHrAccount(Hraccount hraccount);
        public bool DeleteHrAccount(string email);
        public ArrayList GetHraccountByNameOrRole(string? Name, string? role);
    }
}
