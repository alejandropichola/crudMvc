create database sicge;
use sicge;
IF(EXISTS (SELECT * FROM persons))
SELECT 'LA TABLA YA EXISTE :V' AS 'TABLE'
ELSE
create table persons(
	id int identity primary key,
	cui varchar(15),
	first_name varchar(100),
	last_name varchar(100),
	gender varchar(1),
	birthDate date,
	password varchar(100),
	phone varchar(15),
	cell_phone varchar(15),
	email varchar(100),
	creted_at datetime,
	updated_at datetime
);

SELECT * FROM persons;

truncate table persons;

DECLARE @time datetimeoffset(4)= '2018-12-14 00:00:00'
INSERT INTO persons VALUES('93001266', 'Maiver Alejandro',
'Picholá Gua', 'M', '1997-03-03', 'secret', '31583408',
'31583408', 'g4alejandro29@gmail.com',
@time,@time);

/* list person*/
IF (EXISTS(SELECT * FROM sys.objects WHERE name='person_list' AND type='P'))
SELECT 'ya existe el proceso' AS 'PROCESOS'
ELSE
EXEC('CREATE PROCEDURE person_list AS
	BEGIN
SELECT id, cui, first_name as firstName, last_name as lastName, gender, birth_date as birthDate, phone, cell_phone as cellPhone, email FROM persons;
	 END');


/* create procedure search person*/
CREATE PROCEDURE person_search(@search varchar(250)) AS
SELECT id, cui, first_name as firstName, last_name as lastName, gender,
birth_date as birthDate, phone, cell_phone as cellPhone, 
email FROM persons WHERE first_name LIKE '%'+ @search +'%';


/* search person*/
execute person_search @search='alejandro';

SELECT id, cui, first_name as firstName, last_name as lastName, gender, birth_date as birthDate, password, phone, cell_phone as cellPhone, email FROM persons;

/* señect procedure */
SELECT * FROM sys.objects WHERE name='person_list' AND type='P';

/* select database */
SELECT @@SERVERNAME;
/* create procedure update person*/
IF (EXISTS(SELECT * FROM sys.objects WHERE name='person_insert' AND type='P'))
SELECT 'El proceso ya existe' AS 'proseso'
ELSE
EXECUTE('
	CREATE PROCEDURE person_insert(
		@cui varchar(15),
		@firstName varchar(100),
		@lastName varchar(100),
		@gender varchar(1),
		@birthDate date,
		@pass varchar(100),
		@phone varchar(15),
		@cellPhone varchar(15),
		@email varchar(150),
		@createdAt datetime,
		@updatedAt datetime
	)
	AS	INSERT INTO persons values(@cui, @firstName, @lastName, @gender,
	@birthDate, @pass, @phone, @cellPhone, @email, CONVERT(DATETIME, @createdAt, 103), CONVERT(DATETIME, @updatedAt, 103))
');

/* insert person*/
IF (EXISTS(SELECT * FROM sys.objects WHERE name='person_insert' AND type='P'))
BEGIN
	DECLARE @time1 datetimeoffset(4)= '2018-12-15 00:00:00';
EXECUTE person_insert @cui = '93001265',
	@firstName = 'Maiver Alejandro'
	,@lastName = 'Picholá Gua'
	,@gender = 'M', @birthDate = '1997-03-03', @pass = 'secret', @phone = '31583408',
	@cellPhone = '31583408', @email = 'g4alejandro29@gmail.com',
	@createdAt = @time1, @updatedAt = @time1;
END;

/* create procedure delete person*/
IF (EXISTS (SELECT * FROM sys.objects WHERE name='person_delete' AND type='P'))
	SELECT 'el proceso ya existe' as 'PROCESOS'
ELSE
EXECUTE('CREATE PROCEDURE person_delete(@id int) AS DELETE FROM persons WHERE id=@id');
execute person_delete @id = 3;

/* create procedure update person*/
IF (EXISTS(SELECT * FROM sys.objects WHERE name='person_update' AND type='P'))
	SELECT 'Proceso ya existe' AS 'PROCESOS'
ELSE
EXECUTE('
CREATE PROCEDURE person_update(
	@id int,
	@cui varchar(15),
	@firstName varchar(100),
	@lastName varchar(100),
	@gender varchar(1),
	@birthDate date,
	@pass varchar(100),
	@phone varchar(15),
	@cellPhone varchar(15),
	@email varchar(150),
	@updatedAt datetime
) AS
IF (@pass != NULL AND LEN(@pass) > 0)
	UPDATE persons SET cui=@cui, first_name=@firstName,
	last_name=@lastName, gender=@gender, birth_date=@birthDate, password=@pass, phone=@phone, cell_phone=@cellPhone, email=@email, updated_at=@updatedAt WHERE id=@id;
ELSE
	UPDATE persons SET cui=@cui, first_name=@firstName,
	last_name=@lastName, gender=@gender, birth_date=@birthDate, phone=@phone, cell_phone=@cellPhone, email=@email, updated_at=@updatedAt WHERE id=@id;
');

