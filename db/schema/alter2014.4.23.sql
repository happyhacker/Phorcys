/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V9.1.0                     */
/* Target DBMS:           MS SQL Server 2012                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2016-01-06 19:20                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Divers_DiveTeam]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Dives_DiveTeam]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Dives_DiveDetails]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Roles_DiveDetails]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Users_Dives]
GO


ALTER TABLE [DiveUrls] DROP CONSTRAINT [DiveDetails_ContentLinks]
GO


/* ---------------------------------------------------------------------- */
/* Alter table "DiveTeams"                                                */
/* ---------------------------------------------------------------------- */

GO


ALTER TABLE [DiveTeams] ADD
    [RoleId] INTEGER
GO


/* ---------------------------------------------------------------------- */
/* Alter table "Dives"                                                    */
/* ---------------------------------------------------------------------- */

GO


ALTER TABLE [Dives] DROP COLUMN [RoleId]
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

GO


ALTER TABLE [DiveTeams] ADD CONSTRAINT [Dives_DiveTeam] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [DiveTeams] ADD CONSTRAINT [Divers_DiveTeam] 
    FOREIGN KEY ([DiverId]) REFERENCES [Divers] ([DiverId])
GO


ALTER TABLE [DiveTeams] ADD CONSTRAINT [Roles_DiveTeams] 
    FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
GO


ALTER TABLE [Dives] ADD CONSTRAINT [Dives_DiveDetails] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [Dives] ADD CONSTRAINT [Users_Dives] 
    FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO


ALTER TABLE [DiveUrls] ADD CONSTRAINT [DiveDetails_ContentLinks] 
    FOREIGN KEY ([DiveId]) REFERENCES [Dives] ([DiveId])
GO

