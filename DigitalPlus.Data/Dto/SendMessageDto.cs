using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Dto
{
    public class SendMessageDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public int ModuleId { get; set; }

        // Because we're using [FromForm], 
        // we can receive an actual file via IFormFile:
        public IFormFile? File { get; set; }
    }
}
