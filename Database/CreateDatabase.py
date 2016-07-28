#!/usr/bin/env python
#@Author: Devashish Kumar Yadav
#@Github: github.com/yadavdev
#@Application: MessManagement 1.0
#@License: MIT License

import MySQLdb
import sys

DATABASE_NAME = "mess_db"
DATABASE_NAME = "mess_db"
USER = "root"
PASSWORD = "rootpa55word"
SERVER = "localhost"

try:
        # Opening databse connection
        # Preparing cursor object
        # Change password as required here and in application code
    db = MySQLdb.connect(SERVER,USER,PASSWORD)
    cursor= db.cursor()
except MySQLdb.Error, e:
    print "Database Selection Error."
    raise e
cursor.execute("create database if not exists "+DATABASE_NAME)
cursor.execute("use " + DATABASE_NAME)

print "WARNING: This script will create tables in the database named "+DATABASE_NAME+".\nAll previous data and application login information (the login table) might be overwritten"
print "If updating database make sure to take a backup first. Run \" mysqldump -uUSER -pYOURPASSWORD DATABASE_NAME > backup_orig.sql\" "
print "Note: Change the username and password for mysql as and when required"
print "Make sure you have the backup .sql. Want to continue?"
while 1:
        call = raw_input("Press y or n:")
        if call=='y':
                sure = raw_input("Dude you really sure? y or n:")
                if sure=='y':
                        break;
                else:
                        print "Run script after taking proper Backup"
                        sys.exit(0)
        elif call=='n':
                print "Run script after taking proper Backup"
                sys.exit(0)

# Below line is commented to prevent accidental deletion.
#cursor.execute("DROP TABLE IF EXISTS Student")

print "Database has: " + str(cursor.execute("show tables")) + " tables."

print "Creating Table: Student"
createtable = """CREATE TABLE IF NOT EXISTS Student (
                name VARCHAR(45),
                roll INT not null unique primary key,
                remark VARCHAR(45)
                )"""
cursor.execute(createtable)

print "Creating Table: Smt"
createtable = """CREATE TABLE  IF NOT EXISTS Smt(
                sid INT not null auto_increment primary key, 
                roll INT not null,
                date DATETIME not null DEFAULT CURRENT_TIMESTAMP,
                item VARCHAR(45) not null,
                quantity INT not null default 0,
                foreign key(roll) references Student(roll),
                price INT not null default 0
                )"""
cursor.execute(createtable)

print "Creating Table: Salary"
createtable = """CREATE TABLE IF NOT EXISTS Salary (
                slrytype VARCHAR(20) primary key,
                wage DOUBLE not null
                )"""
cursor.execute(createtable)

print "Creating Table: Employees"
createtable = """CREATE TABLE  IF NOT EXISTS Employees(
                sno INT not null auto_increment primary key, 
                name VARCHAR(45) not null,
                addr VARCHAR(100),
                mobno VARCHAR(10),
                category VARCHAR(20)  not null,
                wage LONG not null ,
                Accno   VARCHAR(45),
                BankName VARCHAR(45)
                )"""
cursor.execute(createtable)


print "Creating Table: Supplier"
createtable = """CREATE TABLE  IF NOT EXISTS  Supplier (
                sid INT not null primary key  auto_increment, 
                name VARCHAR(45) not null,
                article VARCHAR(45) not null
                )"""
cursor.execute(createtable)

print "Creating Table: VendorInvoice"
createtable = """CREATE TABLE  IF NOT EXISTS  VendorInvoices(
                sno INT not null auto_increment primary key,
                sid INT not null ,
                invno VARCHAR(30) not null,
                item VARCHAR(50) not null,
                date DATETIME not null Default CURRENT_TIMESTAMP,
                foreign key(sid) references Supplier(sid),
                puramt DOUBLE,
                discount DOUBLE
                )"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS  ClosingStock(
                article VARCHAR(45) primary key,
                unit DOUBLE not null,
                balance DOUBLE not null,
                rate DOUBLE not null
                )"""
cursor.execute(createtable)

print "Creating Table: Menu"
createtable = """CREATE TABLE  IF NOT EXISTS  Menu(  
                article VARCHAR(45) not null,
                rate DOUBLE not null,
                day int not null,
                primary key (article,day) 
                )"""
cursor.execute(createtable)

print "Creating Table: FixedMenu"
createtable = """CREATE TABLE IF NOT EXISTS FixedMenu(  
                article VARCHAR(45) not null primary key,
                rate DOUBLE not null
                )"""
cursor.execute(createtable)

print "Creating Table: WorkerAttendence"
createtable = """CREATE TABLE IF NOT EXISTS WorkerAttendence(  
                name VARCHAR(45) not null ,
                sno INT  not null,
                foreign key(sno) references Employees(sno),
                bio INT not null,
                diff INT not null,
                month VARCHAR(20) not null,
                year VARCHAR(20) not null,
                advance DOUBLE not null,
                primary key (name,month,year)
                )"""
cursor.execute(createtable)

print "Creating Table: EmployeeShare"
createtable = """CREATE TABLE IF NOT EXISTS EmployeeShare(  
                epf DOUBLE not null,
                esi DOUBLE not null
                )"""
cursor.execute(createtable)

db.close()

