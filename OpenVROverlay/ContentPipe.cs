using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenVROverlay
{
    public class ContentPipe
    {
        public static Texture2D LoadTexture(string filePath)
        {
            var bitmap = new Bitmap(filePath);

            int id;
            GL.GenTextures(1, out id);

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

            bitmap.UnlockBits(bmpData);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            
            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }
        
        public static Texture2D LoadTexture(Bitmap bitmap)
        {
            int id = GL.GenTexture();

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

            bitmap.UnlockBits(bmpData);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            
            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }
    }
}