using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace ToDoTomatoClock.Tools
{
    public static class Utils
    {
        /// <summary>
        /// 在指定路径查找指定名称的文件
        /// </summary>
        /// <param name="path">指定路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包括全部符合条件的文件全路径的集合</returns>
        public static List<string> SearchFileInPath(string path, string fileName)
        {
            return new List<string>(Directory.GetFiles(path, fileName, SearchOption.AllDirectories));
        }

        /// <summary>
        /// 获取当前 exe 文件所在目录
        /// </summary>
        /// <returns>文件所在目录的全路径</returns>
        public static string GetCurrentPath()
        {
            return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        public static void PlaySound(string wavFullPath)
        {
            new Thread(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(wavFullPath) || !File.Exists(wavFullPath))
                    {
                        SoundPlayer sp = new SoundPlayer(AppResource.Alarm);
                        sp.PlaySync();
                    }
                    else
                    {
                        FileStream fs = File.OpenRead(wavFullPath);
                        SoundPlayer sp = new SoundPlayer(fs);
                        sp.PlaySync();
                    }
                }).Start();
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            byte[] bytelist = bytes;
            MemoryStream ms1 = new MemoryStream(bytelist);
            return (Bitmap)Image.FromStream(ms1);
        }

        public static ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new System.ComponentModel.Win32Exception();
            }
            return wpfBitmap;
        }

        public static Bitmap ReColorIcon(Bitmap bitmap, int r, int g, int b)
        {
            Bitmap res = bitmap.Clone() as Bitmap;
            for (int x = 0; x < res.Width; x++)
            {
                for (int y = 0; y < res.Height; y++)
                {
                    if (res.GetPixel(x, y).A != 0)
                    {
                        res.SetPixel(x, y,
                            System.Drawing.Color.FromArgb(res.GetPixel(x, y).A, r, g, b));
                    }
                }
            }

            return res;
        }

        public static ImageBrush GetIconFromResource(byte[] resource) =>
            new ImageBrush(Utils.BitmapToImageSource(Utils.BytesToBitmap(resource)));
    }
}
