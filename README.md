# CRIS Connect Specification

*Overview*

CRIS Connect is implemented via a websocket. The messages over the socket are in JSON format and are bi-directional and contains the following

    {command, accession, examCode, nhs, chi, username, password}

NB. accession is described as a list [] of accession numbers.


*Commands*

CRIS Reporting to PACS: When these events happens in CRIS Reporting it will send the following commands:

    Report opened                       - "EVENT" command will be sent with accession, 
                                           examCode, nhs and chi fields if available
    Report saved (verified or finished) - "COMMIT" and "RELEASE" commands will be sent with accession
    Log in                              - "LOGIN" command with no other fields.
    Log out                             - "LOGOUT" command with no other fields.
    Report skipped, canceled or exits   - "RELEASE" commands will be sent with accession.
    A single exam's report is selected  - "EVENT" command will be sent with accession,
                                           examCode, nhs and chi fields if available
    A historic report is opened         - "READONLY" command will be sent with accession, 
                                           examCode, nhs and chi fields if available
                                           
PACS to CRIS Reporting: When following commands are received CRIS Reporting recact in these ways:

    "DISPLAY" - Displays a report. Expects accession to be populated
    "CLEAR" - Clears a report and return to worklist. Expects no other fields.
    "LOGOUT" -  Log out of CRIS Reporting. Expects no other fields.
    "LOGIN" - Log in to CRIS Reporting. Expects username and password to be populated
