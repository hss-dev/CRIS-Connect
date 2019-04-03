'use strict';
const electron = require('electron');
// Module to control application life.
const {
    app,
    Menu,
    ipcMain,
    dialog,
    BrowserWindow
} = require('electron');

const http = require('http');
const path = require('path');
const version = require('./version');
const Config = require('electron-config');
const config = new Config({
    name: 'connect'
});

const DTI_WEB_SOCKET_PORT = 'DTI_WEB_SOCKET_PORT';
const DTI_KEY_PATH = 'DTI_KEY_PATH';
const DTI_CERT_PATH = 'DTI_CERT_PATH';
const USE_SECURE_WEBSOCKET = 'USE_SECURE_WEBSOCKET';
// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mainWindow;
let server;

//load client args
loadAppCli(app);

var shouldQuit = false;

function initApp() {

    initConfig();

    shouldQuit = app.makeSingleInstance(function(commandLine, workingDirectory) {
        if (mainWindow !== null) {
            console.log("Limited to one electron browser window");
            if (mainWindow.isMinimized()) mainWindow.restore();
            mainWindow.focus();
        }
    });
    if (shouldQuit) {
        app.quit();
        return;
    }
    startServer();
    createWindow();
}

function initConfig() {
    console.log("using config file @ " + config.path);
    if (!config.has(DTI_WEB_SOCKET_PORT)) {
        console.log("adding default for DTI_WEB_SOCKET_PORT");
        config.set(DTI_WEB_SOCKET_PORT, 9998);
    }
    if (!config.has(DTI_KEY_PATH)) {
        console.log("adding default for DTI_KEY_PATH");
        config.set(DTI_KEY_PATH, path.join(__dirname, 'key.pem'));
    }
    if (!config.has(DTI_CERT_PATH)) {
        console.log("adding default for DTI_CERT_PATH");
        config.set(DTI_CERT_PATH, path.join(__dirname, 'server.crt'));
    }
    if (!config.has(USE_SECURE_WEBSOCKET)) {
        console.log("adding default for USE_SECURE_WEBSOCKET");
        config.set(USE_SECURE_WEBSOCKET, true);
    }
}

function startServer() {
    init();    

    function callback(msg) {
        console.log(msg);       
    	mainWindow.webContents.send('msg',msg);        
    };

    function init() {
        console.log("starting server...");
        const secure = config.get(USE_SECURE_WEBSOCKET);
        const port = config.get(DTI_WEB_SOCKET_PORT);
        server = require('./express')(secure, port, callback, config.get(DTI_KEY_PATH), config.get(DTI_CERT_PATH));       
    };
    
    ipcMain.on('msg', (event, msg) => {
    	var payload = JSON.stringify(msg);
    	console.log("msg received from PACS, forwarding:" + payload);  
    	var clientContainer = server.getWss('/');
    	clientContainer.clients.forEach((client) => {
    	  client.send(payload);
    	});    	
    });
}

function createWindow() {
    console.log("starting with locale: " + app.getLocale());
    const {
        width,
        height
    } = electron.screen.getPrimaryDisplay().workAreaSize;

    mainWindow = new BrowserWindow({
        icon: path.join(__dirname, 'images/desktop-icon.png'),
        title: "CRIS Connect",
        webPreferences: {
            preload: path.join(__dirname, 'preload.js'),
            nodeIntegration: false,
            webSecurity: true,
            plugins: false
        },
        width,
        height
    });
    // overwrite menu
    createMenu();

    var shutdown = () => {
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow = null;
    };

    mainWindow.on('destroy', shutdown);

    // Emitted when the window is closed.
    mainWindow.on('closed', shutdown);

    // and load the url of the site to renders
    mainWindow.webContents.loadURL("file://" + path.join(__dirname, 'page/index.html'));

    mainWindow.setMenuBarVisibility(false);
}

