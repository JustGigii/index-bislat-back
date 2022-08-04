using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Sort")]
    [ApiController]
    public class SortCycleControllers : Controller
    {
        private readonly ISortCycle _sort;
        private readonly IMapper _mapper;
        public SortCycleControllers(ISortCycle sort, IMapper mapper )
        {
            this._sort = sort;
            this._mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCourse([FromBody] SortCycleDetailsDto sort)
        {
            if (sort == null)
                return BadRequest(ModelState);

            if (await _sort.IsExist(sort.Name))
            {
                ModelState.AddModelError("", "sort already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sortmap = _mapper.Map<SortCycle>(sort);
            if (!await _sort.AddSortCycle(sortmap, sort.courses.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpGet("{sortName}")]
        [ProducesResponseType(200, Type = typeof(CourseDetailsDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCourseById(string sortName)
        {
            try
            {
                if (!await _sort.IsExist(sortName))
                    return NotFound();
                var coures = _mapper.Map<SortCycleDetailsDto>(await _sort.GetSortCycleDetails(sortName));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(coures);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred: {ex}");
            }
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SortCycleDto>))]
        public async Task<IActionResult> GetAllCourses()
        {
            var sort = _mapper.Map<List<SortCycleDto>>(await _sort.GetAllSortCycles());
            //var sort = await _sort.GetAllSortCycles();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sort);

        }

        [HttpDelete("{SortName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DelteCourse(string SortName)
        {
            if (!await _sort.IsExist(SortName))
            {
                ModelState.AddModelError("", "sort not exists");
                return StatusCode(422, ModelState);
            }
            var sort =await _sort.GetSortCycleDetails(SortName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _sort.DeleteSort(sort.Name))
            {
                ModelState.AddModelError("", "Something went wrong while delete");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }
    }
}
