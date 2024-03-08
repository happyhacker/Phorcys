/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases 12.3.1                     */
/* Target DBMS:           MS SQL Server 2017                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2024-03-04 20:42                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop views                                                             */
/* ---------------------------------------------------------------------- */

DROP VIEW [database_firewall_rules]
GO


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [DivePlans] DROP CONSTRAINT [Users_DivePlans]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DiveSites_Dives]
GO


ALTER TABLE [Divers] DROP CONSTRAINT [Contacts_Divers]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Dives_DiveDetails]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Users_Dives]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [Users_Gear]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [Manufacturers_Gear]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [Contacts_Gear]
GO


ALTER TABLE [InsurancePolicies] DROP CONSTRAINT [Contacts_InsurancePolicies]
GO


ALTER TABLE [Tanks] DROP CONSTRAINT [Gear_Tanks]
GO


ALTER TABLE [DivePlansDiveTypes] DROP CONSTRAINT [Dives_DiveTypesXref]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [DiverGear] DROP CONSTRAINT [Gear_DiverGear]
GO


ALTER TABLE [DiverGear] DROP CONSTRAINT [Divers_DiverGear]
GO


ALTER TABLE [DiverQualifications] DROP CONSTRAINT [Divers_DiverQualifications]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Dives_DiveTeam]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Divers_DiveTeam]
GO


ALTER TABLE [DiveUrls] DROP CONSTRAINT [DiveDetails_ContentLinks]
GO


ALTER TABLE [GearOnDive] DROP CONSTRAINT [DivePlans_GearOnDive]
GO


ALTER TABLE [GearOnDive] DROP CONSTRAINT [Gear_GearOnDive]
GO


ALTER TABLE [GearServiceEvents] DROP CONSTRAINT [Gear_GearServiceEvents]
GO


ALTER TABLE [InsuredGear] DROP CONSTRAINT [Gear_InsuredGear]
GO


ALTER TABLE [InsuredGear] DROP CONSTRAINT [InsurancePolicies_InsuredGear]
GO


ALTER TABLE [ServiceSchedules] DROP CONSTRAINT [Gear_ServiceSchedules]
GO


ALTER TABLE [SoldGear] DROP CONSTRAINT [Gear_SoldGear]
GO


ALTER TABLE [TanksOnDive] DROP CONSTRAINT [Tanks_TanksOnDive]
GO


ALTER TABLE [TanksOnDive] DROP CONSTRAINT [DivePlans_TanksOnDive]
GO


/* ---------------------------------------------------------------------- */
/* Alter table "Tanks"                                                    */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Tanks] DROP CONSTRAINT [DF__Tanks__Volume__55009F39]
GO


ALTER TABLE [Tanks] DROP CONSTRAINT [DF__Tanks__WorkingPr__55F4C372]
GO


ALTER TABLE [Tanks] DROP CONSTRAINT [PK_Tanks]
GO


ALTER TABLE [Tanks] ALTER COLUMN [Volume] INTEGER
GO


ALTER TABLE [Tanks] ALTER COLUMN [WorkingPressure] INTEGER
GO


ALTER TABLE [Tanks] ADD CONSTRAINT [PK_Tanks] 
    PRIMARY KEY CLUSTERED ([GearId])
GO


ALTER TABLE [Tanks] ADD CONSTRAINT [DF__Tanks_TMP__Volum__282DF8C2] 
    DEFAULT (0) FOR [Volume]
GO


ALTER TABLE [Tanks] ADD CONSTRAINT [DF__Tanks_TMP__Worki__29221CFB] 
    DEFAULT (0) FOR [WorkingPressure]
GO


/* ---------------------------------------------------------------------- */
/* Drop and recreate table "DivePlans"                                    */
/* ---------------------------------------------------------------------- */

ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_MaxDepth]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_Created]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_LastModified]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [PK_DivePlans]
GO


