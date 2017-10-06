namespace Gestaoaju.Factories
{
    public static class IdFactory
    {
        private static int id = 0;
        public static int Id => ++id;
    }
}
