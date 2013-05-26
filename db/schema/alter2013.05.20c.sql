/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2013-05-20 20:56                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Certifications_DiverCertifications]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [Contacts_Instructors]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [DiveAgencies_Instructors]
GO


/* ---------------------------------------------------------------------- */
/* Modify table "DiverCertifications"                                     */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP COLUMN [InstructorId]
GO


/* ---------------------------------------------------------------------- */
/* Drop and recreate table "Instructors"                                  */
/* ---------------------------------------------------------------------- */

/* Table must be recreated because some of the changes can't be done with the regular commands available. */

ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [DEF_Instructors_InstructorNumber]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [PK_Instructors]
GO


CREATE TABLE [Instructors_TMP] (
    [InstructorId] INTEGER IDENTITY(0,1) NOT NULL,
    [ContactId] INTEGER NOT NULL,
    [DiveAgencyId] INTEGER NOT NULL,
    [InstructorNumber] VARCHAR(10) CONSTRAINT [DEF_Instructors_InstructorNumber] DEFAULT ' ',
    [Notes] VARCHAR(max))
GO



SET IDENTITY_INSERT [Instructors_TMP] ON
GO



INSERT INTO [Instructors_TMP]
    ([InstructorId],[ContactId],[DiveAgencyId],[InstructorNumber],[Notes])
SELECT
    [InstructorId],[ContactId],[DiveAgencyId],[InstructorNumber],[Notes]
FROM [dbo].[Instructors]
GO



SET IDENTITY_INSERT [Instructors_TMP] OFF
GO



DROP TABLE [dbo].[Instructors]
GO


EXEC sp_rename '[Instructors_TMP]', 'Instructors', 'OBJECT'
GO


ALTER TABLE [Instructors] ADD CONSTRAINT [PK_Instructors] 
    PRIMARY KEY ([InstructorId], [ContactId], [DiveAgencyId])
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', NULL, NULL
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'InstructorId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'ContactId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'DiveAgencyId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'InstructorNumber'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N, 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'Notes'
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


ALTER TABLE [Instructors] ADD CONSTRAINT [Contacts_Instructors] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [Instructors] ADD CONSTRAINT [DiveAgencies_Instructors] 
    FOREIGN KEY ([DiveAgencyId]) REFERENCES [dbo].[DiveAgencies] ([DiveAgencyId])
GO

