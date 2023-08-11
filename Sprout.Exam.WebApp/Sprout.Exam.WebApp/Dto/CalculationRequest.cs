namespace Sprout.Exam.WebApp.Dto
{
    public class CalculationRequest
    {
        public int Id { get; set; }
        public decimal AbsentDays { get; set; }
        public decimal WorkedDays { get; set; }
    }
}