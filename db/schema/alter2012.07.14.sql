/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V7.1.0                     */
/* Target DBMS:           MS SQL Server 2008                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2012-07-17 23:26                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[GasMixes] DROP CONSTRAINT [TanksOnDive_GasMixes]
GO


ALTER TABLE [dbo].[GasMixes] DROP CONSTRAINT [Gases_GasMixes]
GO


/* ---------------------------------------------------------------------- */
/* Modify table "GasMixes"                                                */
/* ---------------------------------------------------------------------- */

EXEC sp_rename 'GasMixes.Volume', 'VolumeAdded', 'COLUMN'
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [dbo].[GasMixes] ADD CONSTRAINT [TanksOnDive_GasMixes] 
    FOREIGN KEY ([DivePlanId], [GearId]) REFERENCES [dbo].[TanksOnDive] ([DivePlanId],[GearId])
GO


ALTER TABLE [dbo].[GasMixes] ADD CONSTRAINT [Gases_GasMixes] 
    FOREIGN KEY ([GasId]) REFERENCES [dbo].[Gases] ([GasId])
GO

