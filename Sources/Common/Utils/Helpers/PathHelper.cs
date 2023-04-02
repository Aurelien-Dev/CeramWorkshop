namespace Utils.Helpers
{
    public static class WebPathHelper
    {

        public static string GetDirectoryName(string url)
        {
            return Path.GetDirectoryName(url).Replace("\\", "/");
        }

        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths).Replace("\\", "/");
        }

    }
}
