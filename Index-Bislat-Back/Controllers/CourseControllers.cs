using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("Course")]
    [ApiController]
    public class CourseControllers : Controller
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;
        public CourseControllers(ICourse Course, IMapper mapper)
        {
            _course = Course;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coursetable>))]
        public IActionResult GetAllCourses()
        {
            var coures = _mapper.Map<List<CoursesDto>>(_course.GetAllCourses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(coures);

        }
        [HttpGet("{CourseNumber}")]
        [ProducesResponseType(200, Type = typeof(CourseDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCourseById(string CourseNumber)
        {
            try
            {
                if (!_course.IsExist(CourseNumber))
                    return NotFound();
                var coures = _mapper.Map<CourseDetailsDto>(_course.GetCourseById(CourseNumber).Result);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(coures);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error Occurred: {ex}");
            }
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseDetailsDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);

            if (_course.IsExist(courseCreate.CourseNumber))
            {
                ModelState.AddModelError("", "course already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Coursetable>(courseCreate);
            List<string> bases = new List<string>(courseCreate.CourseBases);
            if (!_course.AddCourse(courseMap, bases))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpDelete("{CourseNumber}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DelteCourse(string CourseNumber)
        {
            if(!_course.IsExist(CourseNumber))
            {
                ModelState.AddModelError("", "course not exists");
                return StatusCode(422, ModelState);
            }
            var course = _course.GetCourseById(CourseNumber).Result;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_course.DeleteCourse(course))
            {
                ModelState.AddModelError("", "Something went wrong while delete");
                return StatusCode(500, ModelState);
            }
            return Ok("succses to delete");
        }

        [HttpPut("UpdateCourse")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory([FromBody] CourseDetailsDto updatedCourse, bool basechange) //pls cheak before you send
        {
            if (updatedCourse == null)
                return BadRequest(ModelState);

            if (!_course.IsExist(updatedCourse.CourseNumber))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var courseMap = _mapper.Map<Coursetable>(updatedCourse);
            List<string> bases = null;
            if(basechange)
            {
              bases = new List<string>(updatedCourse.CourseBases);
            }
            if (!_course.UpdateCourse(courseMap,bases))
            {
                ModelState.AddModelError("", "Something went wrong updating course");
                return StatusCode(500, ModelState);
            }

            return Ok("succses to Update");
        }


    }
}
