SELECT ai.InstructorId, ai.DiveAgencyId, ci.Firstname, ci.LastName, cda.Company, ai.InstructorAgencyId, CDA.Notes FROM AgencyInstructors ai
JOIN Instructors i ON i.InstructorId = ai.InstructorId
JOIN DiveAgencies da ON da.DiveAgencyId = ai.DiveAgencyId
JOIN Contacts cda ON cda.ContactId = da.ContactId
JOIN Contacts ci ON ci.ContactId = i.ContactId
