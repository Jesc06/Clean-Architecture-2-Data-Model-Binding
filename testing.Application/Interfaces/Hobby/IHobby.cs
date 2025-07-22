using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;

namespace testing.Application.Interfaces.Hobby
{
    public interface IHobby
    {
        Task<bool> AddHobby(HobbyDTO dto);
    }
}
