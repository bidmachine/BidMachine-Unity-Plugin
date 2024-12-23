using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Api
{
    [System.Obsolete("This class is deprecated and will be removed in future versions")]
    public sealed class UserPermissions : IUserPermissions
    {
        public UserPermissions() { }

        public UserPermissions(IUserPermissions client) { }

        /// <summary>
        /// Check CoarseLocation and FineLocation permission.
        /// See <see cref="BidMachine.CheckAndroidPermissions"/> for resulting triggered event.
        /// <param name="permission">Permission.CoarseLocation or Permission.CoarseLocation</param>
        /// </summary>
        [System.Obsolete("Check method is deprecated and will be removed in future versions")]
        public bool Check(string permission)
        {
            Debug.LogWarning("UserPermissions.Check is deprecated");
            return false;
        }

        /// <summary>
        /// Request CoarseLocation and FineLocation permissions.
        /// See <see cref="BidMachine.RequestAndroidPermissions"/> for resulting triggered event.
        /// </summary>
        [System.Obsolete("Request method is deprecated and will be removed in future versions")]
        public void Request()
        {
            Debug.LogWarning("UserPermissions.Request is deprecated");
        }
    }
}
