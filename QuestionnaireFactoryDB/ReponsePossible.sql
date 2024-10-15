CREATE TABLE [dbo].[ReponsePossible] (
    [ReponsePossibleId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [QuestionEnregistreeId] BIGINT        NULL,
    [Correct]               BIT           NULL,
    [ReponsePossible]       VARCHAR (200) NULL,
    [TechnologieId]         BIGINT        NOT NULL,
    [OptionQuestionId]      BIGINT        NOT NULL,
    [NiveauQuestionId]      BIGINT        NOT NULL,
    PRIMARY KEY CLUSTERED ([ReponsePossibleId] ASC),
    FOREIGN KEY ([QuestionEnregistreeId]) REFERENCES [dbo].[QuestionEnregistree] ([QuestionEnregistreeId])
);

