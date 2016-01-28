namespace $rootnamespace$
{
    /// <summary>
    /// Embedded resource reader for items from the example content package.
    /// </summary>
    internal static class ExampleReader
    {
        private const string DataFileName = "data.txt";

        /// <summary>
        /// Read data.txt as text from the assembly.
        /// </summary>
        internal static string GetDataText()
        {
            return ContentFilesExample.ExampleInternals.GetFileText(DataFileName).Result;
        }
    }
}
