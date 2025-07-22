using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Information;

namespace testing.Application.Services.Information
{
    public class AddInfoAsyncServices
    {
        private readonly IAddInfo _IAddInfo;
        public AddInfoAsyncServices(IAddInfo IAddInfo)
        {
            _IAddInfo = IAddInfo;
        }

        public async Task<bool> AddAsync(InformationDTO dto)
        { 
            if(dto.name == null || dto.lastname == null)
            {
                return false;
                throw new NullReferenceException("InformationDTO cannot be null");
            }
            else
            {
                var success = await _IAddInfo.AddAsync(dto);
                return success;
            }
                
        }


}
}
