using System;
using System.Collections.Generic;

namespace VideoClubApiRest.Core.Entities
{
    public partial class Rents
    {
        public int RentId { get; set; }
        public string ObjectId { get; set; }
        public string ClientId { get; set; }
        public string Detailssatus { get; set; }
        public string Detailsuntil { get; set; }
    }
}
