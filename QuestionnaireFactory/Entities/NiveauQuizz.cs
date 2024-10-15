using System;
using System.Collections.Generic;

namespace QuestionnaireFactory.Entities;

public partial class NiveauQuizz
{
    public long NiveauQuizzId { get; set; }

    public string? Libelle { get; set; }   

    public virtual ICollection<NiveauQuizzNiveauQuestion> NiveauQuizzNiveauQuestions { get; set; } = new List<NiveauQuizzNiveauQuestion>();

    public virtual ICollection<Quizz> Quizzs { get; set; } = new List<Quizz>();
}

