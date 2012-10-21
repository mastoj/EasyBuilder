namespace EasyBuilder
{
    public static class EasyBuilder
    {
        public static BuilderContext<T> BuildA<T>() where T : new()
        {
            return new BuilderContext<T>();
        }
    }
}