using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorKeyController : ControllerBase
    {
        private readonly IIMentorKeyServiceInterface _mentorKeyService;

        public MentorKeyController(IIMentorKeyServiceInterface mentorKeyService)
        {
            _mentorKeyService = mentorKeyService;
        }

        // GET: api/MentorKey/{keyId}
        [HttpGet("{keyId}")]
        public async Task<IActionResult> GetKeyById(int keyId)
        {
            var key = await _mentorKeyService.GetKeyById(keyId);

            if (key == null)
            {
                return NotFound($"Key with ID {keyId} does not exist.");
            }

            return Ok(new { message = "Key retrieved successfully.", key });
        }

        // POST: api/MentorKey
        [HttpPost]
        public async Task<IActionResult> CreateMentorKey([FromBody] MentorKey mentorKey)
        {
            if (!await _mentorKeyService.MentorExists(mentorKey.MentorId))
            {
                return NotFound($"Mentor with ID {mentorKey.MentorId} does not exist.");
            }

            var createdKey = await _mentorKeyService.CreateMentorKey(mentorKey);

            return CreatedAtAction(nameof(GetKeyById), new { keyId = createdKey.KeyId }, new { message = "Key created successfully.", createdKey });
        }

        // GET: api/MentorKey/keys/{mentorId}
        [HttpGet("keys/{mentorId}")]
        public async Task<IActionResult> GetKeysByMentorId(int mentorId)
        {
            if (!await _mentorKeyService.MentorExists(mentorId))
            {
                return NotFound($"Mentor with ID {mentorId} does not exist.");
            }

            var keys = await _mentorKeyService.GetKeysByMentorId(mentorId);

            if (!keys.Any())
            {
                return NotFound($"No keys found for Mentor ID {mentorId}.");
            }

            return Ok(new { message = "Keys retrieved successfully.", keys });
        }

        // DELETE: api/MentorKey/{keyId}
        [HttpDelete("{keyId}")]
        public async Task<IActionResult> DeleteMentorKey(int keyId)
        {
            var isDeleted = await _mentorKeyService.DeleteMentorKey(keyId);

            if (!isDeleted)
            {
                return NotFound($"Key with ID {keyId} does not exist.");
            }

            return Ok(new { message = "Key deleted successfully." });
        }


        // GET: api/MentorKey/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllKeys()
        {
            var keys = await _mentorKeyService.GetAllKeys();

            if (!keys.Any())
            {
                return NotFound("No keys found.");
            }

            return Ok(new { message = "All keys retrieved successfully.", keys });
        }
    }
}
