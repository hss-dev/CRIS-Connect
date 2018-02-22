# CRIS-Connect c# .net demo

To run

```
CRISConnectDemo.exe
```
optional command line parameters

-http

to use ws instead of wss

-port port-number
  
to change port used (default is 9998)

e.g.
```
CRISConnectDemo.exe -port 8000
```
*Gotchas*
=========
If CRIS Reporting is not connecting two comoon problems are:

No or untrusted certificate
----------------------------
This could manifest it self in CRIS Reporting showing a red banner such as "HSS DTI service unreachable".

Have you generated your own public self certificate as described in the code?

- Look at [DemoForm.cs](https://github.com/hss-dev/CRIS-Connect/blob/master/c%23/PACSRISIntegrationDemo/PACSRISIntegrationDemo/DemoForm.cs)
* Line 78 - location of certificate file
* Line 87 - to the password of your choice

If you are accessing CRIS Reporting from Chrome then you may need to *accept the certificate authorisation* by browsing to
https://localhost:9998 . This should not happen in electron due to a --ignore-certificate-errors switch, which could be run in chrome.





