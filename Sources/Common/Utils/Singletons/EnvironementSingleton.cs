namespace Utils.Singletons
{
    public sealed class EnvironementSingleton
    {
        public EnvironementSingleton Instance { get; } = new EnvironementSingleton();

        static EnvironementSingleton() { }
        private EnvironementSingleton() { }


        public static string WebRootPath { get; set; } = default!;
        public static string ContentRootPath { get; set; } = default!;

        public static bool IsInDev()
        {
            string environement = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(environement) || environement == "Development")
                return true;

            return false;
        }

        public static string GetConnectionString()
        {
            if (IsInDev())
                return @"Host=ec2-52-18-116-67.eu-west-1.compute.amazonaws.com;Database=desbki1pskf9ed;Username=htsfosutwsnach;Password=e2b6c99a5cf7728e1ea17d3d98642be1559db77d8fa5f720635036de497653a7";
            else
                return Environment.GetEnvironmentVariable("ConnectionString");
        }

        public static string GetEnvironmentVariable(string variableName)
        {
            string environementVariable = Environment.GetEnvironmentVariable(variableName);

            if (string.IsNullOrEmpty(environementVariable))
                throw new InvalidOperationException($"Variable ({variableName}) not present on the environement.");

            return environementVariable;
        }
    }
}