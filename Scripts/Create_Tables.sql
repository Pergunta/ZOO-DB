use zooDB;
go

create schema zoodb;
go

create table zoodb.employee(
	fname		varchar(15)  NOT NULL,
	lname		varchar(15) not null,
	ID			INT	 IDENTITY(1,1)	,
	birthdate	VARCHAR(20),
	primary key (ID));

create table zoodb.zookeeper(
	emp_ID		INT	,
	specialty	varchar(20),
	primary key(emp_ID),
	foreign key	(emp_ID) references zoodb.employee(ID));

create table zoodb.cashier(
	emp_ID		INT	not	null,
	primary key(emp_ID),
	foreign key	(emp_ID) references zoodb.employee(ID));

create table zoodb.zone(
	zone_ID		INT IDENTITY(1,1),
	eco_system	varchar(15),
	manager_ID	INT,
	primary key	(zone_ID),
	foreign key (manager_ID) references zoodb.zookeeper(emp_ID));

create table zoodb.merchandise_shop(
	shop_ID		INT IDENTITY(1,1),
	zone_ID		INT,
	manager_ID	INT,
	primary key	(shop_ID),
	foreign key (manager_ID) references zoodb.cashier(emp_ID));
go
alter table zoodb.zookeeper
	add zone int ;
go
alter table zoodb.zookeeper
	add constraint zookeeper_zone_fk
	foreign key	(zone) references zoodb.zone(zone_ID);
go
alter table zoodb.cashier
	add shop_ID INT;
go
alter table zoodb.cashier
	add constraint cashier_shop_fk
	foreign key	(shop_ID) references zoodb.merchandise_shop(shop_ID);
go
create table zoodb.enclosure(
	zone_ID		INT,
	enc_number	INT not null,
	size		int,
	primary key	(zone_ID, enc_number),
	foreign key (zone_ID) references zoodb.zone(zone_ID));

create table zoodb.species(
	species_ID			INT IDENTITY(1,1),
	name				varchar(40),
	scientific_name		varchar(40)	not null,
	conservation_status	varchar(2),
	zone_ID				int,
	enc_number			int	,
	region				varchar(40),
	primary key	(species_ID),
	foreign key (zone_ID, enc_number) references zoodb.enclosure(zone_ID, enc_number));

create table zoodb.animal(
	animal_ID	INT IDENTITY(1,1),
	name		varchar(15),
	birthdate	date,
	species		INT,
	primary key	(animal_ID),
	foreign key	(species) references zoodb.species(species_ID));

create table zoodb.veterinarian(
	fname		varchar(15) not null,
	lname		varchar(15) not null,
	license_ID	INT IDENTITY(1,1),
	specialty	varchar(20),
	primary key	(license_ID));

create table zoodb.visitor(
	fname		varchar(15) not null,
	lname		varchar(15) not null,
	email		varchar(40),
	NIF			int		not null,
	primary key (NIF));

create table zoodb.health_check(
	hc_ID		INT IDENTITY(1,1),
	hc_date		date		not null,
	vet_ID		int		not null,
	patient_ID	INT,
	primary key (hc_ID),
	foreign key	(vet_ID) references zoodb.veterinarian(license_ID),
	foreign key	(patient_ID) references zoodb.animal(animal_ID));

create table zoodb.support_team(
	emp_ID		int,
	hc_ID		int,
	foreign key	(emp_ID) references zoodb.zookeeper(emp_ID),
	foreign key	(hc_ID) references zoodb.health_check(hc_ID));

create table zoodb.product(
	product_ID	INT IDENTITY(1,1),
	name		varchar(20)	not null,
	price		int			not null,
	shop_ID		int		,
	primary key (product_ID),
	foreign key	(shop_ID) references zoodb.merchandise_shop(shop_ID));

create table zoodb.purchase(
	receipt		int		not null,
	NIF			int		not null,
	product_ID 	INT,
	shop_ID		INT,
	primary key (receipt),
	foreign key	(NIF) references zoodb.visitor(NIF),
	foreign key	(product_ID) references zoodb.product(product_ID),
	foreign key	(shop_ID) references zoodb.merchandise_shop(shop_ID));

create table zoodb.exhibit(
	exhibit_ID	INT IDENTITY(1,1),
	name		varchar(30)	not null,
	zone_ID		int,
	primary key (exhibit_ID),
	foreign key (zone_ID) references zoodb.zone(zone_ID));

create table zoodb.goes_to(
	exhibit_ID	INT,
	NIF			int		not null,
	foreign key	(exhibit_ID) references zoodb.exhibit(exhibit_ID),
	foreign key	(NIF) references zoodb.visitor(NIF));

create table zoodb.sponsorship(
	NIF			int		not null,
	animal_ID	int		not null,
	foreign key	(NIF) references zoodb.visitor(NIF),
	foreign key	(animal_ID) references zoodb.animal(animal_ID));
go