namespace BidMachineAds.Unity.Common
{
    [System.Obsolete("This interface is deprecated and will be removed in future versions")]
    public interface IUserPermissions
    {
        bool Check(string permission);

        void Request();
    }
}
