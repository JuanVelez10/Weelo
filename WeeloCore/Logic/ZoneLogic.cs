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
    public class ZoneLogic : ILogic<ZoneEntity>
    {
        private readonly IMapper mapper;
        private ZoneRepository zoneRepository;
        private CityLogic cityLogic;

        public ZoneLogic(IMapper mapper)
        {
            this.mapper = mapper;
            cityLogic = new CityLogic(mapper);
            zoneRepository = new ZoneRepository();
        }

        public List<ZoneEntity> GetAllForCity(Guid? idCity)
        {
            var zonesEntities = new List<ZoneEntity>();
            if (idCity.HasValue)
            {
                var zones = zoneRepository.GetAllForCity(idCity);
                if (zones.Any()) zonesEntities = zones.Select(x=> mapper.Map<ZoneEntity>(x)).ToList();
            }
            return zonesEntities;
        }

        public ZoneInfoEntity GetInfo(Guid? id)
        {
            var zoneInfoEntity = new ZoneInfoEntity();

            if (id.HasValue)
            {
                var zone = Get(id);
                if(zone != null)
                {
                    zoneInfoEntity = mapper.Map<ZoneInfoEntity>(zone);
                    if (zone.City != null)
                    {
                        zoneInfoEntity.NameCity = zone.City.Name;
                        zoneInfoEntity.AbbreviativeCity = zone.City.Abbreviative;
                        zoneInfoEntity.IdState = zone.City.IdState;

                        if (zone.City.State != null)
                        {
                            zoneInfoEntity.NameState = zone.City.State.Name;
                            zoneInfoEntity.AbbreviativeState = zone.City.State.Abbreviative;
                            zoneInfoEntity.IdCountry = zone.City.State.IdCountry;

                            if (zone.City.State.Country != null)
                            {
                                zoneInfoEntity.NameCountry = zone.City.State.Country.Name;
                                zoneInfoEntity.AbbreviativeCountry = zone.City.State.Country.Abbreviative;
                            }
                        }
                    }
                }
            }

            return zoneInfoEntity;
        }

        public ZoneEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public ZoneEntity Get(Guid? id)
        {
            var zone = new ZoneEntity();
            if (id.HasValue)
            {
                zone = mapper.Map<ZoneEntity>(zoneRepository.Get(id));
                if (zone.IdCity.HasValue) zone.City = cityLogic.Get(zone.IdCity);
            }
            return zone;
        }

        public List<ZoneEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public ZoneEntity Insert(ZoneEntity @object)
        {
            throw new NotImplementedException();
        }

        public ZoneEntity Update(ZoneEntity @object)
        {
            throw new NotImplementedException();
        }
    }
}