CREATE TABLE [DivePlans_TMP] (
    [DivePlanId] INTEGER IDENTITY(1,1) NOT NULL,
    [DiveSiteId] INTEGER,
    [Title] VARCHAR(40) NOT NULL,
    [Minutes] INTEGER,
    [ScheduledTime] DATETIME NOT NULL,
    [MaxDepth] INTEGER CONSTRAINT [DEF_DivePlans_MaxDepth] DEFAULT 0,
    [Notes] VARCHAR(max),
    [UserId] INTEGER NOT NULL,
    [Created] DATETIME CONSTRAINT [DEF_DivePlans_Created] DEFAULT getdate() NOT NULL,
    [LastModified] DATETIME CONSTRAINT [DEF_DivePlans_LastModified] DEFAULT getdate() NOT NULL)
GO



SET IDENTITY_INSERT [DivePlans_TMP] ON
GO



INSERT INTO [DivePlans_TMP]
    ([DivePlanId],[DiveSiteId],[Title],[ScheduledTime],[MaxDepth],[Notes],[UserId],[Created],[LastModified])
SELECT
    [DivePlanId],[DiveSiteId],[Title],[ScheduledTime],[MaxDepth],[Notes],[UserId],[Created],[LastModified]
FROM [DivePlans]
GO



SET IDENTITY_INSERT [DivePlans_TMP] OFF
GO



DROP INDEX [DivePlans].[IDX_DivePlans_DiveId]
GO


DROP TABLE [DivePlans]
GO


EXEC sp_rename '[DivePlans_TMP]', 'DivePlans', 'OBJECT'
GO


ALTER TABLE [DivePlans] ADD CONSTRAINT [PK_DivePlans] 
    PRIMARY KEY CLUSTERED ([DivePlanId])
GO


CREATE NONCLUSTERED INDEX [IDX_DivePlans_DiveId] ON [DivePlans] ([DivePlanId] ASC)
GO


/* ---------------------------------------------------------------------- */
/* Alter table "InsurancePolicies"                                        */
/* ---------------------------------------------------------------------- */

ALTER TABLE [InsurancePolicies] DROP CONSTRAINT [DF__Insurance__Deduc__4E53A1AA]
GO


ALTER TABLE [InsurancePolicies] DROP CONSTRAINT [DF__Insurance__Value__4F47C5E3]
GO


ALTER TABLE [InsurancePolicies] DROP CONSTRAINT [PK_InsurancePolicies]
GO


ALTER TABLE [InsurancePolicies] ALTER COLUMN [Deductible] MONEY
GO


ALTER TABLE [InsurancePolicies] ALTER COLUMN [ValueAmount] MONEY
GO


ALTER TABLE [InsurancePolicies] ADD CONSTRAINT [PK_InsurancePolicies] 
    PRIMARY KEY CLUSTERED ([InsurancePolicyId])
GO


ALTER TABLE [InsurancePolicies] ADD CONSTRAINT [DF__Insurance__Deduc__2BFE89A6] 
    DEFAULT (0) FOR [Deductible]
GO


ALTER TABLE [InsurancePolicies] ADD CONSTRAINT [DF__Insurance__Value__2CF2ADDF] 
    DEFAULT (0) FOR [ValueAmount]
GO


/* ---------------------------------------------------------------------- */
/* Drop and recreate table "Dives"                                        */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Dives] DROP CONSTRAINT [DF__DiveDetai__Minut__31B762FC]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DF__DiveDetai__AvgDe__32AB8735]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DF__DiveDetai__MaxDe__339FAB6E]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DF__DiveDetai__Tempe__3493CFA7]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DF__DiveDetai__Addit__3587F3E0]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DEF_Dives_Created]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [DEF_Dives_LastModified]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [PK_Dives]
GO


CREATE TABLE [Dives_TMP] (
    [DiveId] INTEGER IDENTITY(1,1) NOT NULL,
    [DivePlanId] INTEGER,
    [Title] VARCHAR(40),
    [Minutes] INTEGER CONSTRAINT [DF__DiveDetai__Minut__31B762FC] DEFAULT 0,
    [DescentTime] DATETIME,
    [AvgDepth] INTEGER CONSTRAINT [DF__DiveDetai__AvgDe__32AB8735] DEFAULT 0,
    [MaxDepth] INTEGER CONSTRAINT [DF__DiveDetai__MaxDe__339FAB6E] DEFAULT 0,
    [Temperature] INTEGER CONSTRAINT [DF__DiveDetai__Tempe__3493CFA7] DEFAULT 0,
    [AdditionalWeight] INTEGER CONSTRAINT [DF__DiveDetai__Addit__3587F3E0] DEFAULT 0,
    [Notes] VARCHAR(max) NOT NULL,
    [DiveNumber] INTEGER NOT NULL,
    [UserId] INTEGER,
    [Created] DATETIME CONSTRAINT [DEF_Dives_Created] DEFAULT getdate() NOT NULL,
    [LastModified] DATETIME CONSTRAINT [DEF_Dives_LastModified] DEFAULT getdate() NOT NULL)
