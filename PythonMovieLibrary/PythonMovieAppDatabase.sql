USE PythonMovieDatabase
CREATE TABLE MoviesandReviews
(
	MovieID INT NOT NULL IDENTITY(1,1),
	MovieName varchar(255),
	MovieRating varchar(255),
	RottenTomatoesScore varchar(255), 

);

SELECT * FROM MoviesandReviews
INSERT INTO MoviesandReviews (MovieName, MovieRating, Genre, YearReleased)
VALUES ('TheBatman', '5.1', 'Crime/Action', '2022')

 ALTER TABLE MoviesandReviews
 ADD ID INT NOT NULL IDENTITY(1,1)

 DELETE FROM MoviesandReviews WHERE MovieName = 'TheBlackPhone'

UPDATE MoviesandReviews SET MovieName = 'ApocalypseNow' WHERE ID = 4;