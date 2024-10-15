using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class NiveauQuestion
{
    public long NiveauQuestionId { get; set; }

    public string? Libelle { get; set; }

    public virtual ICollection<NiveauQuizzNiveauQuestion> NiveauQuizzNiveauQuestions { get; set; } = new List<NiveauQuizzNiveauQuestion>();

    public virtual ICollection<QuestionEnregistree> QuestionEnregistrees { get; set; } = new List<QuestionEnregistree>();
}
