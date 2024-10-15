using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFactory.Entities;
using QuestionnaireFactory.Models.CreationQuizz;

namespace QuestionnaireFactory.Controllers
{
    public class AdminQuizzController : Controller
    {
        private readonly QuestionnairefactorydbContext context;

        public AdminQuizzController(QuestionnairefactorydbContext context)
        {
            this.context = context;
        }

        /*
         * 
         * CODE : YAJUAN
         * TODO : Modifier la fonction RemplirListe(quizz);
         * AVEC LE CODE DE YAJUAN
         * En bas de la page
         * 
         */

        // GET: Quizzs
        [Route("/Quizz/Creation")]
        public IActionResult Creation()
        {
            var vm = new CreationQuizzViewModel();
            RemplirListesViewModel(vm);
            return View(vm);
        }


       

        private void RemplirListesViewModel(CreationQuizzViewModel vm)
        {
            var items = context.Candidats.ToList().Select(c => new SelectListItem(c.Prenom + " " + c.Nom, c.CandidatId.ToString()));
            vm.Candidats = items;
            var tech = context.Technologies.ToList().Select(t => new SelectListItem(t.Nom, t.TechnologieId.ToString()));
            vm.Technologies = tech;
            var niveau = context.NiveauQuizzs.ToList().Select(n => new SelectListItem(n.Libelle, n.NiveauQuizzId.ToString()));
            vm.NiveauQuizzs = niveau;
        }

        // GET: Quizzs
        [Route("/Quizz/Creation")]

        [HttpPost]
        public IActionResult Creation(CreationQuizzViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Quizz quizz = new Quizz();
                quizz.AgentId = "A007"; // TODO faire le lien quand y aura les autres agents.
                quizz.NiveauQuizzId = vm.NiveauQuizzId;
                quizz.CandidatId = vm.CandidatId;
                quizz.CodeUrl = Guid.NewGuid().ToString();


                var poolQuestions = context.QuestionEnregistrees
                    .Include(q => q.NiveauQuestion)
                    .Include(q => q.OptionQuestion)
                    .Where(q => q.TechnologieId == vm.TechnologieId)
                    .ToList();

                int NombreNiveau1 = 0;
                int NombreNiveau2 = 0;
                int NombreNiveau3 = 0;

                var nbQuestions = vm.NombreQuestion - vm.NombreQuestionLibre;

                if (vm.NiveauQuizzId == 1)
                {
                    NombreNiveau1 = (int)(0.7 * nbQuestions);
                    NombreNiveau2 = (int)(0.2 * nbQuestions);
                    NombreNiveau3 = (int)(0.1 * nbQuestions);
                }
                else if (vm.NiveauQuizzId == 2)
                {
                    NombreNiveau1 = (int)(0.25 * nbQuestions);
                    NombreNiveau2 = (int)(0.5 * nbQuestions);
                    NombreNiveau3 = (int)(0.25 * nbQuestions);
                }
                else if (vm.NiveauQuizzId == 3) 
                {
                    NombreNiveau1 = (int)(0.1 * nbQuestions);
                    NombreNiveau2 = (int)(0.4 * nbQuestions);
                    NombreNiveau3 = (int)(0.5 * nbQuestions);
                }




                if (NombreNiveau1 + NombreNiveau2 + NombreNiveau3 + vm.NombreQuestionLibre != vm.NombreQuestion)
                {


                    NombreNiveau1 = vm.NombreQuestion - NombreNiveau2 - NombreNiveau3 - vm.NombreQuestionLibre;
                }




                Random r = new Random();
                var questionsNiveau1 = poolQuestions.Where(q => q.NiveauQuestionId == 1 && q.OptionQuestion?.Type != "Libre").OrderBy(q => r.Next()).Take(NombreNiveau1).ToList();
                var questionsNiveau2 = poolQuestions.Where(q => q.NiveauQuestionId == 2 && q.OptionQuestion?.Type != "Libre").OrderBy(q => r.Next()).Take(NombreNiveau2).ToList();
                var questionsNiveau3 = poolQuestions.Where(q => q.NiveauQuestionId == 3 && q.OptionQuestion?.Type != "Libre").OrderBy(q => r.Next()).Take(NombreNiveau3).ToList();
                var questionsLibres = poolQuestions.Where(q => q.NiveauQuestionId == vm.NiveauQuizzId && q.OptionQuestion?.Type == "Libre").OrderBy(q => r.Next()).Take(vm.NombreQuestionLibre).ToList();

              

                var questionsVentilees = questionsNiveau1
                    .Concat(questionsNiveau2)
                    .Concat(questionsNiveau3)
                    .Concat(questionsLibres)
                    .OrderBy(q => r.Next()).ToList();

                short ordre = 1;
                var questionsSelectionnes =
                    questionsVentilees
                        .Select(question => new QuizzQuestionEnregistree { QuestionEnregistreeId = question.QuestionEnregistreeId, OrdreQuestion = ordre++ });
                        
                context.Add(quizz);
                quizz.QuizzQuestionEnregistrees = questionsSelectionnes.ToList();


                context.SaveChanges();

                return RedirectToAction("Details", new { id = quizz.QuizzId });
            }

