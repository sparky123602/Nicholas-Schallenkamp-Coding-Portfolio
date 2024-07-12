USE TimeCardDB
CREATE TABLE EmployeeTimeSlots
(
	
	ID INT NOT NULL IDENTITY(1,1),
	EmployeeFirstName VARCHAR(100),
	EmployeeSecondName VARCHAR(100),
	ClockIn VARCHAR(100),
	ClockOut VARCHAR(100),
	Date VARCHAR(100),


)

UPDATE EmployeeTimeSlots SET ClockOut = 'Johns Clock Out Time' WHERE EmployeeFirstName ='John' AND EmployeeSecondName='Schallenkamp';

SELECT * FROM EmployeeTimeSlots 
