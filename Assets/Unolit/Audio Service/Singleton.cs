namespace Medrick.Unolit.Utility
{
    public class Singleton<T> where T : new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                else
                {
                    instance = new T();
                    return instance;
                }
            }
        }
    }
}