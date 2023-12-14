/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases 12.3.1                     */
/* Target DBMS:           MS SQL Server 2017                              */
/* Project file:          SCUBA.dez                                       */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Database drop script                            */
/* Created on:            2023-12-13 21:32                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop procedures                                                        */
/* ---------------------------------------------------------------------- */

DROP FUNCTION [dbo].[CalcVolume]
GO


/* ---------------------------------------------------------------------- */
/* Drop views                                                             */
/* ---------------------------------------------------------------------- */

DROP VIEW [vwCertifications]
GO


DROP VIEW [vwTanksOnDive]
GO


DROP VIEW [vwInstructors]
GO


DROP VIEW [dbo].[vwDiveShops]
GO


DROP VIEW [dbo].[vwDivers]
GO


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Users] DROP CONSTRAINT [Contacts_Users]
GO


ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [Users_DiveLocations]
GO


ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [Contacts_DiveLocations]
GO


ALTER TABLE [dbo].[Friends] DROP CONSTRAINT [Users_Friends1]
GO


ALTER TABLE [dbo].[Friends] DROP CONSTRAINT [Users_Friends2]
GO


ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [Users_DiveSites]
GO


ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [DiveLocations_DiveSites]
GO


ALTER TABLE [DiveUrls] DROP CONSTRAINT [DiveDetails_ContentLinks]
GO


ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [Users_Certifications]
GO


ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [DiveAgencies_Certifications]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [Divers_DiverCertifications]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [Certifications_DiverCertifications]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [Instructors_DiverCertifications]
GO


ALTER TABLE [dbo].[DiveSiteUrls] DROP CONSTRAINT [DiveSites_DiveSiteUrls]
GO


ALTER TABLE [dbo].[GasMixes] DROP CONSTRAINT [TanksOnDive_GasMixes]
GO


ALTER TABLE [dbo].[GasMixes] DROP CONSTRAINT [Gases_GasMixes]
GO


ALTER TABLE [Instructors] DROP CONSTRAINT [Contacts_Instructors]
GO


ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [Gear_Tanks]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [Users_DivePlans]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DiveSites_Dives]
GO


ALTER TABLE [dbo].[InsurancePolicies] DROP CONSTRAINT [Contacts_InsurancePolicies]
GO


ALTER TABLE [dbo].[InsuredGear] DROP CONSTRAINT [Gear_InsuredGear]
GO


ALTER TABLE [dbo].[InsuredGear] DROP CONSTRAINT [InsurancePolicies_InsuredGear]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Users_Dives]
GO


ALTER TABLE [Dives] DROP CONSTRAINT [Dives_DiveDetails]
GO


ALTER TABLE [dbo].[Divers] DROP CONSTRAINT [Contacts_Divers]
GO


ALTER TABLE [dbo].[Qualifications] DROP CONSTRAINT [Users_Qualifications]
GO


ALTER TABLE [dbo].[DiverQualifications] DROP CONSTRAINT [Qualifications_DiverQualifications]
GO


ALTER TABLE [dbo].[DiverQualifications] DROP CONSTRAINT [Divers_DiverQualifications]
GO


ALTER TABLE [dbo].[TanksOnDive] DROP CONSTRAINT [DivePlans_TanksOnDive]
GO


ALTER TABLE [dbo].[TanksOnDive] DROP CONSTRAINT [Tanks_TanksOnDive]
GO


ALTER TABLE [dbo].[DiveShops] DROP CONSTRAINT [Contacts_DiveShops]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Dives_DiveTeam]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Divers_DiveTeam]
GO


ALTER TABLE [DiveTeams] DROP CONSTRAINT [Roles_DiveTeams]
GO


ALTER TABLE [dbo].[DiveShopServices] DROP CONSTRAINT [DiveShops_DiveShopServices]
GO


ALTER TABLE [dbo].[DiveShopServices] DROP CONSTRAINT [Services_DiveShopServices]
GO


ALTER TABLE [dbo].[DiverGear] DROP CONSTRAINT [Divers_DiverGear]
GO


ALTER TABLE [dbo].[DiverGear] DROP CONSTRAINT [Gear_DiverGear]
GO


ALTER TABLE [dbo].[DiveTypes] DROP CONSTRAINT [Users_DiveTypes]
GO


ALTER TABLE [Contacts] DROP CONSTRAINT [Users_Contacts]
GO


