--VETERINARIAN

create procedure zoodb.InsertDelete_Vet(@fname varchar(15), @lname varchar(15), @license_ID char(9), @specialty varchar(20),@StatementType nvarchar(20) = '')
as 
	BEGIN  
	IF @StatementType = 'Insert'  
	BEGIN  
	insert into zoodb.veterinarian (fname, lname, license_ID, specialty) values(@fname, @lname, @license_ID, @specialty)  
	END   
	else IF @StatementType = 'Delete'  
	BEGIN  
	DELETE FROM zoodb.veterinarian WHERE license_ID = @license_ID 
	END  
end  
go

create function zoodb.getVetList() returns table
as
	return(
	SELECT license_ID,fname,lname FROM zoodb.veterinarian
	);
go

create function zoodb.getVet(@license_ID int) returns table
as
	return(
	SELECT fname,lname,specialty FROM zoodb.veterinarian WHERE license_ID=@license_ID
	);
go

create function zoodb.getVetHC(@license_ID int) returns table
as
	return(
	SELECT hc.hc_ID,s.name as species_name ,a.name as animal_name FROM zoodb.veterinarian v 
									join zoodb.health_check hc on v.license_ID = hc.vet_ID 
									join zoodb.animal a on a.animal_ID = hc.patient_ID
									join zoodb.species s on a.species = s.species_ID
									where v.license_ID = @license_ID
	);
go