using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using CashFlowManager.Contract;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Domain;
using UserProfile = CashFlowManager.Contract.Entities.UserProfile;

namespace CashFlowManager.Service
{
    public class CashFlowManagerService : ICashFlowManagerService
    {

        public void AddTransactions(List<TransactionInfo> transactions)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                foreach (TransactionInfo t in transactions)
                {
                    if (t.Id == Guid.Empty)
                        t.Id = Guid.NewGuid();
                    db.Transactions.Add(t.ToEntity<Transaction>());
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public List<TransactionInfo> GetTransactions(Guid? clientId, Guid? transactionId = null)
        {
            List<TransactionInfo> transToReturn = new List<TransactionInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();
                List<Transaction> trans = new List<Transaction>();
                if (transactionId == null)
                {
                    trans = (from t in db.Transactions where t.ClientId == clientId select t).ToList();
                }
                else
                    trans = (from t in db.Transactions where t.Id == transactionId.Value && t.ClientId == clientId select t).ToList();


                foreach (Transaction t in trans)
                {
                    transToReturn.Add(TransactionInfo.FromEntity(t));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return transToReturn;
        }

        public List<TransactionInfo> GetTransactions()
        {
            List<TransactionInfo> transToReturn = new List<TransactionInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();
                List<Transaction> trans = new List<Transaction>();
                trans = (from t in db.Transactions select t).ToList();

                foreach (Transaction t in trans)
                {
                    transToReturn.Add(TransactionInfo.FromEntity(t));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return transToReturn;
        }

        public List<ScheduleInfo> GetScheduleTypes()
        {
            List<ScheduleInfo> scheduleTypes = new List<ScheduleInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var schTypes = (from s in db.Schedules orderby s.Sequence select s).ToList();

                foreach (Schedule s in schTypes)
                {
                    scheduleTypes.Add(ScheduleInfo.FromEntity(s));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return scheduleTypes;

        }

        public void AddPractice(PracticeInfo practice)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                if (practice.Id == Guid.Empty)
                    practice.Id = Guid.NewGuid();
                db.Practices.Add(practice.ToEntity<Practice>());
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void AddClient(List<ClientInfo> clients)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                foreach (ClientInfo c in clients)
                {
                    if (c.Id == Guid.Empty)
                        c.Id = Guid.NewGuid();
                    db.Clients.Add(c.ToEntity<Client>());
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public List<PracticeInfo> GetPractices(Guid? userId = null)
        {
            List<PracticeInfo> practicesToReturn = new List<PracticeInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var pracs = (from t in db.Practices select t).ToList();

                foreach (Practice t in pracs)
                {
                    practicesToReturn.Add(PracticeInfo.FromEntity(t));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return practicesToReturn;
        }

        public List<ClientInfo> GetClients(Guid practiceId)
        {
            List<ClientInfo> clientsToReturn = new List<ClientInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var clients = (from c in db.Clients where c.PracticeId == practiceId select c).ToList();

                foreach (Client c in clients)
                {
                    clientsToReturn.Add(ClientInfo.FromEntity(c));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return clientsToReturn;
        }

        public List<TransactionTypeInfo> GetTransactionTypes()
        {
            List<TransactionTypeInfo> transTypeReturn = new List<TransactionTypeInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var transTypes = (from t in db.TransactionTypes orderby t.Sequence select t).ToList();

                foreach (TransactionType t in transTypes)
                {
                    transTypeReturn.Add(TransactionTypeInfo.FromEntity(t));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return transTypeReturn;
        }

        public void DeleteTransaction(Guid transactionId)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var tran = (from t in db.Transactions where t.Id == transactionId select t).SingleOrDefault();

                if (tran != null)
                {
                    db.Transactions.Remove(tran);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void UpdateTransaction(TransactionInfo transaction)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var tran = (from t in db.Transactions where t.Id == transaction.Id select t).SingleOrDefault();

                if (tran != null)
                {
                    tran.Narration = transaction.Narration;
                    tran.ScheduleId = transaction.ScheduleId;
                    tran.Amount = transaction.Amount;
                    tran.StartDate = transaction.StartDate;
                    tran.TransactionTypeId = transaction.TransactionTypeId;

                    db.Entry(tran).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void AddBankAccount(BankAccountInfo bankAccount)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                if (bankAccount.Id == Guid.Empty)
                    bankAccount.Id = Guid.NewGuid();
                db.BankAccounts.Add(bankAccount.ToEntity<BankAccount>());
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void UpdateBankAccount(BankAccountInfo bankAccount)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();
                BankAccount ba = (from b in db.BankAccounts where b.Id == bankAccount.Id select b).SingleOrDefault();
                if (ba != null)
                {
                    ba.AccountName = bankAccount.AccountName;
                    ba.AccountNumber = bankAccount.AccountNumber;
                    ba.Balance = bankAccount.Balance;
                    ba.ClientId = bankAccount.ClientId;
                }
                db.Entry(ba).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteBankAccount(BankAccountInfo bankAccount)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();
                BankAccount ba = (from b in db.BankAccounts where b.Id == bankAccount.Id select b).SingleOrDefault();
                if (ba != null)
                {
                    db.BankAccounts.Remove(ba);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public List<BankAccountInfo> GetBankAccounts(Guid clientId)
        {
            List<BankAccountInfo> bankAccounts = new List<BankAccountInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                if (clientId != Guid.Empty)
                {
                    var bas = (from b in db.BankAccounts where b.ClientId == clientId select b).ToList();

                    foreach (BankAccount b in bas)
                    {
                        bankAccounts.Add(BankAccountInfo.FromEntity(b));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return bankAccounts;
        }

        public List<BankAccountInfo> GetBankAccounts()
        {
            List<BankAccountInfo> bankAccounts = new List<BankAccountInfo>();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var bas = (from b in db.BankAccounts select b).ToList();

                foreach (BankAccount b in bas)
                {
                    bankAccounts.Add(BankAccountInfo.FromEntity(b));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return bankAccounts;
        }

        public BankAccountInfo GetBankAccount(Guid bankAccountId)
        {
            BankAccountInfo bankAccount = new BankAccountInfo();

            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                if (bankAccountId != Guid.Empty)
                {
                    var ba = (from b in db.BankAccounts where b.Id == bankAccountId select b).SingleOrDefault();

                    if (ba != null)
                    {
                        bankAccount = BankAccountInfo.FromEntity(ba);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return bankAccount;
        }

        public void AssociateAndUpdateUser(int userId, Guid? practiceId, string email)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                if (practiceId != Guid.Empty)
                {
                    var user = (from u in db.UserProfiles where u.UserId == userId select u).SingleOrDefault();

                    if (user != null)
                    {
                        user.PracticeId = practiceId;
                        user.Email = email;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
}

        public UserProfile GetUser(int userId)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var u = (from us in db.UserProfiles where us.UserId == userId select us).SingleOrDefault();
                UserProfile user = UserProfile.FromEntity(u);
                return user;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public UserProfile GetUser(string userName)
        {
            try
            {
                CashFlowManagerEntities db = new CashFlowManagerEntities();

                var u = (from us in db.UserProfiles where us.UserName == userName select us).SingleOrDefault();
                UserProfile user = UserProfile.FromEntity(u);
                return user;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
