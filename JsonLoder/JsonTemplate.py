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