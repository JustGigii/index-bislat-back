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
    [ApiController]
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

        [HttpGet]
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

        [HttpPost("AddBase"), Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBase([FromBody] AifbaseDto baseCreate)
        {
            if(!_service.CheakCorretjwt(_service.GetJson(), Newtonsoft.Json.JsonConvert.SerializeObject(baseCreate)))
                return BadRequest("jwt don't mach");
            if (baseCreate == null)
                return BadRequest(ModelState);

            var bases = await _aifBaseRepository.GetAllBase();
            var IafBase = bases.Where(c => c.BaseName.Contains(baseCreate.BaseName)).FirstOrDefault();
            if (IafBase != null)
            {
                ModelState.AddModelError("", "Base already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var BaseMap = _mapper.Map<Aifbase>(baseCreate);

            if (!await _aifBaseRepository.AddBase(BaseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{baseName}"),Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemoveBase(string baseName )
        {
            if (!_service.CheakCorretjwt(_service.GetJson(), baseName))
                return BadRequest("jwt don't mach");
            if (!await _aifBaseRepository.Isexsit(baseName))
                return NotFound();
            var iafbase = await _aifBaseRepository.GetAifbaseDetails(baseName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _aifBaseRepository.RemoveBase(iafbase))
            {
                ModelState.AddModelError("", "Something went wrong deleting Base");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }
    }
}
