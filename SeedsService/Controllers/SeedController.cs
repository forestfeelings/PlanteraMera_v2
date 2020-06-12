using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeedsService.Filters;
using SeedsService.Models;
using SeedsService.Repositories;

namespace SeedsService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ISeedRepository _seedRepository;

        public SeedController(ISeedRepository seedRepository)
        {
            _seedRepository = seedRepository;
        }

        [HttpGet]

        public ActionResult<Seed> GetById(Guid id)
        {
            var seed = _seedRepository.GetById(id);

            if (seed == null)
            {
                return NotFound();
            }

            return Ok(seed);
        }

        [ApiKeyAuth]
        [HttpGet]

        public ActionResult<Seed> GetAll()
        {
            var seeds = _seedRepository.GetAll();
            return Ok(seeds);
        }

        [HttpPost]

        public ActionResult<Seed> Create([FromBody] Seed seed)
        {
            if (seed == null)
            {
                return BadRequest();
            }

            var createdSeed = _seedRepository.Create(seed);

            return Ok(createdSeed);
        }

        [HttpDelete]

        public ActionResult<Guid> Delete(Guid id)
        {
            var wasDeleted = _seedRepository.Delete(id);

            if (wasDeleted)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }

        [ApiKeyAuth]
        [HttpGet]

        public ActionResult<string> Secret()
        {
            return Ok("Detta är en hemlis...");
        }

    }
}