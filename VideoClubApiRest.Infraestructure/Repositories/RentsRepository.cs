using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubApiRest.Core.Entities;
using VideoClubApiRest.Core.Interfaces;
using VideoClubApiRest.Infraestructure.Data;

namespace VideoClubApiRest.Infraestructure.Repositories
{
    public class RentsRepository : InterfaceRentsRepository
    {
        private readonly VideoClubDBContext _context;

        public RentsRepository(VideoClubDBContext context) {
            _context = context;
        }
        public async Task<IEnumerable<Rents>> GetRents() {
            var rents = await _context.Rents.ToListAsync();
            return rents;
        }

        public async Task InsertRents(Rents rent) {
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();
        }
    }
}