GO



SET IDENTITY_INSERT [Dives_TMP] ON
GO



INSERT INTO [Dives_TMP]
    ([DiveId],[DivePlanId],[Minutes],[DescentTime],[AvgDepth],[MaxDepth],[Temperature],[AdditionalWeight],[Notes],[DiveNumber],[UserId],[Created],[LastModified])
SELECT
    [DiveId],[DivePlanId],[Minutes],[DescentTime],[AvgDepth],[MaxDepth],[Temperature],[AdditionalWeight],[Notes],[DiveNumber],[UserId],[Created],[LastModified]
FROM [Dives]
GO



SET IDENTITY_INSERT [Dives_TMP] OFF
GO



DROP TABLE [Dives]
GO


EXEC sp_rename '[Dives_TMP]', 'Dives', 'OBJECT'
GO


ALTER TABLE [Dives] ADD CONSTRAINT [PK_Dives] 
    PRIMARY KEY CLUSTERED ([DiveId])
GO


/* ---------------------------------------------------------------------- */
/* Alter table "Divers"                                                   */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Divers] DROP CONSTRAINT [DF__Divers__RestingS__3B40CD36]
GO


ALTER TABLE [Divers] DROP CONSTRAINT [PK_Divers]
GO


ALTER TABLE [Divers] ALTER COLUMN [RestingSacRate] REAL
GO


ALTER TABLE [Divers] ADD CONSTRAINT [PK_Divers] 
    PRIMARY KEY CLUSTERED ([DiverId])
GO


ALTER TABLE [Divers] ADD CONSTRAINT [DF__Divers__RestingS__3F3159AB] 
    DEFAULT (0) FOR [RestingSacRate]
GO


/* ---------------------------------------------------------------------- */
/* Alter table "Gear"                                                     */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Gear] DROP CONSTRAINT [DF__Gear__RetailPric__489AC854]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [DF__Gear__Paid__498EEC8D]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [DF__Gear__Weight__4B7734FF]
GO


ALTER TABLE [Gear] DROP CONSTRAINT [PK_Gear]
GO


ALTER TABLE [Gear] ALTER COLUMN [RetailPrice] MONEY
GO


ALTER TABLE [Gear] ALTER COLUMN [Paid] MONEY
GO


ALTER TABLE [Gear] ALTER COLUMN [Weight] FLOAT
GO


ALTER TABLE [Gear] ADD CONSTRAINT [PK_Gear] 
    PRIMARY KEY CLUSTERED ([GearId])
GO


ALTER TABLE [Gear] ADD CONSTRAINT [DF__Gear_TMP__Retail__160F4887] 
    DEFAULT (0) FOR [RetailPrice]
GO


ALTER TABLE [Gear] ADD CONSTRAINT [DF__Gear_TMP__Paid__17036CC0] 
    DEFAULT (0) FOR [Paid]
GO


ALTER TABLE [Gear] ADD CONSTRAINT [DF__Gear_TMP__Weight__17F790F9] 
    DEFAULT (0) FOR [Weight]
GO


/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Tanks] ADD CONSTRAINT [Gear_Tanks] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId]) ON DELETE CASCADE ON UPDATE CASCADE
GO


ALTER TABLE [DivePlans] ADD CONSTRAINT [Users_DivePlans] 
    FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO


ALTER TABLE [DivePlans] ADD CONSTRAINT [DiveSites_Dives] 
    FOREIGN KEY ([DiveSiteId]) REFERENCES [DiveSites] ([DiveSiteId])
GO


ALTER TABLE [InsurancePolicies] ADD CONSTRAINT [Contacts_InsurancePolicies] 
    FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([ContactId])
GO


ALTER TABLE [Dives] ADD CONSTRAINT [Users_Dives] 
    FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO


