#nullable enable
namespace CSharpObserveVersions.NullableReferenceType
{
    public enum QuestionType
    {
        YesNo,
        Number,
        Text
    }
    public class SurveyQuestion
    {
        public string QuestionText { get;}  
        public QuestionType Type { get; }

        public SurveyQuestion(string questionText, QuestionType type) 
            => (QuestionText, Type) = (questionText, type);
    }
}
