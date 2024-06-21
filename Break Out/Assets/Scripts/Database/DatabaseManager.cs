using EHTool;


public class DatabaseManager : MonoSingleton<DatabaseManager> {

    public int MaxScores = 20;
    public IDatabaseReader DBReader { get; private set; }

    protected override void OnCreate()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        DBReader = new FirebaseReader();
#else
        DBREader = new WebFirebaseReader();
#endif
        DBReader.OnCreate();

    }

    public void RemoveObserver(IObserver ops)
    {
        DBReader.RemoveObserver(ops);
    }
}