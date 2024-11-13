using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using static PersonalityQuiz.MainPage;

namespace PersonalityQuiz
{
    public partial class MainPage : ContentPage
    {
        public class Question
        {
            public string QuestionText { get; set; }
            public string QuestionImage { get; set; }
        }
        ObservableCollection<Question> questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions { get { return questions; } }
        public class Result
        {
            public string ResultText { get; set; }
            public string ResultImage { get; set; }
        }
        ObservableCollection<Result> results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results { get { return results; } }

        public MainPage()
        {
            InitializeComponent();

            questions.Add(new Question() { QuestionText = "1. You go outside often.", QuestionImage = "outside.png" });
            questions.Add(new Question() { QuestionText = "2. You can name at least 3 real-life friends.", QuestionImage = "friends.png" });
            questions.Add(new Question() { QuestionText = "3. You get along nicely with most of your family members.", QuestionImage = "family.png" });
            questions.Add(new Question() { QuestionText = "4. The things you've liked for a long time are still exciting to you.", QuestionImage = "fun.png" });
            questions.Add(new Question() { QuestionText = "5. You want one free point on this quiz.", QuestionImage = "points.png" });

            results.Add(new Result() { ResultText = "You are probably trying to appear cool or rebellious by answering 'No' to everything.", ResultImage = "" });
            results.Add(new Result() { ResultText = "You can be glad that you had even one question you could answer 'Yes' to.", ResultImage = "" });
            results.Add(new Result() { ResultText = "You are perfectly balanced, as all things should be.", ResultImage = "" });
            results.Add(new Result() { ResultText = "You are content with your life for the most part.", ResultImage = "" });
            results.Add(new Result() { ResultText = "You are maintaining a very positive attitude.", ResultImage = "" });
            results.Add(new Result() { ResultText = "You are a yes-man who simply must always agree with what other people tell you.", ResultImage = "" });

            QuestionLabel.Text = questions[0].QuestionText;
            QuestionImage.Source = questions[0].QuestionImage;
        }

        int score = 0;
        int questionIndex = 0;
        bool active = true;

        private void TrueBtnClicked(object sender, EventArgs e)
        {
            if (active)
            {
                score++;
                NextQuestion();
            }
        }

        private void FalseBtnClicked(object sender, EventArgs e)
        {
            if (active)
                NextQuestion();
        }

        private void NextQuestion()
        {
            if (questionIndex + 1 > questions.Count - 1)
            {
                QuizResults();
            }
            else
            {
                questionIndex++;
                QuestionLabel.Text = questions[questionIndex].QuestionText;
                QuestionImage.Source = questions[questionIndex].QuestionImage;
            }
        }

        private void ResetQuestions(object sender, EventArgs e)
        {
            active = true;
            //TrueFalseButtons.IsVisible = true;
            TrueFalseSwipe.Text = "Swipe right for True\nSwipe left for False";
            score = 0;
            questionIndex = 0;
            QuestionLabel.Text = questions[questionIndex].QuestionText;
            QuestionImage.Source = questions[questionIndex].QuestionImage;
        }

        private void QuizResults()
        {
            active = false;
            //TrueFalseButtons.IsVisible = false;
            TrueFalseSwipe.Text = "Swipe up to try again";
            Console.WriteLine(score + " / " + results.Count);
            QuestionLabel.Text = $"Score: {score}";
            if (score > results.Count)
            {
                QuestionLabel.Text += "\nYou are a hackerman, you broke the quiz.";
            }
            else
            {
                QuestionLabel.Text += "\n" + results[score].ResultText;
                QuestionImage.Source = results[score].ResultImage;
            }
        }
    }
}
