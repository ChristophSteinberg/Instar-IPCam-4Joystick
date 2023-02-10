using System;
using System.Net;
using SharpDX.DirectInput;

namespace JoystickCameraControl
{
    class Program
    {
        private static bool moveCameraUp;

        static void Main(string[] args)
        {
            DirectInput dinput = new DirectInput();
            Joystick joystick = null;

            foreach (var deviceInstance in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                joystick = new Joystick(dinput, deviceInstance.InstanceGuid);
                break;
            }

            joystick.Acquire();

            while (true)
            {
                joystick.Poll();
                JoystickState state = joystick.GetCurrentState();

                if (state.Buttons[4])
                {
                    Console.WriteLine("Rotation Left");
                    string username = "admin";
                    string password = "Passwort";
                    string domain = "Cam-IP";
                    int port = 80;

                    string command = "ptzctrl.cgi?-step=0&-act=left&-speed=15";
                    string url = "http://" + domain + ":" + port + "/" + command;
                    var client = new WebClient();
                    client.Credentials = new NetworkCredential(username, password);
                    var response = client.DownloadString(url);
                    Console.WriteLine(response);
                }
                else if (state.Buttons[5])
                {
                    Console.WriteLine("Rotation Right");
                    string username = "admin";
                    string password = "Passwort";
                    string domain = "Cam-IP";
                    int port = 80;

                    string command = "ptzctrl.cgi?-step=0&-act=right&-speed=15";
                    string url = "http://" + domain + ":" + port + "/" + command;
                    var client = new WebClient();
                    client.Credentials = new NetworkCredential(username, password);
                    var response = client.DownloadString(url);
                    Console.WriteLine(response);
                }
                else if (state.Buttons[2])
                {
                    Console.WriteLine("Return to Initial Position");
                    string username = "admin";
                    string password = "Passwort";
                    string domain = "Cam-IP";
                    int port = 80;

                    string command = "ptzctrl.cgi?-step=0&-act=home&-speed=0";
                    string url = "http://" + domain + ":" + port + "/" + command;
                    var client = new WebClient();
                    client.Credentials = new NetworkCredential(username, password);
                    var response = client.DownloadString(url);
                    Console.WriteLine(response);
                }
                else if (state.Buttons[3])
                {
                    Console.WriteLine("Move Up or Down");
                    string username = "admin";
                    string password = "Passwort";
                    string domain = "Cam-IP";
                    int port = 80;

                    string command = "";
                    if (moveCameraUp)
                    {
                        Console.WriteLine("Moving Camera Up");
                        command = "ptzctrl.cgi?-step=0&-act=up&-speed=6";
                        moveCameraUp = false;
                    }
                    else
                    {
                        Console.WriteLine("Moving Camera Down");
                        command = "ptzctrl.cgi?-step=0&-act=down&-speed=6";
                        moveCameraUp = true;
                    }

                    string url = "http://" + domain + ":" + port + "/" + command;
                    var client = new WebClient();
                    client.Credentials = new NetworkCredential(username, password);
                    var response = client.DownloadString(url);
                    Console.WriteLine(response);
                }
            }
        }
    }
}