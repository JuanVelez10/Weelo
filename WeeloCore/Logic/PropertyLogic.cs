using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
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

        public PropertyLogic(IMapper mapper)
        {
            this.mapper = mapper;
            accountLogic = new AccountLogic(mapper);
            zoneLogic = new ZoneLogic(mapper);
            propertyImageLogic = new PropertyImageLogic(mapper);
            propertyTraceLogic = new PropertyTraceLogic(mapper);
            propertyRepository = new PropertyRepository();
        }

        //Method to search for properties by city, area, price, year, room number among other filters
        public List<PropertyEntity> Find(FindPropertyEntity find)
        {
            var properties = new List<PropertyEntity>();

            if (find.IdCity.HasValue)
            {
                properties = GetAllForCity(find.IdCity);
                properties = Filter(properties, find);
                properties = Arrive(properties);
            }

            return properties;
        }

        //Method to get a specific property,with detailed information
        public PropertyInfoEntity GetInfo(Guid? id)
        {
            var property = new PropertyInfoEntity();

            if (id.HasValue)
            {
                property = mapper.Map<PropertyInfoEntity>(propertyRepository.Get(id));

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
        public List<PropertyEntity> GetAllForCity(Guid? idCity)
        {
            var propertyEntities = new List<PropertyEntity>();

            if (idCity.HasValue)
            {
                var zones = zoneLogic.GetAllForCity(idCity);
                if (zones.Any())
                {
                    var idZones = zones.Select(x => x.Id).ToList();
                    var properties = propertyRepository.GetAllForZones(idZones);
                    if (properties.Any()) propertyEntities = properties.Select(x => mapper.Map<PropertyEntity>(x)).ToList();
                }

            }

            return propertyEntities;
        }

        //Method to get a specific property,with basic information
        public PropertyEntity Get(Guid? id)
        {
            var propertyEntity = new PropertyEntity();
            if (id.HasValue)  propertyEntity = mapper.Map<PropertyEntity>(propertyRepository.Get(id));
            return propertyEntity;
        }

        //Method to get all system properties
        public List<PropertyEntity> GetAll()
        {
            var propertyEntities = new List<PropertyEntity>();

            var properties = propertyRepository.GetAll();
            if (properties.Any()) {
                propertyEntities = properties.Select(x => mapper.Map<PropertyEntity>(x)).ToList();
                if(propertyEntities.Any()) propertyEntities = Arrive(propertyEntities);
            }
                
            return propertyEntities;
        }

        //Method to add a new property
        public PropertyEntity Insert(PropertyEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to delete a property
        public PropertyEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to update a property
        public PropertyEntity Update(PropertyEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to filter properties
        private List<PropertyEntity> Filter(List<PropertyEntity> properties, FindPropertyEntity find)
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

                if (find.OrderProperty == OrderProperty.PriceMin) properties = properties.OrderBy(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.PriceMax) properties = properties.OrderByDescending(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.YearMax) properties = properties.OrderByDescending(x => x.Year).ToList();

            }
            return properties;
        }

        //Load rest of information to properties
        private List<PropertyEntity> Arrive(List<PropertyEntity> properties)
        {
            if (properties.Any())
            {
                properties.ForEach(x => x.ImageUrl = propertyImageLogic.GetFirstForProperty(x.Id).Url);
                properties.ForEach(x => x.Type = x.PropertyType.ToString());
                properties.ForEach(x => x.Condition = x.ConditionType.ToString());
                properties.ForEach(x => x.Security = x.SecurityType.ToString());
                properties.ForEach(x => x.Area = x.AreaType.ToString());
            }
            return properties;
        }

        //Load rest of information to a property
        private PropertyInfoEntity Arrive(PropertyInfoEntity property)
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

    }
}
