using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace JSON
{
    class Program
    {

        
        static void Main(string[] args)
        {
          
            var groups = new List<Group>();
            var students = new List<Student>();

            for (int i = 1; i < 10; i++)
            {
                var group = new Group(i, "Группа " + i);
                group.SetPrivet(i);
                groups.Add(group);
            }

            for (int i = 1; i < 10; i++)
            {
                var studen = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100)
                {
                    Group = groups[i % 9]
                };

                students.Add(studen);
            }
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));

            using (var file = new FileStream("students.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(file, students);
            }

            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                var newStudens = jsonFormatter.ReadObject(file) as List<Student>;

                if (newStudens != null)
                {
                    foreach (var student in newStudens)
                    {
                        Console.WriteLine(student);
                    }
                }
            }

             Console.ReadLine();

        }
    }
}
