DROP TABLE Products
DROP TABLE Categories
DROP TABLE StockQuantities
DROP TABLE Manufacturers

CREATE TABLE Manufacturers 
(
	Id int not null identity primary key,
	ManufactureName nvarchar(50) not null unique
)

CREATE TABLE StockQuantities 
(
	Id int not null identity primary key,
	Quantity int not null,
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique,
)

CREATE TABLE Products
(
	ArticleNumber varchar(50) primary key,
	Title nvarchar(200) not null unique,
	Description nvarchar(max),

	ManufacturerId int not null references Manufacturers(Id),
	CategoryId int not null references Categories(Id),
	StockQuantityId int not null references StockQuantities(Id)
)