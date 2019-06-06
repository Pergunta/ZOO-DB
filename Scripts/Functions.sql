--VETERINARIAN
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
	SELECT hc.hc_ID,a.animal_ID,s.name as species_name ,a.name as animal_name FROM zoodb.veterinarian v 
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

create function zoodb.getZoneZK(@zone_ID int) returns table
as
	return(
	select emp.fname, emp.lname, emp.ID from zoodb.zookeeper zk
				join zoodb.zone z on z.zone_ID = zk.zone
				join zoodb.employee emp on emp.ID = zk.emp_ID 
				where z.zone_ID = @zone_ID
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

--ENCLOSURES
create function zoodb.getZoneEnc(@zone_ID int) returns table
as
	return(
	select enc.enc_number, sp.name, count(a.animal_ID) as animal_count from zoodb.enclosure enc 
								left join zoodb.species sp on enc.enc_number = sp.enc_number and enc.zone_ID = sp.zone_ID
								left join zoodb.animal a on a.species = sp.species_ID
								where enc.zone_ID = @zone_ID
								group by sp.name, enc.zone_ID, enc.enc_number 
	);
go

create function zoodb.getZoneEncInhabited(@zone_ID int) returns table
as
	return(
	select enc_number, name from zoodb.getZoneEnc(@zone_ID) func
				where func.name is not null
			 
	);
go

create function zoodb.getZoneEncEmpty(@zone_ID int) returns table
as
	return(
	select func.enc_number from zoodb.getZoneEnc(@zone_ID) func
				where func.name is null
			 
	);
go

create function zoodb.getEncAnimals(@zone_ID int, @enc_number int) returns table
as
	return(
	select a.animal_ID, a.name, sp.name as sp_name from zoodb.enclosure enc 
								join zoodb.species sp on sp.enc_number = enc.enc_number and enc.zone_ID = sp.zone_ID
								join zoodb.animal a on sp.species_ID = a.species
								where enc.enc_number = @enc_number
								and enc.zone_ID = @zone_ID);
go

--ANIMAL

create function zoodb.getAnimal(@animal_ID int) returns table
as
	return(
	select a.name,a.animal_ID,sp.name as species_name from zoodb.animal a
									join zoodb.species sp on sp.species_ID = a.species
									where animal_ID=@animal_ID
	);
go

create function zoodb.getHCST(@hc_ID int) returns table
as
	return(
	select stuff((select '; ' + CONVERT(varchar(10), st.emp_ID)
		from zoodb.support_team st
		where st.hc_ID = @hc_ID
		for xml path('')), 1, 1, '') [support_team]

	);
go

create function zoodb.getAnimalHC(@animal_ID int) returns table
as
	return(
	select hc.hc_ID, vet.license_ID, vet.fname, vet.lname, func.support_team from zoodb.animal a 
			join zoodb.health_check hc on hc.patient_ID = a.animal_ID
			join zoodb.veterinarian vet on vet.license_ID = hc.vet_ID
			cross apply zoodb.getHCST(hc_ID) func
			where animal_ID = @animal_ID
	);
go

--SPONSORSHIP

create function zoodb.getSponsorshipList() returns table
as
	return(
	select a.animal_ID, a.name, v.NIF, v.fname, v.lname from zoodb.sponsorship sp
				join zoodb.animal a on sp.animal_ID=a.animal_ID
				join zoodb.visitor v on v.NIF = sp.NIF
	);
go

--EXHIBIT

create function zoodb.getExhibitList() returns table
as
	return(
	select ex.exhibit_ID, ex.name, ex.zone_ID, count(gt.NIF) as visitor_number from zoodb.exhibit ex
				left join zoodb.goes_to gt on gt.exhibit_ID = ex.exhibit_ID
				group by ex.exhibit_ID, ex.name, ex.zone_ID
	);
go

--EMPLOYEE
create function zoodb.getEmployeeZK()returns table 
as 
	return(
	select z.emp_ID, e.fname, e.lname from zoodb.employee e
					join zoodb.zookeeper z on z.emp_ID = e.ID
	)
go

create function zoodb.getEmployeeCashier()returns table 
as 
	return(
	select c.emp_ID, e.fname, e.lname from zoodb.employee e
					join zoodb.cashier c on c.emp_ID = e.ID
	)
go

create function zoodb.getEmployeeUnref()returns table 
as 
	return(
	select e.ID, e.fname, e.lname from zoodb.employee e
					where  e.ID not in (select e.ID from zoodb.employee e					
										join zoodb.cashier c on c.emp_ID = e.ID) 
										and e.ID not in (select e.ID from zoodb.employee e 
										join zoodb.zookeeper z on z.emp_ID = e.ID)
	)
go

create function zoodb.getUnrefData(@emp_ID int)returns table 
as 
	return(
	select e.ID, e.fname, e.lname, e.birthdate from zoodb.employee e
					where  e.ID not in (select e.ID from zoodb.employee e					
										join zoodb.cashier c on c.emp_ID = e.ID) 
						and e.ID not in (select e.ID from zoodb.employee e 
										join zoodb.zookeeper z on z.emp_ID = e.ID)
						and e.ID = @emp_ID
	)
go

create function zoodb.getZKData(@emp_ID int) returns table
as
	return(
	select emp.ID, emp.fname, emp.lname, emp.birthdate, z.zone, z.specialty from zoodb.zookeeper z 
				join zoodb.employee emp on emp.ID = z.emp_ID
				where z.emp_ID = @emp_ID
	);
go

create function zoodb.getCashierData(@emp_ID int) returns table
as
	return(
	select emp.ID, emp.fname, emp.lname, emp.birthdate, c.shop_ID from zoodb.cashier c 
				join zoodb.employee emp on emp.ID = c.emp_ID
				where c.emp_ID = @emp_ID
	);
go

create function zoodb.getEmployeeJob(@ID int )returns table 
as 
	return(
	select z.emp_ID, e.fname, e.lname, e.birthdate , z.specialty , z.zone  from zoodb.employee e
					join zoodb.zookeeper z on z.emp_ID = e.ID
					where e.ID = @ID
	)
go


create function zoodb.getEmployeeJob2(@ID int)returns table
as 
	return(
	select e.fname,e.lname,e.birthdate, c.emp_ID , c.shop_ID from zoodb.employee e
					join zoodb.cashier c on c.emp_ID = e.ID
					where e.ID = @ID

	)
go

--MERCH SHOP
create function zoodb.getShopIDProduct(@shop_ID int)returns table 
as 
	return(
	select  p.name, p.price, p.product_ID , p.quantity from zoodb.merchandise_shop m
					join zoodb.product p on p.shop_ID = m.shop_ID
					where m.shop_ID = @shop_ID
	)
go

