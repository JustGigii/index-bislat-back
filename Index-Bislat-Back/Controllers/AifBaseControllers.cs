using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Helper;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Iafbase")]
    [ApiController,Authorize(Roles = "Mannger")]
    public class IafBaseControllers : Controller
    {
        private readonly IAifBase _aifBaseRepository;
        private readonly IMapper _mapper;
        private readonly IClaimService _service;
        public IafBaseControllers(IAifBase aifBaseRepository, IMapper mapper, IClaimService service)
        {
            _aifBaseRepository = aifBaseRepository;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet,AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AifbaseDto>))]
        public async Task<IActionResult> GetBase()
        {
            try
            { 
            var Bases = _mapper.Map<List<AifbaseDto>>(await _aifBaseRepository.GetAllBase());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Bases);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }

        [HttpPost("AddBase")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBase([FromBody] AifbaseDto baseCreate)
        {
            if (baseCreate == null)
                return BadRequest(ModelState);

            var bases = await _aifBaseRepository.GetAllBase();
            var IafBase = bases.Where(c => c.BaseName.Contains(baseCreate.BaseName)).FirstOrDefault();
            if (IafBase != null)
            {
                return BadRequest("Base already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var BaseMap = _mapper.Map<Aifbase>(baseCreate);

            if (!await _aifBaseRepository.AddBase(BaseMap))
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{baseName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemoveBase(string baseName )
        {

            if (!await _aifBaseRepository.Isexsit(baseName))
                return NotFound();
            var iafbase = await _aifBaseRepository.GetAifbaseDetails(baseName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _aifBaseRepository.RemoveBase(iafbase))
            {
                return BadRequest("Something went wrong deleting Base");
            }
            return Ok("succses to delete");
        }
    }
}
