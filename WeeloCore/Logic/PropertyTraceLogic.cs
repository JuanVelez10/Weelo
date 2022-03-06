using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.DataBase;
using WeeloInfrastructure.Repositories;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Logic
{
    //In this class all the processes associated with the trace of property are managed.
    public class PropertyTraceLogic : ILogic<PropertyTraceEntity>
    {
        private readonly IMapper mapper;
        private PropertyTraceRepository propertyTraceRepository;
        private PropertyRepository propertyRepository;
        private AccountLogic accountLogic;
        private Tools tools;

        //Controller
        public PropertyTraceLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountLogic = new AccountLogic(mapper);
            propertyTraceRepository = new PropertyTraceRepository();
            propertyRepository = new PropertyRepository();
            tools = new Tools();
        }

        //Method to delete trace of property
        public BaseResponse<PropertyTraceEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get a specific trace of property
        public PropertyTraceEntity Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get all traces of property
        public List<PropertyTraceEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        //Method to add a new  trace of property
        public BaseResponse<PropertyTraceEntity> Insert(PropertyTraceEntity propertyTraceEntity)
        {
            BaseResponse<PropertyTraceEntity> response = new BaseResponse<PropertyTraceEntity>();

            response = Validate(propertyTraceEntity);
            if (response.Code > 0) return response;

            var property = propertyTraceRepository.Insert(mapper.Map<PropertyTrace>(propertyTraceEntity));

            if (property == null) return MessageResponse(6, MessageType.Error);

            response = MessageResponse(1, MessageType.Success, "Trace");
            response.Data = mapper.Map<PropertyTraceEntity>(property);

            return response;

        }

        //Method to update trace of property
        public BaseResponse<PropertyTraceEntity> Update(PropertyTraceEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to obtain all the trace of a property
        public List<PropertyTraceEntity> GetAllForProperty(Guid? idProperty)
        {
            var propertyTraceEntities = new List<PropertyTraceEntity>();
            if (idProperty.HasValue)
            {
                var propertyTraces = propertyTraceRepository.GetAllForIdProperty(idProperty);
                if (propertyTraces.Any())
                {
                    propertyTraceEntities = propertyTraces.Select(x => mapper.Map<PropertyTraceEntity>(x)).ToList();
                    if (propertyTraceEntities.Any())
                    {
                        propertyTraceEntities.ForEach(x => x.NameOwnerNew = accountLogic.Get(x.OwnerNew).Name);
                        propertyTraceEntities.ForEach(x => x.NameOwnerOld = accountLogic.Get(x.OwnerOld).Name);
                    }
                }
            }
            return propertyTraceEntities;
        }

        //Method to return response message
        public BaseResponse<PropertyTraceEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyTraceEntity> response = new BaseResponse<PropertyTraceEntity>();
            response.Code = code;
            response.Message = String.Format("{0} {1}", tools.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to Validate trace of property
        private BaseResponse<PropertyTraceEntity> Validate(PropertyTraceEntity propertyTraceEntity)
        {
            if (!propertyTraceEntity.OwnerNew.HasValue) return MessageResponse(4, MessageType.Error, "OwnerNew");
            var ownerNew = accountLogic.Get(propertyTraceEntity.OwnerNew);
            if (ownerNew == null) return MessageResponse(3, MessageType.Error, "OwnerNew");

            if (!propertyTraceEntity.OwnerOld.HasValue) return MessageResponse(4, MessageType.Error, "OwnerOld");
            var ownerOld = accountLogic.Get(propertyTraceEntity.OwnerOld);
            if (ownerOld == null) return MessageResponse(3, MessageType.Error, "OwnerOld");

            if (!propertyTraceEntity.IdProperty.HasValue) return MessageResponse(4, MessageType.Error, "Property");
            var property = propertyRepository.Get(propertyTraceEntity.IdProperty);
            if (property == null) return MessageResponse(3, MessageType.Error, "Property");

            return new BaseResponse<PropertyTraceEntity>();
        }

    }
}
