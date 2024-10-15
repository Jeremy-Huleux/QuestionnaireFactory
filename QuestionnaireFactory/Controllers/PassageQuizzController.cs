using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFactory.Entities;
using QuestionnaireFactory.Models.PassageQuizz;
namespace QuestionnaireFactory.Controllers
{

    public class PassageQuizzController : Controller
    {
        private readonly QuestionnairefactorydbContext context;

        public PassageQuizzController(QuestionnairefactorydbContext context)
        {
            this.context = context;
        }

        // GET: PassageQuizzController
        [Route("PassageQuizz/{codeUrl}")]
        public ActionResult PassageQuizz(string? codeUrl)
        {
            if (codeUrl == null)
            {
                return BadRequest("Le code URL est requis.");
            }
            else
            {
                var quizz = context
                    .Quizzs
                    .Include(q => q.QuizzQuestionEnregistrees)
                    .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                    .Include(q => q.QuizzReponsePossibles)
                    .Select(q => new PassageQuizzViewModel
                    {
                        NomQuizz = q.NomQuizz,
                        Candidat = q.Candidat,
                        CodeUrl = q.CodeUrl,
                        NiveauQuizz = q.NiveauQuizz,
                        PointArret = q.PointArret
                    }).FirstOrDefault(q => q.CodeUrl == codeUrl);
                if (quizz == null)
                {
                    return NotFound($"Aucun quizz trouvé avec le code URL : {codeUrl}.");
                }
                else
                {
                    return View(quizz);
                }
            }
        }


        //// GET: PassageQuizzController
        [Route("PassageQuizz/{codeUrl}/{id}")]
        
