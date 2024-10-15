using QuestionnaireFactory.Entities;

namespace QuestionnaireFactory.Models.PassageQuizz
{
    public class Reponse
    {
        public Reponse(QuizzReponsePossible ra)
        {
            this.contenue = ra.ReponsePossible.ReponsePossible1;
            this.ReponseId = ra.ReponsePossible.ReponsePossibleId;
            this.type = ra.ReponsePossible.OptionQuestionId;
            this.estCorrecte = ra.ReponsePossible.Correct ?? false;
        }
        public Reponse(ReponsePossible ra)
        {
            this.contenue = ra.ReponsePossible1;
            this.ReponseId = ra.ReponsePossibleId;
            this.type = ra.OptionQuestionId;
            this.estCorrecte = ra.Correct ?? false;
        }
        public Reponse()
        {
        }
        public string contenue { get; set; }
        public long ReponseId { get; set; }
        public long type { get; set; }
        public bool? estCorrecte { get; set; }
        public bool? estSelectionner { get; set; }
        
    }
}

