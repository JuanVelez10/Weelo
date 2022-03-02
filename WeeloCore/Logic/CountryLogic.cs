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
    public class CountryLogic : ILogic<CountryEntity>
    {
        private readonly IMapper mapper;
        private CountryRepository countryRepository;

        public CountryLogic(IMapper mapper)
        {
            this.mapper = mapper;
            countryRepository = new CountryRepository();
        }

        public BaseResponse<CountryEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public CountryEntity Get(Guid? id)
        {
            var countryEntity = new CountryEntity();
            if (id.HasValue) countryEntity = mapper.Map<CountryEntity>(countryRepository.Get(id));
            return countryEntity;
        }

        public List<CountryEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<CountryEntity> Insert(CountryEntity @object)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<CountryEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }

        public BaseResponse<CountryEntity> Update(CountryEntity @object)
        {
            throw new NotImplementedException();
        }
    }

}
