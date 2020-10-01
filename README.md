# CRIS Connect Specification

## Demo

#### Running CRIS-Connect
You can run the executable directly, or through the command line. If it doesn't run, you most likely do not have a .pfx certificate. To generate one please follow step 2 of the c# readme.md https://github.com/hss-dev/CRIS-Connect/blob/master/c%23/README.md 
![](/electron/readme-gifs/runningcrisconnect.gif)
<br/><br/><br/>
#### Enabling CRIS-Connect
Integration and CRIS Connect must be enabled and then the port must be set (default 56998 or 56996)
![](/electron/readme-gifs/enablingcrisconnect.gif)
<br/><br/><br/>
#### Opening a report
![](/electron/readme-gifs/openingreport.gif)
<br/><br/><br/>
#### Clearing a report
![](/electron/readme-gifs/clearingreport.gif)
<br/><br/><br/>
#### Requesting a report
For requesting a report to work, the client must already be in a reporting screen.
![](/electron/readme-gifs/requestingreport.gif)

*Overview*
----------
To integrate with CRIS Reporting is done by a simple [websocket](https://en.wikipedia.org/wiki/WebSocket)
. Websockets are very common and have been implemented in all modern programming languages making it very easy for any PACS vendor to implement. Please find here some examples (.net, electron) of how this could be done.  

The messages over the socket are in JSON format and are bi-directional and contains the following

    {command, domain, accession, examCode, nhs, chi, username, password}

NB. *accession* is described as a list [] of accession numbers.


*Commands*
----------
CRIS Reporting to PACS: When these events happens in CRIS Reporting it will send the following commands:

    Report opened                       - "EVENT" command will be sent with accession, domain 
                                           examCode, nhs and chi fields if available
    Report saved (verified or finished) - "COMMIT" and "RELEASE" commands will be sent with accession
                                           and domain
    Log in                              - "LOGIN" command with domain 
    Log out                             - "LOGOUT" command with domain.
    Report skipped, canceled or exits   - "RELEASE" commands will be sent with accession and domain.
    A single exam's report is selected  - "EVENT" command will be sent with accession, domain,
                                           examCode, nhs and chi fields if available
    A historic report is opened         - "READONLY" command will be sent with accession, domain,  
                                           examCode, nhs and chi fields if available
    Request PACS prefetch images        - "PREFETCH" command will be sent with accession numbers, domain 
                                           examCode, nhs and chi fields if available
                                           
PACS to CRIS Reporting: When following commands are received CRIS Reporting recact in these ways:

    "DISPLAY" - Displays a report. Expects accession to be populated
    "CLEAR" - Clears a report and return to worklist. Expects no other fields.
    "LOGOUT" -  Log out of CRIS Reporting. Expects no other fields.
    "LOGIN" - Log in to CRIS Reporting. Expects username and password to be populated

*FAQ*
----------
**What port is CRIS Connect using by default?** 56998 or 56996