ALTER TABLE [Dives] ADD CONSTRAINT [Dives_DiveDetails] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [Divers] ADD CONSTRAINT [Contacts_Divers] 
    FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([ContactId])
GO


ALTER TABLE [Gear] ADD CONSTRAINT [Users_Gear] 
    FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO


ALTER TABLE [Gear] ADD CONSTRAINT [Manufacturers_Gear] 
    FOREIGN KEY ([ManufacturerId]) REFERENCES [Manufacturers] ([ManufacturerId])
GO


ALTER TABLE [Gear] ADD CONSTRAINT [Contacts_Gear] 
    FOREIGN KEY ([PurchasedFromContactId]) REFERENCES [Contacts] ([ContactId])
GO


ALTER TABLE [GearServiceEvents] ADD CONSTRAINT [Gear_GearServiceEvents] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


ALTER TABLE [DiveUrls] ADD CONSTRAINT [DiveDetails_ContentLinks] 
    FOREIGN KEY ([DiveId]) REFERENCES [Dives] ([DiveId])
GO


ALTER TABLE [DiverCertifications] ADD CONSTRAINT [Divers_DiverCertifications] 
    FOREIGN KEY ([DiverId]) REFERENCES [Divers] ([DiverId])
GO


ALTER TABLE [TanksOnDive] ADD CONSTRAINT [DivePlans_TanksOnDive] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [GearOnDive] ADD CONSTRAINT [DivePlans_GearOnDive] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [InsuredGear] ADD CONSTRAINT [Gear_InsuredGear] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


ALTER TABLE [DiverQualifications] ADD CONSTRAINT [Divers_DiverQualifications] 
    FOREIGN KEY ([DiverId]) REFERENCES [Divers] ([DiverId])
GO


ALTER TABLE [ServiceSchedules] ADD CONSTRAINT [Gear_ServiceSchedules] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


ALTER TABLE [DiverGear] ADD CONSTRAINT [Divers_DiverGear] 
    FOREIGN KEY ([DiverId]) REFERENCES [Divers] ([DiverId])
GO


ALTER TABLE [DiverGear] ADD CONSTRAINT [Gear_DiverGear] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


ALTER TABLE [DiveTeams] ADD CONSTRAINT [Dives_DiveTeam] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [DiveTeams] ADD CONSTRAINT [Divers_DiveTeam] 
    FOREIGN KEY ([DiverId]) REFERENCES [Divers] ([DiverId])
GO


ALTER TABLE [TanksOnDive] ADD CONSTRAINT [Tanks_TanksOnDive] 
    FOREIGN KEY ([GearId]) REFERENCES [Tanks] ([GearId])
GO


ALTER TABLE [GearOnDive] ADD CONSTRAINT [Gear_GearOnDive] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


ALTER TABLE [DivePlansDiveTypes] ADD CONSTRAINT [Dives_DiveTypesXref] 
    FOREIGN KEY ([DivePlanId]) REFERENCES [DivePlans] ([DivePlanId])
GO


ALTER TABLE [InsuredGear] ADD CONSTRAINT [InsurancePolicies_InsuredGear] 
    FOREIGN KEY ([InsurancePolicyId]) REFERENCES [InsurancePolicies] ([InsurancePolicyId])
GO


ALTER TABLE [SoldGear] ADD CONSTRAINT [Gear_SoldGear] 
    FOREIGN KEY ([GearId]) REFERENCES [Gear] ([GearId])
GO


/* ---------------------------------------------------------------------- */
/* Repair/add views                                                       */
/* ---------------------------------------------------------------------- */

CREATE VIEW [vwInstructors] AS (
SELECT i.InstructorId, c.FirstName, c.LastName, c.CountryCode, c.Email, dac.Company AS 'Agency', ai.InstructorAgencyId
FROM Instructors AS i LEFT JOIN
	 Contacts AS c ON i.ContactId = c.ContactId LEFT JOIN
	 AgencyInstructors ai ON ai.InstructorId = i.InstructorId LEFT JOIN
     DiveAgencies AS da ON da.DiveAgencyId = ai.DiveAgencyId LEFT JOIN
     Contacts AS dac ON da.ContactId = dac.ContactId
	 )
GO

