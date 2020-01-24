create database PizzaDb;
go
use PizzaDb;
go
create schema PizzaBox;
go

create table PizzaBox.Users( 
	Id int Identity(1,1),
	username varchar not null, 
	pass varchar not null,
	isEmployee bit not null,
	Primary key (Id)
)

create table PizzaBox.Toppings( 
	Id int Identity(1,1),
	toppingName varchar not null,
	price decimal not null,
	Primary key (Id)
)

create table PizzaBox.Orders( 
	Id int Identity(1,1),
	pizzas varchar not null,
	total decimal not null,
	Primary key (Id)
)

create table PizzaBox.Stores( 
	Id int Identity(1,1),
	storeAddress varchar not null,
	inventory varchar null,  
	sales decimal not null,
	Primary key (Id)
)
 
create table PizzaBox.OurPizzas( 
	Id int Identity(1,1),
	pizzaName varchar not null, 
	toppings varchar not null,    
	price decimal not null,
	Primary key (Id)
)

create table PizzaBox.PanSizes( 
	Id int Identity(1,1),
	Size varchar not null,  
	price decimal not null,
	Primary key (Id)
) 

alter table PizzaBox.OurPizzas
	drop column isEmployee


alter table PizzaBox.Orders 
		 add userid int default(1) 

--alter table orders
alter table PizzaBox.Orders
	add constraint FK_User_Id foreign key (userid) references PizzaBox.Users(Id)

alter table PizzaBox.Orders 
		 add storeid int default(1) 

--alter table orders
alter table PizzaBox.Orders
	add constraint FK_Store_Id foreign key (storeid) references PizzaBox.Stores(Id)

--add crustTypes table
create table PizzaBox.CrustTypes( 
	Id int Identity(1,1),
	CrustName varchar not null,  
	price decimal not null,
	Primary key (Id)
)
--alter OurPizzas table to have 3 nullable values
alter table PizzaBox.OurPizzas
	 SP_RENAME 'PizzaBox.OurPizzas.toppings', 'topping1', 'COLUMN'

alter table PizzaBox.OurPizzas
	alter column topping1 varchar null;

alter table PizzaBox.OurPizzas
	add topping2 varchar null,
		topping3 varchar null;

--alter all varchar columns to store more
--alter columns that have prices to be decimal(num of digits, num after decimal)
--Orders
alter table PizzaBox.Orders
	alter column pizzas varchar(max) not null

alter table PizzaBox.Orders
	alter column total decimal(5,2) not null

--OurPizzas
alter table PizzaBox.OurPizzas
	alter column pizzaName varchar(50) not null

--Stores
alter table PizzaBox.Stores
	alter column storeAddress varchar(50) not null

alter table PizzaBox.Stores
	alter column inventory varchar(max) null

--Toppings
alter table PizzaBox.Toppings
	alter column toppingName varchar(50) not null

alter table PizzaBox.Toppings
	alter column price decimal(5,2) not null
--CrustTypes
alter table PizzaBox.CrustTypes
	alter column price decimal(5,2) not null

alter table PizzaBox.CrustTypes
	alter column CrustName varchar(50) not null
--PanSizes
alter table PizzaBox.PanSizes
	alter column price decimal(5,2) not null

alter table PizzaBox.PanSizes
	alter column Size varchar(10) not null
--Users
alter table PizzaBox.Users
	alter column username varchar(20) not null

alter table PizzaBox.Users
	alter column pass varchar(20) not null

--insert at least one store into Stores table
insert into PizzaBox.Stores(storeAddress,sales) 
					values ('1234 Some Place St','0')



 
--drop toppings 1,2,3 from ourPizzas table and create an OurPizzaToppings table
alter table PizzaBox.OurPizzas
	drop column topping1, topping2, topping3

create table PizzaBox.OurPizzaToppings( 
	PizzaId int not null,
	ToppingId int not null,
	Foreign key(PizzaId) references PizzaBox.OurPizzas(Id),
	Foreign key(ToppingId) references PizzaBox.Toppings(Id)
)

 
--add datetime column to orders
alter table PizzaBox.Orders
	add TimeOrdered smalldatetime not null


--add toppings
insert into PizzaBox.Toppings(toppingName,price) 
					values ('pepporoni','0.50'),
						   ('italian sausage','0.50'),
						   ('mushroom','0.50'),
						   ('bacon','0.50'),
						   ('pineapple','0.50'),
						   ('spinach','0.50'),
						   ('chicken','0.50'),
						   ('meatballs','0.50')

select * from PizzaBox.Toppings 
insert into PizzaBox.OurPizzas(pizzaName,price) 
					values ('Hawaiian','5'),
						   ('MeatLovers','7'),
						   ('The Works','6')

insert into PizzaBox.Toppings(toppingName,price) 
					values ('ham','0.50'),
						   ('peppers','0.50'),
						   ('onions','0.50'),
						   ('black olives','0.50')

insert into PizzaBox.OurPizzaToppings(PizzaId,ToppingId)
				values ('1','5'),
					   ('1','9')

drop table PizzaBox.OurPizzaToppings

--works fine
 

--trying with composite keys
create table PizzaBox.OurPizzaToppings( 
	PizzaId int not null,
	ToppingId int not null,
	Foreign key(PizzaId) references PizzaBox.OurPizzas(Id),
	Foreign key(ToppingId) references PizzaBox.Toppings(Id),
	Primary key(PizzaId,ToppingId)
)


--join 3 tables 
 
  
SELECT op.pizzaName AS pizzaName, t.toppingName AS toppingName FROM PizzaBox.OurPizzas op
  INNER JOIN PizzaBox.OurPizzaToppings opt ON op.Id = opt.PizzaId
  INNER JOIN PizzaBox.Toppings t ON opt.ToppingId = t.Id;
 

SELECT op.pizzaName AS pizzaName, t.toppingName AS toppingName FROM PizzaBox.OurPizzas op
  INNER JOIN PizzaBox.OurPizzaToppings opt ON op.Id = opt.PizzaId
  INNER JOIN PizzaBox.Toppings t ON opt.ToppingId = t.Id;

--LEFT
SELECT op.pizzaName AS pizzaName, t.toppingName AS toppingName FROM PizzaBox.OurPizzas op
  Left JOIN PizzaBox.OurPizzaToppings opt ON op.Id = opt.PizzaId
  Left JOIN PizzaBox.Toppings t ON opt.ToppingId = t.Id;

--RIGHT
SELECT op.pizzaName AS pizzaName, t.toppingName AS toppingName FROM PizzaBox.OurPizzas op
  right JOIN PizzaBox.OurPizzaToppings opt ON op.Id = opt.PizzaId
  right JOIN PizzaBox.Toppings t ON opt.ToppingId = t.Id;

  insert into PizzaBox.CrustTypes(CrustName,price)
	values('Classic','0'),
		  ('Thin"','0'),
		  ('Stuffed Crust"','0.75') 
  
  insert into PizzaBox.Stores(storeAddress,sales)
	values('65421 PaPas Street','0'),
		  ('8754 Dominoes Ave','0'),
		  ('9856 Marcos Blvd"','0') 

update PizzaBox.Stores 
set storeAddress = '9856 Marcos Blvd'
where Id = '5'

update PizzaBox.CrustTypes
set CrustName = 'Thin'
where Id = '2'

update PizzaBox.CrustTypes
set CrustName = 'Stuffed'
where Id = '3'

select * from PizzaBox.PanSizes
select * from PizzaBox.CrustTypes
select * from PizzaBox.Stores
select * from PizzaBox.Users
select * from PizzaBox.OurPizzas
select * from PizzaBox.Orders