ALTER TABLE [Contacts] DROP CONSTRAINT [Countries_Contacts]
GO


ALTER TABLE [dbo].[DiveShopStaff] DROP CONSTRAINT [Contacts_DiveShopStaff]
GO


ALTER TABLE [dbo].[DiveShopStaff] DROP CONSTRAINT [DiveShops_DiveShopStaff]
GO


ALTER TABLE [dbo].[GearOnDive] DROP CONSTRAINT [DivePlans_GearOnDive]
GO


ALTER TABLE [dbo].[GearOnDive] DROP CONSTRAINT [Gear_GearOnDive]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Users_Gear]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Manufacturers_Gear]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [Contacts_Gear]
GO


ALTER TABLE [dbo].[DivePlansDiveTypes] DROP CONSTRAINT [DiveTypes_DiveTypesXref]
GO


ALTER TABLE [dbo].[DivePlansDiveTypes] DROP CONSTRAINT [Dives_DiveTypesXref]
GO


ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [DiveAgencies_AgencyInstructors]
GO


ALTER TABLE [AgencyInstructors] DROP CONSTRAINT [Instructors_AgencyInstructors]
GO


ALTER TABLE [dbo].[ServiceSchedules] DROP CONSTRAINT [Gear_ServiceSchedules]
GO


ALTER TABLE [dbo].[DiveAgencies] DROP CONSTRAINT [Contacts_DiveAgencies]
GO


ALTER TABLE [dbo].[Manufacturers] DROP CONSTRAINT [Contacts_Manufactures]
GO


ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [Users_Roles]
GO


ALTER TABLE [dbo].[GearServiceEvents] DROP CONSTRAINT [Gear_GearServiceEvents]
GO


ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [Contacts_SoldGear]
GO


ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [Gear_SoldGear]
GO


ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [Users_Attributes]
GO


ALTER TABLE [dbo].[AttributeAssociations] DROP CONSTRAINT [Attributes_AttributeAssociations]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.AttributeAssociations"                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[AttributeAssociations] DROP CONSTRAINT [PK_AttributeAssociations]
GO


DROP TABLE [dbo].[AttributeAssociations]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Attributes"                                            */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [DEF_Attributes_Created]
GO


ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [DEF_Attributes_LastModified]
GO


ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [PK_Attributes]
GO


DROP TABLE [dbo].[Attributes]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.SoldGear"                                              */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [DF__SoldGear___Amoun__5BAD9CC8]
GO


ALTER TABLE [dbo].[SoldGear] DROP CONSTRAINT [PK_SoldGear]
GO


DROP TABLE [dbo].[SoldGear]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.GearServiceEvents"                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[GearServiceEvents] DROP CONSTRAINT [DEF_GearServiceEvents_Cost]
GO


ALTER TABLE [dbo].[GearServiceEvents] DROP CONSTRAINT [PK_GearServiceEvents]
GO


DROP TABLE [dbo].[GearServiceEvents]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Roles"                                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [PK_Roles]
GO


DROP TABLE [dbo].[Roles]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Manufacturers"                                         */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Manufacturers] DROP CONSTRAINT [PK_Manufacturers]
GO


DROP TABLE [dbo].[Manufacturers]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveAgencies"                                          */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveAgencies] DROP CONSTRAINT [PK_DiveAgencies]
GO


DROP TABLE [dbo].[DiveAgencies]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.ServiceSchedules"                                      */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[ServiceSchedules] DROP CONSTRAINT [PK_ServiceSchedules]
GO


DROP TABLE [dbo].[ServiceSchedules]
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
/* Drop table "dbo.DivePlansDiveTypes"                                    */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DivePlansDiveTypes] DROP CONSTRAINT [PK_DivePlansDiveTypes]
GO


DROP TABLE [dbo].[DivePlansDiveTypes]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Gear"                                                  */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [DF__Gear_TMP__Retail__160F4887]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [DF__Gear_TMP__Paid__17036CC0]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [DF__Gear_TMP__Weight__17F790F9]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [GearCreated]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [GearLastModified]
GO


ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [PK_Gear]
GO


DROP TABLE [dbo].[Gear]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.GearOnDive"                                            */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[GearOnDive] DROP CONSTRAINT [PK_GearOnDive]
GO


DROP TABLE [dbo].[GearOnDive]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveShopStaff"                                         */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveShopStaff] DROP CONSTRAINT [PK_DiveShopStaff]
GO