function showAboutDialog() {
    var aboutMsg = "Electron Version: " + version.electronVersion + "\n" + "Application Version: " + version.number + "\n" + "Build Date: " + version.buildDate + "\n" + "VC: " + version.git + "\n" + "Jenkins: " + version.jenkins;
    dialog.showMessageBox(mainWindow, {
        type: "info",
        title: "Version information",
        message: aboutMsg,
        buttons: []
    });
};

function createMenu() {
    var template = [{
        label: 'Edit',
        submenu: [{
            label: 'Undo',
            accelerator: 'CmdOrCtrl+Z',
            role: 'undo'
        }, {
            label: 'Redo',
            accelerator: 'Shift+CmdOrCtrl+Z',
            role: 'redo'
        }, {
            type: 'separator'
        }, {
            label: 'Cut',
            accelerator: 'CmdOrCtrl+X',
            role: 'cut'
        }, {
            label: 'Copy',
            accelerator: 'CmdOrCtrl+C',
            role: 'copy'
        }, {
            label: 'Paste',
            accelerator: 'CmdOrCtrl+V',
            role: 'paste'
        }, {
            label: 'Select All',
            accelerator: 'CmdOrCtrl+A',
            role: 'selectall'
        }, {
            label: 'Save',
            accelerator: 'CmdOrCtrl+S',
            click: function(item, focusedWindow) {
                if (focusedWindow) {
                    focusedWindow.webContents.savePage('static-data.html', 'HTMLComplete');
                }
            }
        }]
    }, {
        label: 'View',
        submenu: [{
            label: 'Reload',
            accelerator: 'F5',
            click: function(item, focusedWindow) {
                if (focusedWindow) {
                    focusedWindow.reload();
                }
            }
        }, {
            label: 'Toggle Full Screen',
            accelerator: (function() {
                return (process.platform === 'darwin') ? 'Ctrl+Command+F' :
                    'F11'
            })(),
            click: function(item, focusedWindow) {
                if (focusedWindow)
                    focusedWindow.setFullScreen(!focusedWindow
                        .isFullScreen())
            }
        }, {
            label: 'Toggle Developer Tools',
            accelerator: 'Ctrl+Shift+J',
            click: function(item, focusedWindow) {
                if (focusedWindow) {
                    focusedWindow.toggleDevTools();
                }
            }
        }]
    }, {
        label: 'Window',
        role: 'window',
        submenu: [{
            label: 'Minimize',
            accelerator: 'CmdOrCtrl+M',
            role: 'minimize'
        }, {
            label: 'Close',
            accelerator: 'CmdOrCtrl+W',
            role: 'close'
        }]
    }, {
        label: 'Help',
        role: 'help',
        submenu: [{
            label: 'About',
            accelerator: 'F1',
            click: function() {
                showAboutDialog();
            }
        }]
    }];

    var menu = Menu.buildFromTemplate(template);
    Menu.setApplicationMenu(menu);

};

function loadAppCli(app) {
    // try and load from the command line switches.
    var cliArgs = process.argv;
    console.log("cli arguments: " + cliArgs);

    for (var argument in cliArgs) {
        var cliArg = cliArgs[argument];
        if (cliArg.indexOf("--") !== -1) {
            loadCliParam(app, cliArg);
        }

        if (cliArg.indexOf("=") !== -1) {
            var args = cliArg.split("=");
            global[args[0]] = args[1];
        }
    }
}

function loadCliParam(app, cliArg) {
    cliArg = cliArg.replace('--', '');
    if (cliArg.indexOf("=") !== -1) {
        var args = cliArg.split("=");
        console.log("setting command line key value switch: " + cliArg);
        app.commandLine.appendSwitch(args[0], args[1]);
    } else {
        console.log("setting command line value switch: " + cliArg);
        app.commandLine.appendSwitch(cliArg);
    }
};
// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
app.on('ready', initApp);
// Quit when all windows are closed.
app.on('window-all-closed', () => {
    // On OS X it is common for applications and their menu bar
    // to stay active until the user quits explicitly with Cmd + Q
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', () => {
    // On OS X it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if (mainWindow === null) {
        initApp();
    }
});
