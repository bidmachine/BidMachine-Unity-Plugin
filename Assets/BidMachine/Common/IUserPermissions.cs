namespace BidMachineAds.Unity.Common
{
    public interface IUserPermissions
    {
        bool Check(string permission);

        void Request();
    }
}
