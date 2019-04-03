use BooksUse;

Create table suppliers (
	id int identity primary key,
	suppliername varchar(50)
);
GO

Create table supplier_supply_book (
	id int identity primary key,
	supplier_id int,
	book_id int,
	price decimal (5,2),
	deldelay int -- in days
)
GO

alter table supplier_supply_book add constraint fk_sup_sup foreign key (supplier_id) references suppliers(id);
alter table supplier_supply_book add constraint fk_sup_book foreign key (book_id) references books(id);

GO

insert into suppliers (suppliername) values ('Payot'),('Yopat'),('Toyap');
insert into supplier_supply_book(supplier_id,book_id,price,deldelay) values
(1,1,10,11),
(2,1,11,12),
(3,1,12,19),
(1,2,13,18),
(1,1,14,16),
(2,2,15,12),
(1,3,16,14);
