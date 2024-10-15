using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionnaireFactory.Controllers;
using QuestionnaireFactory.Services.ListeCandidat;

namespace QuestionnaireFactory.UnitTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Afficher_view_lors_appel_index()
        {
            // Setup
            var logger = new Logger<HomeController>(new LoggerFactory());
            var listeCandidat = new ListeCandidat(null); 

            var controller = new HomeController(logger, listeCandidat);


            //act
            var result = controller.Index();

            //assert
            Assert.IsType<ViewResult>(result);
        }
    }
}