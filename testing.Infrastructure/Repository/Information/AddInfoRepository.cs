using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Application.DTO;
using testing.Domain.Entities;
using testing.Application.Interfaces.Information;
namespace testing.Infrastructure.Repository.Information
{
    public class AddInfoRepository : IAddInfo
    {
        private readonly ApplicationDbContext _context;
        public AddInfoRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> AddAsync(InformationDTO dto)
        {
            InformationDomain infoDomain = new InformationDomain
            {
                name = dto.name,
                lastname = dto.lastname
            };

           await _context.Information.AddAsync(infoDomain);
           await _context.SaveChangesAsync();

           return  true;
        
        }


    }
}
