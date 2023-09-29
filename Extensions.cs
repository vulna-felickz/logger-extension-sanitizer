namespace logger_extension_sanitizer
{
    public static class Extensions
    {        
        public static string Sanitize(this ILogger _, string message)
        {
            return message.Replace(Environment.NewLine, string.Empty);
        }
        
    }
}
