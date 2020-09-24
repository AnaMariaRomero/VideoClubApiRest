using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoClubApiRest.Core.Entities;
using VideoClubApiRest.Core.Interfaces;

namespace XUnitTestProjectVideoClub
{
    public class MockRentsRepository: InterfaceRentsRepository
    {
        List<Rents> rents;

        public bool FailGet { get; set; }

        public MockRentsRepository()
        {
            rents = new List<Rents> {
                new Rents { RentId = 1, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/01/05" },
                new Rents { RentId = 2, ClientId = (new Guid()).ToString(), ObjectId = (new Guid()).ToString(), Detailssatus = "RENT", Detailsuntil = "2010/02/05" }
            };
        }
        public async Task<IEnumerable<Rents>> GetRents() {
                if (FailGet) {
                    throw new InvalidOperationException();
                }
                await Task.Delay(10);
                return rents;
        }

        public async Task InsertRents(Rents rent)
        {
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();
        }
    }
}
