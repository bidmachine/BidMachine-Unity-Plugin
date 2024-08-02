namespace BidMachineAds.Unity.Common
{
    public interface IPermissionGrantedListener
    {
        void WriteExternalStorageResponse(int result);

        void AccessCoarseLocationResponse(int result);
    }
}
