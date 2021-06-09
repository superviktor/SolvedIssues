using CSharpObserveVersions.NullableReferenceType;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpObserveVersions.Tests
{
    [TestFixture]
    public class SurveyRunTests
    {
        [Test]
        public void CanAddQuestion()
        {
            var surveyRun = new SurveyRun();
            surveyRun.AddQuestion("Has your code ever thrown a NullReferenceException?", QuestionType.YesNo);
            surveyRun.AddQuestion(new SurveyQuestion("How many times (to the nearest 100) has that happened?", QuestionType.Number));
            surveyRun.AddQuestion("What is your favorite color?", QuestionType.Text);

            surveyRun.PerformSurvey(50);

            foreach (var participant in surveyRun.AllParticipants)
            {
                if (participant.AnsweredSurvey)
                {
                    for (int i = 0; i < surveyRun.Questions.Count; i++)
                    {
                        var answer = participant.Answer(i);
                        Console.WriteLine($"\t{surveyRun.GetQuestion(i).QuestionText} : {answer}");
                    }
                }
                else
                {
                    Console.WriteLine("\tNo responses");
                }
            }

        }
    }
}
