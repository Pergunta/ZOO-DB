--ADD EMPLOYEE
insert into zoodb.employee (fname, lname, birthdate) VALUES
--zookeepers
('Chris','Paul', '1985-05-06'),
('James','Harden', '1985-05-06'),
('Clint','Capela', '1985-05-06'),
('Eric','Gordon', '1985-05-06'),
('PJ','Tucker', '1985-05-06'),
('Austin','Rivers', '1985-05-06'),
('Kenneth','Fareed', '1985-05-06'),
('Gerald','Green', '1985-05-06'),
('Russel','Westbrook', '1985-05-06'),
('Paul','George', '1985-05-06'),

--cashiers
('Kawhi','Leonard', '1985-05-06'),
('Danny','Green', '1985-05-06'),
('Kyle','Lowry', '1985-05-06'),
('Pascal','Siakam', '1985-05-06'),
('Marc','Gasol', '1985-05-06');
select * from zoodb.employee;

--ADD ZONE
insert into zoodb.zone ( eco_system) VALUES
('Polar'),
('Savannah'),
('Rainforest');
select * from zoodb.zone;

--ADD ZOOKEEPER
insert into zoodb.zookeeper ( emp_ID , specialty, zone) VALUES
( 1,'Seabirds', 1),
( 2,'Seabirds', 1),
( 3,'Mammals', 1),
( 4,'Mammals', 2),
( 5,'Mammals', 2),
( 6,'Mammals', 2),
( 7,'Mammals', 2),
( 8,'Mammals', 3),
( 9,'Reptilians', 3),
( 10,'Reptilians', 3);
select * from zoodb.zookeeper;

--ADD MERCHANDISE_SHOP
insert into zoodb.merchandise_shop ( zone_ID) VALUES
( 1),
( 2);
select * from zoodb.merchandise_shop;

--ADD CASHIER
insert into zoodb.cashier (emp_ID, shop_ID) VALUES
(11, 1),
(12, 1),
(13, 1),
(14, 2),
(15, 2);
select * from zoodb.cashier;

--ADD MANAGER
update zoodb.zone
set manager_ID = 1
where zone_ID = 1
select * from zoodb.zone;

update zoodb.zone
set manager_ID = 4
where zone_ID = 2
select * from zoodb.zone;

update zoodb.zone
set manager_ID = 8
where zone_ID = 3
select * from zoodb.zone;

update zoodb.merchandise_shop
set manager_ID = 11
where shop_ID = 1
select * from zoodb.merchandise_shop;

update zoodb.merchandise_shop
set manager_ID = 14
where shop_ID = 2
select * from zoodb.merchandise_shop;

--ADD ENCLOSURE
insert into zoodb.enclosure (zone_ID, size) VALUES
(1, 10),
(1, 5),
(1, 5),
(2, 10),
(2, 5),
(2, 10),
(3, 10),
(3, 10),
(3, 20);
select * from zoodb.enclosure;

--ADD EXHIBIT
insert into zoodb.exhibit ( name, zone_ID) VALUES
( 'Penguins being Penguins', 1),
( 'Polar Bears Chilling', 1),
( 'Top Predator', 2),
( 'Pretty and Poisonous', 3);
select * from zoodb.exhibit;

--ADD VISITOR
insert into zoodb.visitor (fname, lname, email, NIF) VALUES
('Jo�o', 'Primeiro', 'j1@live.com.pt',111111111),
('Jo�o', 'Segundo', 'j2@live.com.pt',222222222),
('Jo�o', 'Terceiro', 'j3@live.com.pt',333333333),
('Jo�o', 'Quarto', 'j4live.com.pt',444444444),
('Jo�o', 'Quinto', 'j5@live.com.pt',555555555),
('Jo�o', 'Sexto', 'j6@live.com.pt',666666666),
('Jo�o', 'S�timo', 'j7@live.com.pt',777777777),
('Jo�o', 'Oitavo', 'j8@live.com.pt',888888888),
('Jo�o', 'Nono', 'j9@live.com.pt',999999999),
('Jo�o', 'D�cimo', 'j10@live.com.pt',101010101);
select * from zoodb.visitor;

--ADD GOES TO
insert into zoodb.goes_to (exhibit_ID, NIF) VALUES
(1, 111111111),
(1, 101010101),
(1, 999999999),
(1, 888888888),
(1, 777777777),
(1, 666666666),
(1, 222222222),
(1, 333333333),
(1, 444444444),
(2, 111111111),
(2, 222222222),
(2, 333333333),
(3, 222222222),
(3, 666666666),
(4, 222222222);
select * from zoodb.goes_to;

