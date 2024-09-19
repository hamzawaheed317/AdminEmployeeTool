namespace AdminEmployeeTool.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }

        // Foreign key to Announcement
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
    }

}