DROP TABLE [dbo].[DiveShopStaff]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "Contacts"                                                  */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [Contacts] DROP CONSTRAINT [DEF_Contacts_Created]
GO


ALTER TABLE [Contacts] DROP CONSTRAINT [DEF_Contacts_LastModified]
GO


ALTER TABLE [Contacts] DROP CONSTRAINT [PK_Contacts]
GO


DROP TABLE [Contacts]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveTypes"                                             */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveTypes] DROP CONSTRAINT [DEF_DiveTypes_Created]
GO


ALTER TABLE [dbo].[DiveTypes] DROP CONSTRAINT [DEF_DiveTypes_LastModified]
GO


ALTER TABLE [dbo].[DiveTypes] DROP CONSTRAINT [PK_DiveTypes]
GO


DROP TABLE [dbo].[DiveTypes]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiverGear"                                             */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiverGear] DROP CONSTRAINT [PK_DiverGear]
GO


DROP TABLE [dbo].[DiverGear]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveShopServices"                                      */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveShopServices] DROP CONSTRAINT [PK_DiveShopServices]
GO


DROP TABLE [dbo].[DiveShopServices]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "DiveTeams"                                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [DiveTeams] DROP CONSTRAINT [PK_DiveTeam]
GO


DROP TABLE [DiveTeams]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveShops"                                             */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveShops] DROP CONSTRAINT [PK_DiveShops]
GO


DROP TABLE [dbo].[DiveShops]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.TanksOnDive"                                           */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[TanksOnDive] DROP CONSTRAINT [DF__TanksOnDi__Start__3C34F16F]
GO


ALTER TABLE [dbo].[TanksOnDive] DROP CONSTRAINT [DF__TanksOnDi__Endin__3D2915A8]
GO


ALTER TABLE [dbo].[TanksOnDive] DROP CONSTRAINT [PK_TanksOnDive]
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'DivePlanId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'GearId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'GasContentTitle'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'StartingPressure'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'EndingPressure'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'FillCost'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', 'COLUMN', N'FillDate'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'TanksOnDive', NULL, NULL
GO


DROP TABLE [dbo].[TanksOnDive]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiverQualifications"                                   */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiverQualifications] DROP CONSTRAINT [DEF_DiverQualifications_Created]
GO


ALTER TABLE [dbo].[DiverQualifications] DROP CONSTRAINT [DEF_DiverQualifications_LastModified]
GO


ALTER TABLE [dbo].[DiverQualifications] DROP CONSTRAINT [PK_DiverQualifications]
GO


DROP TABLE [dbo].[DiverQualifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Qualifications"                                        */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Qualifications] DROP CONSTRAINT [DEF_Qualifications_Created]
GO


ALTER TABLE [dbo].[Qualifications] DROP CONSTRAINT [DEF_Qualifications_LastModified]
GO


ALTER TABLE [dbo].[Qualifications] DROP CONSTRAINT [PK_Qualifications]
GO


DROP TABLE [dbo].[Qualifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Divers"                                                */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Divers] DROP CONSTRAINT [DF__Divers_TM__SacRa__114A936A]
GO


ALTER TABLE [dbo].[Divers] DROP CONSTRAINT [DF__Divers__RestingS__3F3159AB]
GO


ALTER TABLE [dbo].[Divers] DROP CONSTRAINT [PK_Divers]
GO


DROP TABLE [dbo].[Divers]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "Dives"                                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

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


DROP TABLE [Dives]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.InsuredGear"                                           */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[InsuredGear] DROP CONSTRAINT [PK_InsuredGear]
GO


DROP TABLE [dbo].[InsuredGear]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.InsurancePolicies"                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[InsurancePolicies] DROP CONSTRAINT [DF__Insurance__Deduc__2BFE89A6]
GO


ALTER TABLE [dbo].[InsurancePolicies] DROP CONSTRAINT [DF__Insurance__Value__2CF2ADDF]
GO


ALTER TABLE [dbo].[InsurancePolicies] DROP CONSTRAINT [PK_InsurancePolicies]
GO


DROP TABLE [dbo].[InsurancePolicies]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "DivePlans"                                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_MaxDepth]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_Created]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [DEF_DivePlans_LastModified]
GO


ALTER TABLE [DivePlans] DROP CONSTRAINT [PK_DivePlans]
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'DivePlanId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'DiveSiteId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'Title'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'ScheduledTime'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'Notes'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'UserId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'Created'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', 'COLUMN', N'LastModified'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DivePlans', NULL, NULL
GO


