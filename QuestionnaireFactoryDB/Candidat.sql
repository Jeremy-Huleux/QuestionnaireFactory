CREATE TABLE [dbo].[Candidat] (
    [CandidatId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [Nom]        VARCHAR (50) NULL,
    [Prenom]     VARCHAR (50) NULL,
    [Email]      VARCHAR (50) NULL,
    [AgentId]    VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([CandidatId] ASC)
);

