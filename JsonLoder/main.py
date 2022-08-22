import json
import requests
from faker import Faker
import random
from threading import Thread


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






def SendThreadetJson():
    url = 'https://localhost:7041/Choise/Addchoise'
    for i in range(5):
        json = CreateJson()
        x = requests.post(url, json=json, verify=False)
        print(x)


def Addcourse():
    url = "https://localhost:7041/Course"
    json = openjsonfile("mock-data.json")
    if len(json["data"]) == CheakUniq(json):
        print('pls add or chance the course on the list')
        return
    AddToApi(json,url)
#["errors"]["errorMessage"]


def Main():

    for i in range(20):
        thread = Thread(target=SendThreadetJson)
        thread.start()
    print("done")


if __name__ == '__main__':
    Main()

