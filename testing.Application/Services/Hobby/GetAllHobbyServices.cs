using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;

namespace testing.Application.Services.Hobby
{
    public class GetAllHobbyServices
    {
        private readonly IHobbyGetAllRecords _IHobbyGetAllRecords;
        public GetAllHobbyServices(IHobbyGetAllRecords IHobbyGetAllRecords)
        {
            _IHobbyGetAllRecords = IHobbyGetAllRecords;
        }

        public async Task<List<HobbyDTO>> AllHobbyRecordsAsync()
        {
           return await _IHobbyGetAllRecords.GetAllHobbyRecords();
        }


    }
}
