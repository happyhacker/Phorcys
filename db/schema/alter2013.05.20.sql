/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.3.6                     */
/* Target DBMS:           MS SQL Server 2012                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2014-01-12 03:10                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop views                                                             */
/* ---------------------------------------------------------------------- */

DROP VIEW [dbo].[vwCertifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [Countries_Contacts]
GO


ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [Users_Contacts]
GO


ALTER TABLE [dbo].[Divers] DROP CONSTRAINT [Contacts_Divers]
GO


ALTER TABLE [dbo].[DiveShopStaff] DROP CONSTRAINT [Contacts_DiveShopStaff]
GO


ALTER TABLE [dbo].[DiveShops] DROP CONSTRAINT [Contacts_DiveShops]
GO


ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [Contacts_DiveLocations]
GO


ALTER TABLE [dbo].[InsurancePolicies] DROP CONSTRAINT [Contacts_InsurancePolicies]
GO


ALTER TABLE [dbo].[Manufacturers] DROP CONSTRAINT [Contacts_Manufactures]
GO


ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [Contacts_SoldGear]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Contacts_Gear]
GO


ALTER TABLE [dbo].[DiveAgencies] DROP CONSTRAINT [Contacts_DiveAgencies]
GO


ALTER TABLE [dbo].[Users] DROP CONSTRAINT [Contacts_Users]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [Contacts_Instructors]
GO


/* ---------------------------------------------------------------------- */
/* Alter table "dbo.Contacts"                                             */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Contacts] ALTER COLUMN [Email] VARCHAR(50) NOT NULL
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT [Users_Contacts] 
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
GO


ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT [Countries_Contacts] 
    FOREIGN KEY ([CountryCode]) REFERENCES [dbo].[Countries] ([CountryCode])
GO


ALTER TABLE [dbo].[Divers] ADD CONSTRAINT [Contacts_Divers] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[DiveShopStaff] ADD CONSTRAINT [Contacts_DiveShopStaff] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[DiveShops] ADD CONSTRAINT [Contacts_DiveShops] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[DiveLocations] ADD CONSTRAINT [Contacts_DiveLocations] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[InsurancePolicies] ADD CONSTRAINT [Contacts_InsurancePolicies] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Manufacturers] ADD CONSTRAINT [Contacts_Manufactures] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[SoldGear] ADD CONSTRAINT [Contacts_SoldGear] 
    FOREIGN KEY ([SoldToContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [Contacts_Gear] 
    FOREIGN KEY ([PurchasedFromContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[DiveAgencies] ADD CONSTRAINT [Contacts_DiveAgencies] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Users] ADD CONSTRAINT [Contacts_Users] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [Contacts_Instructors] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


/* ---------------------------------------------------------------------- */
/* Repair/add views                                                       */
/* ---------------------------------------------------------------------- */

CREATE VIEW [dbo].[vwCertifications] AS (
  SELECT certs.Title, agency.Company 'Agency', dc.certified, diver.FirstName 'DiverFirstName', diver.LastName 'DiverLastName', instructor.FirstName 'InstructorFirstName', instructor.LastName 'InstructorLastName'
  FROM Certifications certs
    JOIN DiverCertifications dc ON dc.CertificationId = certs.CertificationId
    JOIN Users u ON u.UserId = dc.DiverId
    JOIN Contacts diver ON diver.ContactId = u.ContactId
    JOIN Contacts instructor ON instructor.ContactId = dc.Instructor
    JOIN DiveAgencies da ON da.DiveAgencyId = certs.DiveAgencyId
    JOIN Contacts agency ON agency.ContactId = da.ContactId
)
GO

