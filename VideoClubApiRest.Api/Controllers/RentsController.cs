using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoClubApiRest.Core.Entities;
using VideoClubApiRest.Core.Interfaces;

namespace VideoClubApiRest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : Controller
    {
        private readonly InterfaceRentsRepository _rentsRepository;

        public RentsController( InterfaceRentsRepository rentsRepository)
        {
            _rentsRepository = rentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRents() {
            var rents = await _rentsRepository.GetRents();
            return Ok(rents);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRents(Rents rent)
        {
            await _rentsRepository.InsertRents(rent);
            return Ok("Creado con éxito. ");
        }

    }
}
