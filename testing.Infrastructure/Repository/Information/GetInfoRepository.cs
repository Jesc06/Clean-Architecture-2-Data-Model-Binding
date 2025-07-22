using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Application.DTO;
using Microsoft.EntityFrameworkCore;
using testing.Application.Interfaces.Information;

namespace testing.Infrastructure.Repository.Information
{
    public class GetInfoRepository : IGetInfo
    {
        private readonly ApplicationDbContext _context;
        public GetInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InformationDTO>> GetAllInformation()
        {
            return await _context.Information.Select(info => new InformationDTO
            {
                name = info.name,
                lastname = info.lastname
            }).ToListAsync();
        }


    }
}
