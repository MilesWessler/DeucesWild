namespace DeucesWild.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommenterId { get; set; }
        public ApplicationUser Commenter { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public string Content { get; set; }
    }
}