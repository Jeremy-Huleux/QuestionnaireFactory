CREATE TABLE [dbo].[NiveauQuestion] (
    [NiveauQuestionId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [Libelle]          VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([NiveauQuestionId] ASC)
);

