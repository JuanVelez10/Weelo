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

        public BaseResponse<ZoneEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public ZoneEntity Get(Guid? id)
        {
            var zone = new ZoneEntity();
            if (id.HasValue)
            {
                zone = mapper.Map<ZoneEntity>(zoneRepository.Get(id));
                if (zone != null &&  zone.IdCity.HasValue) zone.City = cityLogic.Get(zone.IdCity);
            }
            return zone;
        }

        public List<ZoneEntity> GetAll()
        {
            var zonesEntity = new List<ZoneEntity>();
            var zones = zoneRepository.GetAll();
            if (zones.Any()) zonesEntity = zones.Select(x => mapper.Map<ZoneEntity>(x)).ToList();
            return zonesEntity;
        }

        public BaseResponse<ZoneEntity> Insert(ZoneEntity @object)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<ZoneEntity> Update(ZoneEntity @object)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<ZoneEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }
    }
}
