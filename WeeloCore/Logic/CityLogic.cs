using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.Repositories;

namespace WeeloCore.Logic
{
    //In this class all the processes associated with the cities are managed.
    public class CityLogic : ILogic<CityEntity>
    {
        private readonly IMapper mapper;
        private CityRepository cityRepository;
        private StateLogic stateLogic;

        //Controller
        public CityLogic(IMapper mapper)
        {
            this.mapper = mapper;
            stateLogic = new StateLogic(mapper);
            cityRepository = new CityRepository();
        }

        //Method to delete a city
        public BaseResponse<CityEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get a city
        public CityEntity Get(Guid? id)
        {
            var city = new CityEntity();
            if (id.HasValue)
            {
                city = mapper.Map<CityEntity>(cityRepository.Get(id));
                if (city != null && city.IdState.HasValue) city.State = stateLogic.Get(city.IdState);
            }
            return city;
        }

        //Method to get all system cities
        public List<CityEntity> GetAll()
        {
            var citiesEntity = new List<CityEntity>();
            var cities = cityRepository.GetAll();
            if (cities.Any()) citiesEntity = cities.Select(x => mapper.Map<CityEntity>(x)).ToList();
            return citiesEntity;
        }

        //Method to add a city
        public BaseResponse<CityEntity> Insert(CityEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to return response message
        public BaseResponse<CityEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }

        //Method to update a city
        public BaseResponse<CityEntity> Update(CityEntity @object)
        {
            throw new NotImplementedException();
        }
    }
}
