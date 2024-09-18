using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Task_Inficare.Models
{
    public class User:IdentityUser
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        [EmailAddress]
        public string Email { get; set; }  // User's email address

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  

        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public DateTime DateOfBirth { get; set; } 
    }
}
