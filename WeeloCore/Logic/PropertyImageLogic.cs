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
    public class PropertyImageLogic : ILogic<PropertyImageEntity>
    {
        private readonly IMapper mapper;
        private PropertyImageRepository propertyImageRepository;
        private Tools tools;

        public PropertyImageLogic(IMapper mapper)
        {
            this.mapper = mapper;
            propertyImageRepository = new PropertyImageRepository();
            tools = new Tools();
        }

        public BaseResponse<PropertyImageEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get a specific image of property
        public PropertyImageEntity Get(Guid? id)
        {
            var property = new PropertyImageEntity();

            if (id.HasValue)
            {
                property = mapper.Map<PropertyImageEntity>(propertyImageRepository.Get(id));
            }
            return property;
        }

        public List<PropertyImageEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<PropertyImageEntity> Insert(PropertyImageEntity @object)
        {
            BaseResponse<PropertyImageEntity> response = new BaseResponse<PropertyImageEntity>();

           // response = Validate(@object, true);
            if (response.Code > 0) return response;

            var property = propertyImageRepository.Insert(mapper.Map<PropertyImage>(@object));

            if (property == null) return MessageResponse(6, MessageType.Error);

            response = MessageResponse(1, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyImageEntity>(property);

            return response;
        }

        public BaseResponse<PropertyImageEntity> Update(PropertyImageEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to obtain all the images of a property
        public List<PropertyImageBasicEntity> GetAllForProperty(Guid? idProperty)
        {
            var propertyImageEntities = new List<PropertyImageBasicEntity>();
            if (idProperty.HasValue)
            {
                var propertyImages = propertyImageRepository.GetAllForIdProperty(idProperty);
                if (propertyImages.Any()) propertyImageEntities = propertyImages.Select(x => mapper.Map<PropertyImageBasicEntity>(x)).ToList();
            }
            return propertyImageEntities;
        }

        //Method to obtain one the image of a property
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

        //Method to return response message
        public BaseResponse<PropertyImageEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyImageEntity> response = new BaseResponse<PropertyImageEntity>();
            response.Code = code;
            response.Message = String.Format("{0} {1}", tools.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to update enable image of property
        public BaseResponse<PropertyImageEntity> UpdatePropertyImageEnable(Guid? id, bool enable)
        {
            BaseResponse<PropertyImageEntity> response = new BaseResponse<PropertyImageEntity>();

            if (!id.HasValue) return MessageResponse(4, MessageType.Error, "Image");
            var exitspropertyImage = Get(id);
            if (exitspropertyImage == null) return MessageResponse(3, MessageType.Error, "Image");

            var property = propertyImageRepository.UpdateEnable(id, enable);

            if (property == null) return MessageResponse(6, MessageType.Error);

            response = MessageResponse(1, MessageType.Success, "Image");
            response.Data = mapper.Map<PropertyImageEntity>(property);

            return response;
        }


    }

}
