using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFactory.Entities;
using QuestionnaireFactory.Models;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;


namespace QuestionnaireFactory.Controllers
{
	public class CandidatController : Controller
	{
		private readonly QuestionnairefactorydbContext context;

		public CandidatController(QuestionnairefactorydbContext context)
		{
			this.context = context;
		}

		// /Candidat/Index/1
		public IActionResult Index(int id)
		{
			var q = context.Quizzs
				.Include(q => q.Candidat)
				.Include(q => q.NiveauQuizz)
				.Include(q => q.QuizzQuestionEnregistrees)
				.ThenInclude(qqe => qqe.QuestionEnregistree)
						.ThenInclude(qe => qe.Technologie) // Inclure la technologie via QuestionEnregistree
				.Include("QuizzQuestionEnregistrees.QuestionEnregistree")
				.Include(q => q.QuizzReponsePossibles)
				.Include("QuizzReponsePossibles.ReponsePossible")

				.SingleOrDefault(q => q.QuizzId == id);

			if (q == null)
			{
				return NotFound();
			}


			var listQuestionsReponsesCorrectes = new List<QuestionEnregistree>();
			var listQuestionsReponsesIncorrectes = new List<QuestionEnregistree>();
			bool quizzTermine = q.PointArret == 0;

			foreach (var question in q.QuizzQuestionEnregistrees)
			{
				var reponsesPossibles = question.QuestionEnregistree.ReponsePossibles;
				bool bonnesReponses = true;
				foreach (var reponse in reponsesPossibles)
				{
					if (reponse.Correct == true)
					{
						// vérifier que la réponse a été sélectionnée par le candidat
						if (!q.QuizzReponsePossibles.Any(r => r.ReponsePossibleId == reponse.ReponsePossibleId))
						{
							bonnesReponses = false;
							break;
						}
					}
					else
					{
						// vérifier que la réponse n'a PAS été sélectionnée par le candidat
						if (q.QuizzReponsePossibles.Any(r => r.ReponsePossibleId == reponse.ReponsePossibleId))
						{
							bonnesReponses = false;
							break;
						}
					}
				}
				if (bonnesReponses)
				{
					listQuestionsReponsesCorrectes.Add(question.QuestionEnregistree);
				}
				else
				{
					listQuestionsReponsesIncorrectes.Add(question.QuestionEnregistree);
				}
			}

			return View(new RestitutionViewModel
			{
				Quizz = q,
				ReponsesCorrectes = listQuestionsReponsesCorrectes,
				ReponsesIncorrectes = listQuestionsReponsesIncorrectes
			});
		}

		
        public IActionResult Pdf(int id)
        {
            var q = context.Quizzs
                .Include(q => q.Candidat)
                .Include(q => q.NiveauQuizz)
                .Include(q => q.QuizzQuestionEnregistrees)
                .ThenInclude(qqe => qqe.QuestionEnregistree)
                        .ThenInclude(qe => qe.Technologie) // Inclure la technologie via QuestionEnregistree
                .Include("QuizzQuestionEnregistrees.QuestionEnregistree")
                .Include(q => q.QuizzReponsePossibles)
                .Include("QuizzReponsePossibles.ReponsePossible")

                .SingleOrDefault(q => q.QuizzId == id);

            if (q == null)
            {
                return NotFound();
            }


            var listQuestionsReponsesCorrectes = new List<QuestionEnregistree>();
            var listQuestionsReponsesIncorrectes = new List<QuestionEnregistree>();
            bool quizzTermine = q.PointArret == 0;

            foreach (var question in q.QuizzQuestionEnregistrees)
            {
                var reponsesPossibles = question.QuestionEnregistree.ReponsePossibles;
                bool bonnesReponses = true;
                foreach (var reponse in reponsesPossibles)
                {
                    if (reponse.Correct == true)
                    {
                        // vérifier que la réponse a été sélectionnée par le candidat
                        if (!q.QuizzReponsePossibles.Any(r => r.ReponsePossibleId == reponse.ReponsePossibleId))
                        {
                            bonnesReponses = false;
                            break;
                        }
                    }
                    else
                    {
                        // vérifier que la réponse n'a PAS été sélectionnée par le candidat
                        if (q.QuizzReponsePossibles.Any(r => r.ReponsePossibleId == reponse.ReponsePossibleId))
                        {
                            bonnesReponses = false;
                            break;
                        }
                    }
                }
                if (bonnesReponses)
                {
                    listQuestionsReponsesCorrectes.Add(question.QuestionEnregistree);
                }
                else
                {
                    listQuestionsReponsesIncorrectes.Add(question.QuestionEnregistree);
                }
            }

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12)); // Adjusted for better readability

