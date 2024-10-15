using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class Technologie
{
    public long TechnologieId { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<QuestionEnregistree> QuestionEnregistrees { get; set; } = new List<QuestionEnregistree>();
}



