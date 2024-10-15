using Microsoft.AspNetCore.Mvc;
using QuestionnaireFactory.Models;
using QuestionnaireFactory.Services.ListeCandidat;
using System.Diagnostics;


namespace QuestionnaireFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ListeCandidat listeCandidat;

        public HomeController(ILogger<HomeController> logger, ListeCandidat listeCandidat)
        {
            this.logger = logger;
            this.listeCandidat = listeCandidat;
        }

        public IActionResult Index()
        {
            //Log pour chaque connexion en page d'accueil
            logger.LogInformation("Page d'accueil vue par quelqu'un");

            // Récupérer la liste des candidats à partir du service ListeCandidat
            var candidats = listeCandidat.GetCandidats();
            return View(candidats); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
