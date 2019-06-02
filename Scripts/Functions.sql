--VETERINARIAN

create procedure zoodb.Insert_Vet(@fname varchar(15), @lname varchar(15), @specialty varchar(20))
as 
	BEGIN  
	insert into zoodb.veterinarian (fname, lname, specialty) values(@fname, @lname, @specialty)  
	END   

  
go

create procedure zoodb.Delete_Vet(@license_ID int)
as 
	BEGIN  
	DELETE FROM zoodb.veterinarian WHERE license_ID = @license_ID 
	END  

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

--ZONE

create function zoodb.getZoneList() returns table
as
	return(
	select * from zoodb.zone
	);
go

create function zoodb.getZoneManager(@zone_ID int) returns table
as
	return(
	select mgr.fname, mgr.lname, mgr.ID  from zoodb.zone zo 
								join zoodb.employee mgr on zo.manager_ID = mgr.ID
								where zo.zone_ID = @zone_ID
	);
go

create function zoodb.getZoneEnc(@zone_ID int) returns table
as
	return(
	select enc.enc_number, sp.name, count(a.animal_ID) as animal_count from zoodb.enclosure enc 
								join zoodb.species sp on enc.enc_number = sp.enc_number and enc.zone_ID = sp.zone_ID
								join zoodb.animal a on a.species = sp.species_ID
								where enc.zone_ID = @zone_ID
								group by enc.enc_number, sp.name, enc.zone_ID
	);
go

create function zoodb.getEncAnimals(@zone_ID int, @enc_number int) returns table
as
	return(
	select a.animal_ID, a.name from zoodb.enclosure enc 
								join zoodb.species sp on sp.enc_number = enc.enc_number and enc.zone_ID = sp.zone_ID
								join zoodb.animal a on sp.species_ID = a.species
								where enc.enc_number = @enc_number
								and enc.zone_ID = @zone_ID);
go