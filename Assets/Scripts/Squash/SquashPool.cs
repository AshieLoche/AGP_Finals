public class SquashPool : ObjectPool
{

    public static SquashPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Squash");
    }

}