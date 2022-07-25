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
        [ProducesResponseType(200, Type = typeof(CourseDetailsDio))]
        [ProducesResponseType(400)]
        public IActionResult GetCourseById(string CourseNumber)
        {
            try
            {
                if (!_course.IsExist(CourseNumber))
                    return NotFound();
                var coures = _mapper.Map<CourseDetailsDio>(_course.GetCourseById(CourseNumber).Result);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(coures);
            }
            catch (Exception ex)
            {
                       return BadRequest($"Error Occurred: {ex}");
            }
        }
        


    }
}
