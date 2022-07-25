using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.AspNetCore.Mvc;
//{
//    "category": "מערך מטוסי קרב",
//  "courseNumber": "0888",
//  "courseName": "טכנאי דרג א' בז",
//  "courseTime": "6 שבועות",
//  "courseDescription": "מומחה בטיפול ותחזוקה שוטפת במטוסים הוצאת וקבלת מטוסים מטיסה וזיהוי תקלות. מבצע בדיקות מקיפות למטוס",
//  "youTubeUrl": "EQ3gHqLIC5w",
//  "imgUrl": "",
//  "note": "דרג א' הינו מקצוע המוגדר תומך לחימה, מקנה הטבות בשחרור",
//  "baseofcoursesDto": [
//    {
//        "base": "בח\"א 8"
//    }
//  ]
//}
namespace Index_Bislat_Back.Controllers
{
    [Route("Course")]
    [ApiController]
    public class CourseControllers : Controller
    {
        private readonly ICourse _course;
        private readonly IAifBase _base;
        private readonly IMapper _mapper;
        public CourseControllers(ICourse Course, IAifBase aifbase, IMapper mapper)
        {
            _course = Course;
            _mapper = mapper;
            _base = aifbase;
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
            List<string> bases =new List<string>();
            foreach (var item in courseCreate.BaseofcoursesDto)
            {
                bases.Add(item.Base);
            }
            if (!_course.AddCourse(courseMap, bases,_base))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }




    }
}
