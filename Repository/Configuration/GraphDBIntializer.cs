namespace Grafos.Repository.Configuration
{
    public static class DbInitializer
    {
        public static void Initialize(GraphDataContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}