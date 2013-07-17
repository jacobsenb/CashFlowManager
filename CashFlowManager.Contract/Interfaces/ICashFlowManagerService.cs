using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowManager.Contract.Entities;
using CashFlowManager.Domain;
using UserProfile = CashFlowManager.Contract.Entities.UserProfile;

namespace CashFlowManager.Contract
{
    public interface ICashFlowManagerService
    {
        void AddTransactions(List<TransactionInfo> transactions);

        List<TransactionInfo> GetTransactions(Guid? clientId, Guid? transactionId = null);

        List<TransactionInfo> GetTransactions();

        List<ScheduleInfo> GetScheduleTypes();

        void AddPractice(PracticeInfo practice);

        void AddClient(List<ClientInfo> clients);

        List<PracticeInfo> GetPractices(Guid? userId = null);

        List<ClientInfo> GetClients(Guid practiceId);

        List<TransactionTypeInfo> GetTransactionTypes();

        void DeleteTransaction(Guid transactionId);

        void UpdateTransaction(TransactionInfo transaction);

        void AddBankAccount(BankAccountInfo bankAccount);

        void UpdateBankAccount(BankAccountInfo bankAccount);

        void DeleteBankAccount(BankAccountInfo bankAccount);

        List<BankAccountInfo> GetBankAccounts();

        List<BankAccountInfo> GetBankAccounts(Guid clientId);

        BankAccountInfo GetBankAccount(Guid bankAccountId);

        void AssociateAndUpdateUser(int userId,Guid? practiceId, string email);

        UserProfile GetUser(int userId);
        
        UserProfile GetUser(string userName);

    }
}
