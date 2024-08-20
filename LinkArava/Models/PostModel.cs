
namespace LinkArava.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string body { get; set; }

        public UserModel user { get; set; }

        public int likes { get; set; }

        public static implicit operator bool(PostModel v)
        {
            throw new NotImplementedException();
        }
    }
}
