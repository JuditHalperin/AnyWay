filep = open("C:\Users\user\Desktop\\t.txt","r")
s=filep.readlines()
filep.close()
j=0
y="\t\t\t\t"
for i in s:
    i=i.replace("\t","")
    i=i.replace(" ","")
    i=i.replace("\n","")
    i=i.split("*")
    print(y+"new Station{")
    print(y+"\tID = "+i[0]+",")
    print(y+"\tName = \""+i[1]+"\",")
    print(y+"\tLatitude = "+i[2]+",")
    print(y+"\tLongitude = "+i[3]+"")
    print(y+"},\n")
    j+=1
print
print
print
print(j)
t=input()
