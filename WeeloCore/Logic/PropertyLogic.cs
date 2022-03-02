using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using WeeloInfrastructure.DataBase;
using WeeloInfrastructure.Repositories;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Logic
{
    //In this class, all the processes associated with the buildings or homes are managed.
    public class PropertyLogic : ILogic<PropertyEntity>
    {
        private readonly IMapper mapper;
        private PropertyRepository propertyRepository;
        private AccountLogic accountLogic;
        private ZoneLogic zoneLogic;
        private PropertyImageLogic propertyImageLogic;
        private PropertyTraceLogic propertyTraceLogic;
        private Tools tools;

        public PropertyLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountLogic = new AccountLogic(mapper);
            zoneLogic = new ZoneLogic(mapper);
            propertyImageLogic = new PropertyImageLogic(mapper);
            propertyTraceLogic = new PropertyTraceLogic(mapper);
            propertyRepository = new PropertyRepository();
            tools = new Tools();
        }

        //Method to search for properties by city, area, price, year, room number among other filters
        public List<PropertyBasicEntity> Find(FindPropertyEntity find)
        {
            var properties = new List<PropertyBasicEntity>();

            if (find.IdCity.HasValue)
            {
                properties = GetAllForCity(find.IdCity);
                properties = Arrive(properties);
                properties = Filter(properties, find);
            }

            return properties;
        }

        //Method to get a specific property,with detailed information
        public PropertyEntity Get(Guid? id)
        {
            var property = new PropertyEntity();

            if (id.HasValue)
            {
                property = mapper.Map<PropertyEntity>(propertyRepository.Get(id));

                if (property != null)
                {
                    if (property.IdOwner.HasValue) property.Owner = accountLogic.Get(property.IdOwner);
                    if (property.IdZone.HasValue) property.Zone = zoneLogic.GetInfo(property.IdZone);

                    property.PropertyImages = propertyImageLogic.GetAllForProperty(property.Id);
                    property.PropertyTraces = propertyTraceLogic.GetAllForProperty(property.Id);

                    property = Arrive(property);
                }
            }

            return property;
        }

        //Search properties by a city
        public List<PropertyBasicEntity> GetAllForCity(Guid? idCity)
        {
            var propertyEntities = new List<PropertyBasicEntity>();

            if (idCity.HasValue)
            {
                var zones = zoneLogic.GetAllForCity(idCity);
                if (zones.Any())
                {
                    var idZones = zones.Select(x => x.Id).ToList();
                    var properties = propertyRepository.GetAllForZones(idZones);
                    if (properties.Any()) propertyEntities = properties.Select(x => mapper.Map<PropertyBasicEntity>(x)).ToList();
                }

            }

            return propertyEntities;
        }

        //Method to get all system properties
        public List<PropertyEntity> GetAll()
        {
            var propertyEntities = new List<PropertyEntity>();

            var properties = propertyRepository.GetAll();
            if (properties.Any()) {
                propertyEntities = properties.Select(x => mapper.Map<PropertyEntity>(x)).ToList();
                if(propertyEntities.Any()) propertyEntities.ForEach(x => Arrive(x));
            }
                
            return propertyEntities;
        }

        //Method to add a new property
        public BaseResponse<PropertyEntity> Insert(PropertyEntity propertyInfoEntity)
        {
            BaseResponse<PropertyEntity> response = new BaseResponse<PropertyEntity>();

            response = Validate(propertyInfoEntity, true);
            if (response.Code > 0) return response;

            var property = propertyRepository.Insert(mapper.Map<Property>(propertyInfoEntity));

            if (property == null) return MessageResponse(6, MessageType.Error);

            response = MessageResponse(1, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyEntity>(property);

            return response;
        }

        //Method to delete a property
        public BaseResponse<PropertyEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to update a property
        public BaseResponse<PropertyEntity> Update(PropertyEntity propertyInfoEntity)
        {
            BaseResponse<PropertyEntity> response = new BaseResponse<PropertyEntity>();

            response = Validate(propertyInfoEntity, false);
            if (response.Code > 0) return response;



            return response;
        }

        //Method to return response message
        public BaseResponse<PropertyEntity> MessageResponse(int code, MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyEntity> response = new BaseResponse<PropertyEntity>();
            response.Code = code;
            response.Message = String.Format("{0} {1}", tools.GetMessage(code, messageType), additionalMessage);
            return response;
        }

        //Method to filter properties
        private List<PropertyBasicEntity> Filter(List<PropertyBasicEntity> properties, FindPropertyEntity find)
        {
            if (find != null && properties.Any())
            {
                if (string.IsNullOrEmpty(find.IdZone.ToString())) properties = properties.Where(x => x.IdZone == find.IdZone).ToList();
                if (find.YearMin > 0 && find.YearMax > find.YearMin) properties = properties.Where(x => x.Year >= find.YearMin && x.Year <= find.YearMax).ToList();
                if (find.PriceMin > 0 && find.PriceMax > find.PriceMin) properties = properties.Where(x => x.Price >= find.PriceMin && x.Price <= find.PriceMax).ToList();
                if (find.RoomsMin > 0 && find.RoomsMax > find.RoomsMin) properties = properties.Where(x => x.Rooms >= find.RoomsMin && x.Rooms <= find.RoomsMax).ToList();
                if (find.PropertyType != PropertyType.None) properties = properties.Where(x => x.PropertyType == find.PropertyType).ToList();
                if (find.ConditionType != ConditionType.None) properties = properties.Where(x => x.ConditionType == find.ConditionType).ToList();
                if (find.SecurityType != SecurityType.None) properties = properties.Where(x => x.SecurityType == find.SecurityType).ToList();
                if (find.AreaType != AreaType.None) properties = properties.Where(x => x.AreaType == find.AreaType).ToList();
                if (find.WithFurnished == WithFurnished.Furnished) properties = properties.Where(x => x.Furnished == true).ToList();
                if (find.WithFurnished == WithFurnished.NotFurnished) properties = properties.Where(x => x.Furnished == false).ToList();
                if (find.WithGarages == WithGarages.Garages) properties = properties.Where(x => x.Garages > 0).ToList();
                if (find.WithGarages == WithGarages.NotGarages) properties = properties.Where(x => x.Garages < 0).ToList();
                if (find.WithSwimmingPool == WithSwimmingPool.SwimmingPool) properties = properties.Where(x => x.SwimmingPool == true).ToList();
                if (find.WithSwimmingPool == WithSwimmingPool.NotSwimmingPool) properties = properties.Where(x => x.SwimmingPool == false).ToList();
                if (find.WithGym == WithGym.Gym) properties = properties.Where(x => x.Gym == true).ToList();
                if (find.WithGym == WithGym.NotGym) properties = properties.Where(x => x.Gym == false).ToList();
                if (find.WithOceanfront == WithOceanfront.Oceanfront) properties = properties.Where(x => x.Oceanfront == true).ToList();
                if (find.WithOceanfront == WithOceanfront.NotOceanfront) properties = properties.Where(x => x.Oceanfront == false).ToList();
                if (find.WithImages == WithImages.Images) properties = properties.Where(x => !string.IsNullOrEmpty(x.ImageUrl)).ToList();
                if (find.WithImages == WithImages.NotImages) properties = properties.Where(x => string.IsNullOrEmpty(x.ImageUrl)).ToList();

                if (find.OrderProperty == OrderProperty.PriceMin) properties = properties.OrderBy(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.PriceMax) properties = properties.OrderByDescending(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.YearMax) properties = properties.OrderByDescending(x => x.Year).ToList();

                if (find.EnabledProperty == EnabledProperty.Enabled) properties = properties.Where(x => x.Enabled == true).ToList();
                if (find.EnabledProperty == EnabledProperty.NotEnabled) properties = properties.Where(x => x.Enabled == false).ToList();

            }
            return properties;
        }

        //Load rest of information to properties
        private List<PropertyBasicEntity> Arrive(List<PropertyBasicEntity> properties)
        {
            if (properties.Any())
            {
                properties.ForEach(x => x.ImageUrl = propertyImageLogic.GetFirstForProperty(x.Id));
                properties.ForEach(x => x.Type = x.PropertyType.ToString());
                properties.ForEach(x => x.Condition = x.ConditionType.ToString());
                properties.ForEach(x => x.Security = x.SecurityType.ToString());
                properties.ForEach(x => x.Area = x.AreaType.ToString());
            }
            return properties;
        }

        //Load rest of information to a property
        private PropertyEntity Arrive(PropertyEntity property)
        {
            if (property != null)
            {
                property.Type = property.PropertyType.ToString();
                property.Condition = property.ConditionType.ToString();
                property.Security = property.SecurityType.ToString();
                property.Area = property.AreaType.ToString();
            }
            return property;
        }

        //Method to Validate property
        private BaseResponse<PropertyEntity> Validate(PropertyEntity propertyInfoEntity, bool add)
        {
            if (!propertyInfoEntity.IdZone.HasValue) return MessageResponse(4, MessageType.Error, "Zone");
            var zone = zoneLogic.Get(propertyInfoEntity.IdZone);
            if (zone == null) return MessageResponse(3, MessageType.Error, "Zone");

            if (!propertyInfoEntity.IdOwner.HasValue) return MessageResponse(4, MessageType.Error, "Zone");
            var owner = accountLogic.Get(propertyInfoEntity.IdOwner);
            if (owner == null) return MessageResponse(3, MessageType.Error, "Owner");

            if (add)
            {
                var exitsproperty = BuyProperty(propertyInfoEntity);
                if (exitsproperty) return MessageResponse(7, MessageType.Error, "Property");
            }
            else
            {
                if (!propertyInfoEntity.Id.HasValue) return MessageResponse(4, MessageType.Error, "Property");
                var exitsproperty = Get(propertyInfoEntity.Id);
                if (exitsproperty == null) return MessageResponse(3, MessageType.Error, "Property");
            }

            return new BaseResponse<PropertyEntity>();

        }

        //Method to buy property with existing
        private bool BuyProperty(PropertyEntity property)
        {
            return GetAll().Where(x => x.Address == property.Address && x.IdZone == property.IdZone && x.Name == property.Name && x.IdOwner == property.IdOwner && x.Year == property.Year).Any();
        }


    }
}
