# Instar-IPCam- USB Joystick Controller
This is a C# program that uses the SharpDX DirectInput library to control a camera. 
The camera is connected to a network and can be controlled by sending HTTP requests to it. 
The program polls a joystick for button presses and sends the appropriate commands to the camera based on the button presses.

The program starts by initializing DirectInput and acquiring the first joystick that it finds. 
Then it enters an infinite loop to poll the joystick and check the state of its buttons. 
Depending on which button is pressed, the program sends a specific command to the camera using the WebClient class. 
The command is constructed as a URL string, which includes the camera's IP address, port, username, and password, 
as well as the specific action to be performed by the camera. 
The WebClient class sends an HTTP request to the URL and retrieves the response from the camera.

It's important to note that the hardcoded username, password, 
and IP address of the camera in the code should not be used in production and should be replaced with secure and unique values. 
Additionally, the code does not handle exceptions and errors, which should be added to improve its robustness.

You can edit the code to add more functions and buttons, also on this website you can check your configuration of your pad, joystick e.g. 
---> https://gamepad-tester.com/

All CGI actions for Instar Cam 9020HD on: https://wiki.instar.com/de/1080p_Serie_CGI_Befehle/Komplette_CGI_Liste/
Microsoft.NET.Sdk
.NETFramework,Version=v4.0
SharpDX.DirectInput
SharpDX --version 4.2.0
actions to build an .exe file:
dotnet build
dotnet run
