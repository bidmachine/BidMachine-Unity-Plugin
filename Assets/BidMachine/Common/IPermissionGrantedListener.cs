using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Common
{
   public interface IPermissionGrantedListener
    {
        void writeExternalStorageResponse(int result);
        void accessCoarseLocationResponse(int result);
    }
}
