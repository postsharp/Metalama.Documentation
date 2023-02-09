using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDemo
{
    public class Student 
    {
        public void ChangeCourse(string newCourse) { }
        public decimal GetCourseFee(string courseID)
        {
            //TODO
            return 0;
        }
    }
    public class Department
    {
        private decimal _calculateCostHelper()
        {
            //TODO
            return 0;
        }
        public decimal CalculateCost()
        {
            //TODO
            return 1.5M * _calculateCostHelper();
        }
    }
}
