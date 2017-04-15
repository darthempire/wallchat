namespace wallchat.Model.App.Entity
{
    public class New
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}