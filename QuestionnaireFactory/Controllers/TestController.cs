using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using QuestionnaireFactory.Entities;


namespace QuestionnaireFactory.Controllers
{
    public class TestController : Controller
    {
        private readonly QuestionnairefactorydbContext context;

        public TestController(QuestionnairefactorydbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
