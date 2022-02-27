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
    public class PropertyImageLogic : ILogic<PropertyImageEntity>
    {
        private readonly IMapper mapper;
        private PropertyImageRepository propertyImageRepository;

        public PropertyImageLogic(IMapper mapper)
        {
            this.mapper = mapper;
            propertyImageRepository = new PropertyImageRepository();
        }

        public PropertyImageEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public PropertyImageEntity Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public List<PropertyImageEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public PropertyImageEntity Insert(PropertyImageEntity @object)
        {
            throw new NotImplementedException();
        }

        public PropertyImageEntity Update(PropertyImageEntity @object)
        {
            throw new NotImplementedException();
        }

        public List<PropertyImageEntity> GetAllForProperty(Guid? idProperty)
        {
            var propertyImageEntities = new List<PropertyImageEntity>();
            if (idProperty.HasValue)
            {
                var propertyImages = propertyImageRepository.GetAllForIdProperty(idProperty);
                if (propertyImages.Any()) propertyImageEntities = propertyImages.Select(x => mapper.Map<PropertyImageEntity>(x)).ToList();
            }
            return propertyImageEntities;
        }

        public PropertyImageEntity GetFirstForProperty(Guid? idProperty)
        {
            var propertyImageEntity = new PropertyImageEntity();
            if (idProperty.HasValue)
            {
                var propertyImage = propertyImageRepository.GetFirstForIdProperty(idProperty);
                if (propertyImage != null) propertyImageEntity = mapper.Map<PropertyImageEntity>(propertyImage);
            }
            return propertyImageEntity;
        }


    }

}
