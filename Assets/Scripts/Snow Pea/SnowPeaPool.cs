public class SnowPeaPool : ObjectPool
{

    public static SnowPeaPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Snow Pea");
    }

}