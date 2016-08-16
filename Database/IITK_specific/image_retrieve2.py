#!/usr/bin/python

# @Author Devashish Kumar Yadav (github.com/yadavdev)
# @Code available under MIT License
# @Mess Management Software expects images to be in C:\MessManagement\MemberImages\
import urllib, urllib2

# Adding students images to database

# The files contain data in the format
# see populate_student_data_withFile for more info

with open("hall9.csv") as f:
    for line in f:
        rollno = line.split(',')[3]
        print rollno
        needretry = 0
        image_url = "http://oa.cc.iitk.ac.in:8181/Oa/Jsp/Photo/"+ rollno + "_0.jpg"
        try:
        	data = urllib2.urlopen(image_url)
        	img = data.read()
        	#print len(img)
        	filename = rollno + "_0.jpg"
        	localFile = open(filename, 'w')
        	localFile.write(img)
        	localFile.close()
        except urllib2.URLError,e:
    		if not hasattr(e, "code"):
    			raise
    		#raise
    		needretry = 1;

		if len(rollno) == 8 and needretry == 1:
			print "PG Image not found. Trying corresponding UG rollno"
			ugrollno = rollno[:2] + rollno[-3:]
			image_url2 = "http://oa.cc.iitk.ac.in:8181/Oa/Jsp/Photo/"+ugrollno+"_0.jpg"
			try:
				data = urllib2.urlopen(image_url2)
				img = data.read()
				#print len(img)
				filename = rollno + "_0.jpg"
				localFile = open(filename, 'w')
				localFile.write(img)
				localFile.close()
			except urllib2.URLError,e:
				#raise e
				if not hasattr(e, "code"):
					raise
				print "Manually add Image for rollno: " + rollno