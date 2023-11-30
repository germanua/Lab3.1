using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ReadWrite
{
    private string filename = "Students.txt";
    private InputOutput io;
    public ReadWrite(InputOutput io)
    {
        this.io = io;
    }

    public List<T> ReadData<T>()
    {
        List<T> data = new List<T>();
        if (File.Exists(filename))
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    T item = JsonConvert.DeserializeObject<T>(line);
                    data.Add(item);
                }
            }
        }
        return data;
    }

    public void WriteData<T>(List<T> data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filename, jsonData);
    }

}


