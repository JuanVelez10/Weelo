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

        //Controller
        public AccountLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountRepository = new AccountRepository();
            tools = new Tools();
        }

        //Method to obtain an account for login
        public BaseResponse<AccountEntity> GetAccountLogin(LoginEntity loginEntity)
        {
            BaseResponse<AccountEntity> response = new BaseResponse<AccountEntity>(); 

            if (loginEntity == null) return MessageResponse(4, MessageType.Error, "LoginEntity");
            if (string.IsNullOrEmpty(loginEntity.Email)) return MessageResponse(4, MessageType.Error, "Email");
            if (string.IsNullOrEmpty(loginEntity.Password)) return MessageResponse(4, MessageType.Error, "Passwork");

            var account = GetForEmailAndPassword(loginEntity.Email, loginEntity.Password);

            if (account == null) return MessageResponse(3, MessageType.Error, "Account");
            response = MessageResponse(1, MessageType.Success, "Account");
            response.Data = account;

            return response;
        }

        //Method to delete a account
        public BaseResponse<AccountEntity> Delete(Guid? id)
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
        public BaseResponse<AccountEntity> Insert(AccountEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to update a account
        public BaseResponse<AccountEntity> Update(AccountEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to obtain an account by email and password
        private AccountEntity GetForEmailAndPassword(string email, string password)
        {
            return mapper.Map<AccountEntity>(accountRepository.GetForEmailAndPassword(email, password));
        }

        //Method to return response message
        public BaseResponse<AccountEntity> MessageResponse(int code, MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<AccountEntity> response = new BaseResponse<AccountEntity>();
            response.Code = code;
            response.Message = String.Format("{0} {1}", tools.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

    }
}
