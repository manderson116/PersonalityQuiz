using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using static PersonalityQuiz.MainPage;
using PersonalityQuiz.Models;
using System.Collections.Generic;

namespace PersonalityQuiz
{
    public partial class MainPage : ContentPage
    {
        /*public class Question
        {
            public string QuestionText { get; set; }
            public string QuestionImage { get; set; }
        }
        ObservableCollection<Question> questions = new ObservableCollection<Question>();
        public ObservableCollection<Question> Questions { get { return questions; } }
        */
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

            List<Question> questions = new List<Question>();
            questions.Add(new Question() { Text = "You go outside often.", Image = "outside.png" });
            questions.Add(new Question() { Text = "You can name at least 3 real-life friends.", Image = "friends.png" });
            questions.Add(new Question() { Text = "You get along nicely with most of your family members.", Image = "family.png" });
            questions.Add(new Question() { Text = "The things you've liked for a long time are still exciting to you.", Image = "fun.png" });
            questions.Add(new Question() { Text = "You want one free point on this quiz.", Image = "points.png" });

            foreach (Question question in questions)
            {
                App.QuestionRepo.AddNewQuestion(question.Text, question.Image);
            }

            List<Question> outquestions = App.QuestionRepo.GetAllQuestions();
            //questionList.ItemsSource = outquestions;
            
            results.Add(new Result() { ResultText = "You are probably trying to appear cool or rebellious by answering 'No' to everything.", ResultImage = "laugh.png" });
            results.Add(new Result() { ResultText = "You can be glad that you had even one question you could answer 'Yes' to.", ResultImage = "sad.png" });
            results.Add(new Result() { ResultText = "You are perfectly balanced, as all things should be.", ResultImage = "silly.png" });
            results.Add(new Result() { ResultText = "You are content with your life for the most part.", ResultImage = "thumbup.png" });
            results.Add(new Result() { ResultText = "You are maintaining a very positive attitude.", ResultImage = "thumbup.png" });
            results.Add(new Result() { ResultText = "You are a yes-man who simply must always agree with what other people tell you.", ResultImage = "laugh.png" });

            SemanticScreenReader.Announce(TitleLabel.Text);
            SemanticScreenReader.Announce(TrueFalseSwipe.Text);
            NextQuestion();
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
            if (questionIndex + 1 > App.QuestionRepo.Count())
            {
                QuizResults();
            }
            else
            {
                questionIndex++;
                QuestionLabel.Text = App.QuestionRepo.GetById(questionIndex).Id + ". " + App.QuestionRepo.GetById(questionIndex).Text;
                QuestionImage.Source = App.QuestionRepo.GetById(questionIndex).Image;
                SemanticScreenReader.Announce(QuestionLabel.Text);
            }
        }

        private void ResetQuestions(object sender, EventArgs e)
        {
            active = true;
            //TrueFalseButtons.IsVisible = true;
            TrueFalseSwipe.Text = "Swipe right for True\nSwipe left for False";
            score = 0;
            questionIndex = 0;
            NextQuestion();
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
            SemanticScreenReader.Announce(QuestionLabel.Text);
            SemanticScreenReader.Announce(TrueFalseSwipe.Text);
        }
    }
}
