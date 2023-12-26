/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases 12.3.1                     */
/* Target DBMS:           MS SQL Server 2017                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2023-12-25 21:29                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop views                                                             */
/* ---------------------------------------------------------------------- */

DROP VIEW [vwCertifications]
GO


/* ---------------------------------------------------------------------- */
/* Repair/add views                                                       */
/* ---------------------------------------------------------------------- */

CREATE VIEW [vwCertifications]
AS
SELECT certs.CertificationId, certs.Title, divers.DiverId, agency.Company AS Agency, dc.Certified, dc.CertificationNum, diverContact.FirstName AS DiverFirstName, diverContact.LastName AS DiverLastName, instructor.FirstName AS InstructorFirstName,
         instructor.LastName AS InstructorLastName
FROM  dbo.Certifications AS certs INNER JOIN
         dbo.DiverCertifications AS dc ON dc.CertificationId = certs.CertificationId INNER JOIN
         dbo.Users AS u ON u.UserId = dc.DiverId INNER JOIN
         dbo.Divers AS divers ON divers.DiverId = dc.DiverId INNER JOIN
         dbo.Contacts AS diverContact ON diverContact.ContactId = divers.ContactId LEFT OUTER JOIN
         dbo.Instructors ON dbo.Instructors.InstructorId = dc.InstructorId LEFT OUTER JOIN
         dbo.Contacts AS instructor ON instructor.ContactId = dbo.Instructors.ContactId INNER JOIN
         dbo.DiveAgencies AS da ON da.DiveAgencyId = certs.DiveAgencyId INNER JOIN
         dbo.Contacts AS agency ON agency.ContactId = da.ContactId
GO

