import json
import requests

def openjsonfile(file):
    f = open(file,encoding='utf-8')
    return json.load(f)


def AddToApi(json,url):
    for i in json["data"]:
        print(i["CourseNumber"])
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


def Main():
    url = "https://localhost:7041/Course"
    json = openjsonfile("mock-data.json")
    if len(json["data"]) == CheakUniq(json):
        print('pls add or chance the course on the list')
        return
    AddToApi(json,url)
#["errors"]["errorMessage"]

if __name__ == '__main__':
    Main()

