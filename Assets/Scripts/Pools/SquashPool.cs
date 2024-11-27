public class SquashPool : ObjectPool
{

    #region Attribute Declaration 

    #region Singleton  Attribute
    public static SquashPool instance;
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
        SetObjects("Squash");
    }
    #endregion

    #endregion

}