public class FrozenPeaPool : ObjectPool
{

    public static FrozenPeaPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Pea");
    }

}