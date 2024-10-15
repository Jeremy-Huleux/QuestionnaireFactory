using Microsoft.EntityFrameworkCore;
using QuestionnaireFactory.Entities;
using System.Collections.Generic;
using System.Linq;


namespace QuestionnaireFactory.Services.ListeCandidat
{
    public class ListeCandidat
    {

        private readonly QuestionnairefactorydbContext _context;

        public ListeCandidat(QuestionnairefactorydbContext context)
        {
            _context = context;
        }

        // Service qui génère la liste des candidats et le ou les quizz qu'ils ont passé.
        // Un candidat peu apparaître plusieurs fois car il peut passer des Quizz différents.
        public IEnumerable<Candidat> GetCandidats()
        {
            // Si _context est null, renvoie une liste vide pour les tests
            if (_context == null)
            {
                return new List<Candidat>();
            }
            return _context.Candidats
                           .Include(c => c.Quizzs)
                           .ThenInclude(q => q.NiveauQuizz)
                           .Include(c => c.Quizzs)
                           .ThenInclude(q => q.QuizzQuestionEnregistrees)
                           .ThenInclude(qqe => qqe.QuestionEnregistree)
                           .ToList();
        }
    }
}

