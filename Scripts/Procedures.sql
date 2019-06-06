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

--ZONE

create procedure zoodb.changeZoneManager(@zone_ID int, @emp_ID int)
as
	update zoodb.zone
	set manager_ID = @emp_ID
	where zone_ID = @zone_ID
go

--ENCLOSURES

create procedure zoodb.moveSpecies(@zone_ID int , @enc1 int, @enc2 int)
as
begin
	update zoodb.species
	set enc_number = @enc2
	where zone_ID = @zone_ID and enc_number = @enc1
end
go

--SPONSORSHIP
create procedure zoodb.addSponsorship(@NIF int, @animal_ID int)
as 
	BEGIN  
	insert into zoodb.sponsorship (NIF, animal_ID) values (@NIF, @animal_ID)  
	END   
  
go

create procedure zoodb.removeSponsorship(@animal_ID int, @NIF int)
as 
	BEGIN  
	DELETE FROM zoodb.sponsorship WHERE animal_ID = @animal_ID and NIF = @NIF
	END  

go

--EXHIBIT
create procedure zoodb.addExhibit(@name varchar(30), @zone_ID int)
as 
	BEGIN  
	insert into zoodb.exhibit (name, zone_ID) values (@name, @zone_ID)  
	END   
  
go

create procedure zoodb.removeExhibit(@exhibit_ID int)
as 
	BEGIN  
	DELETE FROM zoodb.exhibit WHERE exhibit_ID = @exhibit_ID
	END  

go

--EMPLOYEE
create procedure zoodb.addZookeeper( @zone int , @speciality varchar(20),@ID int)
as 
	BEGIN  
	insert into zoodb.zookeeper(emp_ID, specialty, zone)
	values(@ID, @speciality, @zone)
	END   
go

create procedure zoodb.addCashier(@ID int , @shop_ID int)
as
	BEGIN
	insert into zoodb.cashier(emp_ID, shop_ID)
	values(@ID, @shop_ID)
	END   
go

--MERCH SHOP
create procedure zoodb.ADDproduct(@name varchar(15), @price int,@shop_ID int ,@quantity int)
as 
	BEGIN  
	insert into zoodb.product (name, price, shop_ID , quantity) values(@name, @price, @shop_ID , @quantity)  
	END   
go

create procedure zoodb.DeleteProduct(@product_ID int)
as 
	BEGIN  
	DELETE FROM zoodb.product WHERE product_ID = @product_ID 
	END   
go

create procedure zoodb.UpdateProduct(@product_ID int , @name varchar(20),@price int ,@shop_ID int, @quantity int)
as 
	begin
	UPDATE zoodb.product 
	SET name = @name , price = @price , quantity=@quantity , shop_ID = @shop_ID
	WHERE product_ID = @product_ID
	end
go
