'use strict';
/**
 * add server checker for loading by the browser
 */
 
var server =  
	function(secure, port, callback, keyPath, certPath) {
		var express = require('express');		
		var app = express();
		var expressWs;
		if(secure === true) {
			var fs = require('fs');
			var expressWs = require('express-ws');    	
		  var https = require('https');
		  console.log("using wss within a https protected page");
		  var options = {
		    key: fs.readFileSync(keyPath),
		  	cert: fs.readFileSync(certPath)
		  };  
		  var httpsServer = https.createServer(options, app);  	
		  expressWs = expressWs(app, httpsServer);
		  httpsServer.listen(port);
			
		} else {				  
		  expressWs = require('express-ws')(app);	  
		  console.log("using ws within a http protected page");
			app.listen(port); 
		} 

		app.use((req, res, next) => {  	
		  return next();
		});

		app.get('/', (req, res, next) => {    
		  res.end();
		});

		app.ws('/', (ws, req) => {
		  ws.on('message', (msg) => {    	
		    callback(JSON.parse(msg));
		  });
		});
		
		return expressWs;
	};

module.exports = server;