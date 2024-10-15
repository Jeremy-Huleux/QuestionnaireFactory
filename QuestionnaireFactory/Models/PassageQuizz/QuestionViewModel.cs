using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireFactory.Models.PassageQuizz
{
    public class QuestionViewModel
    {
        [Key]
        public long QuestionId { get; set; }
        [DisplayName("Question :")]
        public string ContenuQuestion { get; set; }

        public List<Reponse> Reponses { get; set; }
        public string TypeQuestion { get; set; }
        public string CodeUrl { get; set; }
        public string? Commentaire { get; set; }
        public List<long> ReponsesCheckbox { get; set; }
        public string? ReponseLibre { get; set; }

    }

}