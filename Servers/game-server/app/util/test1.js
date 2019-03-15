
var dic= {};

dic["b"] = "2";
dic["c"] = "3";
dic["a"] = [];
dic["a"].push("111");
dic["a"].push("112");
console.log(dic);
var db = dic.b;
delete dic.b;
console.log(db);
console.log(dic);


/*
var array = [];
array.push(1);
array.push(2);
array.push(3);
console.log(array);

for (let index = 0; index < array.length; index++) {
    const element = array[index];
    if(element === 2){
        array.splice(index,1);
    }
}

console.log(array);
*/