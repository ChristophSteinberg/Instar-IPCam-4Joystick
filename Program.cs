using System;
using Silk.NET.Windowing;
using Silk.NET.Input;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace JoystickCameraControl;

class Program
{
    private static IWindow window;
    private static IJoystick joystick;
    private static HttpClient client;
    private static string username;
    private static string password;
    private static string host;
    private static string port;

    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("myapp [user] [password] [domain] [port]");
            Environment.Exit(1);
        }

        username = args[0];
        password = args[1];
        host = args[2];
        port = args[3];

        InitializeWebClient();

        var options = WindowOptions.Default;
        options.UpdatesPerSecond = 4;
        options.IsVisible = false;
        window = Window.Create(options);

        window.Load += OnLoad;
        window.Update += OnUpdate;
        window.Run();
    }

    private static void InitializeWebClient()
    {
        client = new HttpClient();
        var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

    private static void MoveHorizontal(int speed)
    {
        string direction = speed > 0 ? "right" : "left";
        SendCommand($"ptzctrl.cgi?-step=0&-act={direction}&-speed={Math.Abs(speed)}");
    }

    private static void MoveVertical(int speed)
    {
        string direction = speed > 0 ? "down" : "up";
        SendCommand($"ptzctrl.cgi?-step=0&-act={direction}&-speed={Math.Abs(speed)}");
    }

    private static void SendCommand(string command)
    {
        string url = host + ":" + port + "/" + command;
        Console.WriteLine(url);
        try
        {
            var response = client.GetAsync(url).GetAwaiter().GetResult();
            Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void OnLoad()
    {
        var input = window.CreateInput();
        joystick = input.Joysticks.FirstOrDefault();
    }

    private static void OnUpdate(double deltaTime)
    {
        MoveHorizontal((int)(joystick.Axes[0].Position * 30));
        MoveVertical((int)(joystick.Axes[1].Position * 30));
    }
}



//     if (state.Buttons[4])
//     {
//         Console.WriteLine("Rotation Left");
//         string username = "admin";
//         string password = "Passwort";
//         string domain = "Cam-IP";
//         int port = 80;

//         string command = "ptzctrl.cgi?-step=0&-act=left&-speed=15";
//         string url = "http://" + domain + ":" + port + "/" + command;
//         var client = new WebClient();
//         client.Credentials = new NetworkCredential(username, password);
//         var response = client.DownloadString(url);
//         Console.WriteLine(response);
//     }
//     else if (state.Buttons[5])
//     {
//         Console.WriteLine("Rotation Right");
//         string username = "admin";
//         string password = "Passwort";
//         string domain = "Cam-IP";
//         int port = 80;

//         string command = "ptzctrl.cgi?-step=0&-act=right&-speed=15";
//         string url = "http://" + domain + ":" + port + "/" + command;
//         var client = new WebClient();
//         client.Credentials = new NetworkCredential(username, password);
//         var response = client.DownloadString(url);
//         Console.WriteLine(response);
//     }
//     else if (state.Buttons[2])
//     {
//         Console.WriteLine("Return to Initial Position");
//         string username = "admin";
//         string password = "Passwort";
//         string domain = "Cam-IP";
//         int port = 80;

//         string command = "ptzctrl.cgi?-step=0&-act=home&-speed=0";
//         string url = "http://" + domain + ":" + port + "/" + command;
//         var client = new WebClient();
//         client.Credentials = new NetworkCredential(username, password);
//         var response = client.DownloadString(url);
//         Console.WriteLine(response);
//     }
//     else if (state.Buttons[3])
//     {
//         Console.WriteLine("Move Up or Down");
//         string username = "admin";
//         string password = "Passwort";
//         string domain = "Cam-IP";
//         int port = 80;

//         string command = "";
//         if (moveCameraUp)
//         {
//             Console.WriteLine("Moving Camera Up");
//             command = "ptzctrl.cgi?-step=0&-act=up&-speed=6";
//             moveCameraUp = false;
//         }
//         else
//         {
//             Console.WriteLine("Moving Camera Down");
//             command = "ptzctrl.cgi?-step=0&-act=down&-speed=6";
//             moveCameraUp = true;
//         }

//         string url = "http://" + domain + ":" + port + "/" + command;
//         var client = new WebClient();
//         client.Credentials = new NetworkCredential(username, password);
//         var response = client.DownloadString(url);
//         Console.WriteLine(response);
//     }
// }
// }
//     }
// }