CREATE TABLE [dbo].[QuizzReponsePossible] (
    [QuizzId]           BIGINT        NOT NULL,
    [ReponsePossibleId] BIGINT        NOT NULL,
    [Commentaire]       VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([QuizzId] ASC, [ReponsePossibleId] ASC),
    FOREIGN KEY ([QuizzId]) REFERENCES [dbo].[Quizz] ([QuizzId]),
    FOREIGN KEY ([ReponsePossibleId]) REFERENCES [dbo].[ReponsePossible] ([ReponsePossibleId])
);

