using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Application.DTO;
using Microsoft.EntityFrameworkCore;
using testing.Application.Interfaces.Hobby;

namespace testing.Infrastructure.Repository.Hobby
{
    public class GellAllHobbyRepository : IHobbyGetAllRecords
    {
        private readonly ApplicationDbContext _context; 
        public GellAllHobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<HobbyDTO>> GetAllHobbyRecords()
        {
            return await _context.Hobby.Select(hobbyDomain => new HobbyDTO
            {
                hobbyname = hobbyDomain.HobbyName,
                secondhobbyname = hobbyDomain.SecondHobbyName
            }).ToListAsync();
        }


    }
}
