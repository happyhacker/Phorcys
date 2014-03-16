/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.3.6                     */
/* Target DBMS:           MS SQL Server 2012                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2014-03-15 18:18                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Certifications_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Instructors_DiverCertifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop and recreate table "dbo.DiverCertifications"                      */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [DEF_DiverCertifications_Created]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [DEF_DiverCertifications_LastModified]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [PK_DiverCertifications]
GO


CREATE TABLE [dbo].[DiverCertifications_TMP] (
    [DiverCertificationId] INTEGER IDENTITY(1,1) NOT NULL,
    [DiverId] INTEGER NOT NULL,
    [CertificationId] INTEGER NOT NULL,
    [Certified] DATE,
    [CertificationNum] VARCHAR(20) NOT NULL,
    [Notes] VARCHAR(max),
    [Created] DATETIME CONSTRAINT [DEF_DiverCertifications_Created] DEFAULT getdate() NOT NULL,
    [LastModified] DATETIME CONSTRAINT [DEF_DiverCertifications_LastModified] DEFAULT getdate() NOT NULL,
    [InstructorId] INTEGER)
GO


INSERT INTO [dbo].[DiverCertifications_TMP]
    ([DiverId],[CertificationId],[Certified],[CertificationNum],[Notes],[Created],[LastModified],[InstructorId])
SELECT
    [DiverId],[CertificationId],[Certified],[CertificationNum],[Notes],[Created],[LastModified],[InstructorId]
FROM [dbo].[DiverCertifications]
GO


DROP TABLE [dbo].[DiverCertifications]
GO


EXEC sp_rename '[dbo].[DiverCertifications_TMP]', 'DiverCertifications', 'OBJECT'
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [PK_DiverCertifications] 
    PRIMARY KEY CLUSTERED ([DiverCertificationId])
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', NULL, NULL
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'DiverCertificationId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'DiverId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'CertificationId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Certified'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'CertificationNum'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Notes'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Created'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'LastModified'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'InstructorId'
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Divers_DiverCertifications] 
    FOREIGN KEY ([DiverId]) REFERENCES [dbo].[Divers] ([DiverId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Certifications_DiverCertifications] 
    FOREIGN KEY ([CertificationId]) REFERENCES [dbo].[Certifications] ([CertificationId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Instructors_DiverCertifications] 
    FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructors] ([InstructorId])
GO

