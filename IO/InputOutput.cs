using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class InputOutput
{
    private string filename;

    public InputOutput(string filename)
    {
        this.filename = filename;
    }

    public List<Student> ReadStudents()
    {
        try
        {
            if (File.Exists(filename))
            {
                string jsonData = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    return JsonConvert.DeserializeObject<List<Student>>(jsonData);
                }
            }
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error reading data from the file: {e.Message}");
        }

        return new List<Student>(); // Return an empty list if file doesn't exist or is empty.
    }
    
    public void WriteStudents(List<Student> students)
    {
        try
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(students, Formatting.Indented));
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error writing data to the file: {e.Message}");
        }
    }


}