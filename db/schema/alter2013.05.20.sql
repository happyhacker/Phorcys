/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.3.6                     */
/* Target DBMS:           MS SQL Server 2012                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2014-01-07 23:16                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [Contacts_Instructors]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Instructors_DiverCertifications]
GO


ALTER TABLE [dbo].[AgencyInstructors] DROP CONSTRAINT [Instructors_AgencyInstructors]
GO


/* ---------------------------------------------------------------------- */
/* Drop and recreate table "dbo.Instructors"                              */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [PK_Instructors]
GO


CREATE TABLE [dbo].[Instructors_TMP] (
    [InstructorId] INTEGER IDENTITY(1,1) NOT NULL,
    [ContactId] INTEGER NOT NULL,
    [Notes] VARCHAR(max))
GO



SET IDENTITY_INSERT [dbo].[Instructors_TMP] ON
GO



INSERT INTO [dbo].[Instructors_TMP]
    ([InstructorId],[ContactId],[Notes])
SELECT
    [InstructorId],[ContactId],[Notes]
FROM [dbo].[Instructors]
GO



SET IDENTITY_INSERT [dbo].[Instructors_TMP] OFF
GO



DROP TABLE [dbo].[Instructors]
GO


EXEC sp_rename '[dbo].[Instructors_TMP]', 'Instructors', 'OBJECT'
GO


ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [PK_Instructors] 
    PRIMARY KEY CLUSTERED ([InstructorId])
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', NULL, NULL
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'InstructorId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'ContactId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'Notes'
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [Contacts_Instructors] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Instructors_DiverCertifications] 
    FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructors] ([InstructorId])
GO


ALTER TABLE [dbo].[AgencyInstructors] ADD CONSTRAINT [Instructors_AgencyInstructors] 
    FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructors] ([InstructorId])
GO

