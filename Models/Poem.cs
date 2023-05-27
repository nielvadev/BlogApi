namespace blogApi.Models
{
    public partial class Poem
    {
        public int IdPoem { get; set; }
        public string Title { get; set; }
        public string TextPoem { get; set; }
        public int IdUser { get; set; }
        public int IdType { get; set; }

        public int pLikes { get; set; }
        public int pDislikes { get; set; }
    }
}