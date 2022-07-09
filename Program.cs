
using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var DatabaseSetup = new DatabaseSetup(databaseConfig);

var studentRepository = new StudentRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    if(modelAction == "New")
    {
        var registration = args[2];
        var name = args[3];
        var city = args[4];
        var former = false;

        var student = new Student(registration, name, city, former);
        if(studentRepository.ExistsById(registration))
        {
            Console.WriteLine($"Estudante com Registration {registration} já existe");
        }else
        {
            studentRepository.Save(student);
            Console.WriteLine($"Estudante {name} cadastrado com sucesso");
        }
    }

    if(modelAction == "Delete")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido com sucesso");
        }else
        {
            Console.WriteLine($"Estudante {registration} não encontrado!");
        }
    }

    if(modelAction == "List")
    {
        if(studentRepository.GetAll().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAll())
            {
                if(student.Former == true)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não Formado");
                }                
            } 
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        } 
    }

    if(modelAction == "ListFormed")
    {
        if(studentRepository.GetAllFormed().Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllFormed())
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");          
            } 
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        } 
    }

    if(modelAction == "ListByCity")
    {
        string city = args[2];

        if(studentRepository.GetAllStudentByCity(city).Any())
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllStudentByCity(city))
            {
                if(student.Former == true)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não Formado");
                }
            } 
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        } 
    }

    if(modelAction == "ListByCities")
    {
	    var cities = new string[args.Length - 2];
        
        for(int i = 2; i < args.Length; i++)
        {
            cities[i-2] = args[i];
        }

        var students = studentRepository.GetAllByCities(cities);
        Console.WriteLine("Student List");

        if(students.Any())
        {
            foreach (var student in students)
            {
                if(student.Former == true)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não Formado");
                }
            } 
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        } 
    }

    if(modelAction == "Report")
    {
        var countBy = args[2];
        if(countBy == "CountByCities")
        {
            var students = studentRepository.CountByCities();
            if(students.Any())
            {
                foreach(var student in students)
                {
                    Console.WriteLine($"{student.AttributeName}, {student.StudentNumber}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum estudante cadastrado");
            }
        }
        if(countBy == "CountByFormed")
        {
            var students = studentRepository.CountByFormed();
            if(students.Any())
            {
                foreach(var student in students)
                {
                    if(student.AttributeName.Equals("1"))
                    {
                        Console.WriteLine($"Formados, {student.StudentNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Não Formados, {student.StudentNumber}");
                    }
                }
            }
            else
            {
             Console.WriteLine("Nenhum estudante cadastrado");
            }
        }
    }
}