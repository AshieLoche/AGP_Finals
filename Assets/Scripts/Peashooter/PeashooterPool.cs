public class PeashooterPool : ObjectPool
{

    public static PeashooterPool instance;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        SetObjects("Peashooter");
    }

}