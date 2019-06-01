--ADD EMPLOYEE
insert into zoodb.employee (fname, lname, birthdate) VALUES
('Chris','Paul', '1985-05-06');
select * from zoodb.employee;

insert into zoodb.employee (fname, lname,birthdate) VALUES
('Satan',':)', '1966-06-06');
select * from zoodb.employee;

--ADD ZONE
insert into zoodb.zone ( eco_system) VALUES
('Polar');
select * from zoodb.zone;

--ADD ZOOKEEPER
insert into zoodb.zookeeper ( emp_ID , specialty, zone) VALUES
( 1,'Seabirds', 1);
select * from zoodb.zookeeper;

--ADD MERCHANDISE_SHOP
insert into zoodb.merchandise_shop ( zone_ID) VALUES
( 1);
select * from zoodb.merchandise_shop;

--ADD CASHIER
insert into zoodb.cashier (emp_ID, shop_ID) VALUES
(1, 1);
select * from zoodb.cashier;

--ADD MANAGER
update zoodb.zone
set manager_ID = 1
where zone_ID = 1
select * from zoodb.zone;

update zoodb.merchandise_shop
set manager_ID = 1
where shop_ID = 1
select * from zoodb.merchandise_shop;

--ADD ENCLOSURE
insert into zoodb.enclosure (zone_ID, enc_type,size) VALUES
(1, 'Cold water tank',10);
select * from zoodb.enclosure;

--ADD EXHIBIT
insert into zoodb.exhibit ( name, zone_ID) VALUES
( 'Penguins being Penguins', 1);
select * from zoodb.exhibit;

--ADD VISITOR
insert into zoodb.visitor (fname, lname, email, NIF) VALUES
('Chico', 'da Tina', 'cavernicula@live.com.pt',123456789);
select * from zoodb.visitor;

--ADD GOES TO
insert into zoodb.goes_to (exhibit_ID, NIF) VALUES
(1, 123456789);
select * from zoodb.goes_to;

--ADD SPECIES
insert into zoodb.species ( name, scientific_name, conservation_status, zone_ID, enc_number, region) VALUES
( 'King Penguin', 'Aptenodytes Patagonicus', 'LC', 1, 1, 'Subantartic Islands');
select * from zoodb.species;

--ADD ANIMAL
insert into  zoodb.animal ( name, birthdate, species) VALUES
( 'Mike', '2010-10-10', 1);
select * from zoodb.animal;

--ADD SPONSORSHIP
insert into  zoodb.sponsorship (NIF, animal_ID) VALUES
(123456789, 1);
select * from zoodb.sponsorship;

--ADD PRODUCT
insert into  zoodb.product ( name, price, shop_ID) VALUES
( 'King Penguin', 10, 1);
select * from zoodb.product;

--ADD PURCHASE
insert into  zoodb.purchase (receipt, NIF, product_ID, shop_ID) VALUES
(1, 123456789, 1, 1);
select * from zoodb.purchase;

--ADD VETERINARIAN
insert into zoodb.veterinarian (fname, lname, license_ID, specialty) VALUES
('Doctor', 'Medico', 1121 ,'Veterinary Nutrition' );
select * from zoodb.veterinarian;

--ADD HEALTH CHECK
insert into zoodb.health_check ( hc_date, vet_ID, patient_ID) VALUES
( '2015-06-07', 11321, 1);
select * from zoodb.health_check;

--ADD SUPPORT TEAM
insert into zoodb.support_team (emp_ID, hc_ID) VALUES
(1, 2);
select * from zoodb.support_team;