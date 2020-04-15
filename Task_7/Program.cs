using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security;

namespace Task_7
{
    //Класс Студент
    [DataContract]
    public class Student
    {
        //Поля для сериализации
        [DataMember]
        public int mathMark;
        [DataMember]
        public int englishMark;
        [DataMember]
        public int progMark;
        [DataMember]
        public string name;
        [DataMember]
        public int year;

        //Переопределенный метод ToString для вывода информации
        public override string ToString() =>
            $"Name: {name}, Year: {year}, MathMark: {mathMark}, EnglishMark: {englishMark}, ProgMark: {progMark}";
    }

    class Program
    {
        const string INPUT_FILE = "Students.json";
        static void Main()
        {
            //Переменные для средних оценок
            double eng = 0, math = 0, prog = 0;

            try
            {
                using (FileStream sr = File.Open(INPUT_FILE, FileMode.Open))
                {
                    //Создаем сериализатор
                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Student[]));

                    //Считываем объект
                    Student[] students = (Student[])js.ReadObject(sr);
                    if (students == null)
                        Console.WriteLine("File is null.");

                    //Подсчитываем средние оценки и выводим информацию о студентах
                    foreach (Student st in students)
                    {
                        Console.WriteLine(st);
                        eng += st.englishMark;
                        math += st.mathMark;
                        prog += st.progMark;
                    }

                    eng = eng / students.Length;
                    math = math / students.Length;
                    prog = prog / students.Length;

                    //Выводим средние оценки
                    Console.WriteLine($"English: {eng:f2}, Math: {math:f2}, Programming: {prog:f2}");
                }
            }
            catch (SerializationException)
            {
                Console.WriteLine("Couldn't deserialize object, file is broken.");
            }
            catch (IOException)
            {
                Console.WriteLine("I/O error.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong file path given.");
            }
            catch(SecurityException)
            {
                Console.WriteLine("Security error.");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Couldn't deserialize object, file has an incorrect format.");
            }
        }
    }
}
