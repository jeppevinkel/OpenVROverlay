using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES11;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenVROverlay
{
    public class Game : GameWindow
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };
        int VertexBufferObject;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpenTK.Windowing.Desktop.GameWindow" /> class with sensible default attributes.
        /// </summary>
        /// <param name="gameWindowSettings">The <see cref="T:OpenTK.Windowing.Desktop.GameWindow" /> related settings.</param>
        /// <param name="nativeWindowSettings">The <see cref="T:OpenTK.Windowing.Desktop.NativeWindow" /> related settings.</param>
        /// <remarks>
        /// <para>
        /// Use GameWindowSettings.Default and NativeWindowSettings.Default to get some sensible default attributes.
        /// </para>
        /// </remarks>
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <summary>Run when the window is ready to update.</summary>
        /// <param name="args">The event arguments for this frame.</param>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
            
            base.OnUpdateFrame(args);
        }

        /// <summary>Run immediately after Run() is called.</summary>
        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(All.ArrayBuffer, VertexBufferObject);
            GL.BufferData(All.ArrayBuffer, vertices.Length * sizeof(float), vertices, All.StaticDraw);
            
            base.OnLoad();
        }

        /// <summary>Run when the window is about to close.</summary>
        protected override void OnUnload()
        {
            GL.BindBuffer(All.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            
            base.OnUnload();
        }

        /// <summary>Run when the window is ready to update.</summary>
        /// <param name="args">The event arguments for this frame.</param>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        /// <summary>
        /// Raises the <see cref="E:OpenTK.Windowing.Desktop.NativeWindow.Resize" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:OpenTK.Windowing.Common.ResizeEventArgs" /> that contains the event data.</param>
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            base.OnResize(e);
        }
    }
}