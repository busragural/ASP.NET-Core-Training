namespace _00_Basics.Models
{
    public class AllStudents
    {
        public static List<Student> Students { get; set; } = new List<Student>{
                new Student
                {
                    Id = 1,
                    Name = "mert",
                    Email = "mert@gmail.com",
                    Address = "Mert address",
                },
                new Student
                {
                    Id = 2,
                    Name = "tuna",
                    Email = "tuna@gmail.com",
                    Address = "Tuna address",
                }
            };
    }
}
