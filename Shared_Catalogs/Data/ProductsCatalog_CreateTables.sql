DROP TABLE ProductReviews
DROP TABLE Products
DROP TABLE Categories
DROP TABLE Manufacturers

CREATE TABLE Manufacturers 
(
	Id int not null identity primary key,
	ManufactureName nvarchar(50) not null unique
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique,
)

CREATE TABLE Products
(
    ArticleNumber nvarchar(50) primary key,
    Title nvarchar(200) not null unique,
    Description nvarchar(max),
    ManufacturerId int not null references Manufacturers(Id),
    CategoryId int not null references Categories(Id),

)

CREATE TABLE ProductReviews
(
    Id int not null identity primary key,
    Reviews nvarchar(max) not null,
    ArticleNumber nvarchar(50) not null references Products(ArticleNumber)
)
