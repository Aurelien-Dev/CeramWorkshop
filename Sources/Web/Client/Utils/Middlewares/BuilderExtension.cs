namespace Client.Utils.Middlewares
{
    public static class BuilderExtension
    {
        public static void UseGlobalCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticateMiddleware>();
        }
    }
}
