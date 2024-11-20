using SQLite;

namespace PersonalityQuiz.Models
{
    [Table("questions")]
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(500), Unique]
        public string Text { get; set; }

        public string Image { get; set; }
    }
}
