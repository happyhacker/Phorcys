/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2012-12-15 17:38                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [Gear_Tanks]
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[Tanks] ADD CONSTRAINT [Gear_Tanks] 
    FOREIGN KEY ([GearId]) REFERENCES [dbo].[Gear] ([GearId]) ON DELETE CASCADE
GO

