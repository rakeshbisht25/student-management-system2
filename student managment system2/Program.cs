
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentManagementSystem
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Course { get; set; } = "";
        public double Marks { get; set; }

        public Student(int id, string name, int age,
                       string course, double marks)
        {
            Id = id;
            Name = name;
            Age = age;
            Course = course;
            Marks = marks;
        }

        public void Display()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"ID     : {Id}");
            Console.WriteLine($"Name   : {Name}");
            Console.WriteLine($"Age    : {Age}");
            Console.WriteLine($"Course : {Course}");
            Console.WriteLine($"Marks  : {Marks}");
        }
    }

    class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== STUDENT MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student by ID");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Search Student by Name");
                Console.WriteLine("7. Show Total Students");
                Console.WriteLine("8. Show Topper");
                Console.WriteLine("9. Sort Students by Marks");
                Console.WriteLine("10. Exit");

                Console.Write("\nEnter your choice: ");
                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        ViewStudents();
                        break;

                    case "3":
                        SearchById();
                        break;

                    case "4":
                        UpdateStudent();
                        break;

                    case "5":
                        DeleteStudent();
                        break;

                    case "6":
                        SearchByName();
                        break;

                    case "7":
                        ShowTotalStudents();
                        break;

                    case "8":
                        ShowTopper();
                        break;

                    case "9":
                        SortByMarks();
                        break;

                    case "10":
                        running = false;
                        Console.WriteLine("Thank you!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            int id = ReadInt("Enter Student ID: ");

            if (students.Any(s => s.Id == id))
            {
                Console.WriteLine("This ID already exists.");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            int age = ReadInt("Enter Age: ");

            if (age < 1 || age > 120)
            {
                Console.WriteLine("Enter a valid age.");
                return;
            }

            Console.Write("Enter Course: ");
            string course = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(course))
            {
                Console.WriteLine("Course cannot be empty.");
                return;
            }

            double marks = ReadDouble("Enter Marks (0-100): ");

            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Marks must be between 0 and 100.");
                return;
            }

            Student student = new Student(
                id, name.Trim(), age, course.Trim(), marks);

            students.Add(student);

            Console.WriteLine("Student added successfully!");
        }

        static void ViewStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (Student student in students)
            {
                student.Display();
            }
        }

        static void SearchById()
        {
            int id = ReadInt("Enter Student ID: ");

            Student student = students
                .FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                student.Display();
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static void UpdateStudent()
        {
            int id = ReadInt("Enter Student ID to update: ");

            Student student = students
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter New Name: ");
            string name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            int age = ReadInt("Enter New Age: ");

            if (age < 1 || age > 120)
            {
                Console.WriteLine("Enter a valid age.");
                return;
            }

            Console.Write("Enter New Course: ");
            string course = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(course))
            {
                Console.WriteLine("Course cannot be empty.");
                return;
            }

            double marks = ReadDouble("Enter New Marks (0-100): ");

            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Marks must be between 0 and 100.");
                return;
            }

            student.Name = name.Trim();
            student.Age = age;
            student.Course = course.Trim();
            student.Marks = marks;

            Console.WriteLine("Student updated successfully!");
        }

        static void DeleteStudent()
        {
            int id = ReadInt("Enter Student ID to delete: ");

            Student student = students
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            students.Remove(student);

            Console.WriteLine("Student deleted successfully!");
        }

        static void SearchByName()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine() ?? "";

            List<Student> results = students
                .Where(s => s.Name.IndexOf(
                    name.Trim(),
                    StringComparison.OrdinalIgnoreCase)>=0)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No matching students found.");
                return;
            }

            foreach (Student student in results)
            {
                student.Display();
            }
        }

        static void ShowTotalStudents()
        {
            Console.WriteLine(
                $"Total Students: {students.Count}");
        }

        static void ShowTopper()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            Student topper = students
                .OrderByDescending(s => s.Marks)
                .First();

            Console.WriteLine("Student Topper:");
            topper.Display();
        }

        static void SortByMarks()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            List<Student> sortedStudents = students
                .OrderByDescending(s => s.Marks)
                .ToList();

            Console.WriteLine("\nStudents sorted by marks:");

            foreach (Student student in sortedStudents)
            {
                student.Display();
            }
        }

        static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid whole number.");
            }
        }

        static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine() ?? "";

                if (double.TryParse(input, out double value))
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}
