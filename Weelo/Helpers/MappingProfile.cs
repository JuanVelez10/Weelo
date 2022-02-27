using AutoMapper;
using WeeloAPI.References;
using WeeloCore.Entities;
using WeeloInfrastructure.DataBase;

namespace WeeloAPI.Helpers
{
    // This class is where entities are mapped or converted to other entities.
    public class MappingProfile : Profile
    {
        //Method to convert one entity to another
        public MappingProfile()
        {
            CreateMap<FindPropertyRequest, FindPropertyEntity>();
            CreateMap<LoginRequest, LoginEntity>();

            CreateMap<AccountEntity, LoginResponse>();

            CreateMap<Property, PropertyEntity>();
            CreateMap<Property, PropertyInfoEntity>();
            CreateMap<Account, AccountEntity>();
            CreateMap<Zone, ZoneEntity>();
            CreateMap<City, CityEntity>();
            CreateMap<State, StateEntity>();
            CreateMap<Country, CountryEntity>();
            CreateMap<PropertyImage, PropertyImageEntity>();
            CreateMap<PropertyTrace, PropertyTraceEntity>();

            CreateMap<ZoneEntity, ZoneInfoEntity>();

        }

    }
}
