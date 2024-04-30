DROP TABLE IF EXISTS catalog1.Movie;
DROP TABLE IF EXISTS catalog1.Book;
DROP TABLE IF EXISTS catalog1.TvShow;
DROP TABLE IF EXISTS catalog1.User;
DROP TABLE IF EXISTS catalog1.Review;


--====================
--TABELA FILME

CREATE TABLE catalog1.Movie (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,titulo varchar(30)
  ,diretor varchar(30)
  ,elenco_principal varchar(30)
  ,pais varchar(30)
  ,ano int
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


--====================
-- TABELA LIVRO

CREATE TABLE catalog1.Book (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,titulo varchar(50)
  ,autor varchar(40)
  ,editora varchar(50)
);

INSERT INTO catalog1.Book (titulo, autor, editora)
VALUES ("Mito de sisifo", "Albert Camus", "Editora Record");

--====================
-- TABELA TV SHOW

CREATE TABLE catalog1.TvShow(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,titulo varchar(50)
  ,diretor varchar(40)
  ,elenco_principal varchar(255)
);

INSERT INTO catalog1.TvShow (titulo, diretor, elenco_principal)
VALUES ("Breaking bad","Vince Gilligan", "Aaron Paul, Bryan Cranston");


--====================
-- TABELA USER

CREATE TABLE catalog1.User(
   id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,name VARCHAR(255)
  ,age INT
  ,email VARCHAR(255) UNIQUE
  ,password VARCHAR(255)
  ,userRole VARCHAR(255)
  ,rememberMe BOOLEAN
  ,returnUrl VARCHAR(255)
);

INSERT INTO catalog1.User
VALUES
(100, "giovanna G Micher", 21, "giovanna@gmail", "euamoolucas","user",true,NULL),
(101, "Lucas G", 21, "admin", "admin","user",false,NULL);



--====================
-- TABELA REVIEW 

CREATE TABLE catalog1.Review (
  reviewId INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,userId INT NOT NULL
  ,itemId INT NOT NULL
  ,rating INT NOT NULL
  ,reviewText VARCHAR(255)
  ,datetimeReview TIMESTAMP
  ,CONSTRAINT FK_UserID FOREIGN KEY (userId)
    REFERENCES User(id)
  ,CONSTRAINT FK_MovieID FOREIGN KEY (itemId)
    REFERENCES Movie(id)
);


INSERT INTO catalog1.Review
VALUES 
(10000, 101, 1,3,"Filme excelente", CURRENT_TIMESTAMP),
(10001, 100, 1,3,"Filme muito legal", CURRENT_TIMESTAMP),
(10002, 101, 2,1,"Filme fraco", CURRENT_TIMESTAMP);

