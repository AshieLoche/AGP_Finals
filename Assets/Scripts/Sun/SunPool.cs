public class SunPool : ObjectPool
{

    public static SunPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Sun");
    }

}