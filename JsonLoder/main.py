import datetime
import json
import requests
from faker import Faker
import random
from threading import Thread
import jwt
import Config
import  JsonTemplate
from jose import JWTError, jwt
from datetime import datetime, timedelta

def openjsonfile(file):
    f = open(file,encoding='utf-8')
    return json.load(f)


def AddToApi(json,url):
    for i in json["data"]:
       # print(i["CourseNumber"])
        x = requests.post(url, json=i, verify=False)
        if (x.status_code == 402):
            print(x.json()['']["errors"])
        else:
            x.status_code


def CheakUniq(json):
    course = []
    for i in json["data"]:
        if i['CourseNumber'] in course:
            print(course)
        else:
            course.append(i['CourseNumber'])
    print('in course have: ',len(course))


def CreateJson():
    pass

def postjwttoken(paylod,url):
    to_encode = {"http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata": paylod}
    expire = datetime.utcnow() + timedelta(minutes=15)
    to_encode.update({"exp": expire})
    encoded_jwt = jwt.encode(to_encode, Config.key, algorithm="HS256")
    print(to_encode)
    head = {'Authorization': 'Bearer {}'.format(encoded_jwt)}
    #head = {'Authorization': encoded_jwt}
    x = requests.post(url, json=paylod, verify=False,headers =head)
    print(x.text)

def putjwttoken(paylod, url):
    to_encode = {"http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata": paylod}
    expire = datetime.utcnow() + timedelta(minutes=15)
    to_encode.update({"exp": expire})
    encoded_jwt = jwt.encode(to_encode, Config.key, algorithm="HS256")
    print(to_encode)
    head = {'Authorization': 'Bearer {}'.format(encoded_jwt)}
    # head = {'Authorization': encoded_jwt}
    x = requests.put(url, json=paylod, verify=False, headers=head)
    print(x.text)



def SendThreadetJson():
    url = 'https://localhost:7041/Course'
    for i in range(5):
        json = Addcourse()
        #x = requests.post(url, json=json, verify=False)
        #print(x)


def Addcourse():
    url = "https://localhost:7041/Course"
    json = openjsonfile("mock-data.json")
    if len(json["data"]) == CheakUniq(json):
        print('pls add or chance the course on the list')
        return
    AddToApi(json,url)
#["errors"]["errorMessage"]

def Delete(url,id):
    to_encode = {"http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata": id}
    expire = datetime.utcnow() + timedelta(minutes=15)
    to_encode.update({"exp": expire})
    encoded_jwt = jwt.encode(to_encode, Config.key, algorithm="HS256")
    print(encoded_jwt)
    head = {'Authorization': 'Bearer {}'.format(encoded_jwt)}
    # head = {'Authorization': encoded_jwt}
    x = requests.delete(url+id, verify=False, headers=head)
    print(x)
def Deletesort(url,sort,id):
    to_encode = {"http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata": {"id":id,"sort":sort}}
    expire = datetime.utcnow() + timedelta(minutes=15)
    to_encode.update({"exp": expire})
    encoded_jwt = jwt.encode(to_encode, Config.key, algorithm="HS256")
    print(encoded_jwt)
    head = {'Authorization': 'Bearer {}'.format(encoded_jwt)}
    # head = {'Authorization': encoded_jwt}
    x = requests.delete(url, verify=False, headers=head)
    print(x)
def Main():
    url = "https://localhost:7041/Course"
    json = openjsonfile("mock-data.json")
    for i in json["data"]:
        x = requests.post(url, json=i, verify=False)
        if (x.status_code == 402):
            print(x.json()['']["errors"])
        else:
            x.status_code
    # for i in range(5):
    #     thread = Thread(target=SendThreadetJson)
    #     thread.start()
    #print(postjwttoken(JsonTemplate.choiseTamplate(), "https://localhost:7041/Choise/Addchoise"))
    # print(putjwttoken(JsonTemplate.sortTamplate(), "https://localhost:7041/Sort/UpdateSort"))
    # Delete("https://localhost:7041/Sort/","מחזור 2000")
    # Deletesort("https://localhost:7041/Choise/{0}?sort={1}".format("581795411","מחזור 2000"),"מחזור 2000","581795411")
    print("done")


if __name__ == '__main__':
    Main()

