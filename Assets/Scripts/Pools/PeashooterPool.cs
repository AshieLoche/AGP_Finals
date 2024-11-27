public class PeashooterPool : ObjectPool
{

    #region Attribute Declaration 

    #region Singleton  Attribute
    public static PeashooterPool instance;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
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
    #endregion

    #endregion

}