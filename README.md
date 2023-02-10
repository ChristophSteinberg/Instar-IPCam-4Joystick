# Instar-IPCam-4Joystick
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
