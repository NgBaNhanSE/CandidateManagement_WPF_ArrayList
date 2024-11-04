using Candidate_BusinessObjects;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class HRAccountDAO
    {


        private readonly string _filePath;

        public HRAccountDAO()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "DataContext");
        }
        public Hraccount getHraccountByEmail(string email)
        {
            try
            {

                var list = this.GetHraccounts();
                foreach (Hraccount acc in list)
                {
                    if (acc.Email.Equals(email))
                    {
                        return acc;
                    }
                }
                return null;
            }

            catch
            {
                return null;
            }
        }
        public ArrayList GetHraccounts()
        {
            ArrayList arrayList = new ArrayList();
            string filePath = Path.Combine(_filePath, "HrAccount.txt");
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
                    var account = new Hraccount
                    {
                        Email = fields[0],
                        Password = fields[1],
                        FullName = fields[2],
                        MemberRole = int.Parse(fields[3])
                    };
                    arrayList.Add(account);
                }
            }
            return arrayList;
        }
        public ArrayList GetHraccountByNameOrRole(string? Name, string? role)
        {

            int? _role = string.IsNullOrEmpty(role) ? (int?)null : int.Parse(role);
            ArrayList result = new ArrayList();
            var HrAccountList = this.GetHraccounts();

            foreach (Hraccount hraccount in HrAccountList)
            {

                if ((string.IsNullOrEmpty(Name) || hraccount.FullName.Contains(Name)) &&
                     (!_role.HasValue || hraccount.MemberRole.Equals(_role.Value)))
                {
                    result.Add(hraccount);
                }
            }
            return result;
        }
        public bool AddHrAccount(Hraccount hraccount)
        {

            try
            {

                var list = this.GetHraccounts();
                list.Add(hraccount);
                return this.SaveCandidate(list);
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateHrAccount(Hraccount hraccount)
        {
            try
            {
                var accounts = this.GetHraccounts();
                bool result = false;

                for (int i = accounts.Count - 1; i >= 0; i--)
                {
                    var existing = accounts[i] as Hraccount;
                    if (existing.Email == hraccount.Email)
                    {
                        accounts[i] = hraccount;
                        result = true;
                        break;
                    }
                }

                if (result)
                    return this.SaveCandidate(accounts);
                return false;

            }
            catch
            {
                return false;
            }
        }
        public bool DeleteHrAccount(string email)
        {
            try
            {
                var accounts = this.GetHraccounts();
                bool result = false;

                for (int i = accounts.Count - 1; i >= 0; i--)
                {
                    var account = accounts[i] as Hraccount;
                    if (account.Email == email)
                    {
                        accounts.RemoveAt(i);
                        result = true;
                        break;
                    }
                }

                if (result)
                    return this.SaveCandidate(accounts);
                return false;

            }
            catch
            {
                return false;
            }
        }
        private bool SaveCandidate(ArrayList hraAccount)
        {
            try
            {
                string filePath = Path.Combine(_filePath, "HrAccount.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("CandidateID\tFullname\tBirthday\tProfileShortDescription\tProfileURL\tPostingID");
                    foreach (Hraccount hraccount in hraAccount)
                    {
                        writer.WriteLine($"{hraccount.Email}\t{hraccount.Password}\t{hraccount.FullName}\t{hraccount.MemberRole}");
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
