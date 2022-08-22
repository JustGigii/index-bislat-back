from faker import Faker
import random
import main
corsemap = []
fake = Faker()

def choiseTamplate():
    rand = random.sample(range(1, len(corsemap)), 3)
    jsonsend={
            "title": "מחזור גיוס",
            "id": fake.bothify("#########"),
            "fullName": fake.name(),
            "sortFrame": random.randrange(1, 20),
            "first": corsemap[rand[0]],
            "second": corsemap[rand[1]],
            "third": corsemap[rand[2]]
            }
    return jsonsend
def sortTamplate():
    jsonsend=  {
        "name": "מחזור 2000",
        "courses": [
            "1400",
            "196",
            "1807"
        ]
    }
    if __name__ == '__main__':
            cursejson = main.openjsonfile("course.json")
            for i in cursejson:
                    corsemap.append(i["CourseNumber"])