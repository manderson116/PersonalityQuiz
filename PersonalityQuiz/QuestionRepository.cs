using SQLite;
using PersonalityQuiz.Models;

namespace PersonalityQuiz;

public class QuestionRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Question>();
    }

    public QuestionRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewQuestion(string text, string image)
    {
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure text was entered
            if (string.IsNullOrEmpty(text))
                throw new Exception("Valid text required");
            if (string.IsNullOrEmpty(image))
                image = "";

            result = conn.Insert(new Question { Text = text, Image = image });

            StatusMessage = string.Format("{0} record(s) added (Question: \"{1}\", Image: \"{2}\")", result, text, image);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add (Question: \"{0}\", Image: \"{1}\"). Error: {2}", text, image, ex.Message);
        }

    }

    public Question GetById(int id)
    {
        var question = from u in conn.Table<Question>()
                   where u.Id == id
                   select u;
        return question.FirstOrDefault();
    }

    public int Count()
    {
        var question = from u in conn.Table<Question>()
                       select u;
        return question.Count();
    }

    public List<Question> GetAllQuestions()
    {
        try
        {
            Init();
            return conn.Table<Question>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Question>();
    }
}
