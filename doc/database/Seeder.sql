USE BooksUse
GO

INSERT INTO Books (title, ISBN, author, unitsInStock, price)
VALUES ('Business Result second edition Pre-intermediate', '425-0-19-473886-5', 'David Grant, John Hughes & Jon Naunton', 5, 45),
('Business Result first edition Pre-intermediate', '978-0-19-245874-9', 'David Grant, John Hughes & Jon Naunton', 25, 40),
('Business Result second edition Intermediate', '333-5-20-247325-6', 'John Hughes & Jon Naunton', 0, 42),
('Business Result first edition Intermediate', '176-0-98-241212-8', 'John Hughes & Jon Naunton', 3, 39),
('Formulaires et tables', '978-2-940501-41-0', 'Commissions romandes de mathématique', 54, 15),
('Physique', '978-2-10-007-169-2', 'Joseph Kane & Morton Sternheim', 13, 85),
('Book 3', '754-0-42-698591-6', 'Author 2 & Author 4', 20, 10),
('Book 4', '358-0-58-587784-6', 'Author 4', 1, 90),
('Book 5', '844-0-55-985785-6', 'Author 4, Author 5 & Author 6', 8, 15.40),
('Book 6', '245-0-44-365986-6', 'Author 1', 2, 4.50),
('Book 7', '984-0-57-245551-6', 'AUthor 34', 80, 120),
('Book 8', '542-0-42-142842-6', 'Author 45', 1, 9.90);

INSERT INTO Roles (name)
VALUES ('Professeurs'),
('Secrétariat');

INSERT INTO SchoolClasses (name, studentsnumber)
VALUES ('SI-T2a', 7),
('SI-T2b', 5),
('SI-T1a', 12),
('SI-T1b', 10),
('SI-C2a', 20),
('SI-C3b', 18),
('SI-C4a', 13),
('SI-C4b', 17);

INSERT INTO Years (title, [open])
VALUES (2016, 0),
(2017, 0),
(2018, 1);

INSERT INTO Users (intranetUserID, initials, firstName, lastName, FK_Roles, email, phone, [password])
VALUES (12, 'CXA', 'Xavier', 'Carrel', 1, 'xavier.carrel@cpnv.ch', '0799865324', '3i2gf8d7gr3wigusodfp8'),
(24, 'BPA', 'Pascal', 'Benzonana', 1, 'pascal.benzonana@cpnv.ch', '0768754298', 'nsoidrz8bhsbr37sab'),
(34, 'DJE', 'Jean', 'Dupont', 2, 'jean.dupont@cpnv.ch', '0786452424', 'uz23zdsuifiub23ib');

INSERT INTO Requests (approved, FK_Years, FK_Users, FK_Books)
VALUES (0, 1, 2, 1),
(0, 1, 2, 2),
(1, 2, 2, 3),
(1, 2, 2, 4),
(0, 3, 1, 5),
(1, 3, 1, 6),
(0, 3, 1, 7),
(0, 3, 1, 8),
(1, 3, 1, 9),
(0, 3, 1, 10),
(0, 3, 1, 4),
(0, 3, 1, 4),
(1, 3, 2, 6),
(0, 3, 2, 5),
(1, 3, 2, 4);

INSERT INTO SchoolClasses_Requests (FK_SchoolClasses, FK_Requests)
VALUES (1, 7),
(2, 7),
(3, 7),
(4, 7),
(1, 5),
(2, 5),
(4, 4),
(5, 5),
(6, 5);