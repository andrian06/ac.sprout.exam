using System;

namespace Sprout.Exam.Common.Dto
{
    public class BaseEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
    }
}
