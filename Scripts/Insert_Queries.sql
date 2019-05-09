--ADD EMPLOYEE
insert into zoodb.employee (fname, lname, ID, birthdate) VALUES
('Chris','Paul', 1, '1985-05-06');
select * from zoodb.employee;

--ADD ZONE
insert into zoodb.zone (zone_ID, eco_system) VALUES
(1,'Polar');
select * from zoodb.zone;

--ADD ENCLOSURE
insert into zoodb.enclosure (zone_ID, enc_number, enc_type) VALUES
(1, 1, 'Cold water tank');
select * from zoodb.enclosure;

--ADD MANAGER
update zoodb.zone
set manager_ID = 1
where zone_ID = 1
select * from zoodb.zone;

--ADD ZOOKEEPER
insert into zoodb.zookeeper (emp_ID, specialty, zone) VALUES
(1, 'Seabirds', 1);
select * from zoodb.zookeeper;

--ADD SPECIES
insert into zoodb.species (species_ID, name, scientific_name, conservation_status, zone_ID, enc_number, region) VALUES
(1, 'King Penguin', 'Aptenodytes Patagonicus', 'LC', 1, 1, 'Subantartic Islands');
select * from zoodb.species;


