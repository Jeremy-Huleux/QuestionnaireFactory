using QuestionnaireFactory.Entities;

namespace QuestionnaireFactory.Models
{
	public class RestitutionViewModel
	{
        public Quizz Quizz { get; set; }

		public List<QuestionEnregistree> ReponsesCorrectes { get; set; }
		public List<QuestionEnregistree> ReponsesIncorrectes { get; set; }
	}

}
