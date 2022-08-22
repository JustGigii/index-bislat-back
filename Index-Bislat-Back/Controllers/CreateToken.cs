using AutoMapper;
using Index_Bislat_Back.Dto;
using Index_Bislat_Back.Helper;
using Index_Bislat_Back.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Index_Bislat_Back.Controllers
{
    [Route("token")]
    [ApiController]
    public class CreateToken : Controller
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;
        private readonly IClaimService _service;

        public CreateToken(ICourse Course, IMapper mapper, IConfiguration configuration, IClaimService service)
        {
            _course = Course;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("CourseNumber")]
        public async Task<IActionResult> GetCourseByIdToken(string CourseNumber)
        {
            try
            {
                if (!await _course.IsExist(CourseNumber))
                    return NotFound();
                var coures = _mapper.Map<CourseDetailsDto>(await _course.GetCourseById(CourseNumber));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string token = _service.CreateToken(coures);
                return Ok(token);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }
       
        [HttpGet("/help/{CourseNumber}")]
        public async Task<IActionResult> GetCourseByIdTokennumber(string CourseNumber)
        {
            try
            {
                if (!await _course.IsExist(CourseNumber))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string token = _service.CreateToken(CourseNumber);
                return Ok(token);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> RemoveChosie(string id, string sort)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string token = _service.CreateToken(new { id = id, sort = sort });
                return Ok(token);
            }
            catch (Exception err) { Console.WriteLine(err.Message); return BadRequest($"Error Occurred: pls contact to backend team"); }
        }

    }
}
