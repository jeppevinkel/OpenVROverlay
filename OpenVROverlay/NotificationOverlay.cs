using System;
using System.IO;
using System.Numerics;
using OVRUtils;
using Valve.VR;

namespace OpenVROverlay
{
    public class NotificationOverlay
    {
        private readonly Application _app;
        private readonly Overlay _notifOverlay;

        public NotificationOverlay()
        {
            _app = new Application(Application.ApplicationType.Overlay);

            var m = Matrix4x4.CreateWorld(new Vector3(10, 0, 800), -Vector3.UnitZ, Vector3.UnitY);

            _notifOverlay = new Overlay("twitch_message_overlay", "Twitch Chat Overlay")
            {
                WidthInMeters = 0.5f,
                TrackedDevice = OpenVR.k_unTrackedDeviceIndex_Hmd,
                Transform = new HmdMatrix34_t
                {
                    m0 = m.M11,
                    m1 = m.M12,
                    m2 = -m.M13,
                    m3 = m.M14,
                    m4 = m.M21,
                    m5 = m.M22,
                    m6 = -m.M23,
                    m7 = 1.7f,
                    m8 = -m.M31,
                    m9 = -m.M32,
                    m10 = m.M33,
                    m11 = -1
                }
            };
            
            var overlayImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", ".\\img.jpg");
            Console.WriteLine(overlayImagePath);
            _notifOverlay.SetTextureFromFile(overlayImagePath);
            _notifOverlay.Show();
        }

        public void Show()
        {
            _notifOverlay.Show();
        }

        public void Hide()
        {
            _notifOverlay.Hide();
        }

        public void Shutdown()
        {
            _app.Shutdown();
        }
    }
}