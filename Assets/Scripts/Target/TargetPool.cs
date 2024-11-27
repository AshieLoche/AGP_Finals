public class TargetPool : ObjectPool
{

    public static TargetPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Target");
    }

}