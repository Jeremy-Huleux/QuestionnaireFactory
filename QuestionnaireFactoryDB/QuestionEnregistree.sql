CREATE TABLE [dbo].[QuestionEnregistree] (
    [QuestionEnregistreeId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [TechnologieId]         BIGINT        NULL,
    [OptionQuestionId]      BIGINT        NULL,
    [NiveauQuestionId]      BIGINT        NULL,
    [ContenuQuestion]       VARCHAR (MAX) NULL,
    [Explication]           VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([QuestionEnregistreeId] ASC),
    FOREIGN KEY ([NiveauQuestionId]) REFERENCES [dbo].[NiveauQuestion] ([NiveauQuestionId]),
    FOREIGN KEY ([OptionQuestionId]) REFERENCES [dbo].[OptionQuestion] ([OptionQuestionId]),
    FOREIGN KEY ([TechnologieId]) REFERENCES [dbo].[Technologie] ([TechnologieId])
);

