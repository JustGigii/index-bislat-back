const { json } = require('express');
const fs = require('fs')
/* {
    fullName: OmriGigi,
    id: 3233,
    sortFrame: 3,
    first: קרוס 135
    secend: קורס 245
    third: קורס 456
}

*/
module.exports =
{
    InitJasonFile: (fileName,data) =>
{
    let answer = {
        userCount: 1,
        title: fileName,
        users:
        {
        fullName: data.fullName,
        id: data.id,
        sortFrame: data.sortFrame,
        first: data.first,
        secend: data.secend,
        third: data.third
        }
    };
    let jasondata = JSON.stringify(answer);
    fs.writeFileSync(fileName+'.json', jasondata);
},

Update: (fileName, data)=>
{
    const fs = require('fs');

   const jsons= fs.readFile(fileName+'.json', (err, data) => {
        if (err) throw err;
        let jasonFile = JSON.parse(data);
        return jasonFile;
    });

    let text = 
    {
        userCount: jsons.userCount+1,
        title: fileName,
        users: appenedUser(jsons.users,data)
    }
    let jasondata = JSON.stringify(text);
    fs.writeFileSync(fileName+'.json', jasondata);
}
};

function appenedUser(users_String,user_String)
{
    var users = JSON.parse(users_String)
    users.push(user_String)
    return JSON.stringify(users);
}
    
