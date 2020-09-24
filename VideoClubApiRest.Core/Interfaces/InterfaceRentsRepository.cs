using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoClubApiRest.Core.Entities;

namespace VideoClubApiRest.Core.Interfaces
{
    public interface InterfaceRentsRepository
    {
        Task <IEnumerable<Rents>> GetRents();
        Task InsertRents(Rents rent);
    }
}
