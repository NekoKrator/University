public class Singleton<T> where T : class, new()
{
    private static readonly Lazy<Queue<T>> instances = new Lazy<Queue<T>>(() => new Queue<T>());
    private static int maxInstances = 10; // Максимальна кількість об'єктів
    protected Singleton() { }

    public static T Instance
    {
        get
        {
            lock (instances.Value)
            {
                if (instances.Value.Count == 0)
                {
                    if (typeof(T).GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Any())
                    {
                        for (int i = 0; i < maxInstances; i++)
                        {
                            System.Reflection.ConstructorInfo cInfo = typeof(T).GetConstructor(
                                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                                null,
                                new Type[0],
                                new System.Reflection.ParameterModifier[0]);
                            instances.Value.Enqueue((T)cInfo.Invoke(null));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < maxInstances; i++)
                        {
                            instances.Value.Enqueue(new T());
                        }
                    }
                }
                return instances.Value.Dequeue();
            }
        }
    }

    public static void ReleaseInstance(T instance)
    {
        lock (instances.Value)
        {
            instances.Value.Enqueue(instance);
        }
    }
}
