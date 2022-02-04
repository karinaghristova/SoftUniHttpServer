namespace SoftUniHttpServer.Server.Common
{
    public static class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "Value";
                throw new ArgumentNullException($"{name} cannot be null");
            }
        }
    }
}
