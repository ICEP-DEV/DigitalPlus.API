using System.ComponentModel.DataAnnotations;

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
        public string? Question1 { get; set; }
        public string? Question2 { get; set; }
        public string? Question3 { get; set; }
        public string? Question4 { get; set; }
        public string? Question5 { get; set; }
        public string? Question6 { get; set; }
        public string? Question7 { get; set; }
        public string? Question8 { get; set; }
        public string? Question9 { get; set; }
        public string? Question10 { get; set; }
        public string? Question11 { get; set; }
        public string? Question12 { get; set; }
        public string? Question13 { get; set; }
        public string? Question14 { get; set; }
        public string? Question15 { get; set; }
        public string? Question16 { get; set; }
        public string? Question17 { get; set; }
        public string? Question18 { get; set; }
        public string? Question19 { get; set; }
        public string? Question20 { get; set; }


    }
}
