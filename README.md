# Instar-IPCam- USB Joystick Controller

This is a C# program that uses the Silk.NET library to control a camera through a joystick. 
The program takes four command line arguments: the username, password, domain, and port 
used to connect to the camera's API.
Functions: Move Up, Move Down, Move Left, Move Right, Zoom In, Zoom Out, Auotfocus after Zooming.

The main function creates a window and sets event handlers for the load and update events.
The OnLoad function initializes the joystick and the OnUpdate function moves the camera based on the joystick's position.

The InitializeWebClient function sets up an HTTP client to communicate with the camera's API, 
using basic authentication with the provided username and password. 
The MoveHorizontal and MoveVertical functions send commands to the camera to move in a specific direction 
with a specified speed. The SendCommand function sends a command to the camera by sending an HTTP GET request to the specified URL.


You can edit the code to add more functions and buttons, also on this website you can check your configuration of your pad, joystick e.g.
---> <https://gamepad-tester.com/>

All CGI actions for Instar Cam 9020HD on: <https://wiki.instar.com/de/1080p_Serie_CGI_Befehle/Komplette_CGI_Liste/>

## Developer info

### dotnet 7

<https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.102-windows-x64-installer>

### Install Visual Studio Code

```bash
net6.0
Silk.NET.Input Version="2.16.0"
```

### Run and build the app

First you have to install all packages from above.
To build an exe file:

```bash
dotnet build
dotnet run
```

you can use a batchfile to start the app minimized and open the browser to your cam

```bash
start /min "" "x:\git\Instar-IPCam-4Joystick\bin\Debug\net6.0\camcontrol.exe" user password http://192.168.xxx.xxx 80 & ping localhost -n 2 > nul & start "" "http://camera-ip:port" 
```
