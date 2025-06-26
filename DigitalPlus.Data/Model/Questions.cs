using DigitalPlus.API.Model;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DigitalPlus.Data.Model
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }  // The date when the question becomes active
        public DateTime EndDate { get; set; }    // The date when the question expires


        // Question-specific properties up to Question 20
        [Required] public int ModuleId { get; set; }
        public LearningModule Module { get; set; } 

        public ICollection<QuizQuestion> Question { get; set; } = new List<QuizQuestion>();


    }
}
