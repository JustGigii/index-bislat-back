from faker import Faker
import random
from faker import Factory as FakerFactory
import main
corsemap = []
fake = Faker()
faker = FakerFactory.create()

def choiseTamplate():
    cursejson = main.openjsonfile("course.json")
    for i in cursejson:
        corsemap.append(i["CourseNumber"])
    rand = random.sample(range(1, len(corsemap)), 3)
    jsonsend={
            "title": "מחזור גיוס",
            "gender": "1",
            "fullName": fake.name(),
            "id": fake.bothify("#########"),
            "sortFrame": random.randrange(1, 20),
            "first": corsemap[rand[0]],
            "resonef": "אני לא רוצה להיות פהיותר",
            "second": corsemap[rand[1]],
            "resones":"בא לי ללכת הביתה",
            "third": corsemap[rand[2]],
            "resonet":"בא לי ללכת הביתה",
            }
    return jsonsend

def CourseTamplate():
    jsonsend =  {
        "Category": "מערך הכטמ''מ",
        "CourseNumber": "בדיקה",
        "Gender": "1",
        "CourseName": "טכנאי דרג א' קרונות כטמ''מ",
        "CourseTime": "8 שבועות",
        "CourseBases": ["בח''א 30", "בח''א 8", "כנף 1"],
        "CourseDescription": "אחזקת מטוסי ומערכות הכטמ''מ במסגרת דרגי א' ו-ב' ברמת התחזוקה השוטפת שלו בקורסי כטמ''מ נלמדים כל מערכות המטוס",
        "YouTubeURL": "",
        "ImgURL": "",
        "note": "דרג א' הינו מקצוע המוגדר תומך לחימה, מקנה הטבות בשחרור. הקורס לא מתקיים בבח''א 21"
    }
    return  jsonsend
def sortTamplate():
    jsonsend=  {
        "Name": "מחזור 2000",
        "courses": [
            "1400",
            "196",
            "1807",
            "1233"
        ]
    }
    return jsonsend
    if __name__ == '__main__':
            cursejson = main.openjsonfile("course.json")
            for i in cursejson:
                    corsemap.append(i["CourseNumber"])