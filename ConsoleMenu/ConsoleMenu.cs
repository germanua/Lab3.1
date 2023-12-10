using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ConsoleMenu
{
    private List<Student> students;
    private IPerformAction[] entities;
    private ReadWrite readWrite;

    public ConsoleMenu(List<Student> students, IPerformAction[] entities, ReadWrite readWrite)
    {
        this.students = students;
        this.entities = entities;
        this.readWrite = readWrite;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display Students");
            Console.WriteLine("3. Perform Actions");
            Console.WriteLine("4. Change Student Information");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Sort Students");
            Console.WriteLine("7. Save Data and Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    DisplayStudents();
                    break;
                case 3:
                    PerformActions();
                    break;
                case 4:
                    ChangeStudentInformation();
                    break;
                case 5:
                    DeleteStudent();
                    break;
                case 6:
                    SortStudents();
                    break;
                case 7:
                    SaveDataAndExit();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

private void AddStudent()
{
    Console.WriteLine("Enter student details:");
    Console.Write("First Name: ");
    string firstName = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(firstName) || !Regex.IsMatch(firstName, "^[a-zA-Z]+$"))
    {
        Console.WriteLine("Invalid First Name. Please enter a valid name.");
        return;
    }

    Console.Write("Last Name: ");
    string lastName = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(lastName) || !Regex.IsMatch(lastName, "^[a-zA-Z]+$"))
    {
        Console.WriteLine("Invalid Last Name. Please enter a valid name.");
        return;
    }

    Console.Write("Course: ");
    if (!int.TryParse(Console.ReadLine(), out int course) || course < 1 || course > 5)
    {
        Console.WriteLine("Invalid Course. Please enter a number between 1 and 5.");
        return;
    }

    Console.Write("Student Card: ");
    string studentCard = Console.ReadLine();
    if (!Regex.IsMatch(studentCard, "^[0-9]{5,10}$"))
    {
        Console.WriteLine("Invalid Student Card. It must be a numeric value between 5 and 10 digits.");
        return;
    }

    DateTime dateOfBirth;
    while (true)
    {
        Console.Write("Date of Birth (in format XX-XX-XXXX): ");
        if (TryParseDateOfBirth(out dateOfBirth))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid date format. Please use XX-XX-XXXX format.");
        }
    }

    Console.Write("Enter student skills (comma-separated, type 'None' for no skills): ");
    string skillsInput = Console.ReadLine();

    List<string> skills = new List<string>();

    if (string.Equals(skillsInput, "None", StringComparison.OrdinalIgnoreCase))
    {
        skills.Add("None");
    }
    else if (!string.IsNullOrWhiteSpace(skillsInput))
    {
        skills.AddRange(skillsInput.Split(',').Select(skill => skill.Trim()));
    }

    Student newStudent = new Student(firstName, lastName, course, studentCard, dateOfBirth, skills);
    students.Add(newStudent);

    Console.WriteLine("Student added successfully.");
}




private bool TryParseDateOfBirth(out DateTime dateOfBirth)
{
    dateOfBirth = default(DateTime);
    while (true)
    {
        string dateOfBirthInput = Console.ReadLine();
        if (DateTime.TryParseExact(dateOfBirthInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dateOfBirth))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


private void DisplayStudents()
{
    const int pageSize = 10;
    int totalPages = (int)Math.Ceiling((double)students.Count / pageSize);

    if (students.Count == 0)
    {
        Console.WriteLine("No students to display.");
        return;
    }

    int currentPage = 1;

    while (true)
    {
        Console.WriteLine($"Page {currentPage} of {totalPages}:");

        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(currentPage * pageSize, students.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i].FirstName} {students[i].LastName}");
        }

        Console.WriteLine("Options:");
        Console.WriteLine("N - Next Page, P - Previous Page, E - Exit");
        Console.Write("Enter an option: ");

        string option = Console.ReadLine().ToUpper();
        switch (option)
        {
            case "N":
                if (currentPage < totalPages)
                {
                    currentPage++;
                }
                break;
            case "P":
                if (currentPage > 1)
                {
                    currentPage--;
                }
                break;
            case "E":
                return;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}


    private void PerformActions()
    {
        while (true)
        {
            Console.WriteLine("Choose an entity to perform an action:");
            for (int i = 0; i < entities.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {entities[i].GetType().Name}");
            }
            Console.WriteLine($"{entities.Length + 1}. Back");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());
            if (choice == entities.Length + 1)
            {
                break;
            }

            if (choice >= 1 && choice <= entities.Length)
            {
                entities[choice - 1].PerformAction();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private void ChangeStudentInformation()
    {
        Console.Write("Enter the student's full name (First Last): ");
        string fullName = Console.ReadLine();
        Student student = students.Find(s => (s.FirstName + " " + s.LastName) == fullName);

        if (student != null)
        {
            Console.WriteLine("Choose information to update:");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Course");
            Console.WriteLine("4. Student Card");
            Console.WriteLine("5. Date of Birth");
            Console.WriteLine("6. Skills");
            // Add other attributes here...

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the new First Name: ");
                    string newFirstName = Console.ReadLine();
                    student.FirstName = newFirstName;
                    break;
                case 2:
                    Console.Write("Enter the new Last Name: ");
                    string newLastName = Console.ReadLine();
                    student.LastName = newLastName;
                    break;
                case 3:
                    Console.Write("Enter the new Course: ");
                    int newCourse = int.Parse(Console.ReadLine());
                    student.Course = newCourse;
                    break;
                case 4:
                    Console.Write("Enter the new Student Card: ");
                    string newStudentCard = Console.ReadLine();
                    student.StudentCard = newStudentCard;
                    break;
                case 5:
                    // Handle date of birth input
                    DateTime newDateOfBirth;
                    while (true)
                    {
                        Console.Write("Enter the new Date of Birth (in format XX-XX-XXXX): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out newDateOfBirth))
                        {
                            student.DateOfBirth = newDateOfBirth;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please use XX-XX-XXXX format.");
                        }
                    }
                    break;
                case 6:
                    Console.WriteLine("Enter the new Skills (comma-separated):");
                    string newSkillsInput = Console.ReadLine();
                    List<string> newSkills = new List<string>(newSkillsInput.Split(','));
                    student.Skills = newSkills;
                    break;
                default:
                    Console.WriteLine("Invalid choice. No changes were made.");
                    break;
            }
            Console.WriteLine("Student information updated successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    private void SortStudents()
    {
        // Filter students who are in the 3rd year and born in summer
        var sortedStudents = students
            .Where(s => s.IsThirdYearStudent() && s.IsBornInSummer())
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .ToList();

        if (sortedStudents.Count == 0)
        {
            Console.WriteLine("There are no students to sort.");
            return;
        }

        Console.WriteLine($"Number of 3rd year students born in summer: {sortedStudents.Count}");

        // Display the sorted students
        Console.WriteLine("Sorted Students:");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName} - {student.DateOfBirth:dd-MM-yyyy}");
        }
    }



    private void DeleteStudent()
    {
        Console.Write("Enter the student's full name (First Last) to delete: ");
        string fullName = Console.ReadLine();
        Student student = students.Find(s => (s.FirstName + " " + s.LastName) == fullName);
        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student deleted successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    private void SaveDataAndExit()
    {
        readWrite.WriteData(students);
        Console.WriteLine("Data saved to file. Exiting...");
    }
}
