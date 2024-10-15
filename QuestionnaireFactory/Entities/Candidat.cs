using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class Candidat
{
    public long CandidatId { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public string? Email { get; set; }

    public string? AgentId { get; set; }

    public virtual ICollection<Quizz> Quizzs { get; set; } = new List<Quizz>();
}
