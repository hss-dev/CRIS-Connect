'use strict';

const {
    ipcRenderer
} = require('electron');

window.onload = function() {
    //command templates
    const clearCommand = {
        command: "CLEAR"
    };
    const displayCommand = {
        command: "DISPLAY",
        accession: ['<Add accession number>']
    };

    var incomingJson = document.getElementById('incoming-json');
    var incomingmapsto = document.getElementById('incomingmapstodiv');

    //setup listeners	
    document.getElementById('displayCommand').addEventListener('click', () => {
        const outgoing = document.getElementById('outgoing');
        outgoing.value = JSON.stringify(displayCommand);
    });

    document.getElementById('clearCommand').addEventListener('click', () => {
        const outgoing = document.getElementById('outgoing');
        outgoing.value = JSON.stringify(clearCommand);
    });

    document.getElementById('executeCommand').addEventListener('click', () => {
        //send message back to the express server in the main process
        const outgoing = document.getElementById('outgoing');
        ipcRenderer.send('msg', JSON.parse(outgoing.value));
    });

    //handler consuming messages from the express server in the main process
    ipcRenderer.on('msg', (event, msg) => {
        var payload = JSON.stringify(msg);
        console.log(msg.command);
        if (msg.command === 'EVENT') {
            incomingmapsto.innerHTML = "Maps to &quot;SHOW_REPORT&quot;";
        } else if (msg.command === 'RELEASE') {
            incomingmapsto.innerHTML = "Maps to CLEAR_DISPLAY";
        }

        console.log("msg received:" + payload);
        incomingJson.innerHTML = payload;
    });
};
