public class PeaPool : ObjectPool
{

    public static PeaPool instance;

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