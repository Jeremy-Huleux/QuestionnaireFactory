using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol.Core.Types;
using QuestionnaireFactory.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireFactory.Models.PassageQuizz
{
    public class PassageQuizzViewModel
    { 

        [DisplayName("Nom du Quizz")]
        public string NomQuizz { get; set;  }

        public virtual Candidat Candidat { get; set; }

        public string CodeUrl { get; set; }

        [DisplayName("Niveau du Quizz")]
        public virtual NiveauQuizz NiveauQuizz { get; set; }
        public short? PointArret { get; set; }
    }
}

//    public virtual ICollection<QuizzQuestionEnregistree> QuizzQuestionEnregistrees { get; set;  } = new List<QuizzQuestionEnregistree>();
//    public List<string?> ContenusQuestions { get; set; } = new List<string?>();
//    public void RemplirContenusQuestions()
//    {
//        if (QuizzQuestionEnregistrees != null && QuizzQuestionEnregistrees.Any())
//        {
//            //QuestionsAll = new PasseQuizz(QuizzQuestionEnregistrees.ToList());
//            //ReponsesAll = new ReponsesPossibleAll(QuizzReponsePossibles);
//            foreach (var q in QuizzQuestionEnregistrees)
//            {
//                Questiontest.Add(new Question(q));
//            }


//        }
//        if(QuizzReponsePossibles != null && QuizzReponsePossibles.Any()){
//            foreach (var r in QuizzReponsePossibles)
//            {
//                ReponseTest.Add(new Reponse(r));
//            }
//        }

//    }

//    /*
//     * 
//     * WIP
//     * public Dictionnary<Question, List<Reponse>>
//     */
//    //public PasseQuizz QuestionsAll { get; set; } = new PasseQuizz();
//    //public ReponsesPossibleAll ReponsesAll { get; set; } = new ReponsesPossibleAll();
//    public virtual ICollection<QuizzReponsePossible> QuizzReponsePossibles { get; set; } = new List<QuizzReponsePossible>();
//    public List<Question> Questiontest { get; set; } = new List<Question>();
//    public List<Reponse> ReponseTest { get; set; } = new List<Reponse>();

//    public int ReponsePossibles { get; set; }

//    //public Dictionary<Question, List<Reponse>>)  { get; set; } = new Dictionary<Question, List<Reponse>>();
//}
//public class ReponsesPossibleAll
//{
//    public ReponsesPossibleAll()
//    {
//    }
//    public ReponsesPossibleAll(ICollection<QuizzReponsePossible> r)
//    {
//        if (r != null && r.Any())
//        {
//            List<string> Contenu = r.Select(r => r.ReponsePossible).ToList()
//                                    .Select(t => t.ReponsePossible1.ToString()).ToList();

//            IdReponse = r.Select(r => r.ReponsePossible).ToList()
//                         .Select(t => t.ReponsePossibleId).ToList();

//            List<long> TypeReponseId = r.Select(r => r.ReponsePossible).ToList()
//                                        .Select(t => t.OptionQuestionId).ToList();


//            if (IdReponse.Count == Contenu.Count)
//            {
//                for (int i = 0; i < IdReponse.Count; i++)
//                {
//                    ContenuReponse.Add(IdReponse[i], Contenu[i]);
//                    TypeReponse.Add(IdReponse[i], TypeReponseId[i]);

//                }
//            }

//        }

//    }

//    public Dictionary<long, string> ContenuReponse { get; set; } = new Dictionary<long, string>();
//    public Dictionary<long, long> TypeReponse { get; set; } = new Dictionary<long, long>();

//    public List<long> IdReponse { get; set; }


//}


////public List<ReponsePossibleQuestion> funcRecupérationGroupeReponses(UneQuestion q)
////{

////    return new List<ReponsePossibleQuestion>(); }
////}

//public class PasseQuizz
//{
//    public PasseQuizz()
//    {

//    }
//    public PasseQuizz(List<QuizzQuestionEnregistree> q)
//    {

//        if (q != null && q.Any())
//        {

//            List<string> Contenu = q.Select(q => q.QuestionEnregistree).ToList()
//                        .Select(t => t.ContenuQuestion.ToString()).ToList();

//            List<long> IdQuestion = q.Select(q => q.QuestionEnregistree).ToList()
//                                .Select(t => t.QuestionEnregistreeId).ToList();

//            if (IdQuestion.Count == Contenu.Count)
//            {
//                for (int i = 0; i < IdQuestion.Count; i++)
//                {
//                    ContenuQuestion.Add(IdQuestion[i], Contenu[i]);
//                    Questions.Add(new Question(q[i]));
//                }
//            }

//        }
//    }

//    public Dictionary<long, string> ContenuQuestion { get; set; } = new Dictionary<long, string>();
//    public Dictionary<long, long> ReponseCandidat { get; set; } = new Dictionary<long, long>();
//    public Dictionary<long, string> ReponsePossible { get; set; } = new Dictionary<long, string>();
//    public List<Question> Questions { get; set; } = new List<Question>();


//    public bool EstCorrecte { get; set; }
//    public string Explication { get; set; }

