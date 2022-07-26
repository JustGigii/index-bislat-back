import json
import requests


def openjsonfile(file):
    f = open(file,encoding='utf-8')
    return json.load(f)

def Main():
    url = "https://localhost:7041/Course"
    json = openjsonfile("mock-data.json")
    # for i in json["data"]:
    #     print(i["CourseNumber"])
    #     x = requests.post(url, json=i, verify=False)
    #     print(x.json()['']["errors"])
    print(len(json["data"]))
#["errors"]["errorMessage"]

if __name__ == '__main__':
    Main()