--ADD SPECIES
insert into zoodb.species ( name, scientific_name, conservation_status, zone_ID, enc_number, region) VALUES
( 'King Penguin', 'Aptenodytes Patagonicus', 'LC', 1, 1, 'Subantartic Islands'),
( 'Polar Bear', 'Lorem Ipsum', 'CR', 1, 2, 'Artic Circle'),
( 'Seal', 'Lorem Ipsum', 'VU', 1, 3, 'Artic Circle'),
( 'Lion', 'Lorem Ipsum', 'CR', 2, 4, 'Savannah'),
( 'Rhinoceros', 'Lorem Ipsum', 'EW', 2, 5, 'Savannah'),
( 'Gazelle', 'Lorem Ipsum', 'NT', 2, 6, 'Savannah'),
( 'Orangotang', 'Lorem Ipsum', 'VU', 3, 7, 'Rainforest'),
( 'Baboon', 'Lorem Ipsum', 'LC', 3, 8, 'Rainforest'),
( 'Tree Frog', 'Lorem Ipsum', 'LC', 3, 9, 'Rainforest');
select * from zoodb.species;

--ADD ANIMAL
insert into  zoodb.animal ( name, birthdate, species) VALUES
( 'Aike', '2010-10-10', 1),
( 'Bike', '2010-10-10', 1),
( 'Cike', '2010-10-10', 1),
( 'Dike', '2010-10-10', 1),

( 'Eike', '2010-10-10', 2),
( 'Fike', '2010-10-10', 2),

( 'Gike', '2010-10-10', 3),
( 'Hike', '2010-10-10', 3),
( 'Iike', '2010-10-10', 3),

( 'Like', '2010-10-10', 4),
( 'Mike', '2010-10-10', 4),
( 'Nike', '2010-10-10', 4),

( 'Oike', '2010-10-10', 5),

( 'Pike', '2010-10-10', 6),
( 'Qike', '2010-10-10', 6),
( 'Rike', '2010-10-10', 6),

( 'Sike', '2010-10-10', 7),
( 'Tike', '2010-10-10', 7),
( 'Uike', '2010-10-10', 7),

( 'Vike', '2010-10-10', 8),
( 'Xike', '2010-10-10', 8),
( 'Yike', '2010-10-10', 8),

( 'Zike', '2010-10-10', 9),
( 'Zzike', '2010-10-10', 9),
( 'Zzzike', '2010-10-10', 9),
( 'Zzzzike', '2010-10-10', 9),
( 'Zzzzzike', '2010-10-10', 9),
( 'Zzzzzzike', '2010-10-10', 9);
select * from zoodb.animal;

--ADD SPONSORSHIP
insert into  zoodb.sponsorship (NIF, animal_ID) VALUES
(111111111, 2),
(222222222, 2),
(111111111, 4),
(333333333, 4),
(111111111, 5),
(555555555, 5),
(666666666, 5),
(111111111, 7);
select * from zoodb.sponsorship;

--ADD PRODUCT
insert into  zoodb.product ( name, price, shop_ID) VALUES
( 'Penguin Doll', 10, 1),
( 'Seal Doll', 10, 1),
( 'Polar Bear Mask', 30, 1),
( 'Ice Cube', 30, 1),
( 'Lion Pencil', 2, 2),
( 'Rhino Doll', 10, 2),
( 'Lion Doll', 10, 2),
( 'Lion Mask', 30, 2),
( 'Rhino Mask', 30, 2);
select * from zoodb.product;

--ADD PURCHASE
insert into  zoodb.purchase (receipt, NIF, product_ID, shop_ID) VALUES
(000000001, 111111111, 1, 1),
(000000002, 111111111, 1, 1),
(000000003, 111111111, 3, 1),
(000000004, 111111111, 7, 2),
(000000005, 111111111, 8, 2),
(000000006, 111111111, 9, 2),
(000000007, 222222222, 1, 1),
(000000008, 222222222, 8, 2),
(000000009, 444444444, 8, 2);
select * from zoodb.purchase;

--ADD VETERINARIAN
insert into zoodb.veterinarian (fname, lname, specialty) VALUES
('Steph', 'Curry', 'Veterinary Nutrition' ),
('Kevin', 'Durant', 'Veterinary Nutrition' ),
('Klay', 'Thompson', 'Veterinary Nutrition' ),
('Draymond', 'Green', 'Veterinary Nutrition' ),
('Andre', 'Igudala', 'Veterinary Nutrition' );
select * from zoodb.veterinarian;

--ADD HEALTH CHECK
insert into zoodb.health_check ( hc_date, vet_ID, patient_ID) VALUES
( '2015-06-07', 1, 1),
( '2015-06-07', 1, 4),
( '2015-06-07', 1, 5),
( '2015-06-07', 2, 20),
( '2015-06-07', 2, 2),
( '2015-06-07', 3, 3),
( '2015-06-07', 4, 6),
( '2015-06-07', 5, 1),
( '2015-06-07', 5, 11);
select * from zoodb.health_check;

--ADD SUPPORT TEAM
insert into zoodb.support_team (emp_ID, hc_ID) VALUES
(1, 2),
(3, 2),
(4, 2),
(5, 2),
(1, 3),
(1, 4),
(3, 4);
select * from zoodb.support_team;