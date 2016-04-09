#!/usr/bin/env python

import MySQLdb
import sys
# Opening databse connection
# Preparing cursor object

DATABASE_NAME = "test"
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


cursor.execute("DROP TABLE IF EXISTS Student")

print cursor.execute("show tables")

createtable = """CREATE TABLE IF NOT EXISTS Student (
				name CHAR(50),
				roll INT(6) not null unique primary key
				)"""
cursor.execute(createtable)


createtable = """CREATE TABLE IF NOT EXISTS Salary (
				slrytype CHAR(20) primary key,
				wage FLOAT(10) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS Employees(
				sid INT(12) not null auto_increment primary key, 
				name CHAR(50) not null,
				addr CHAR(100),
				mobno INT(11),
				category CHAR(25) not null,
				foreign key(slrytype) references Salary(slrytype),
				slrytype CHAR(20) not null,
				Accno	CHAR(35)
				)"""
cursor.execute(createtable)



createtable = """CREATE TABLE  IF NOT EXISTS  Supplier (
				sid INT(12) not null primary key  auto_increment, 
				name CHAR(50) not null,
				article CHAR(50) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS  DailyVendorPayment(
				date CHAR(15) primary key,
				sid INT(12) not null ,
				foreign key(sid) references Supplier(sid),
				invoice CHAR(30) not null,
				puramt FLOAT(15),
				discount FLOAT(10)
				)"""
cursor.execute(createtable)




createtable = """CREATE TABLE  IF NOT EXISTS  ClosingStock(
				article CHAR(15) primary key,
				unit FLOAT(15) not null,
				bal FLOAT(15) not null,
				rate FLOAT(15) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE  IF NOT EXISTS  Menu(  
				article CHAR(15) primary key unique not null,
				rate FLOAT(12) not null,
				day INT(5) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE IF NOT EXISTS FixedMenu(  
				article CHAR(15) not null primary key unique,
				rate FLOAT(12) not null
				)"""
cursor.execute(createtable)

createtable = """CREATE TABLE IF NOT EXISTS WorkerAttendence(  
				name CHAR(15) not null ,
				sid INT (12) not null,
				foreign key(sid) references Employees(sid),
				bio INT(5) not null,
				diff INT(5) not null,
				month CHAR(20) not null,
				year CHAR(20) not null,
				advance FLOAT(10) not null,
				primary key (name,month,year)
				)"""
cursor.execute(createtable)


createtable = """CREATE TABLE IF NOT EXISTS EmployeeShare(  
				epf FLOAT(4) not null,
				esi FLOAT(4) not null
				)"""
cursor.execute(createtable)



