using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Sello
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        static private List<PrivateFontCollection> _fontCollections;

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Inicio());
            Application.ApplicationExit += delegate {
                if (_fontCollections != null)
                {
                    foreach (var fc in _fontCollections) if (fc != null) fc.Dispose();
                    _fontCollections = null;
                }
            };
        }
        static public Font GetCustomFont(byte[] fontData, float size, FontStyle style)
        {
            if (_fontCollections == null) _fontCollections = new List<PrivateFontCollection>();
            PrivateFontCollection fontCol = new PrivateFontCollection();
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCol.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);
            _fontCollections.Add(fontCol);
            return new Font(fontCol.Families[0], size, style);
        }
    }
}