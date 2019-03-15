module.exports = function(app) {
  return new Handler(app);
};

var Handler = function(app) {
  this.app = app;
};

/**
 * New client entry.
 *
 * @param  {Object}   msg     request message
 * @param  {Object}   session current session object
 * @param  {Function} next    next step callback
 * @return {Void}
 */
Handler.prototype.entry = function(msg, session, next) {
	var self = this;
	var uid = msg.uid;	
	var roottype = msg.roottype;

	var sessionService = self.app.get('sessionService');
	//duplicate log in
	if( !! sessionService.getByUid(uid)) {
		next(null, {
			code: 500,
			error: true
		});
		return;
	}

	session.bind(uid);
	session.on('closed', onUserLeave.bind(null, self.app));
	//put user into channel
	self.app.rpc.game.gameRemote.joinRoom(session, uid, self.app.get('serverId'), true, function(users){
		next(null, {
			users:users
		});
	});
};

/**
 * User log out handler
 *
 * @param {Object} app current application
 * @param {Object} session current session object
 *
 */
var onUserLeave = function(app, session) {
	if(!session || !session.uid) {
		return;
	}
	app.rpc.chat.gameRemote.leaveRoom(session, session.uid, app.get('serverId'), session.get('rid'), null);
};