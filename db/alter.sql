/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases 12.3.1                     */
/* Target DBMS:           MS SQL Server 2017                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2023-12-13 22:36                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [DiveAgencies_AgencyInstructors]
GO


ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [Instructors_AgencyInstructors]
GO


ALTER TABLE [Tanks] DROP CONSTRAINT [Gear_Tanks]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "AgencyInstructors"                                         */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [PK_AgencyInstructors]
GO


DROP TABLE [AgencyInstructors]
GO


/* ---------------------------------------------------------------------- */
/* Add table "CertificationInstructors"                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE [CertificationInstructors] (
    [InstructorId] INTEGER NOT NULL,
    [CertificationId] INTEGER NOT NULL,
    CONSTRAINT [PK_CertificationInstructors] PRIMARY KEY CLUSTERED ([InstructorId], [CertificationId])
)
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Tanks] ADD CONSTRAINT [Gear_Tanks] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId]) ON DELETE CASCADE ON UPDATE CASCADE
GO


ALTER TABLE [CertificationInstructors] ADD CONSTRAINT [Instructors_CertificationInstructors] 
    FOREIGN KEY ([InstructorId]) REFERENCES [Instructors] ([InstructorId])
GO


ALTER TABLE [CertificationInstructors] ADD CONSTRAINT [Certifications_CertificationInstructors] 
    FOREIGN KEY ([CertificationId]) REFERENCES [Certifications] ([CertificationId])
GO


/* ---------------------------------------------------------------------- */
/* Repair/add views                                                       */
/* ---------------------------------------------------------------------- */

CREATE VIEW [vwCertificationInstructors] AS (
SELECT c.ContactId, c.Company, c.FirstName, c.LastName,  i.InstructorId, da.DiveAgencyId,
  c2.Company AS 'Agency', cer.Title AS 'Certification'
FROM Contacts c
JOIN Instructors i ON i.ContactId = c.ContactId
JOIN CertificationInstructors ci ON ci.InstructorId = i.InstructorId
JOIN Certifications cer ON cer.CertificationId = ci.CertificationId
JOIN DiveAgencies da ON da.DiveAgencyId = cer.DiveAgencyId
JOIN Contacts c2 ON C2.ContactId = da.ContactId
)
GO

