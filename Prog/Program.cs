using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        List<Student> students;
        InputOutput io = new InputOutput("Students.txt");
        ReadWrite readWrite = new ReadWrite(io);

   
        students = io.ReadStudents();

        if (students == null)
        {
            students = new List<Student>();
        }

        // Instances of  entities
        Astronaut astronaut = new Astronaut();
        Teacher teacher = new Teacher();

        IPerformAction[] entities = new IPerformAction[]
        {
            astronaut,
            teacher
        };

        ConsoleMenu menu = new ConsoleMenu(students, entities, readWrite);
        menu.ShowMenu();
    }
}
