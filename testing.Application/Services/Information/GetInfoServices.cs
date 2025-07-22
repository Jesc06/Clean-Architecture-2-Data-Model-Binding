using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Information;

namespace testing.Application.Services.Information
{
    public class GetInfoServices
    {
        private readonly IGetInfo _IgetInfo;
        public GetInfoServices(IGetInfo IgetInfo)
        {
           _IgetInfo = IgetInfo;
        }


        public async Task<List<InformationDTO>> GetAllRecordsFromInfo()
        {
            return await _IgetInfo.GetAllInformation();
        }



    }
}
