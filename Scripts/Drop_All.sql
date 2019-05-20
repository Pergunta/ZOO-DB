alter table zoodb.zookeeper
	drop constraint zookeeper_zone_fk
go

alter table zoodb.cashier
	drop constraint cashier_shop_fk
go

drop table zoodb.employee;
drop table zoodb.zookeeper;
drop table zoodb.cashier;
drop table zoodb.zone;
drop table zoodb.merchandise_shop;
drop table zoodb.enclosure;
drop table zoodb.species;
drop table zoodb.animal;
drop table zoodb.veterinarian;
drop table zoodb.visitor;
drop table zoodb.health_check;
drop table zoodb.support_team;
drop table zoodb.product;
drop table zoodb.purchase;
drop table zoodb.exhibit;
drop table zoodb.goes_to;
drop table zoodb.sponsorship;
drop schema zoodb;