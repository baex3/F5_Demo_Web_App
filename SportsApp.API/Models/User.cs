namespace SportsApp.API.Models
{
    public class User
    {
        public int Id {get;set;}

        public string Username {get; set;}

        public byte[] PasswordHash {get; set;}
    //Salt is used to obscure user hash by injecting string into calculation
    //PasswordSalt will act as a key to reverse calculate a user's pw
        public byte[] PasswordSalt {get; set;}


    }
}