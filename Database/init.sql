CREATE TABLE catalog1.Filme (
  id int
  ,titulo varchar(30)
  ,diretor varchar(30)
  ,elenco_principal varchar(30)
  ,pais varchar(30)
  ,ano int
);

INSERT INTO catalog1.Filme
VALUES (1, "Goodfellas", "Martin Scorsese", "...", "USA", 1990),
(2, "O Poderoso Chefão", "Francis Ford Coppola", "...", "USA", 1972),
(3, "12 Homens e Uma Sentença", "Sidney Lumet", "...", "USA", 1957),
(4, "A Lista de Schindler", "Steven Spielberg", "...", "USA", 1993),
(5, "O Senhor dos Anéis", "Peter Jackson", "...", "Nova Zelândia", 2003),
(6, "A Viagem de Chihiro", "Hayao Miyazaki", "...", "Japão", 2001),
(7, "Toy Story", "John Lasseter", "...", "USA", 1995),
(8, "Parasita", "Bong Joon-ho", "...", "Coreia do Sul", 2019),
(9, "Cidade de Deus", "Fernando Meirelles", "...", "Brasil", 2002),
(10, "O Silêncio dos Inocentes", "Jonathan Demme", "...", "USA", 1991);

