using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace CSharpObserveVersions.NullableReferenceType
{
    public class SurveyResponse
    {
        private static readonly Random randomGenerator = new Random();
        private Dictionary<int, string>? _surveyResponses;

        public int Id { get; }

        public SurveyResponse(int id) => Id = id;

        private bool ConsentToSurvey() => randomGenerator.Next(0, 2) == 1;

        public static SurveyResponse GetRandomId() => new SurveyResponse(randomGenerator.Next());

        public bool AnswerSurvey(IEnumerable<SurveyQuestion> questions)
        {
            if (ConsentToSurvey())
            {
                _surveyResponses = new Dictionary<int, string>();
                int index = 0;
                foreach (var question in questions)
                {
                    var answer = GenerateAnswer(question);
                    if (answer != null)
                    {
                        _surveyResponses.Add(index, answer);
                    }
                    index++;
                }
            }
            return _surveyResponses != null;
        }

        private string? GenerateAnswer(SurveyQuestion question)
        {
            switch (question.Type)
            {
                case QuestionType.YesNo:
                    int n = randomGenerator.Next(-1, 2);
                    return (n == -1) ? default : (n == 0) ? "No" : "Yes";
                case QuestionType.Number:
                    n = randomGenerator.Next(-30, 101);
                    return (n < 0) ? default : n.ToString();
                case QuestionType.Text:
                default:
                    switch (randomGenerator.Next(0, 5))
                    {
                        case 0:
                            return default;
                        case 1:
                            return "Red";
                        case 2:
                            return "Green";
                        case 3:
                            return "Blue";
                    }
                    return "Red. No, Green. Wait.. Blue... AAARGGGGGHHH!";
            }
        }

        public bool AnsweredSurvey => _surveyResponses != null;

        public string Answer(int index) => _surveyResponses?.GetValueOrDefault(index) ?? "No answer";
    }
}