using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using static WeeloCore.Helpers.EnumType;

namespace WeeloCore.Logic
{
    public interface ILogic<TDomainObject>
    {
        public abstract List<TDomainObject> GetAll();
        public abstract TDomainObject Get(Guid? id);
        public abstract BaseResponse<TDomainObject> Insert(TDomainObject @object);
        public abstract BaseResponse<TDomainObject> Delete(Guid? id);
        public abstract BaseResponse<TDomainObject> Update(TDomainObject @object);
        public abstract BaseResponse<TDomainObject> MessageResponse(int code, MessageType messageType, string additionalMessage = "");
    }
}
