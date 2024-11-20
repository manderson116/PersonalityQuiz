namespace PersonalityQuiz;

public partial class App : Application
{
    public static QuestionRepository QuestionRepo { get; private set; }

    public App(QuestionRepository repo)
    {
        InitializeComponent();

        MainPage = new AppShell();

        QuestionRepo = repo;
    }
}
