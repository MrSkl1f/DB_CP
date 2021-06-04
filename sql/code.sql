create table if not exists UserInfo (
    id int not null primary key,
    login varchar(30) not null,
    hash varchar(30) not null,
    permission int not null
);
--\COPY UserInfo FROM 'C:/Users/MrSklif/Desktop/BMSTU/6sem/DB_CP/sql/UserInfo.csv' DELIMITER ',' csv;

create table if not exists Management (
    ManagementID int not null primary key,
    AnalysistID int references UserInfo(id),
    ManagerID int references UserInfo(id)
);

create table if not exists Team (
    TeamID int not null primary key,
    ManagementID int references Management(ManagementID),
    Name varchar(30) not null,
    HeadCoach varchar(30) not null,
    Country varchar(30) not null,
    Stadium varchar(60) not null,
    Balance int not null
);

create table if not exists Statistics (
    StatisticsID int not null primary key,
    numberOfWashers int not null,
    averageGameTime int not null
);

create table if not exists Player (
    PlayerID int not null primary key,
    TeamID int references Team(TeamID),
    Statistics int references Statistics(StatisticsID),
    name varchar(30) not null,
    position varchar(10) not null,
    weight int not null,
    height int not null,
    number int not null,
    age int not null,
    country varchar(50) not null,
    cost int not null
);

create table if not exists AvailableDeals (
    Id int primary key not null,
    PlayerID int references Player(PlayerID),
    ToManagementID int references Management(ManagementID),
    FromManagementID int references Management(ManagementID),
    cost int not null,
    status int not null
);

create table if not exists DesiredPlayers (
    Id int primary key not null,
    PlayerID int references Player(PlayerID),
    TeamID int references Team(TeamID),
    ManagementID int references Management(ManagementID)
); 

-- Password encryption
create or replace procedure password_encryption(new_password varchar(30), UserId int)
language plpgsql
as $$
begin 
	update UserInfo
	set hash = new_password
	where id = UserId;
end
$$;

insert into UserInfo values (1, '567', '567', 1);
insert into UserInfo values (2, '567', '567', 2);
insert into Management values (1, 1, 2);
insert into Team values (1, 1, 'Dynamo', 'Denis Sklif', 'Russia', 'VTB', 5000);
insert into Statistics values (1, 30, 25);
insert into Statistics values (2, 20, 15);
insert into Player values (1, 1, 1, 'Vlad', 'ca', 75, 180, 17, 27, 'Russia', 500);
insert into Player values (2, 1, 2, 'Denis', 'ca', 93, 193, 21, 20, 'Russia', 500);
