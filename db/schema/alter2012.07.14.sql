/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2012-07-17 10:36                                */
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


ALTER TABLE [dbo].[GearServiceEvents] DROP CONSTRAINT [Gear_GearServiceEvents]
GO


ALTER TABLE [dbo].[InsuredGear] DROP CONSTRAINT [Gear_InsuredGear]
GO


ALTER TABLE [dbo].[ServiceSchedules] DROP CONSTRAINT [Gear_ServiceSchedules]
GO


ALTER TABLE [dbo].[DiverGear] DROP CONSTRAINT [Gear_DiverGear]
GO


ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [Gear_Tanks]
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


ALTER TABLE [dbo].[Tanks] ADD CONSTRAINT [Gear_Tanks] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[GearOnDive] ADD CONSTRAINT [Gear_GearOnDive] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO


ALTER TABLE [dbo].[SoldGear] ADD CONSTRAINT [Gear_SoldGear] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId])
GO

