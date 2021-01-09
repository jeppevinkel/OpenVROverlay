using System;
using System.IO;
using OpenTK.Windowing.Desktop;
using Valve.VR;

namespace OpenVROverlay
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterManifest();
            
            var app = new NotificationOverlay();

            Console.ReadLine();

            DeregisterManifest();
            
            app.Shutdown();

            /*using (var game = new Game(GameWindowSettings.Default, NativeWindowSettings.Default))
            {
                game.Run();
            }*/
        }
        
        static void RegisterManifest()
        {
            EVRInitError initErr = EVRInitError.None;
            OpenVR.Init(ref initErr, EVRApplicationType.VRApplication_Utility);

            if (initErr != EVRInitError.None)
            {
                Console.WriteLine($"Error initializing OpenVR handle: {initErr}");
                Environment.Exit(1);
            }

            var err = OpenVR.Applications.AddApplicationManifest(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "manifest.vrmanifest"), false);

            if (err != EVRApplicationError.None)
            {
                Console.WriteLine($"Error registering manifest with OpenVR runtime: {err}");
                Environment.Exit(1);
            }

            Console.WriteLine("Application manifest registered with OpenVR runtime!");

            OpenVR.Shutdown();
        }

        static void DeregisterManifest()
        {
            EVRInitError initErr = EVRInitError.None;
            OpenVR.Init(ref initErr, EVRApplicationType.VRApplication_Utility);

            if (initErr != EVRInitError.None)
            {
                Console.WriteLine($"Error initializing OpenVR handle: {initErr}");
                Environment.Exit(1);
            }

            var err = OpenVR.Applications.RemoveApplicationManifest(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "manifest.vrmanifest"));

            if (err != EVRApplicationError.None)
            {
                Console.WriteLine($"Error de-registering manifest with OpenVR runtime: {err}");
                Environment.Exit(1);
            }

            Console.WriteLine("Application manifest de-registered with OpenVR runtime!");

            OpenVR.Shutdown();
        }
    }
}
