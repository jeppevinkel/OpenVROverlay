using System;
using System.IO;
using System.Numerics;
using OVRUtils;
using Valve.VR;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vector3 = System.Numerics.Vector3;

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
                // _notifOverlay.SetTextureFromFile(overlayImagePath);
                var gameWindow = new GameWindow(1000, 1000);
                var game = new Game(gameWindow);
                gameWindow.Run(1.0 / 60.0);
                
                GL.Enable(EnableCap.Texture2D);
                Texture2D myTexture = ContentPipe.LoadTexture(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources",
                    ".\\img.jpg"));

                GL.Enable(EnableCap.Multisample);
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, myTexture.Id);
                GL.Viewport(0, 0, myTexture.Width, myTexture.Height);
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                GL.Disable(EnableCap.Multisample);

                GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, myTexture.Id);
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, myTexture.Id);
                GL.BlitFramebuffer(0, 0, myTexture.Width, myTexture.Height, 0, 0, myTexture.Width, myTexture.Height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);

                GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
                
                Texture_t textureT = new Texture_t{handle = new IntPtr(myTexture.Id), eType = ETextureType.OpenGL, eColorSpace = EColorSpace.Gamma};
                // textureT.handle = (IntPtr)game.Texture.Id;
                // textureT.eType = ETextureType.OpenGL;
                // textureT.eColorSpace = EColorSpace.Gamma;
                _notifOverlay.SetTexture(textureT);
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