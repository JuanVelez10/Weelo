using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloInfrastructure.Repositories;

namespace WeeloCore.Logic
{
    public class CityLogic : ILogic<CityEntity>
    {
        private readonly IMapper mapper;
        private CityRepository cityRepository;
        private StateLogic stateLogic;

        public CityLogic(IMapper mapper)
        {
            this.mapper = mapper;
            stateLogic = new StateLogic(mapper);
            cityRepository = new CityRepository();
        }

        public CityEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public CityEntity Get(Guid? id)
        {
            var city = new CityEntity();
            if (id.HasValue)
            {
                city = mapper.Map<CityEntity>(cityRepository.Get(id));
                if (city.IdState.HasValue) city.State = stateLogic.Get(city.IdState);
            }
            return city;
        }

        public List<CityEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public CityEntity Insert(CityEntity @object)
        {
            throw new NotImplementedException();
        }

        public CityEntity Update(CityEntity @object)
        {
            throw new NotImplementedException();
        }
    }
}
