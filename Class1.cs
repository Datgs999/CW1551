using System;
using System.Collections.Generic;

namespace CW1551
{
    // ============================================================
    // BASE CLASS: Person
    // ============================================================
    public class Person
    {
        // Private fields (encapsulated)
        private int _id;
        private string _address;
        private string _role;
        private string _name;
        private string _phone;
        private string _email;

        // ================= PROPERTY DEFINITIONS ==================
        public int ID
        {
            get => _id;
            set => _id = (value > 0) ? value : throw new Exception("ID must be positive");
        }

        public string Address
        {
            get => _address;
            set => _address = value ?? "Unknown";
        }

        public string Role
        {
            get => _role;
            set => _role = value ?? "Unknown";
        }

        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value : "No Name";
        }

        public string Phone
        {
            get => _phone;
            set => _phone = !string.IsNullOrWhiteSpace(value) ? value : "No Phone";
        }

        public string Email
        {
            get => _email;
            set => _email = !string.IsNullOrWhiteSpace(value) ? value : "No Email";
        }

        // ============================================================
        // POLYMORPHISM: DisplayInfo() is overridden by child classes
        // ============================================================
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[{ID}] {Name} | {Address} | {Phone} | {Email} | Role: {Role}");
        }
    }

    // ============================================================
    // TEACHER CLASS (inherits from Person)
    // Now supports two subjects
    // ============================================================
    public class Teacher : Person
    {
        private int _salary;
        private string _subject1;
        private string _subject2;

        public int Salary
        {
            get => _salary;
            set => _salary = (value >= 0) ? value : throw new Exception("Salary must be >= 0");
        }

        public string Subject1
        {
            get => _subject1;
            set => _subject1 = value ?? "Unknown";
        }

        public string Subject2
        {
            get => _subject2;
            set => _subject2 = value ?? "Unknown";
        }

        // Default constructor assigns role
        public Teacher() { Role = "Teacher"; }

        // Constructor with two subjects
        public Teacher(string name, string phone, string email, int salary, string subject1, string subject2)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Salary = salary;
            Subject1 = subject1;
            Subject2 = subject2;
            Role = "Teacher";
        }

        // Override method to display teacher-specific info
        public override void DisplayInfo()
        {
            Console.WriteLine($"[{ID}] Teacher: {Name} | {Phone} | {Email} | Salary: {Salary} | Subjects: {Subject1} | {Subject2}");
        }
    }

    // ============================================================
    // ADMIN CLASS (inherits from Person)
    // ============================================================
    public class Admin : Person
    {
        private string _workType;
        private string _time;
        private int _salary;

        public string WorkType
        {
            get => _workType;
            set => _workType = value ?? "Unknown";
        }

        public string Time
        {
            get => _time;
            set => _time = value ?? "Unknown";
        }

        public int Salary
        {
            get => _salary;
            set => _salary = (value >= 0) ? value : throw new Exception("Salary must be >= 0");
        }

        public Admin() { Role = "Admin"; }

        public Admin(string name, string phone, string email, int salary, string workType, string time)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Salary = salary;
            WorkType = workType;
            Time = time;
            Role = "Admin";
        }

        // Override display method
        public override void DisplayInfo()
        {
            Console.WriteLine($"[{ID}] Admin: {Name} | {Phone} | {Email} | Salary: {Salary} | Work: {WorkType} | Time: {Time}");
        }
    }

    // ============================================================
    // STUDENT CLASS (inherits from Person)
    // ============================================================
    public class Student : Person
    {
        private string _subject1;
        private string _subject2;
        private string _subject3;

        public string Subject1
        {
            get => _subject1;
            set => _subject1 = value ?? "None";
        }

        public string Subject2
        {
            get => _subject2;
            set => _subject2 = value ?? "None";
        }

        public string Subject3
        {
            get => _subject3;
            set => _subject3 = value ?? "None";
        }

        public Student() { Role = "Student"; }

        // Constructor with full info
        public Student(string name, string phone, string email, string s1, string s2, string s3)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Subject1 = s1;
            Subject2 = s2;
            Subject3 = s3;
            Role = "Student";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[{ID}] Student: {Name} | {Phone} | {Email} | Subjects: {Subject1} | {Subject2} | {Subject3}");
        }
    }

    // ============================================================
    // MAIN PROGRAM – MENU + CRUD OPERATIONS
    // ============================================================
    class Program
    {
        // Auto-increment ID for all objects
        static int nextId = 0;

        // Predefined data (initial lists) — now Teacher entries supply 2 subjects
        static List<Teacher> teachers = new List<Teacher>
        {
            new Teacher("Anna", "0123456789", "anna@example.com", 1500, "Math", "History"),
            new Teacher("Brian", "0987654321", "brian@example.com", 1400, "Physics", "Music"),
        };

        static List<Admin> admins = new List<Admin>
        {
            new Admin("John", "0111222333", "john@example.com", 2000, "Full-time", "8:00 - 17:00"),
            new Admin("Sarah", "0445566778", "sarah@example.com", 1800, "Part-time", "9:00 - 13:00"),
        };

        static List<Student> students = new List<Student>
        {
            new Student("Alice", "0909123456", "alice@example.com", "Math", "English", "History"),
            new Student("Ben", "0909765432", "ben@example.com", "Physics", "Chemistry", "Biology"),
        };

        // Assign IDs to initial objects
        static void AssignInitialIds()
        {
            foreach (var t in teachers) t.ID = ++nextId;
            foreach (var a in admins) a.ID = ++nextId;
            foreach (var s in students) s.ID = ++nextId;
        }

        static void Main()
        {
            AssignInitialIds();  // Prepare initial database

            bool running = true;

            // MAIN MENU LOOP
            while (running)
            {
                Console.WriteLine("\n==== MAIN MENU ====");
                Console.WriteLine("1. Manage Teacher");
                Console.WriteLine("2. Manage Student");
                Console.WriteLine("3. Manage Admin");
                Console.WriteLine("4. Exit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine()?.Trim();

                // Menu handler
                switch (choice)
                {
                    case "1": ManageTeacher(); break;
                    case "2": ManageStudent(); break;
                    case "3": ManageAdmin(); break;
                    case "4":
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        // ============================================================
        // COMMON HELPER FUNCTIONS (Validations, Search, Displays)
        // ============================================================
        static void ShowList<T>(List<T> list) where T : Person
        {
            Console.WriteLine("\n--- LIST ---");

            if (list.Count == 0)
            {
                Console.WriteLine("No items.");
                return;
            }

            foreach (var item in list)
                item.DisplayInfo();
        }

        // Input integer with range validation
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int v))
                {
                    if (v < min || v > max)
                    {
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                        continue;
                    }
                    return v;
                }
                Console.WriteLine("Invalid number, try again.");
            }
        }

        // Input string (empty option configurable)
        static string ReadString(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();

                if (!allowEmpty && string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("Input cannot be empty.");
                    continue;
                }
                return s ?? "";
            }
        }

        // Find person by name (case-insensitive)
        static T FindByName<T>(List<T> list, string name) where T : Person
        {
            return list.Find(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        // ============================================================
        // TEACHER MANAGEMENT (CRUD)
        // ============================================================
        static void ManageTeacher()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n==== TEACHER MENU ====");
                Console.WriteLine("1. Show List");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Edit");
                Console.WriteLine("5. Back");
                Console.Write("Choice: ");
                string option = Console.ReadLine()?.Trim();

                switch (option)
                {
                    case "1": ShowList(teachers); break;
                    case "2": AddTeacher(); break;
                    case "3": DeleteTeacher(); break;
                    case "4": EditTeacher(); break;
                    case "5": back = true; break;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }

        static void AddTeacher()
        {
            // Collect user input
            string name = ReadString("Enter Name: ");
            string phone = ReadString("Enter Phone: ");
            string email = ReadString("Enter Email: ");
            int salary = ReadInt("Enter Salary: ", 0);
            string subject1 = ReadString("Enter Subject 1: ");
            string subject2 = ReadString("Enter Subject 2: ");

            // Create new teacher
            var t = new Teacher(name, phone, email, salary, subject1, subject2)
            {
                ID = ++nextId
            };

            teachers.Add(t);
            Console.WriteLine("Teacher added successfully!");
        }

        static void DeleteTeacher()
        {
            string name = ReadString("Enter Name to Delete: ");

            int removed = teachers.RemoveAll(
                t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase)
            );

            Console.WriteLine(removed > 0 ? "Teacher deleted!" : "Teacher not found!");
        }

        static void EditTeacher()
        {
            string name = ReadString("Enter Name to Edit: ");
            var t = FindByName(teachers, name);

            if (t != null)
            {
                string newSubject1 = ReadString("Enter new Subject 1 (leave blank to keep): ", allowEmpty: true);
                if (!string.IsNullOrWhiteSpace(newSubject1)) t.Subject1 = newSubject1;

                string newSubject2 = ReadString("Enter new Subject 2 (leave blank to keep): ", allowEmpty: true);
                if (!string.IsNullOrWhiteSpace(newSubject2)) t.Subject2 = newSubject2;

                Console.Write("Enter new Salary (leave blank to keep): ");
                string sSalary = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(sSalary) && int.TryParse(sSalary, out int newSalary))
                {
                    t.Salary = newSalary;
                }

                Console.WriteLine("Updated successfully!");
            }
            else Console.WriteLine("Teacher not found!");
        }

        // ============================================================
        // STUDENT MANAGEMENT (CRUD)
        // ============================================================
        static void ManageStudent()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n==== STUDENT MENU ====");
                Console.WriteLine("1. Show List");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Edit");
                Console.WriteLine("5. Back");
                Console.Write("Choice: ");
                string option = Console.ReadLine()?.Trim();

                switch (option)
                {
                    case "1": ShowList(students); break;
                    case "2": AddStudent(); break;
                    case "3": DeleteStudent(); break;
                    case "4": EditStudent(); break;
                    case "5": back = true; break;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }

        static void AddStudent()
        {
            string name = ReadString("Enter Name: ");
            string phone = ReadString("Enter Phone: ");
            string email = ReadString("Enter Email: ");
            string s1 = ReadString("Enter Subject 1: ");
            string s2 = ReadString("Enter Subject 2: ");
            string s3 = ReadString("Enter Subject 3: ");

            var st = new Student(name, phone, email, s1, s2, s3)
            {
                ID = ++nextId
            };

            students.Add(st);
            Console.WriteLine("Student added successfully!");
        }

        static void DeleteStudent()
        {
            string name = ReadString("Enter Name to Delete: ");

            int removed = students.RemoveAll(
                s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)
            );

            Console.WriteLine(removed > 0 ? "Student deleted!" : "Student not found!");
        }

        static void EditStudent()
        {
            string name = ReadString("Enter Name to Edit: ");
            var s = FindByName(students, name);

            if (s != null)
            {
                // Each subject can be replaced or kept
                string ns1 = ReadString("Enter new Subject 1 (leave blank to keep): ", allowEmpty: true);
                string ns2 = ReadString("Enter new Subject 2 (leave blank to keep): ", allowEmpty: true);
                string ns3 = ReadString("Enter new Subject 3 (leave blank to keep): ", allowEmpty: true);

                if (!string.IsNullOrWhiteSpace(ns1)) s.Subject1 = ns1;
                if (!string.IsNullOrWhiteSpace(ns2)) s.Subject2 = ns2;
                if (!string.IsNullOrWhiteSpace(ns3)) s.Subject3 = ns3;

                Console.WriteLine("Student updated successfully!");
            }
            else Console.WriteLine("Student not found!");
        }

        // ============================================================
        // ADMIN MANAGEMENT (CRUD)
        // ============================================================
        static void ManageAdmin()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n==== ADMIN MENU ====");
                Console.WriteLine("1. Show List");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Edit");
                Console.WriteLine("5. Back");
                Console.Write("Choice: ");
                string option = Console.ReadLine()?.Trim();

                switch (option)
                {
                    case "1": ShowList(admins); break;
                    case "2": AddAdmin(); break;
                    case "3": DeleteAdmin(); break;
                    case "4": EditAdmin(); break;
                    case "5": back = true; break;
                    default: Console.WriteLine("Invalid option!"); break;
                }
            }
        }

        static void AddAdmin()
        {
            string name = ReadString("Enter Name: ");
            string phone = ReadString("Enter Phone: ");
            string email = ReadString("Enter Email: ");
            int salary = ReadInt("Enter Salary: ", 0);
            string workType = ReadString("Enter Work Type: ");
            string time = ReadString("Enter Time: ");

            var a = new Admin(name, phone, email, salary, workType, time)
            {
                ID = ++nextId
            };

            admins.Add(a);
            Console.WriteLine("Admin added successfully!");
        }

        static void DeleteAdmin()
        {
            string name = ReadString("Enter Name to Delete: ");

            int removed = admins.RemoveAll(
                a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase)
            );

            Console.WriteLine(removed > 0 ? "Admin deleted!" : "Admin not found!");
        }

        static void EditAdmin()
        {
            string name = ReadString("Enter Name to Edit: ");
            var a = FindByName(admins, name);

            if (a != null)
            {
                string newWork = ReadString("Enter new Work Type (leave blank to keep): ", allowEmpty: true);
                if (!string.IsNullOrWhiteSpace(newWork)) a.WorkType = newWork;

                string newTime = ReadString("Enter new Time (leave blank to keep): ", allowEmpty: true);
                if (!string.IsNullOrWhiteSpace(newTime)) a.Time = newTime;

                Console.Write("Enter new Salary (leave blank to keep): ");
                string sSalary = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(sSalary) && int.TryParse(sSalary, out int newSalary))
                {
                    a.Salary = newSalary;
                }

                Console.WriteLine("Admin updated successfully!");
            }
            else Console.WriteLine("Admin not found!");
        }
    }
}
