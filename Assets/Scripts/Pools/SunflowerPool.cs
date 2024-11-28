public class SunflowerPool : ObjectPool
{

    public static SunflowerPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Sunflower");
    }

}