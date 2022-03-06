using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.Repositories;

namespace WeeloCore.Logic
{
    //In this class all the processes associated with the country are managed.
    public class CountryLogic : ILogic<CountryEntity>
    {
        private readonly IMapper mapper;
        private CountryRepository countryRepository;

        //Controller
        public CountryLogic(IMapper mapper)
        {
            this.mapper = mapper;
            countryRepository = new CountryRepository();
        }

        //Method to delete a country
        public BaseResponse<CountryEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get a country
        public CountryEntity Get(Guid? id)
        {
            var countryEntity = new CountryEntity();
            if (id.HasValue) countryEntity = mapper.Map<CountryEntity>(countryRepository.Get(id));
            return countryEntity;
        }

        //Method to get all countries
        public List<CountryEntity> GetAll()
        {
            var countriesEntity = new List<CountryEntity>();
            var countries = countryRepository.GetAll();
            if (countries.Any()) countriesEntity = countries.Select(x => mapper.Map<CountryEntity>(x)).ToList();
            return countriesEntity;
        }

        //Method to add a country
        public BaseResponse<CountryEntity> Insert(CountryEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to return response message
        public BaseResponse<CountryEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }

        //Method to update a country
        public BaseResponse<CountryEntity> Update(CountryEntity @object)
        {
            throw new NotImplementedException();
        }
    }

}
