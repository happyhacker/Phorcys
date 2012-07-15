SELECT * FROM Contacts

INSERT INTO Contacts (Company, FirstName, LastName, Address1, Address2, City, State,  PostalCode, CountryCode, Email, CellPhone, HomePhone, workphone, Birthday, Gender, UserId, notes)
  VALUES ('','Johnny', 'Richards', '','', '', 'FL', '', 'US', '', '','','','', '', 3, '')

SELECT * FROM Users

INSERT INTO Users (LoginId, Password) VALUES ('larryhack','brenda')

SELECT * FROM Countries WHERE CountryCode = 'ZQ'

INSERT INTO DiveShops (ContactId,Notes) VALUES (8,'')

SELECT * FROM DiveShops

INSERT INTO Divers (ContactId, SacRate, Notes) VALUES (5,.65,'') 

INSERT INTO DiveAgencies (ContactId, Notes) VALUES (10,'Offers Wakulla Award after 100 hours of safe cave diving')
delete from DiveAgencies where ContactId = 5
SELECT * FROM DiveAgencies

INSERT INTO Manufactures (ContactId) VALUES (11)

INSERT INTO Certifications (DiveAgencyId, Title, UserId, Notes) VALUES (2, 'Cave', 3, '')

INSERT INTO DiverCertifications (Certified, DiverId, CertificationId, CertificationNum, Instructor, Notes) VALUES ('2005-11-01', 3,1,0,12,'' )

CREATE VIEW vwCertifications AS (   
  SELECT certs.Title, agency.Company 'Agency', dc.certified, diver.FirstName 'DiverFirstName', diver.LastName 'DiverLastName', instructor.FirstName 'InstructorFirstName', instructor.LastName 'InstructorLastName'
  FROM Certifications certs 
    JOIN DiverCertifications dc ON dc.CertificationId = certs.CertificationId 
    JOIN Users u ON u.UserId = dc.DiverId
    JOIN Contacts diver ON diver.ContactId = u.ContactId
    JOIN Contacts instructor ON instructor.ContactId = dc.Instructor
    JOIN DiveAgencies da ON da.DiveAgencyId = certs.DiveAgencyId
    JOIN Contacts agency ON agency.ContactId = da.ContactId
)
SELECT * FROM vwCertifications

SELECT * FROM Certifications  
SELECT * FROM DiverCertifications

SELECT * FROM DiveLocations
INSERT INTO DiveLocations (Title, UserId) VALUES ('Merrits Mill Pond',3)

SELECT * FROM DiveSites
INSERT INTO DiveSites (title, IsFreshWater, Notes, UserId) VALUES ('Goodenough Spring', 1, '', 3)

SELECT * FROM Manufactures

SELECT * FROM Gear
INSERT INTO Gear (Title, ManufactureId, Paid, RetailPrice,SN, Notes, userid ) VALUES ('Nomad Transpac',1,0, 0,'','',3 )

update Gear set PurchasedFromContactId = 8