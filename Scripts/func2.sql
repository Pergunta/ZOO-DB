--MERCH SHOP
create function zoodb.getShopIDProduct(@shop_ID int)returns table 
as 
	return(
	select  p.name, p.price, p.product_ID , p.quantity from zoodb.merchandise_shop m
					join zoodb.product p on p.shop_ID = m.shop_ID
					where m.shop_ID = @shop_ID
	)
go

go
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


--EMPLOYEE

create function zoodb.getEmployeeJob(@ID int )returns table 
as 
	return(
	select  e.fname, e.lname, e.birthdate , z.specialty , z.zone  from zoodb.employee e
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