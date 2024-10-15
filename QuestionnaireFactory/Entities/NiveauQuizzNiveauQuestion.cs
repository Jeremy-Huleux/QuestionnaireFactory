using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class NiveauQuizzNiveauQuestion
{
    public long NiveauQuizzId { get; set; }

    public long NiveauQuestionId { get; set; }

    public short? Pourcentage { get; set; }

    public virtual NiveauQuestion NiveauQuestion { get; set; } = null!;

    public virtual NiveauQuizz NiveauQuizz { get; set; } = null!;
}
