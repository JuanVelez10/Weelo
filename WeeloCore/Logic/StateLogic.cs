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
    //In this class all the processes associated with the state are managed.
    public class StateLogic : ILogic<StateEntity>
    {
        private readonly IMapper mapper;
        private StateRepository stateRepository;
        private CountryLogic countryLogic;

        //Controller
        public StateLogic(IMapper mapper)
        {
            this.mapper = mapper;
            countryLogic = new CountryLogic(mapper);
            stateRepository = new StateRepository();
        }

        //Method to delete a state
        public BaseResponse<StateEntity> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get a state
        public StateEntity Get(Guid? id)
        {
            var state = new StateEntity();
            if (id.HasValue)
            {
                state = mapper.Map<StateEntity>(stateRepository.Get(id));
                if (state != null && state.IdCountry.HasValue) state.Country = countryLogic.Get(state.IdCountry);
            }
            return state;
        }

        //Method to get all states
        public List<StateEntity> GetAll()
        {
            var statesEntity = new List<StateEntity>();
            var states = stateRepository.GetAll();
            if (states.Any()) statesEntity = states.Select(x => mapper.Map<StateEntity>(x)).ToList();
            return statesEntity;
        }

        //Method to add a state
        public BaseResponse<StateEntity> Insert(StateEntity @object)
        {
            throw new NotImplementedException();
        }

        //Method to return response message
        public BaseResponse<StateEntity> MessageResponse(int code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            throw new NotImplementedException();
        }

        //Method to update a state
        public BaseResponse<StateEntity> Update(StateEntity @object)
        {
            throw new NotImplementedException();
        }
    }
}
