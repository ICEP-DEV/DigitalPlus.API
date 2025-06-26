using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalPlus.Data.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Dto;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly DigitalPlusDbContext _context;

        public QuizController(DigitalPlusDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            var quizzes = await _context.Questions
                .Include(q => q.Question)
                .ToListAsync();

            return Ok(quizzes);
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var quiz = await _context.Questions
                .Include(q => q.Question)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/Quiz/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, [FromBody] QuizCreateDto dto)
        {
            var existingQuiz = await _context.Questions
                .Include(q => q.Question)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (existingQuiz == null)
            {
                return NotFound();
            }

            // Update quiz info
            existingQuiz.Title = dto.Title;
            existingQuiz.Description = dto.Description;
            existingQuiz.StartDate = dto.StartDate;
            existingQuiz.EndDate = dto.EndDate;
            existingQuiz.ModuleId = dto.ModuleId;

            // Remove existing questions and add new ones
            _context.QuizQuestions.RemoveRange(existingQuiz.Question);

            foreach (var q in dto.Question)
            {
                existingQuiz.Question.Add(new QuizQuestion
                {
                    Text = q.Text,
                    Type = q.Type,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    Answer = q.Answer
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quiz
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions([FromBody] QuizCreateDto dto)
        {
            var quiz = new Questions
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ModuleId = dto.ModuleId
            };

            foreach (var q in dto.Question)
            {
                quiz.Question.Add(new QuizQuestion
                {
                    Text = q.Text,
                    Type = q.Type,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    Answer = q.Answer
                });
            }

            _context.Questions.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestions), new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions(int id)
        {
            var quiz = await _context.Questions
                .Include(q => q.Question)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            _context.QuizQuestions.RemoveRange(quiz.Question);
            _context.Questions.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
