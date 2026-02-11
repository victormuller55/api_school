using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    [Table("estudante")]
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public Student(string name, string email, string password, DateTime creationDate)
        {
            Name = name;
            this.Email = email;
            this.Password = password;
            this.CreationDate = creationDate;
        }
    }
}
