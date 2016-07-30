#!/usr/bin/python

# @Author Devashish Kumar Yadav (github.com/yadavdev)
# @Code available under MIT License
# @Mess Management Software expects images to be in C:\MessManagement\MemberImages\
import urllib

# Adding students images to database

# The files contain data in the format
# ROLLNO NAME
# Example:
# 11001 John Adams
# 11002 Ali Jafar

with open("y14.txt") as f:
    for line in f:
        rollno = line.split()[0]
        print rollno
        image_url = "http://oa.cc.iitk.ac.in:8181/Oa/Jsp/Photo/"+ rollno + "_0.jpg"
        urllib.urlretrieve(image_url, rollno + "_0.jpg")

with open("y15.txt") as f:
    for line in f:
        rollno = line.split()[0]
        print rollno
        image_url = "http://oa.cc.iitk.ac.in:8181/Oa/Jsp/Photo/"+ rollno + "_0.jpg"
        urllib.urlretrieve(image_url, rollno + "_0.jpg")

with open("y16.txt") as f:
    for line in f:
        rollno = line.split()[0]
        print rollno
        image_url = "http://oa.cc.iitk.ac.in:8181/Oa/Jsp/Photo/"+ rollno + "_0.jpg"
        urllib.urlretrieve(image_url, rollno + "_0.jpg")
