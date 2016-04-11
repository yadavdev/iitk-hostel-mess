#!/usr/bin/env python

import MySQLdb
import sys
# Opening databse connection
# Preparing cursor object

DATABASE_NAME = "mess_db"
try:
	conn = MySQLdb.connect("localhost","root","gaurav")
	cursor = conn.cursor()
except MySQLdb.Error, e:
	print "Database Connection Error"
	raise e

#print cursor.execute( "CREATE DATABASE IF NOT EXISTS DATABASE_NAME)

try:
	db = MySQLdb.connect("localhost","root","gaurav",DATABASE_NAME)
	cursor= db.cursor()
except MySQLdb.Error, e:
	print "Database selection Error"
	raise e

# print db
# Adding students to database
#y12 = open("hall9_list.txt")
#y14 = open("y14.txt")
#y15 = open("y15.txt")
# db = conn.database


# print "This script will create tables in the database named test.\nAll previous data using application login information (the login table) will be overwritten"
# print "If upgdating database make sure to take a backup first. Run \" mysqldump -uroot -ptiger test > backup_orig.sql\" "
# print "Note: Change the username and password for mysql as and when required"
# print "Make sure you have the backup .sql. Want to continue?"
# while 1:
# 	call = raw_input("Press y or n:")
# 	if call=='y':
# 		sure = raw_input("Dude you really sure? y or n:")
# 		if sure=='y':
# 			break;
# 		else:
# 			sys.exit(0)
# 	elif call=='n':
# 		sys.exit(0)


#cursor.execute("DROP TABLE IF EXISTS Student")

print cursor.execute("show tables")

createtable = """CREATE TABLE IF NOT EXISTS Student (
				name VARCHAR(45),
				roll INT not null unique primary key,
				remark VARCHAR(45)
				)"""
cursor.execute(createtable)

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

createtable = """CREATE TABLE IF NOT EXISTS Salary (
				slrytype VARCHAR(20) primary key,
				wage DOUBLE not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS Employees(
				sno INT not null auto_increment primary key, 
				name VARCHAR(45) not null,
				addr VARCHAR(100),
				mobno VARCHAR(10),
				category VARCHAR(20)  not null,
				wage LONG not null ,
				Accno	VARCHAR(45),
				BankName VARCHAR(45)
				)"""
cursor.execute(createtable)



createtable = """CREATE TABLE  IF NOT EXISTS  Supplier (
				sid INT not null primary key  auto_increment, 
				name VARCHAR(45) not null,
				article VARCHAR(45) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS  VendorInvoices(
				sno INT not null primary key,
				sid INT not null ,
				invno VARCHAR(30) not null,
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

createtable = """CREATE TABLE  IF NOT EXISTS  Menu(  
				article VARCHAR(45) not null,
				rate DOUBLE not null,
				day int not null,
				primary key (article,day) 
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE IF NOT EXISTS FixedMenu(  
				article VARCHAR(45) not null primary key,
				rate DOUBLE not null
				)"""
cursor.execute(createtable)

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


createtable = """CREATE TABLE IF NOT EXISTS EmployeeShare(  
				epf DOUBLE not null,
				esi DOUBLE not null
				)"""
cursor.execute(createtable)



