using System;
using System.IO;

using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mime;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace OpenVROverlay
{
    public class Game
    {
        private GameWindow _window;

        public Texture2D Texture;

        public Game(GameWindow window)
        {
            _window = window;

            window.Load += WindowLoad;
            window.UpdateFrame += WindowUpdateFrame;
            window.RenderFrame += WindowRenderFrame;
            window.Resize += WindowResize;
        }

        private void WindowLoad(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            
            GL.Enable(EnableCap.Texture2D);

            Texture = ContentPipe.LoadTexture(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources",
                ".\\img.jpg"));
        }

        private void WindowUpdateFrame(object sender, FrameEventArgs e)
        {
            
        }

        private void WindowRenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Matrix4 projMatrix = Matrix4.CreateOrthographicOffCenter(0, _window.Width, _window.Height, 0, 0, 1);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projMatrix);

            Matrix4 modelViewMatrix = Matrix4.CreateScale(0.5f, 0.5f, 1f) *
                                      Matrix4.CreateRotationZ(0f) *
                                      Matrix4.CreateTranslation(0f, 0f, 0f);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
            
            DrawTexture(Texture);

            modelViewMatrix = Matrix4.CreateScale(0.6f, 0.4f, 1f) *
                              Matrix4.CreateRotationZ(0f) * 
                              Matrix4.CreateTranslation(300f, 0f, 0f);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
            
            DrawTexture(Texture);

            modelViewMatrix = Matrix4.CreateScale(0.5f, 0.5f, 1f) *
                              Matrix4.CreateRotationZ(0.4f) *
                              Matrix4.CreateTranslation(200f, 250f, 0f);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelViewMatrix);
            
            DrawTexture(Texture);

            _window.SwapBuffers();
        }

        private void WindowResize(object sender, EventArgs e)
        {
            
        }

        public void DrawTexture(Texture2D texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, Texture.Id);
            
            GL.Begin(PrimitiveType.Triangles);
            
            GL.Color4(1f, 1f, 1f, 1f);
            GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
            GL.TexCoord2(1, 1); GL.Vertex2(Texture.Width, Texture.Height);
            GL.TexCoord2(0, 1); GL.Vertex2(0, Texture.Height);
            
            GL.TexCoord2(0, 0); GL.Vertex2(0, 1);
            GL.TexCoord2(1, 0); GL.Vertex2(Texture.Width, 1);
            GL.TexCoord2(1, 1); GL.Vertex2(Texture.Width, Texture.Height);
            
            GL.End();
        }
    }
    
    // public class Game: GameWindow
    // {
    //     public Game() : base(800, 600, GraphicsMode.Default, "OpenVROverlay")
    //     {
    //         GL.ClearColor(0, 0.1f, 0.4f, 1);
    //
    //         string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "img.jpg");
    //         texture = LoadTexture(path);
    //     }
    //
    //     private int texture;
    //
    //     public int LoadTexture(string file)
    //     {
    //         Bitmap bitmap = new Bitmap(file);
    //
    //         int tex;
    //         GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
    //
    //         GL.GenTextures(1, out tex);
    //         GL.BindTexture(TextureTarget.Texture2D, tex);
    //
    //         BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
    //             ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    //
    //         GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
    //             OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
    //         bitmap.UnlockBits(data);
    //
    //
    //         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
    //         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
    //         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
    //         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
    //
    //         return tex;
    //     }
    //
    //     public static void DrawImage(int image)
    //     {
    //         GL.MatrixMode(MatrixMode.Projection);
    //         GL.PushMatrix();
    //         GL.LoadIdentity();
    //
    //         GL.Ortho(0, 600, 0, 1110, -1, 1);
    //
    //         GL.MatrixMode(MatrixMode.Modelview);
    //         GL.PushMatrix();
    //         GL.LoadIdentity();
    //
    //         GL.Disable(EnableCap.Lighting);
    //
    //         GL.Enable(EnableCap.Texture2D);
    //
    //         GL.BindTexture(TextureTarget.Texture2D, image);
    //
    //         GL.Begin(BeginMode.Quads);
    //
    //         GL.TexCoord2(0, 0);
    //         GL.Vertex3(0, 0, 0);
    //
    //         GL.TexCoord2(1, 0);
    //         GL.Vertex3(256, 0, 0);
    //
    //         GL.TexCoord2(1, 1);
    //         GL.Vertex3(256, 256, 0);
    //
    //         GL.TexCoord2(0, 1);
    //         GL.Vertex3(0, 256, 0);
    //
    //         GL.End();
    //
    //         GL.Disable(EnableCap.Texture2D);
    //         GL.PopMatrix();
    //
    //         GL.MatrixMode(MatrixMode.Projection);
    //         GL.PopMatrix();
    //
    //         GL.MatrixMode(MatrixMode.Modelview);
    //     } 
    //
    //     protected override void OnRenderFrame( FrameEventArgs e )
    //     {
    //         GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
    //
    //         DrawImage(texture);
    //
    //         SwapBuffers();
    //     }
    // }
}