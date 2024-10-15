CREATE TABLE [dbo].[QuizzQuestionEnregistree] (
    [QuizzId]               BIGINT        NOT NULL,
    [QuestionEnregistreeId] BIGINT        NOT NULL,
    [OrdreQuestion]         SMALLINT      NULL,
    [ReponseCandidatLibre]  VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([QuizzId] ASC, [QuestionEnregistreeId] ASC),
    FOREIGN KEY ([QuestionEnregistreeId]) REFERENCES [dbo].[QuestionEnregistree] ([QuestionEnregistreeId]),
    FOREIGN KEY ([QuizzId]) REFERENCES [dbo].[Quizz] ([QuizzId])
);

