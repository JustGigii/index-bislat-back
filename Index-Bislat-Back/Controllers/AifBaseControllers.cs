using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Iafbase")]
    [ApiController]
    public class IafBaseControllers : Controller
    {
        private readonly IAifBase _aifBaseRepository;
        private readonly IMapper _mapper;

        public IafBaseControllers(IAifBase aifBaseRepository, IMapper mapper)
        {
            this._aifBaseRepository = aifBaseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Aifbase>))]
        public IActionResult GetCategories()
        {
            var Bases = _mapper.Map<List<AifbaseDto>>(_aifBaseRepository.GetAllBase());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Bases);
        }

        [HttpPost("AddBase")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBase([FromBody] AifbaseDto baseCreate)
        {
            if (baseCreate == null)
                return BadRequest(ModelState);

            var bases = _aifBaseRepository.GetAllBase()
                .Where(c => c.BaseName.Trim().ToUpper() == baseCreate.BaseName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (bases != null)
            {
                ModelState.AddModelError("", "Base already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var BaseMap = _mapper.Map<Aifbase>(baseCreate);

            if (!_aifBaseRepository.AddBase(BaseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{baseName}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult RemoveBase(string baseName )
        {
            if (!_aifBaseRepository.Isexsit(baseName))
                return NotFound();
            var iafbase = _aifBaseRepository.GetAifbaseDetails(baseName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_aifBaseRepository.RemoveBase(iafbase))
            {
                ModelState.AddModelError("", "Something went wrong deleting Base");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }
    }
}
