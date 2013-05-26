/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2013-05-20 21:02                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Certifications_DiverCertifications]
GO


/* ---------------------------------------------------------------------- */
/* Modify table "DiverCertifications"                                     */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] ADD
    [InstructorId] INTEGER
GO


ALTER TABLE [dbo].[DiverCertifications] ADD
    [ContactId] INTEGER
GO


ALTER TABLE [dbo].[DiverCertifications] ADD
    [DiveAgencyId] INTEGER
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
    FOREIGN KEY ([InstructorId], [ContactId], [DiveAgencyId]) REFERENCES [dbo].[Instructors] ([InstructorId],[ContactId],[DiveAgencyId])
GO

