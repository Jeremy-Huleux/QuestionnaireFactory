using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class ReponsePossible
{
    public long ReponsePossibleId { get; set; }

    public long? QuestionEnregistreeId { get; set; }

    public bool? Correct { get; set; }

    public string? ReponsePossible1 { get; set; }

    public long TechnologieId { get; set; }

    public long OptionQuestionId { get; set; }

    public long NiveauQuestionId { get; set; }

    public virtual QuestionEnregistree? QuestionEnregistree { get; set; }

    public virtual ICollection<QuizzReponsePossible> QuizzReponsePossibles { get; set; } = new List<QuizzReponsePossible>();
}
