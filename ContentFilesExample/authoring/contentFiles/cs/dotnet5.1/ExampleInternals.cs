using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace ContentFilesExample
{
    /// <summary>
    /// Internal helpers for <see cref="ExampleReader"/>.
    /// </summary>
    internal static class ExampleInternals
    {
        /// <summary>
        /// Retrieve the contents of data.txt
        /// </summary>
        internal static async Task<string> GetFileText(string fileName)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var dataFile = await folder.GetFileAsync(fileName);

            using (var dataContent = await dataFile.OpenAsync(FileAccessMode.Read))
            using (var reader = new StreamReader(dataContent.AsStreamForRead()))
            {
                return reader.ReadToEnd().Trim();
            }
        }
    }
}
