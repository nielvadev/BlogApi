using System.ComponentModel.DataAnnotations;

namespace blogApi.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        
        

    }
}
