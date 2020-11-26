using JH_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JH_Service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void updateAlertUsers(ReservAccount r, int accessLevel);
        [OperationContract]
        void updateVacanciItems(Vacanci v, int accessLevel);
        [OperationContract]
        void updateJobSeeker(JobSeeker j, int accessLevel);
        [OperationContract]
        List<ReservAccount> GetReservs();
        [OperationContract]
        List<Account> GetAccount( int accessLevel);
        [OperationContract]
        List<JobSeeker> GetJob( int accessLevel);
        [OperationContract]
        List<Vacanci> GetVacanci( int accessLevel);
        [OperationContract]
        void UpdateVacanci(string id, string v, int accessLevel);
        [OperationContract]
        List<Vacanci> FindVacanci(string nj, int accessLevel);
        [OperationContract]
        void AddVacanci(Vacanci v, int accessLevel);
        [OperationContract]
        void AddJob(JobSeeker j, int accessLevel);
        [OperationContract]
        void AddAccount(string login, string email, int accessLvl, string compami, string hexpassword);
        [OperationContract]
        int Authentication(string login, string password);
        [OperationContract]
        void updateUser(Account a, int accessLevel);
        [OperationContract]
        bool CrashTable();
    }


    
}
