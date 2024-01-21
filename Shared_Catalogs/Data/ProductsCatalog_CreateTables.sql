DROP TABLE Products
DROP TABLE Categories
DROP TABLE StockQuantities
DROP TABLE Manufacturers

CREATE TABLE Manufacturers 
(
	Id int not null identity primary key,
	Manufacturer nvarchar(50) not null unique
)

CREATE TABLE StockQuantities 
(
	Id int not null identity primary key,
	Quantity bit not null,
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique,
)

CREATE TABLE Products
(
	ArticleNumber varchar(30) primary key,
	Title nvarchar(200) not null unique,
	Description nvarchar(max),

	ManufacturerId int not null references Manufacturers(Id),
	CategoryId int not null references Categories(Id),
	StockQuantity int not null references StockQuantities(Id)
)