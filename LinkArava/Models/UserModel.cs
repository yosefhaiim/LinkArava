using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace LinkArava.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string? password { get; set; }


    }

}


