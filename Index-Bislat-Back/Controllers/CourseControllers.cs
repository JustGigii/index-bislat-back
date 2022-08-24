using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Helper;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Index_Bislat_Back.Controllers
{
    [Route("Course")]
    [ApiController]
    public class CourseControllers : Controller
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;
        private readonly IClaimService _service;

        public CourseControllers(ICourse Course, IMapper mapper, IClaimService service)
        {
            _course = Course;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coursetable>))]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {

            var coures = _mapper.Map<List<CoursesDto>>(await _course.GetAllCourses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(coures);
            }
            catch (Exception err) {Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
            }

        [HttpGet("{CourseNumber}")]
        [ProducesResponseType(200, Type = typeof(CourseDetailsDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCourseById(string CourseNumber)
        {
            try
            {
                if (!await _course.IsExist(CourseNumber))
                    return NotFound();
                var coures = _mapper.Map<CourseDetailsDto>(await _course.GetCourseById(CourseNumber));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(coures);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }

        [HttpPost, Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDetailsDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);
            if (!_service.CheakCorretjwt(_service.GetJson(), Newtonsoft.Json.JsonConvert.SerializeObject(courseCreate)))
                return BadRequest("jwt don't mach");
                if (await _course.IsExist(courseCreate.CourseNumber))
            {
                ModelState.AddModelError("", "course already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Coursetable>(courseCreate);
            List<string> bases = new List<string>(courseCreate.CourseBases);
            if (! await _course.AddCourse(courseMap, bases))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{CourseNumber}"),Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCourse(string CourseNumber)
        {
            if (!_service.CheakCorretjwt(_service.GetJson(), CourseNumber))
                return BadRequest("jwt don't mach");
            if (!await _course.IsExist(CourseNumber))
            {
                ModelState.AddModelError("", "course not exists");
                return StatusCode(422, ModelState);
            }
            var course =await _course.GetCourseById(CourseNumber);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!await _course.DeleteCourse(course))
            {
                ModelState.AddModelError("", "Something went wrong while delete");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }

        [HttpPut("UpdateCourse"),Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory([FromBody] CourseDetailsDto updatedCourse)
        {
            if (updatedCourse == null)
                return BadRequest(ModelState);
            if (!_service.CheakCorretjwt(_service.GetJson(), Newtonsoft.Json.JsonConvert.SerializeObject(updatedCourse)))
                return BadRequest("jwt don't mach");
            if (!await _course.IsExist(updatedCourse.CourseNumber))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var courseMap = _mapper.Map<Coursetable>(updatedCourse);
            List<string> bases = new List<string>(updatedCourse.CourseBases);
            if (!await _course.UpdateCourse(courseMap,bases))
            {
                ModelState.AddModelError("", "Something went wrong updating course");
                return StatusCode(500, ModelState);
            }

            return Ok("succses to Update");
        }

        
    }
}
