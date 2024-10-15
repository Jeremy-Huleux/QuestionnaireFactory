using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class OptionQuestion
{
    public long OptionQuestionId { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<QuestionEnregistree> QuestionEnregistrees { get; set; } = new List<QuestionEnregistree>();
}
