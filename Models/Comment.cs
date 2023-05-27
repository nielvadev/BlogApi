namespace blogApi.Models
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public string TxtCom { get; set; }
        public int IdUser { get; set; }
        public int IdPoem { get; set; }
    }
}
