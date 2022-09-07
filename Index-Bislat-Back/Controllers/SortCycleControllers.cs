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
    [ApiController, Authorize(Roles = "Mannger")]
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCourse([FromBody] SortCycleDetailsStringDto sort)
        {
            if (sort == null)
                return BadRequest(ModelState);

            if (await _sort.IsExist(sort.Name))
            {
                return BadRequest("sort already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sortmap = _mapper.Map<SortCycle>(sort);
            if (!await _sort.AddSortCycle(sortmap, sort.courses))
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        private static List<string> ConvertToString(SortCycleDetailsDto sort)
        {
            List<string> Courses = new List<string>();
            sort.courses.ToList().ForEach(item => Courses.Add(item.CourseNumber));
            return Courses;
        }

        [HttpGet("{sortName}"),AllowAnonymous]
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
        [HttpGet, AllowAnonymous]
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

        [HttpDelete("{SortName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DelteCourse(string SortName)
        {
            if (!await _sort.IsExist(SortName))
            {
                return BadRequest("sort not existsh");
            }
            var sort =await _sort.GetSortCycleDetails(SortName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _sort.DeleteSort(sort.Name))
            {
                return BadRequest("Something went wrong while delete");
            }
            return Ok("succses to delete");
        }
        [HttpPut("UpdateSort")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSort([FromBody] SortCycleDetailsStringDto updatedSort)
        {
            if (updatedSort == null)
                return BadRequest(ModelState);

            if (!await _sort.IsExist(updatedSort.Name))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sortMap = _mapper.Map<SortCycle>(updatedSort);
            if (!await _sort.UpdateSort(sortMap, updatedSort.courses))
            {
                return BadRequest("Something went wrong updating course");
            }

            return Ok("succses to Update");
        }
    }
}
