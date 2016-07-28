#!/usr/bin/env python

import MySQLdb
import sys
# Opening databse connection
# Preparing cursor object

DATABASE_NAME = "mess_db"
USER = "root"
PASSWORD = "rootpa55word"
SERVER = "localhost"

try:
	db = MySQLdb.connect(SERVER,USER,PASSWORD, DATABASE_NAME)
	cursor = db.cursor()
except MySQLdb.Error, e:
	print "Database Connection Error"
	raise e

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

#cursor.execute("DROP TABLE IF EXISTS login")

print "Creating Table if not already existing: Student"
createtable = """CREATE TABLE IF NOT EXISTS Student (
                name VARCHAR(45),
                roll INT not null unique primary key,
                remark VARCHAR(45)
                )"""
cursor.execute(createtable)
print "Insert Student Details"

with open("y14.txt") as f:
    for line in f:
           data = line.split()
           rollno = data[0]
           print rollno
           name = ' '.join(data[1:])
           cursor.execute('insert into Student (roll,name) values ('+rollno+',"'+name+'")')
           db.commit()
with open("y15.txt") as f:
    for line in f:
           data = line.split()
           rollno = data[0]
           print rollno
           name = ' '.join(data[1:])
           cursor.execute('insert into Student (roll,name) values ('+rollno+',"'+name+'")')
           db.commit()
with open("y16.txt") as f:
    for line in f:
           data = line.split()
           rollno = data[0]
           print rollno
           name = ' '.join(data[1:])
           cursor.execute('insert into Student (roll,name) values ('+rollno+',"'+name+'")')
           db.commit()
db.close()
