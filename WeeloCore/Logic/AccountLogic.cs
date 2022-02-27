using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.Repositories;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Logic
{
    //In this class all the processes associated with the accounts are managed.
    public class AccountLogic : ILogic<AccountEntity>
    {

        private readonly IMapper mapper;
        private AccountRepository accountRepository;
        private Tools tools;

        public AccountLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountRepository = new AccountRepository();
            tools = new Tools();
        }

        //Method to obtain an account for login
        public AccountEntity GetAccountLogin(LoginEntity loginEntity)
        {
            if (loginEntity == null) return ErrorLogin(4, MessageType.Error, "LoginEntity");
            if (string.IsNullOrEmpty(loginEntity.Email)) return ErrorLogin(4, MessageType.Error, "Email");
            if (string.IsNullOrEmpty(loginEntity.Password)) return ErrorLogin(4, MessageType.Error, "Passwork");

            var account = GetForEmailAndPassword(loginEntity.Email, loginEntity.Password);

            if (account == null) return ErrorLogin(3, MessageType.Error, "Account");

            return account;
        }

        //Method to delete a account
        public AccountEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get account for id
        public AccountEntity Get(Guid? id)
        {
            AccountEntity accountEntity = new AccountEntity();
            if (id.HasValue) accountEntity = mapper.Map<AccountEntity>(accountRepository.Get(id));
            return accountEntity; 
        }

        //Method to get all system accounts
        public List<AccountEntity> GetAll()
        {
            var accountEntities = new List<AccountEntity>();

            var accounts = accountRepository.GetAll();
            if (accounts.Any())
            {
                accountEntities = accounts.Select(x => mapper.Map<AccountEntity>(x)).ToList();
            }

            return accountEntities;
        }

        //Method to add a account
        public AccountEntity Insert(AccountEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to update a account
        public AccountEntity Update(AccountEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to obtain an account by email and password
        private AccountEntity GetForEmailAndPassword(string email, string password)
        {
            return mapper.Map<AccountEntity>(accountRepository.GetForEmailAndPassword(email, password));
        }

        //Method to handle login errors
        private AccountEntity ErrorLogin(int code, MessageType messageType,string additionalMessage)
        {
            AccountEntity accountEntity = new AccountEntity();
            accountEntity.Code = code;
            accountEntity.Message = String.Format("{0} {1}" , tools.GetMessage(code, messageType) , additionalMessage);
            return accountEntity;
        }

    }
}
