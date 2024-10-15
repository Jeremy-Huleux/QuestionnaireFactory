namespace QuestionnaireFactory.Models.PassageQuizz
{
    public class QuizzTermineViewModel
    {
        public string? nomCandidat{ get; set; }
        public string? prenomCandidat{ get; set; }
        public int? nbQuestions{ get; set; }
        public string? niveauQuizz { get; set; }
        public string? agent { get; set; }
        /*
        [Key]
        public long QuestionId { get; set; }
        public string ContenuQuestion { get; set; }

        public List<Reponse> Reponses { get; set; }
        public string TypeQuestion { get; set; }
        public string CodeUrl { get; set; }
        public string? Commentaire { get; set; }

         */
    }
}
