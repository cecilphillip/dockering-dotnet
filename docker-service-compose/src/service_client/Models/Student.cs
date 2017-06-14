using System.ComponentModel.DataAnnotations;

namespace service_client.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsFullTime { get; set; }
        [Required]
        public string Email { get; set; }
    }
}