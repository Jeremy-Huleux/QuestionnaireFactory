using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class QuizzQuestionEnregistree
{
    public long QuizzId { get; set; }

    public long QuestionEnregistreeId { get; set; }

    public short? OrdreQuestion { get; set; }

    public string? ReponseCandidatLibre { get; set; }

    public virtual QuestionEnregistree QuestionEnregistree { get; set; } = null!;

    public virtual Quizz Quizz { get; set; } = null!;
}
