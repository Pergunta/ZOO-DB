--SPECIES
create trigger zoodb.validMoveSpecies on zoodb.species
instead of update
as
begin
	declare @popcount as int;
	declare @species_ID as int;
	declare @zone_ID as int;
	declare @nenc_number as int;
	declare @oenc_number as int;
	declare @enc_size as int;
	select @species_ID = species_ID, @zone_ID = zone_ID, @nenc_number = enc_number from inserted;
	select @enc_size = enc.size from zoodb.enclosure enc where enc.enc_number = @nenc_number and enc.zone_ID = @zone_ID;
	select @popcount = count(a.animal_ID) from zoodb.animal a where a.species = @species_ID;
	select @oenc_number = sp.enc_number from zoodb.species sp where sp.species_ID = @species_ID

	if @popcount > @enc_size
		raiserror('Enclosure not big enough', 16, 1);
	else
		update zoodb.species
		set enc_number = @nenc_number
		where zone_ID = @zone_ID and enc_number = @oenc_number;
end
go

--VETERINARIAN
create trigger zoodb.validVets on zoodb.veterinarian
instead of insert
as 
begin
	declare @fname as varchar(15);
	declare @lname as varchar(15);
	declare @specialty as varchar(20);
	select @fname = fname, @lname = lname, @specialty = specialty from inserted;
	if exists (
	select *
	from zoodb.veterinarian vet
	where vet.fname = @fname and vet.lname = @lname)
		raiserror('Veterinarian already exists', 16, 1);

	else
		insert into zoodb.veterinarian(fname, lname, specialty) values
		(@fname, @lname, @specialty)

end
go

--EXHIBIT
create trigger zoodb.validExhibit on zoodb.exhibit
instead of insert
as 
begin
	declare @name as varchar(30);
	declare @zone_ID as int;
	select @name = name, @zone_ID = zone_ID from inserted;
	if exists (
	select *
	from zoodb.exhibit ex
	where ex.name = @name and ex.zone_ID = @zone_ID)
		raiserror('Exhibit already exists', 16, 1);

	else
		insert into zoodb.exhibit(name, zone_ID) values
		(@name, @zone_ID)
		
end
go

--EMPLOYEE
create trigger zoodb.validEmp on zoodb.employee
instead of insert
as 
begin
	declare @fname as varchar(15);
	declare @lname as varchar(15);
	declare @birthdate as varchar(20);
	select @fname = fname, @lname = lname, @birthdate = birthdate from inserted;
	if exists (
	select *
	from zoodb.employee emp
	where emp.fname = @fname and emp.lname = @lname)
		raiserror('Employee already exists', 16, 1);

	else
		insert into zoodb.employee(fname, lname, birthdate) values
		(@fname, @lname, @birthdate)

end
go