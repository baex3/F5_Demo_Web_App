using System.ComponentModel.DataAnnotations;

namespace SportsApp.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Please Enter valid username."), MinLengthAttribute(3, ErrorMessage = "Username cannot be less than 3 characters")]
        
        public string Username {get; set;}
        [Required(ErrorMessage = "Please enter a password"), MinLength(8, ErrorMessage ="Password must be 8 characters or more")]
        public string Password {get; set;}
    }
}