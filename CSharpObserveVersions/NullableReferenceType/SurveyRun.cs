using System.Collections.Generic;
using System.Linq;
#nullable enable

namespace CSharpObserveVersions.NullableReferenceType
{
    public class SurveyRun
    {
        private readonly List<SurveyQuestion> _surveyQuestions = new();
        private List<SurveyResponse>? _respondents;

        public void AddQuestion(string text, QuestionType type) =>
            AddQuestion(new SurveyQuestion(text, type));

        public void AddQuestion(SurveyQuestion surveyQuestion) =>
            _surveyQuestions.Add(surveyQuestion);

        public void PerformSurvey(int numberOfRespondents)
        {
            int respondentsConsenting = 0;
            _respondents = new List<SurveyResponse>();
            while (respondentsConsenting < numberOfRespondents)
            {
                var respondent = SurveyResponse.GetRandomId();
                if (respondent.AnswerSurvey(_surveyQuestions))
                    respondentsConsenting++;
                _respondents.Add(respondent);
            }
        }

        public IEnumerable<SurveyResponse> AllParticipants => (_respondents ?? Enumerable.Empty<SurveyResponse>());
        public ICollection<SurveyQuestion> Questions => _surveyQuestions;
        public SurveyQuestion GetQuestion(int index) => _surveyQuestions[index];
    }
}
