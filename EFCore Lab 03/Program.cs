using EFCore_Lab_03.Context;
using EFCore_Lab_03.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Lab_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             Implement CRUD operations (Using LINQ to EFCore and SQL)   
                Use the generated models to implement:
                •	Insert
                •	Select
                •	Update
                •	Delete

             */

            var context = new ITIDbContext();

            //var student2 = new Student
            //{
            //    StId = 100,
            //    StFname = "Mohamed",
            //    StLname = "Salah",
            //    StAddress = "Tanta",
            //    StAge = 23,
            //    DeptId = 20
            //};

            //context.Students.Add(student2);
            //context.SaveChanges();

            var students = context.Students.ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"{s.StId} - {s.StFname}");
            }


            var student = context.Students.FirstOrDefault(s => s.StId == 100);

            if (student != null)
            {
                student.StFname = "Ahmed";
                context.SaveChanges();
            }


            var student2 = context.Students.FirstOrDefault(s => s.StId == 101);

            if (student2 != null)
            {
                context.Students.Remove(student2);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Student not found");
            }


            var allstudents = context.Students
                .FromSqlRaw("SELECT * FROM Student")
                .ToList();

            context.Database.ExecuteSqlRaw(
                "INSERT INTO Student (St_Id, St_Fname, St_Lname, St_Address, St_Age, Dept_Id) " +
                "VALUES (102, 'Ali', 'Hassan', 'Giza', 23, 10)"
            );

            context.Database.ExecuteSqlRaw(
                "UPDATE Student SET St_Fname = 'Omar' WHERE St_Id = 102"
            );

            context.Database.ExecuteSqlRaw(
                    "DELETE FROM Student WHERE St_Id = 102"
                );
        }
    }
}
