using JH_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JH_Service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        private JHContext db = new JHContext();
        static public bool alert = false;
        public List<ReservAccount> GetReservs()
        {
            
                var reservAccount = db.ReservAccounts;
                return reservAccount.ToList();
           
            
        }
        public List<Account> GetAccount(int accessLevel)
        {
            if (!alert || accessLevel==0)
            {
                var users = db.Accounts;
                return users.ToList();
            }
            return null;
        }
        public List<JobSeeker> GetJob(int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                var job = db.JobSeekers;
                return job.ToList();

            }
            return null;


        }
        public List<Vacanci> GetVacanci(int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                var vacanci = db.Vacancis;
                return vacanci.ToList();
            }

            return null;
        }
        public void AddJob(JobSeeker j, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.JobSeekers.Add(j);
                db.SaveChanges();
            }

        }
        public void AddVacanci(Vacanci v, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.Vacancis.Add(v);
                db.SaveChanges();
            }

        }
        public async void UpdateVacanci(string id, string v, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                var vacanci = (from x in db.Vacancis
                               where (x.Id.ToString() == id)
                               select x).First();
                vacanci.list_summary = vacanci.list_summary + "," + v;
                await db.SaveChangesAsync();
            }

        }
        public List<Vacanci> FindVacanci(string nj, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                var vacanci = db.Vacancis.Where(x => x.name_job == nj);
                return vacanci.ToList();
            }
            return null;

        }
        public void AddAccount(string login, string email, int accessLvl, string compami, string hexpassword)
        {
            if (!alert)
            {
                db.Accounts.Add(new Account { create_date = DateTime.Now.Date, login = login, email = email, company = compami, access_level = accessLvl, password = hexpassword });
                db.SaveChanges();
            }


        }
        public void updateUser(Account a, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void updateAlertUsers(ReservAccount r, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.Entry(r).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void updateVacanciItems(Vacanci v, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void updateJobSeeker(JobSeeker j, int accessLevel)
        {
            if (!alert || accessLevel == 0)
            {
                db.Entry(j).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public int Authentication(string login, string password)
        {
            password = password.GetHashCode().ToString();
            if (alert)
            {
                var ConfirmUser = db.ReservAccounts.FirstOrDefault(x => x.login == login && x.password == password);
                if (ConfirmUser == null)
                    return -1;
                return ConfirmUser.access_level;

            }
            if (!alert)
            {
                var ConfirmUser = db.Accounts.FirstOrDefault(x => x.login == login && x.password == password);
                if (ConfirmUser == null)
                    return -1;
                return ConfirmUser.access_level;
            }
            return -1;
        }
        public bool CrashTable()
        {
            if (!alert)
            {
                alert = true;
                return alert;
            }
            else if (alert)
            {
                alert = false;
                return alert;
            }
            else
                return alert;
            
        }
    }
}
