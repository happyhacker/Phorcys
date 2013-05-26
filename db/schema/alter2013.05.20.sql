/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2013-05-20 21:26                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Manufacturers_Gear]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Contacts_Gear]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Users_Gear]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Certifications_DiverCertifications]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP CONSTRAINT [Instructors_DiverCertifications]
GO


ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [Gear_Tanks]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [Contacts_Instructors]
GO


ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [DiveAgencies_Instructors]
GO


ALTER TABLE [dbo].[GearServiceEvents] DROP CONSTRAINT [Gear_GearServiceEvents]
GO


ALTER TABLE [dbo].[InsuredGear] DROP CONSTRAINT [Gear_InsuredGear]
GO


ALTER TABLE [dbo].[ServiceSchedules] DROP CONSTRAINT [Gear_ServiceSchedules]
GO


ALTER TABLE [dbo].[DiverGear] DROP CONSTRAINT [Gear_DiverGear]
GO


ALTER TABLE [dbo].[GearOnDive] DROP CONSTRAINT [Gear_GearOnDive]
GO


ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [Gear_SoldGear]
GO


/* ---------------------------------------------------------------------- */
/* Modify table "Gear"                                                    */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [GearCreated]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [GearLastModified]
GO


ALTER TABLE [dbo].[Gear] ALTER COLUMN [Created] DATETIME
GO


ALTER TABLE [dbo].[Gear] ALTER COLUMN [LastModified] DATETIME
GO


ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [GearCreated] 
    DEFAULT (getdate()) FOR [Created]
GO


ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [GearLastModified] 
    DEFAULT (getdate()) FOR [LastModified]
GO


/* ---------------------------------------------------------------------- */
/* Modify table "DiverCertifications"                                     */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[DiverCertifications] DROP COLUMN [ContactId]
GO


ALTER TABLE [dbo].[DiverCertifications] DROP COLUMN [DiveAgencyId]
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'DivePlanId'
GO


EXECUTE sp_addextendedproperty N'MS_Description', N'N', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'FillDate'
GO


/* ---------------------------------------------------------------------- */
/* Modify table "Instructors"                                             */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Instructors] DROP CONSTRAINT [PK_Instructors]
GO


ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [PK_Instructors] 
    PRIMARY KEY CLUSTERED ([InstructorId])
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [Manufacturers_Gear] 
    FOREIGN KEY ([ManufacturerId]) REFERENCES [dbo].[Manufacturers] ([ManufacturerId])
GO


ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [Contacts_Gear] 
    FOREIGN KEY ([PurchasedFromContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Gear] ADD CONSTRAINT [Users_Gear] 
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Divers_DiverCertifications] 
    FOREIGN KEY ([DiverId]) REFERENCES [dbo].[Divers] ([DiverId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Certifications_DiverCertifications] 
    FOREIGN KEY ([CertificationId]) REFERENCES [dbo].[Certifications] ([CertificationId])
GO


ALTER TABLE [dbo].[DiverCertifications] ADD CONSTRAINT [Instructors_DiverCertifications] 
    FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructors] ([InstructorId])
GO


ALTER TABLE [dbo].[Tanks] ADD CONSTRAINT [Gear_Tanks] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [Contacts_Instructors] 
    FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId])
GO


ALTER TABLE [dbo].[Instructors] ADD CONSTRAINT [DiveAgencies_Instructors] 
    FOREIGN KEY ([DiveAgencyId]) REFERENCES [dbo].[DiveAgencies] ([DiveAgencyId])
GO


ALTER TABLE [dbo].[GearServiceEvents] ADD CONSTRAINT [Gear_GearServiceEvents] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[InsuredGear] ADD CONSTRAINT [Gear_InsuredGear] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[ServiceSchedules] ADD CONSTRAINT [Gear_ServiceSchedules] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[DiverGear] ADD CONSTRAINT [Gear_DiverGear] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[GearOnDive] ADD CONSTRAINT [Gear_GearOnDive] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[SoldGear] ADD CONSTRAINT [Gear_SoldGear] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO

