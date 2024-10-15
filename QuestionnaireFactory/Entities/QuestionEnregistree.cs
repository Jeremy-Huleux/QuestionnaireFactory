using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class QuestionEnregistree
{
    public long QuestionEnregistreeId { get; set; }

    public long? TechnologieId { get; set; }

    public long? OptionQuestionId { get; set; }

    public long? NiveauQuestionId { get; set; }

    public string? ContenuQuestion { get; set; }

    public string? Explication { get; set; }

    public virtual NiveauQuestion? NiveauQuestion { get; set; }

    public virtual OptionQuestion? OptionQuestion { get; set; }

    public virtual ICollection<QuizzQuestionEnregistree> QuizzQuestionEnregistrees { get; set; } = new List<QuizzQuestionEnregistree>();

    public virtual ICollection<ReponsePossible> ReponsePossibles { get; set; } = new List<ReponsePossible>();

    public virtual Technologie? Technologie { get; set; }
}
