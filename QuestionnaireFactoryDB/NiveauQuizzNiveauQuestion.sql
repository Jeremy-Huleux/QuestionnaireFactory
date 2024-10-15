CREATE TABLE [dbo].[NiveauQuizzNiveauQuestion] (
    [NiveauQuizzId]    BIGINT   NOT NULL,
    [NiveauQuestionId] BIGINT   NOT NULL,
    [Pourcentage]      SMALLINT NULL,
    PRIMARY KEY CLUSTERED ([NiveauQuizzId] ASC, [NiveauQuestionId] ASC),
    FOREIGN KEY ([NiveauQuestionId]) REFERENCES [dbo].[NiveauQuestion] ([NiveauQuestionId]),
    FOREIGN KEY ([NiveauQuizzId]) REFERENCES [dbo].[NiveauQuizz] ([NiveauQuizzId])
);

