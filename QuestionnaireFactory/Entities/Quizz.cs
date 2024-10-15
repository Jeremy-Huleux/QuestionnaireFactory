using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireFactory.Entities;

public partial class Quizz
{
   
    public long QuizzId { get; set; }

    public string? AgentId { get; set; }

    public long? CandidatId { get; set; }

    public long? NiveauQuizzId { get; set; }

    public string? CodeUrl { get; set; }

    public short? PointArret { get; set; }

    public string? NomQuizz { get; set; }

    public virtual Candidat? Candidat { get; set; }

    public virtual NiveauQuizz? NiveauQuizz { get; set; }

    public virtual ICollection<QuizzQuestionEnregistree> QuizzQuestionEnregistrees { get; set; } = new List<QuizzQuestionEnregistree>();

    public virtual ICollection<QuizzReponsePossible> QuizzReponsePossibles { get; set; } = new List<QuizzReponsePossible>();
}
