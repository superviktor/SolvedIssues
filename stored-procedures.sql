--tables
create table Product
(Id int, Name varchar(100))
create table ProductDescription
(ProductId int, Description varchar(100))
--data
insert into Product values (680,'HL Road Frame - Black, 58')
,(706,'HL Road Frame - Red, 58')
,(707,'Sport-100 Helmet, Red')
insert into ProductDescription values (680,'Replacement mountain wheel for entry-level rider.')
,(706,'Sturdy alloy features a quick-release hub.')
,(707,'Aerodynamic rims for smooth riding.')

--sp: join two tables
create procedure GetProductsWithDescription
as
begin
set nocount on
select p.Id, p.Name, pd.Description 
from Product p
inner join ProductDescription pd on p.Id = pd.ProductId
end
--exec
EXEC GetProductsWithDescription

--sp: with params
create procedure GetProductWithDescriptionById
(@id int)
as
begin
set nocount on
select p.Id, p.Name, pd.Description 
from Product p
inner join ProductDescription pd on p.Id = pd.ProductId
where p.Id = @id
end
--exec
EXEC GetProductWithDescriptionById 706

--sp: default parmas
create procedure GetProductByIdWithDefaultParam
(@id int = 706)
as
begin
set nocount on
select p.Id, p.Name, pd.Description 
from Product p
inner join ProductDescription pd on p.Id = pd.ProductId
where p.Id = @id
end
--exec
EXEC GetProductByIdWithDefaultParam

--table
CREATE TABLE Employee (Id int identity(1,1),Name varchar(100))
--sp: output param
create procedure NewEmployee
(@name varchar(100), @id int output)
as 
begin 
set nocount on
insert into Employee values(@name)
select @id = SCOPE_IDENTITY()
end
--exec
declare @empId int
exec NewEmployee 'Pert', @empId output
select @empId

--sp: encrypted
create procedure GetEmplployees
with encryption
as 
begin
set nocount on
select * from Employee
end
--view it
sp_helptext GetEmplployees

--rename sp
sp_rename 'SpNameBefore', 'SpNameAfter'