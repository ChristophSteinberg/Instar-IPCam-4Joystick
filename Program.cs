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

    private static void ZoomIn()
    {
        SendCommand($"ptzctrl.cgi?-step=0&-act=zoomin&-speed=63");
    }

    private static void ZoomOut()
    {
        SendCommand($"ptzctrl.cgi?-step=0&-act=zoomout&-speed=63");
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
        // return;
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

        var h2 = input.Gamepads.Where(j => j.IsConnected).ToArray();
        var h1 = input.Joysticks.Where(j => j.IsConnected).ToArray();
        var h3 = input.OtherDevices.Where(j => j.IsConnected).ToArray();



        joystick = input.Joysticks.FirstOrDefault();
    }

    private static void OnUpdate(double deltaTime)
    {
        var horizontal = (int)(joystick.Axes[0].Position * 30);
        var vertical = (int)(joystick.Axes[1].Position * 30);

        if (joystick.Hats[0].Position == Position2D.Up)
        {
            ZoomIn();
        }
        else if (joystick.Hats[0].Position == Position2D.Down)
        {
            ZoomOut();
        }
        else
        {

            if (Math.Abs(horizontal) > Math.Abs(vertical))
            {
                MoveHorizontal(horizontal);
            }
            else
            {
                MoveVertical(vertical);
            }
        }
    }
}



