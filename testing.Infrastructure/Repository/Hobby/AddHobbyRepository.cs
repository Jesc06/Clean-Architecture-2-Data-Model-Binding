using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Domain.Entities;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;


namespace testing.Infrastructure.Repository.Hobby
{
    public class AddHobbyRepository : IHobby
    {
        private readonly ApplicationDbContext _context; 
        public AddHobbyRepository(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task<bool> AddHobby(HobbyDTO dto)
        {
            HobbyDomain Domain = new HobbyDomain
            {
                HobbyName = dto.hobbyname,
                SecondHobbyName = dto.secondhobbyname
            };
            await _context.Hobby.AddAsync(Domain);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}
