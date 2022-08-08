using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Choise")]
    [ApiController]
    public class ChoisetableControllers : Controller
    {
        private readonly IChoisetable _IChoisetableRepository;
        private readonly IMapper _mapper;

        public ChoisetableControllers(IChoisetable IChoisetableRepository, IMapper mapper)
        {
            this._IChoisetableRepository = IChoisetableRepository;
            _mapper = mapper;
        }

        [HttpGet("sort")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ChoisetableDto>))]
        public async Task<IActionResult> GetCategories(string sort)
        {
            try
            {
                int sortid = await _IChoisetableRepository.GetSortId(sort);
                var choise = _mapper.Map<List<ChoisetableDto>>(await _IChoisetableRepository.GetAllChoise(sortid));
                for (int i = 0; i < choise.Count; i++)
                {
                    choise[i].Title = sort;
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(choise);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }

        [HttpPost("Addchoise")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChoise([FromBody] ChoisetableDto choiseCreate)
        {
            if (choiseCreate == null)
                return BadRequest(ModelState);
            int sortid = await _IChoisetableRepository.GetSortId(choiseCreate.Title);
            if (sortid == -1)
            {
                ModelState.AddModelError("", "sort not exists");
                return StatusCode(422, ModelState);
            }
            if (await _IChoisetableRepository.Isexsit(choiseCreate.Id, sortid))
            {
                ModelState.AddModelError("", "choise already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var choiseMap = _mapper.Map<Choisetable>(choiseCreate);
            choiseMap.Sortid = sortid;
            if (!await _IChoisetableRepository.AddChoise(choiseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemoveChosie(string id,string sort)
        {
            int sortid = await _IChoisetableRepository.GetSortId(sort);
            if (!await _IChoisetableRepository.Isexsit(id,sortid))
                return NotFound();
            var chosie = await _IChoisetableRepository.GetChoise(id,sortid);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _IChoisetableRepository.Removechoise(chosie))
            {
                ModelState.AddModelError("", "Something went wrong deleting Base");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }
    }
}
