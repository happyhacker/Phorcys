/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases 12.3.1                     */
/* Target DBMS:           MS SQL Server 2017                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2023-12-13 21:47                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [DiveAgencies_AgencyInstructors]
GO


ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [Instructors_AgencyInstructors]
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
    CONSTRAINT [PK_CertificationInstructors] PRIMARY KEY ([InstructorId], [CertificationId])
)
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [CertificationInstructors] ADD CONSTRAINT [Instructors_CertificationInstructors] 
    FOREIGN KEY ([InstructorId]) REFERENCES [Instructors] ([InstructorId])
GO


ALTER TABLE [CertificationInstructors] ADD CONSTRAINT [Certifications_CertificationInstructors] 
    FOREIGN KEY ([CertificationId]) REFERENCES [Certifications] ([CertificationId])
GO

