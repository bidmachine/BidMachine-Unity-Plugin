#if PLATFORM_ANDROID
using UnityEngine;
using BidMachineAds.Unity.Api;
using BidMachineAds.Unity.Common;
using UnityEngine.Android;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidUserPermissions : IUserPermissions
    {
        public bool Check(string permission)
        {
            if (string.IsNullOrEmpty(permission))
            {
                return false;
            }

            var flag = false;
            switch (permission)
            {
                case Permission.CoarseLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
                case Permission.FineLocation:
                    if (!Permission.HasUserAuthorizedPermission(permission))
                    {
                        Debug.Log(permission + " - wasn't granded");
                        flag = false;
                    }
                    else
                    {
                        Debug.Log(permission + " - was granded");
                        flag = true;
                    }

                    break;
            }

            return flag;
        }

        public void Request()
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
}
#endif
