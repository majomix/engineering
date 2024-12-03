using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode
{
    public static class EmbeddedResourceHelper
    {
        public static string GetResourceText(Assembly assembly, string resourceFile)
        {
            var names = assembly.GetManifestResourceNames();
            using var stream = assembly.GetManifestResourceStream(resourceFile);
            Debug.Assert(stream != null, nameof(stream) + " != null");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string[] GetResourceLines(string resourceFile)
        {
            var assembly = Assembly.GetCallingAssembly();

            var names = assembly.GetManifestResourceNames();
            using var stream = assembly.GetManifestResourceStream( resourceFile);
            Debug.Assert(stream != null, nameof(stream) + " != null");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.None).ToArray();
        }

        public static byte[] GetResourceBytes(string resourceFile)
        {
            var assembly = Assembly.GetCallingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceFile);
            Debug.Assert(stream != null, nameof(stream) + " != null");
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static Stream GetResourceStream(string resourceFile)
        {
            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceFile);
            Debug.Assert(stream != null, nameof(stream) + " != null");
            return stream;
        }

        public static string GetResourceText(string resourceFile)
        {
            var assembly = Assembly.GetCallingAssembly();
            return GetResourceText(assembly, resourceFile);
        }
    }
}
