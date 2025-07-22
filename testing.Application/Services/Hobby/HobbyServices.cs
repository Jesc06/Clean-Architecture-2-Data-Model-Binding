using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;

namespace testing.Application.Services.Hobby
{
    public class HobbyServices
    {
        private readonly IHobby _Ihobby;
        public HobbyServices(IHobby Ihobby)
        {
            _Ihobby = Ihobby;
        }

        public async Task<bool> AddHobbyAsync(HobbyDTO dto)
        {
            if(dto.hobbyname == null || dto.secondhobbyname == null)
            {
                return false;
                throw new NullReferenceException("Hobby names cannot be null");
            }
            else
            {
                await _Ihobby.AddHobby(dto);
                return true;
            }
        }


    }
}
