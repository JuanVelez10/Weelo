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
    public class PropertyImageLogic : ILogic<PropertyImageEntity>
    {
        private readonly IMapper mapper;
        private PropertyImageRepository propertyImageRepository;

        public PropertyImageLogic(IMapper mapper)
        {
            this.mapper = mapper;
            propertyImageRepository = new PropertyImageRepository();
        }

        public BaseResponse<PropertyImageEntity> Delete(Guid? id)
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

        public BaseResponse<PropertyImageEntity> Insert(PropertyImageEntity @object)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<PropertyImageEntity> Update(PropertyImageEntity @object)
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

        public string GetFirstForProperty(Guid? idProperty)
        {
            var imagenUrl = string.Empty;
            if (idProperty.HasValue)
            {
                var propertyImage = propertyImageRepository.GetFirstForIdProperty(idProperty);
                if (propertyImage != null) imagenUrl = propertyImage.Url;
            }
            return imagenUrl;
        }

        public BaseResponse<PropertyImageEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }
    }

}
