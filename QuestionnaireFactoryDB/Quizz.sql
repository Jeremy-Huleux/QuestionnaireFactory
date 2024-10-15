CREATE TABLE [dbo].[Quizz] (
    [QuizzId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [AgentId]       VARCHAR (50)  NULL,
    [CandidatId]    BIGINT        NULL,
    [NiveauQuizzId] BIGINT        NULL,
    [CodeUrl]       VARCHAR (50)  NULL,
    [PointArret]    SMALLINT      NULL,
    [NomQuizz]      VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([QuizzId] ASC),
    FOREIGN KEY ([CandidatId]) REFERENCES [dbo].[Candidat] ([CandidatId]),
    FOREIGN KEY ([NiveauQuizzId]) REFERENCES [dbo].[NiveauQuizz] ([NiveauQuizzId])
);