                    page.Header()
                        .Text("Résultat du questionnaire de recrutement")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium); // Adjusted font size

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(5); // Reduced spacing for a compact look

                            // Candidate Information
                            column.Item().Element(ComposeCandidateInformation);

                            // Questions and Answers
                            column.Item().Element(ComposeQuestionsAndAnswers);

                            // Score
                            column.Item().Element(ComposeScore);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });
            void ComposeCandidateInformation(IContainer container)
            {
                container.Column(column =>
                {
                    column.Spacing(10); // Increased spacing
                    column.Item().Text($"Candidat: {q.Candidat.Nom} {q.Candidat.Prenom}").Bold();
                    column.Item().Text($"Technologie: {q.QuizzQuestionEnregistrees.First().QuestionEnregistree.Technologie.Nom}").Bold();
                    column.Item().Text($"Niveau du Quizz: {q.NiveauQuizz.Libelle}").Bold();
                });
            }

            void ComposeQuestionsAndAnswers(IContainer container)
            {
                container.Column(column =>
                {
                    column.Spacing(10); // Increased spacing
                    column.Item().Text($"Nombre de questions au total: {q.QuizzQuestionEnregistrees.Count}").Bold();
                    column.Item().Text($"Nombre de questions correctes: {listQuestionsReponsesCorrectes.Count}").Bold();
                    column.Item().Text($"Nombre de questions incorrectes: {listQuestionsReponsesIncorrectes.Count}").Bold();

                    // Correct Questions
                    column.Item().Text("Liste des questions correctes:").Bold().FontSize(16);
                    foreach (var reponse in listQuestionsReponsesCorrectes)
                    {
                        column.Item().Text($"Question: {reponse.ContenuQuestion}");
                    }

                    // Incorrect Questions
                    column.Item().Text("");
                    column.Item().Text("Liste des questions incorrectes:").Bold().FontSize(16);
                    foreach (var reponse in listQuestionsReponsesIncorrectes)
                    {
                        column.Item().Text($"Question: {reponse.ContenuQuestion}");
                        column.Item().Text($"Explication: {reponse.Explication}");
                    }
                });
            }

            void ComposeScore(IContainer container)
            {
                var correctCount = listQuestionsReponsesCorrectes.Count();
                var totalCount = q.QuizzQuestionEnregistrees.Count;
                var score = (correctCount / (float)totalCount) * 100; // Ensure float division

                var scoreTextStyle = score >= 60 ? TextStyle.Default.FontColor(Colors.Green.Medium).Bold() : TextStyle.Default.FontColor(Colors.Red.Medium).Bold();

                container.Text($"Pourcentage de réussite: {score}%", style: scoreTextStyle);
            }
            QuestPDF.Settings.License = LicenseType.Community;
            // Conversion du document PDF en un flux de données
            using (var stream = new MemoryStream())
            {
                pdf.GeneratePdf(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Retourner le PDF comme un fichier à télécharger ou à afficher dans le navigateur
                return File(stream.ToArray(), "application/pdf", "ResultatQuestionnaire.pdf");
            }

        }
    }
}
