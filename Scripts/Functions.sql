create procedure zoodb.IUD_Vet(@fname varchar(15), @lname varchar(15), @license_ID char(9), @specialty varchar(20),@StatementType nvarchar(20) = '')
as 
	BEGIN  
	IF @StatementType = 'Insert'  
	BEGIN  
	insert into zoodb.veterinarian (fname, lname, license_ID, specialty) values(@fname, @lname, @license_ID, @specialty)  
	END  
	IF @StatementType = 'Update'  
	BEGIN  
	UPDATE zoodb.veterinarian SET  
	fname = fname, lname = lname, 
	specialty = @specialty  
	WHERE  license_ID = @license_ID 
	END  
	else IF @StatementType = 'Delete'  
	BEGIN  
	DELETE FROM zoodb.veterinarian WHERE license_ID = @license_ID 
	END  
end  
go

create function zoodb.getVetList() returns table
as
	return(
	SELECT license_ID,fname,lname FROM zoodb.veterinarian
	);
go

select * from zoodb.getVetList()
go

create function zoodb.getVet(@license_ID char(9)) returns table
as
	return(
	SELECT fname,lname,specialty FROM zoodb.veterinarian WHERE license_ID=@license_ID
	);
go

CREATE TRIGGER zoodb.validateVet ON zoodb.veterinarian
INSTEAD OF UPDATE,INSERT
as
begin
	Select *
	Into   #Temp
	From   inserted

	WHILE (SELECT count(*) FROM #Temp) >0
	begin
		DECLARE @license_ID as char(9);
		SELECT TOP 1 @license_ID = license_ID FROM #Temp;
		if  (SELECT count(*) FROM zoodb.veterinarian WHERE license_ID=@license_ID) > 0
		begin
			DELETE #TEMP WHERE license_ID=@license_ID;
			RAISERROR('LICENSE_ID ALREADY EXISTS', 16, 1);
		end
		ELSE
		begin
			insert into zoodb.veterinarian SELECT * FROM #TEMP WHERE license_ID=@license_ID;
			DELETE #TEMP WHERE license_ID=@license_ID;
		end
	end	
end
go