            RemplirListesViewModel(vm);

            return View(vm);
        }

        //public IActionResult EnvoiMailCandidat(int id)
        //{
        //    var quizz = context.Quizzs.SingleOrDefault(q => q.QuizzId == id);
        //    // TODO ENVOYER LE MAIL
        //    return View(quizz);
        //}


        /*
         * 
         * CODE GENERER PAR VISUAL STUDIO
         * 
         */

        [Route("/Quizz/Index")]
        public async Task<IActionResult> Index()
        {
            var questionnairefactorydbContext = context.Quizzs.Include(q => q.Candidat).Include(q => q.NiveauQuizz);
            return View(await questionnairefactorydbContext.ToListAsync());
        }

        // GET: Quizzs/Details/5
        [Route("/Quizz/Details/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await context.Quizzs
                .Include(q => q.Candidat)
                .Include(q => q.NiveauQuizz)
                .FirstOrDefaultAsync(m => m.QuizzId == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        // POST: Quizzs/Create TODO : A revoir pour la création de quizz avec les questions associées
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Quizz/Create")]
        public async Task<IActionResult> Create([Bind("QuizzId,AgentId,CandidatId,NiveauQuizzId,CodeUrl,PointArret,NomQuizz")] Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                context.Add(quizz);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            RemplirListe(quizz);
            return View(quizz);
        }

        // GET: Quizzs/Edit/5
        [Route("/Quizz/Edit/{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await context.Quizzs.FindAsync(id);
            if (quizz == null)
            {
                return NotFound();
            }
            RemplirListe(quizz);
            return View(quizz);
        }

        // POST: Quizzs/Edit/5

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Quizz/Edit/{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("QuizzId,AgentId,CandidatId,NiveauQuizzId,CodeUrl,PointArret,NomQuizz")] Quizz quizz)
        {
            if (id != quizz.QuizzId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(quizz);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzExists(quizz.QuizzId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            RemplirListe(quizz);
            return View(quizz);

            
        }

        // GET: Quizzs/Delete/5
        [Route("/Quizz/Delete/{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await context.Quizzs
                .Include(q => q.Candidat)
                .Include(q => q.NiveauQuizz)
                .FirstOrDefaultAsync(m => m.QuizzId == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        // POST: Quizzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Quizz/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var quizz = await context.Quizzs.FindAsync(id);
            if (quizz != null)
            {
                context.QuizzQuestionEnregistrees.RemoveRange(context.QuizzQuestionEnregistrees.Where(qqe => qqe.QuizzId == quizz.QuizzId));
                context.Quizzs.Remove(quizz);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzExists(long id)
        {
            return context.Quizzs.Any(e => e.QuizzId == id);
        }

        // TODO 
        // Fonction pour remplir les listes déroulantes
        // A modifier pour prendre en compte des modif de Yajuan

        void RemplirListe(Quizz quizz)
        {
            ViewData["CandidatId"] = new SelectList(context.Candidats, "CandidatId", "Nom", quizz?.CandidatId);
            ViewData["NiveauQuizzId"] = new SelectList(context.NiveauQuizzs, "NiveauQuizzId", "Libelle", quizz?.NiveauQuizzId);
        }
    }
}
