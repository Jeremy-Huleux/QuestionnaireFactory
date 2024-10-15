using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class QuizzReponsePossible
{
    public long QuizzId { get; set; }

    public long ReponsePossibleId { get; set; }

    public string? Commentaire { get; set; }

    public virtual Quizz Quizz { get; set; } = null!;

    public virtual ReponsePossible ReponsePossible { get; set; } = null!;
}
