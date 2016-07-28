CREATE SCHEMA `mess_login` ;
CREATE TABLE `mess_login`.`login` (id VARCHAR(45) not null primary key, password VARCHAR(45) not null, privilege INT not null DEFAULT 1);
INSERT into `mess_login`.`login` (id, password,privilege) values ("admin", "admin", 0);
INSERT into `mess_login`.`login` (id, password,privilege) values ("operator", "operator", 1);