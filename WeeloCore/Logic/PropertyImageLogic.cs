using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private PropertyRepository propertyRepository;
        private Tools tools;

        public PropertyImageLogic(IMapper mapper)
        {
            this.mapper = mapper;
            propertyImageRepository = new PropertyImageRepository();
            propertyRepository = new PropertyRepository();
            tools = new Tools();
        }

        //Method to delete image of property
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

        //Method to get all images
        public List<PropertyImageEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        //Method to create new image of property
        public BaseResponse<PropertyImageEntity> Insert(PropertyImageEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to create new image of property
        public BaseResponse<PropertyImageEntity> New(IConfiguration config,HttpRequest request)
        {
            BaseResponse<PropertyImageEntity> response = new BaseResponse<PropertyImageEntity>();

            response = ValidateRequest(request);
            if (response.Code > 0) return response;

            var idProperty = request.Form.Where(x => x.Key == "id").FirstOrDefault().Value;
            var file = request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).FirstOrDefault();

            response = ValidateProperty(idProperty);
            if (response.Code > 0) return response;

            response = ValidateImage(file.FileName);
            if (response.Code > 0) return response;

            var urlImage = tools.UpLoadImage(file.OpenReadStream(), file.FileName, config).Result;
            if (string.IsNullOrEmpty(urlImage)) return MessageResponse(3, MessageType.Error, "Image");

            var propertyImage = propertyImageRepository.Insert(mapper.Map<PropertyImage>(new PropertyImageEntity(urlImage,Guid.Parse(idProperty))));

            if (propertyImage == null) return MessageResponse(6, MessageType.Error);

            response = MessageResponse(1, MessageType.Success, "Image");
            response.Data = mapper.Map<PropertyImageEntity>(propertyImage);

            return response;
        }

        //Method to update image of property
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

        //Method to Validate Request image
        private BaseResponse<PropertyImageEntity> ValidateRequest(HttpRequest request)
        {
            if (request.Form == null) return MessageResponse(4, MessageType.Error, "Data");
            if (!request.Form.Keys.Any()) return MessageResponse(4, MessageType.Error, "Id");
            if (!request.Form.Where(x => x.Key == "id").Any()) return MessageResponse(4, MessageType.Error, "Id");
            if (!request.Form.Files.Any()) return MessageResponse(4, MessageType.Error, "Image");
            if (!request.Form.Files.Where(x => x.Name == "image" && x.Length > 0).Any()) return MessageResponse(4, MessageType.Error, "Image");

            return new BaseResponse<PropertyImageEntity>();

        }

        //Method to Validate property string
        private BaseResponse<PropertyImageEntity> ValidateProperty(string id)
        {
            if (string.IsNullOrEmpty(id)) return MessageResponse(4, MessageType.Error, "Id");
            Guid guidProperty;
            if (!Guid.TryParse(id, out guidProperty)) return MessageResponse(5, MessageType.Error, "Id");
            var property = propertyRepository.Get(Guid.Parse(id));
            if (property == null) return MessageResponse(3, MessageType.Error, "Property");

            return new BaseResponse<PropertyImageEntity>();
        }

        //Method to Validate image string
        public BaseResponse<PropertyImageEntity> ValidateImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return MessageResponse(3, MessageType.Error, "Image");
            if (!tools.IsImage(fileName)) return MessageResponse(3, MessageType.Error, "Image");

            return new BaseResponse<PropertyImageEntity>();
        }


    }

}
