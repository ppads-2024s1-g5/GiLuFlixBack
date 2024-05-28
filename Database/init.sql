DROP TABLE IF EXISTS catalog1.Review;
DROP TABLE IF EXISTS catalog1.Book;
DROP TABLE IF EXISTS catalog1.TvShow;
DROP TABLE IF EXISTS catalog1.User;
DROP TABLE IF EXISTS catalog1.Movie;

#TABELA FILME

CREATE TABLE catalog1.Movie (
  Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,Title varchar(30)
  ,Director varchar(30)
  ,Cast varchar(30)
  ,Country varchar(30)
  ,Year int
);

INSERT INTO catalog1.Movie
VALUES 
(1, "Goodfellas", "Martin Scorsese", "...", "USA", 1990),
(2, "O Poderoso Chefão", "Francis Ford Coppola", "...", "USA", 1972),
(3, "12 Homens e Uma Sentença", "Sidney Lumet", "...", "USA", 1957),
(4, "A Lista de Schindler", "Steven Spielberg", "...", "USA", 1993),
(5, "O Senhor dos Anéis", "Peter Jackson", "...", "Nova Zelândia", 2003),
(6, "A Viagem de Chihiro", "Hayao Miyazaki", "...", "Japão", 2001),
(7, "Toy Story", "John Lasseter", "...", "USA", 1995),
(8, "Parasita", "Bong Joon-ho", "...", "Coreia do Sul", 2019),
(9, "Cidade de Deus", "Fernando Meirelles", "...", "Brasil", 2002),
(10, "O Silencio dos Inocentes", "Jonathan Demme", "...", "USA", 1991);


# TABELA LIVRO

CREATE TABLE catalog1.Book (
  Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,Title varchar(50)
  ,Author varchar(40)
  ,Publisher varchar(50)
);

INSERT INTO catalog1.Book (Id, Title, Author, Publisher)
VALUES (1000,"Mito de sisifo", "Albert Camus", "Editora Record"),
       (1001,"Clean Code", "Uncle Bob", "Editora Record"),
       (1002,"Mito de sisifo", "Albert Camus", "Editora Record"),
       (1003,"Mito de sisifo", "Albert Camus", "Editora Record")
       ;

# TABELA TV SHOW

CREATE TABLE catalog1.TvShow(
  Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,Title varchar(50)
  ,Director varchar(40)
  ,Cast varchar(255)
);

INSERT INTO catalog1.TvShow (Id,Title, Director, Cast)
VALUES (3000, "Breaking bad","Vince Gilligan", "Aaron Paul, Bryan Cranston"),
        (3001, "The office","...", "Michael Scott, Bryan Ryan");


# TABELA USER

CREATE TABLE catalog1.User(
   Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,Name VARCHAR(255)
  ,Age INT
  ,Email VARCHAR(255) UNIQUE
  ,Password VARCHAR(255)
  ,UserRole VARCHAR(255)
  ,RememberMe BOOLEAN
  ,ReturnUrl VARCHAR(255)
);

INSERT INTO catalog1.User
VALUES
(100, "giovanna G Micher", 21, "giovanna@gmail", "euamoolucas","user",true,NULL),
(101, "Lucas G", 21, "admin", "admin","user",false,NULL);
(102, "Vitor", 29, "vitor@gmail", "123","user",true,NULL),
(103, "Arthur", 30, "arthur@gmail.com", "123","user",false,NULL);

# TABELA AMIZADE E PEDIDOS DE AMIZADE
CREATE TABLE catalog1.Friendships (
    FriendshipId INT PRIMARY KEY AUTO_INCREMENT,
    UserId1 INT,
    UserId2 INT,
    FOREIGN KEY (UserId1) REFERENCES User(Id),
    FOREIGN KEY (UserId2) REFERENCES User(Id),
    UNIQUE KEY (UserId1, UserId2)
);

CREATE TABLE catalog1.FriendshipRequests (
    RequestId INT PRIMARY KEY AUTO_INCREMENT,
    RequesterId INT,
    RecipientId INT,
    FOREIGN KEY (RequesterId) REFERENCES User(Id),
    FOREIGN KEY (RecipientId) REFERENCES User(Id)
);

CREATE VIEW catalog1.AllItems AS 
SELECT Id FROM catalog1.Book
UNION ALL 
SELECT Id FROM catalog1.Movie
UNION ALL 
SELECT Id FROM catalog1.TvShow;


# TABELA REVIEW 

CREATE TABLE catalog1.Review (
  ReviewId INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,UserId INT NOT NULL
  ,ItemId INT NOT NULL
  ,Rating INT NOT NULL
  ,ReviewText VARCHAR(255)
  ,Likes INT
  ,DatetimeReview TIMESTAMP
  ,CONSTRAINT FK_UserID FOREIGN KEY (userId)
    REFERENCES User(Id)
);

INSERT INTO catalog1.Review 
VALUES 
(10000, 101, 1,3,"Filme excelente", 0, CURRENT_TIMESTAMP),
(10001, 100, 1,3,"Filme muito legal", 0, CURRENT_TIMESTAMP),
(10002, 101, 2,1,"Filme fraco", 10, CURRENT_TIMESTAMP);

