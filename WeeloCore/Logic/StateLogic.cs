﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using WeeloInfrastructure.Repositories;

namespace WeeloCore.Logic
{
    public class StateLogic : ILogic<StateEntity>
    {
        private readonly IMapper mapper;
        private StateRepository stateRepository;
        private CountryLogic countryLogic;

        public StateLogic(IMapper mapper)
        {
            this.mapper = mapper;
            countryLogic = new CountryLogic(mapper);
            stateRepository = new StateRepository();
        }

        public StateEntity Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public StateEntity Get(Guid? id)
        {
            var state = new StateEntity();
            if (id.HasValue)
            {
                state = mapper.Map<StateEntity>(stateRepository.Get(id));
                if (state.IdCountry.HasValue) state.Country = countryLogic.Get(state.IdCountry);
            }
            return state;
        }

        public List<StateEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public StateEntity Insert(StateEntity @object)
        {
            throw new NotImplementedException();
        }

        public StateEntity Update(StateEntity @object)
        {
            throw new NotImplementedException();
        }
    }
}
