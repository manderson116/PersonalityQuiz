using Microsoft.Extensions.Logging;

namespace PersonalityQuiz;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string dbPath = FileAccessHelper.GetLocalFilePath("questions.db3");
        Console.WriteLine(dbPath);
        builder.Services.AddSingleton<QuestionRepository>(s => ActivatorUtilities.CreateInstance<QuestionRepository>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
