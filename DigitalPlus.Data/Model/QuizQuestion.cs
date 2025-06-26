using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public enum QuestionType
    {
        TrueFalse,
        MultipleChoice,
        FillInTheBlank,
        LongAnswer
    }

    public class QuizQuestion
    {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; } = string.Empty;
        [Required] public QuestionType Type { get; set; }

        // Multiple-choice options
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }

        public string? Answer { get; set; }

        [Required] public int QuizId { get; set; }
        public Questions Quiz { get; set; } = null!;
    }
}
