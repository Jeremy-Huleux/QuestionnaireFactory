using QuestionnaireFactory.Entities;

namespace QuestionnaireFactory.Models.PassageQuizz
{
    public class Question
    {
        public Question()
        {
        }
        public Question(QuizzQuestionEnregistree q)
        {
            if (q != null)
            {
                ContenuQuestion = q.QuestionEnregistree.ContenuQuestion;
                QuestionId = q.QuestionEnregistree.QuestionEnregistreeId;

            }
        }
        public string ContenuQuestion { get; set; }
        public long QuestionId { get; set; }
    }
}

