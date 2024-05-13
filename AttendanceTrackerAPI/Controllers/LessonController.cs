﻿using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.Domain;
using AttendanceTracker.DTO;
using AttendanceTrackerAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonLogic _logic;

        public LessonController(ILessonLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLessons()
        {
            return Ok(await _logic.GetAllLessons());
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetLessonById(Guid id)
        {
            return Ok(await _logic.GetLessonById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(LessonCreateDTO dto)
        {
            var code=await _logic.AddLesson(dto.ToLessonFromCreateDTO());
            return Ok(code);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLesson(Lesson lesson)
        {
            await _logic.UpdateLesson(lesson);
            return Ok("Added");
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            await _logic.DeleteLesson(id);
            return Ok("Lesson removed");
        }
    }
}
