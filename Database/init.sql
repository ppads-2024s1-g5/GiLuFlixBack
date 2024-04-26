CREATE TABLE catalog1.Movie (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY
  ,titulo varchar(30)
  ,diretor varchar(30)
  ,elenco_principal varchar(30)
  ,pais varchar(30)
  ,ano int
);

INSERT INTO catalog1.Movie
VALUES (1, "Goodfellas", "Martin Scorsese", "...", "USA", 1990),
(2, "O Poderoso Chefão", "Francis Ford Coppola", "...", "USA", 1972),
(3, "12 Homens e Uma Sentença", "Sidney Lumet", "...", "USA", 1957),
(4, "A Lista de Schindler", "Steven Spielberg", "...", "USA", 1993),
(5, "O Senhor dos Anéis", "Peter Jackson", "...", "Nova Zelândia", 2003),
(6, "A Viagem de Chihiro", "Hayao Miyazaki", "...", "Japão", 2001),
(7, "Toy Story", "John Lasseter", "...", "USA", 1995),
(8, "Parasita", "Bong Joon-ho", "...", "Coreia do Sul", 2019),
(9, "Cidade de Deus", "Fernando Meirelles", "...", "Brasil", 2002),
(10, "O Silencio dos Inocentes", "Jonathan Demme", "...", "USA", 1991);

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


CREATE TABLE catalog1.Review (
  reviewId INT NOT NULL
  ,userId INT NOT NULL
  ,movieId INT NOT NULL
  ,rating INT NOT NULL
  ,reviewText VARCHAR(255)
  ,PRIMARY KEY (idAvaliacao)
  ,CONSTRAINT FK_UserID FOREIGN KEY (userId)
    REFERENCES User(id)
  ,CONSTRAINT FK_MovieID FOREIGN KEY (movieId)
    REFERENCES Movie(id)
);