        public ActionResult Question(string? codeUrl, long? id)
        {
            if (codeUrl == null)
            {
                return BadRequest("Le code URL est requis.");
            }
            else
            {
                var verifQuizzTerminer = context
                    .Quizzs
                    .Include(q => q.QuizzQuestionEnregistrees)
                    .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                    .Include(q => q.QuizzReponsePossibles)
                    .FirstOrDefault(q => q.CodeUrl == codeUrl)
                    .QuizzQuestionEnregistrees
                    .Select(q => q.QuestionEnregistree)
                    .Count();
                if (id == null)
                {
                    return RedirectToAction(nameof(PassageQuizz), new { codeUrl = codeUrl });
                }
                else if(id == 0)
                {
                    var pointArret = context.Quizzs.FirstOrDefault(q => q.CodeUrl == codeUrl);
                    var idPA = pointArret.PointArret;
                    if(id == idPA)
                    {
                        return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                    }
                    
                    else if(id != idPA)
                    {
                       return RedirectToAction(nameof(Question), new { codeUrl = codeUrl, id = idPA });
                    }
                }else
                {
                    if (id == 1)
                    {
                        var pointArretMAJ = context.Quizzs.FirstOrDefault(q => q.CodeUrl == codeUrl);
                        if (pointArretMAJ.PointArret == null)
                        {
                            pointArretMAJ.PointArret = (short)1;
                            context.SaveChanges();
                        }
                        else if(pointArretMAJ.PointArret != null && pointArretMAJ.PointArret != 1)
                        {
                            return RedirectToAction(nameof(PassageQuizz), new { codeUrl = codeUrl });
                        }
                    }
                    var question = context
                    .QuizzQuestionEnregistrees
                    .Where(q => q.OrdreQuestion == id && q.Quizz.CodeUrl == codeUrl && q.Quizz.PointArret == id)
                    .Select(q => new QuestionViewModel
                    {
                        QuestionId = q.QuestionEnregistreeId,
                        ContenuQuestion = q.QuestionEnregistree.ContenuQuestion,
                        Reponses = q.QuestionEnregistree.ReponsePossibles.Select(r => new Reponse(r)).ToList(),
                        TypeQuestion = q.QuestionEnregistree.OptionQuestion.Type,
                        CodeUrl = q.Quizz.CodeUrl
                    }).FirstOrDefault();
                    if (question == null && id > verifQuizzTerminer)
                    {
                        if(id == verifQuizzTerminer + 1)
                        {
                            var pointArretRAZ = context
                                            .Quizzs
                                            .Include(q => q.QuizzQuestionEnregistrees)
                                            .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                                            .Include(q => q.QuizzReponsePossibles)
                                            .FirstOrDefault(q => q.CodeUrl == codeUrl);
                            pointArretRAZ.PointArret = 0;//le point d'arrêt est remis a 0
                            context.SaveChanges();
                            return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                        }
                        else
                        {
                            return RedirectToAction(nameof(PassageQuizz), new { codeUrl = codeUrl });
                        }
                        
                    }
                    else if (question == null)
                    {
                        return NotFound($"Aucune question trouvé à la page {id}");
                    }
                    else
                    {
                        return View(question);
                    }
                }
                return BadRequest("Problème d'enregistrement du point d'arrêt");
            }
        }
        // POST: PassageQuizzController/Edit/5
        [HttpPost]
        [Route("PassageQuizz/{codeUrl}/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionSuivante(string? codeUrl, long? id, long Reponse, string? Commentaire, QuestionViewModel model)
        {
            var typequestion = model.TypeQuestion;
            var checkBoxRep = model.ReponsesCheckbox;
            if(TesteurdUrl(codeUrl, id))
            {
                var majPointArret = context
                    .Quizzs
                    .Include(q => q.QuizzQuestionEnregistrees)
                    .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                    .Include(q => q.QuizzReponsePossibles)
                    .FirstOrDefault(q => q.CodeUrl == codeUrl);
                if (majPointArret != null && id.HasValue && majPointArret.PointArret != 0 )
                {
                    if (typequestion == "Radio")
                    {
                        var nbQuestions = majPointArret.QuizzQuestionEnregistrees.Count();
                        var nouveauPointArret = id.Value + 1;
                        //Verification si le point d'arrêt est égale au nombre de question
                        //Si oui on continue sinon on depasse le nombre de question
                        //donc le quizz est terminé
                        //On retourne dans le controleur Question avec un id = 0
                        if (nbQuestions > 0 && nbQuestions + 1 >= nouveauPointArret)
                        {
                            majPointArret.PointArret = (short)nouveauPointArret;
                            context.Update(majPointArret);
                            context.SaveChanges();
                            var test = context
                                    .QuizzReponsePossibles
                         .Add(new QuizzReponsePossible
                         {
                             QuizzId = majPointArret.QuizzId,
                             ReponsePossibleId = Reponse,
                             Commentaire = Commentaire
                         });
                            context.SaveChanges();
                            return RedirectToAction(nameof(Question), new { codeUrl = codeUrl, id = nouveauPointArret });
                        }
                        else
                        {
                            return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                        }
                    }
                    else if (typequestion == "Checkbox")
                    {
                        var nbQuestions = majPointArret.QuizzQuestionEnregistrees.Count();
                        var nouveauPointArret = id.Value + 1;
                        if (nbQuestions > 0 && nbQuestions + 1 >= nouveauPointArret)
                        {
                            majPointArret.PointArret = (short)nouveauPointArret;
                            context.Update(majPointArret);
                            context.SaveChanges();
                            foreach(var item in checkBoxRep)
                            {
                                var repCheckbox = context
                                    .QuizzReponsePossibles
                                    .Add(new QuizzReponsePossible
                                    {
                                        QuizzId = majPointArret.QuizzId,
                                        ReponsePossibleId = item,
                                        Commentaire = Commentaire
                                    });
                                context.SaveChanges();
                            }
                            return RedirectToAction(nameof(Question), new { codeUrl = codeUrl, id = nouveauPointArret });
                        }
                        else
                        {
                            return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                        }
                    }else if (typequestion == "Libre")
                    {
                        var nbQuestions = majPointArret.QuizzQuestionEnregistrees.Count();
                        var nouveauPointArret = id.Value + 1;
                        if (nbQuestions > 0 && nbQuestions + 1 >= nouveauPointArret)
                        {
                            //Sauvegarde du point d'arret
                            majPointArret.PointArret = (short)nouveauPointArret;
                            context.Update(majPointArret);
                            context.SaveChanges();
                            //Enregistrement dans la base de donnée
                            var saveLibre = context
                                .QuizzQuestionEnregistrees
                                .Where(q => q.OrdreQuestion == id && q.Quizz.CodeUrl == codeUrl && q.QuizzId == majPointArret.QuizzId)
                                .FirstOrDefault();
                            saveLibre.ReponseCandidatLibre = model.ReponseLibre;
                            context.SaveChanges();

                            return RedirectToAction(nameof(Question), new { codeUrl = codeUrl, id = nouveauPointArret });
                        }
                        else
                        {
                            return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                        }
                    }
                    
                }else if (majPointArret.PointArret == 0)
                {
                    return RedirectToAction(nameof(QuizzTermine), new { codeUrl = codeUrl });
                }
                else
                {
                    return BadRequest("Problème d'enregistrement du point d'arrêt");
                }
                //return pour pouvoir build, mais je ne vois pas a quoi il sert donc je retourne sur passage quizz
                return RedirectToAction(nameof(PassageQuizz), new { codeUrl = codeUrl });
            }
            else
            {
                return BadRequest("Le code URL est requis.");
            }

        }
        // GET: PassageQuizzController/Details/5
        [Route("QuizzTermine")]
        public ActionResult QuizzTermine(string? codeUrl)
        {
            if(codeUrl == null)
            {
                return BadRequest("Le code URL est requis.");
            }
            else
            {
                var req = context
                        .Quizzs
                        .Include(q => q.QuizzQuestionEnregistrees)
                        .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                        .Include(q => q.QuizzReponsePossibles)
                        .Where(q => q.CodeUrl == codeUrl)
                        .Select(q => new QuizzTermineViewModel
                        {
                            nomCandidat = q.Candidat.Nom,
                            prenomCandidat = q.Candidat.Prenom,
                            nbQuestions = q.QuizzQuestionEnregistrees.Count(),
                            niveauQuizz = q.NiveauQuizz.Libelle,
                            agent = q.AgentId
                        }).FirstOrDefault();
                return View(req);
            }
           
        }

        public bool TesteurdUrl(string? UrlCode, long? id)
        {
            if (UrlCode == null)
            {
                return false;
            }
            else { 
                var verifUrl = context.Quizzs.FirstOrDefault(q => q.CodeUrl == UrlCode);
                if (verifUrl == null)
                {
                    return false;
                }
                else if(verifUrl != null)
                {
                    if (id == null)
                    {
                        return false;
                    }
                    else
                    {
                        var verifId = context
                            .QuizzQuestionEnregistrees
                            .Where(q => q.OrdreQuestion == id && q.Quizz.CodeUrl == UrlCode);
                        if(verifId == null)
                        {
                            return false;
                        }
                        else if(verifId != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }



        // GET: PassageQuizzController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassageQuizzController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassageQuizzController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



        // GET: PassageQuizzController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PassageQuizzController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
