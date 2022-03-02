using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.Repositories;

namespace WeeloCore.Logic
{
    public class PropertyTraceLogic : ILogic<PropertyTraceEntity>
    {
        private readonly IMapper mapper;
        private PropertyTraceRepository propertyTraceRepository;
        private AccountLogic accountLogic;

        public PropertyTraceLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountLogic = new AccountLogic(mapper);
            propertyTraceRepository = new PropertyTraceRepository();
        }

        public BaseResponse<PropertyTraceEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public PropertyTraceEntity Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public List<PropertyTraceEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<PropertyTraceEntity> Insert(PropertyTraceEntity @object)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<PropertyTraceEntity> Update(PropertyTraceEntity @object)
        {
            throw new NotImplementedException();
        }

        public List<PropertyTraceEntity> GetAllForProperty(Guid? idProperty)
        {
            var propertyTraceEntities = new List<PropertyTraceEntity>();
            if (idProperty.HasValue)
            {
                var propertyTraces = propertyTraceRepository.GetAllForIdProperty(idProperty);
                if (propertyTraces.Any()) {
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

        public BaseResponse<PropertyTraceEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }
    }
}
