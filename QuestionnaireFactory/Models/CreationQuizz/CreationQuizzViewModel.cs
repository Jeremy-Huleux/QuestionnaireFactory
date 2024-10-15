using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using QuestionnaireFactory.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace QuestionnaireFactory.Models.CreationQuizz
{
    public class CreationQuizzViewModel
    {
        public IEnumerable<SelectListItem>? Candidats { get; set; }

        [DisplayName ("Candidat")]
        public int CandidatId { get; set; }

        [DisplayName("Technologie")]
        public int TechnologieId { get; set; }

        public IEnumerable<SelectListItem>? Technologies { get; set; }

        [DisplayName("Niveau du Quizz")]
        public int NiveauQuizzId { get; set; }

        public IEnumerable<SelectListItem>? NiveauQuizzs { get; set; }

        [Range(10, 40, ErrorMessage = "Le Nombre de questions doit être compris entre 10 et 40")]

        [DisplayName("Nombre de Questions au total")]
        public int NombreQuestion { get; set; }

        [Range(0, 5, ErrorMessage = "Le Nombre de questions libres doit être compris entre 0 et 5")]

        [DisplayName("Nombre de Questions libres")]
        public int NombreQuestionLibre { get; set; }

        
    }
}
