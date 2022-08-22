using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Helper;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Sort")]
    [ApiController]
    public class SortCycleControllers : Controller
    {
        private readonly ISortCycle _sort;
        private readonly IMapper _mapper;
        private readonly IClaimService _service;
        public SortCycleControllers(ISortCycle sort, IMapper mapper, IClaimService service)
        {
            this._sort = sort;
            this._mapper = mapper;
            _service = service;
        }

        [HttpPost,Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCourse([FromBody] SortCycleDetailsDto sort)
        {
            if (!_service.CheakCorretjwt(_service.GetJson(), Newtonsoft.Json.JsonConvert.SerializeObject(sort)))
                return BadRequest("jwt don't mach");
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
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SortCycleDto>))]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            { 
            var sort = _mapper.Map<List<SortCycleDto>>(await _sort.GetAllSortCycles());
            //var sort = await _sort.GetAllSortCycles();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sort);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }

        [HttpDelete("{SortName}"),Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DelteCourse(string SortName)
        {
            if (!_service.CheakCorretjwt(_service.GetJson(), SortName))
                return BadRequest("jwt don't mach");
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
        [HttpPut("UpdateSort"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSort([FromBody] SortCycleDetailsDto updatedSort)
        {
            if (!_service.CheakCorretjwt(_service.GetJson(), Newtonsoft.Json.JsonConvert.SerializeObject(updatedSort)))
                return BadRequest("jwt don't mach");
            if (updatedSort == null)
                return BadRequest(ModelState);

            if (!await _sort.IsExist(updatedSort.Name))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sortMap = _mapper.Map<SortCycle>(updatedSort);
            List<string> courses = new List<string>(updatedSort.courses);
            if (!await _sort.UpdateSort(sortMap, courses))
            {
                ModelState.AddModelError("", "Something went wrong updating course");
                return StatusCode(500, ModelState);
            }

            return Ok("succses to Update");
        }
    }
}