DROP TABLE [DivePlans]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Tanks"                                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [DF__Tanks_TMP__Volum__282DF8C2]
GO


ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [DF__Tanks_TMP__Worki__29221CFB]
GO


ALTER TABLE [dbo].[Tanks] DROP CONSTRAINT [PK_Tanks]
GO


DROP TABLE [dbo].[Tanks]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "Instructors"                                               */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [Instructors] DROP CONSTRAINT [PK_Instructors]
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'InstructorId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'ContactId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', 'COLUMN', N'Notes'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'Instructors', NULL, NULL
GO


DROP TABLE [Instructors]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.GasMixes"                                              */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[GasMixes] DROP CONSTRAINT [PK__GasMixes__56FD4E221D9B5BB6]
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'DivePlanId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'GearId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'GasId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'VolumeAdded'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'Percentage'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', 'COLUMN', N'CostPerVolumeOfMeasure'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'GasMixes', NULL, NULL
GO


DROP TABLE [dbo].[GasMixes]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveSiteUrls"                                          */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveSiteUrls] DROP CONSTRAINT [PK_DiveSiteUrls]
GO


DROP TABLE [dbo].[DiveSiteUrls]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "DiverCertifications"                                       */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [DiverCertifications] DROP CONSTRAINT [DEF_DiverCertifications_Created]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [DEF_DiverCertifications_LastModified]
GO


ALTER TABLE [DiverCertifications] DROP CONSTRAINT [PK_DiverCertifications]
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'DiverCertificationId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'DiverId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'CertificationId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Certified'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'CertificationNum'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Notes'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'Created'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'LastModified'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', 'COLUMN', N'InstructorId'
GO


EXECUTE sp_dropextendedproperty N'MS_Description', 'SCHEMA', N'dbo', 'TABLE', N'DiverCertifications', NULL, NULL
GO


DROP TABLE [DiverCertifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Certifications"                                        */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [DEF_Certifications_Created]
GO


ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [DEF_Certifications_LastModified]
GO


ALTER TABLE [dbo].[Certifications] DROP CONSTRAINT [PK_Certifications]
GO


DROP TABLE [dbo].[Certifications]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "DiveUrls"                                                  */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [DiveUrls] DROP CONSTRAINT [PK_ContentLinks]
GO


DROP TABLE [DiveUrls]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveSites"                                             */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [DEF_DiveSites_IsFreshWater]
GO


ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [DEF_DiveSites_Created]
GO


ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [DEF_DiveSites_LastModified]
GO


ALTER TABLE [dbo].[DiveSites] DROP CONSTRAINT [PK_DiveSites]
GO


DROP TABLE [dbo].[DiveSites]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Friends"                                               */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Friends] DROP CONSTRAINT [PK_Friends]
GO


DROP TABLE [dbo].[Friends]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.DiveLocations"                                         */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [DEF_DiveLocations_Created]
GO


ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [DEF_DiveLocations_LastModified]
GO


ALTER TABLE [dbo].[DiveLocations] DROP CONSTRAINT [PK_DiveLocations]
GO


DROP TABLE [dbo].[DiveLocations]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "Users"                                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [Users] DROP CONSTRAINT [DEF_Users_LoginCount]
GO


ALTER TABLE [Users] DROP CONSTRAINT [DEF_Users_Created]
GO


ALTER TABLE [Users] DROP CONSTRAINT [DEF_Users_LastModified]
GO


ALTER TABLE [Users] DROP CONSTRAINT [PK_Users]
GO


ALTER TABLE [Users] DROP CONSTRAINT [TUC_Users_1]
GO


DROP TABLE [Users]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Services"                                              */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DEF_Services_Created]
GO


ALTER TABLE [dbo].[Services] DROP CONSTRAINT [DEF_Services_LastModified]
GO


ALTER TABLE [dbo].[Services] DROP CONSTRAINT [PK_Services]
GO


DROP TABLE [dbo].[Services]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Gases"                                                 */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Gases] DROP CONSTRAINT [PK_Gases]
GO


DROP TABLE [dbo].[Gases]
GO


/* ---------------------------------------------------------------------- */
/* Drop table "dbo.Countries"                                             */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE [dbo].[Countries] DROP CONSTRAINT [PK_Countries]
GO


DROP TABLE [dbo].[Countries]
GO

