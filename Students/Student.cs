using System;
using System.Collections.Generic;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Course { get; set; }
    public string StudentCard { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> Skills { get; set; }

    public Student(string firstName, string lastName, int course, string studentCard, DateTime dateOfBirth, List<string> skills)
    {
        FirstName = firstName;
        LastName = lastName;
        Course = course;
        StudentCard = studentCard;
        DateOfBirth = dateOfBirth;
        Skills = skills;
    }

    public void AddSkill(string skill)
    {
        Skills.Add(skill);
    }
}
public interface IPerformAction
{
    void PerformAction();
}
public class Astronaut : IPerformAction
{
    public void PerformAction()
    {
        // Implement the action specific to the Astronaut entity
        Console.WriteLine("Performing astronaut-specific action.");
    }
}

public class Teacher : IPerformAction
{
    public void PerformAction()
    {
        // Implement the action specific to the Teacher entity
        Console.WriteLine("Performing teacher-specific action.");
    }
}

