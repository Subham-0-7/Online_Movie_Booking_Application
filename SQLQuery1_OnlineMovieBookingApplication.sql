Create Database OnlineMovieBookingApplication

Use OnlineMovieBookingApplication

Create table Users
(
UserId int primary key identity(1, 1),
UserName varchar(50),
Password varchar(50),
Email varchar(50),
FirstName varchar(50),
LastName varchar(50),
Address varchar(100),
ContactNo varchar(50),
IsAdmin int not null
)

Update Users
Set isAdmin = 1
Where UserId = 1


Create table Movies
(
MovieId int primary key identity(101, 1),
MovieName varchar(100),
Synopsis varchar(1000),
Director varchar(50),
Duration varchar(50),
Genre varchar(50),
Rating int,
MovieImage varchar(100),
MoviePrice int
)

CREATE TABLE Bookings (
	BookingId int primary key identity(100, 1),
	UserId int,
	MovieId int,
	MoviePrice int,
    BookingDate DATE NOT NULL,
	ShowTime varchar(100),
    NumberOfTickets INT NOT NULL,
    TotalPrice AS (NumberOfTickets * MoviePrice) PERSISTED
);


CREATE TABLE PaymentDetails (
    PaymentId INT PRIMARY KEY IDENTITY(1001,1),
	TransactionId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    UserId INT NOT NULL,
    MovieId INT NOT NULL,
	Amount INT NOT NULL,
	PaymentDate DATETIME,
    IsConfirmed BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (MovieId) REFERENCES Movies(MovieId)
);


ALTER TABLE Bookings
ADD CONSTRAINT FK_Bookings_Users
FOREIGN KEY (UserId) REFERENCES Users(UserId)
ON DELETE CASCADE;

ALTER TABLE Bookings
ADD CONSTRAINT FK_Bookings_Movies
FOREIGN KEY (MovieId) REFERENCES Movies (MovieId)
ON DELETE CASCADE;


Select * from Bookings
Select * from PaymentDetails
Select * from Users
Select * from Movies

--truncate table PaymentDetails
--truncate table Bookings




