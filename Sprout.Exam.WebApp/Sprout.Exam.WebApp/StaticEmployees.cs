using Sprout.Exam.Common.Dto;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp
{
    public static class StaticEmployees
    {
        public static List<EmployeeDto> ResultList = new()
        {
            new EmployeeDto
            {
                sBirthdate = "1993-03-25",
                FullName = "Jane Doe",
                Id = 1,
                Tin = "123215413",
                TypeId = 1
            },
            new EmployeeDto
            {
                sBirthdate = "1993-05-28",
                FullName = "John Doe",
                Id = 2,
                Tin = "957125412",
                TypeId = 2
            }
        };
    }
}
