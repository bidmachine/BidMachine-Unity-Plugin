namespace BidMachineAds.Unity.Common
{
    public interface IUserPermissions
    {
        bool Check(string permission);

        void Request();

        public interface IListener
        {
            void WriteExternalStorageResponse(int result);

            void AccessCoarseLocationResponse(int result);
        }
    }
}
