module.exports = function() {
    return new Manager();
};

const roomName = "PPLR-";

var Manager = function() {
  this.rooms = {};  // {"roomname":[uid1,uid2..]}
  this.users = null;  // {"uid1":"roomname","uid2":"roomname"} 
  this.roomCount = 0;
};
/*
 * uid  :
 * type :房间类型
*/
Manager.prototype.userJoin = function(uid,type,cb){
  var room = this.users[uid];
  //如果有房间，返回房间
  if(!!room){
    cb(room);
    return;
  }
  //没有房间，查找一个合适的房间
  for (const key in this.rooms) {
    if (this.rooms.hasOwnProperty(key)) {
      const usersInRoom = this.rooms[key];
      if(usersInRoom.length < 2){
        usersInRoom.push(uid);
        this.users[uid] = key;
        cb(key);
        return;
      }
    }
  }
  //创建一个新房间
  var newRoomName = roomName + type;
  this.rooms[newRoomName] = [];
  this.rooms[newRoomName].push(uid);
  this.users[uid] = newRoomName;
  cb(newRoomName);
  return;
}

Manager.prototype.userLeave = function(uid,cb){
  var userRoom = this.users[uid];
  if(!userRoom){
    cb(new Error("can't find user info"));
    return;
  }
  delete this.users.uid;
  var usersInRoom = this.rooms[userRoom];
  for (let index = 0; index < usersInRoom.length; index++) {
    const element = usersInRoom[index];
    if(element === uid){
      usersInRoom.splice(index,1);
    }
  }
  cb(null);
}