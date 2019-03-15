USE BooksUse
GO

INSERT INTO Books (title, ISBN, [year], author, unitsInStock, price)
VALUES ('Business Result second edition Pre-intermediate', '425-0-19-473886-5', '20190101', 'David Grant, John Hughes & Jon Naunton', 5, 45),
('Business Result first edition Pre-intermediate', '978-0-19-245874-9', '20190101', 'David Grant, John Hughes & Jon Naunton', 25, 40),
('Business Result second edition Intermediate', '333-5-20-247325-6', '20190101', 'John Hughes & Jon Naunton', 0, 42),
('Business Result first edition Intermediate', '176-0-98-241212-8', '20190101', 'John Hughes & Jon Naunton', 3, 39),
('Book 3', '754-0-42-698591-6', '20190101', 'Author 2 & Author 4', 20, 10),
('Book 4', '358-0-58-587784-6', '20190101', 'Author 4', 1, 90),
('Book 5', '844-0-55-985785-6', '20190101', 'Author 4, Author 5 & Author 6', 8, 15.40),
('Book 6', '245-0-44-365986-6', '20190101', 'Author 1', 2, 4.50),
('Book 7', '984-0-57-245551-6', '20190101', 'AUthor 34', 80, 120);

INSERT INTO Roles (name)
VALUES ('Professeurs'),
('Secrétariat');

INSERT INTO Users (intranetUserID, initials, firstName, lastName, FK_Roles, email, phone, [password])
VALUES (12, 'CXA', 'Xavier', 'Carrel', 1, 'xavier.carrel@cpnv.ch', '0799865324', '3i2gf8d7gr3wigusodfp8'),
(24, 'BPA', 'Pascal', 'Benzonana', 1, 'pascal.benzonana@cpnv.ch', '0768754298', 'nsoidrz8bhsbr37sab'),
(34, 'DJE', 'Jean', 'Dupont', 2, 'jean.dupont@cpnv.ch', '0786452424', 'uz23zdsuifiub23ib');

INSERT INTO Requests (forYear, approved, FK_Users, FK_Books)
VALUES (2018, 0, 2, 1),
(2017, 0, 2, 2),
(2018, 1, 2, 3),
(2017, 1, 2, 4),
(2017, 0, 1, 5),
(2018, 0, 1, 6),
(2017, 1, 1, 7);
