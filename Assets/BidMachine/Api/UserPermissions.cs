using BidMachineAds.Unity.Common;
using UnityEngine;

namespace BidMachineAds.Unity.Api
{
    public sealed class UserPermissions : IUserPermissions
    {
        private readonly IUserPermissions client;

        public UserPermissions()
        {
            this.client = BidMachineClientFactory.GetUserPermissions();
        }

        public UserPermissions(IUserPermissions client)
        {
            this.client = client;
        }

        /// <summary>
        /// Check CoarseLocation and FineLocation permission.
        /// See <see cref="BidMachine.CheckAndroidPermissions"/> for resulting triggered event.
        /// <param name="permission">Permission.CoarseLocation or Permission.CoarseLocation</param>
        /// </summary>
        public bool Check(string permission)
        {
            return client.Check(permission);
        }

        /// <summary>
        /// Request CoarseLocation and FineLocation permissions.
        /// See <see cref="BidMachine.RequestAndroidPermissions"/> for resulting triggered event.
        /// </summary>
        public void Request()
        {
            client.Request();
        }
    }
}
