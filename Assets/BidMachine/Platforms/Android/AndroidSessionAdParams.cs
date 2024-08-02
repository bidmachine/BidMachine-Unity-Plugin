using System;
using BidMachineAds.Unity.Api;
using UnityEngine;

namespace BidMachineAds.Unity.Android
{
    internal class AndroidSessionAdParams
    {
        private readonly AndroidJavaObject javaObject;

        public AndroidJavaObject JavaObject => javaObject;

        public AndroidSessionAdParams(SessionAdParams sessionAdParams)
        {
            javaObject = new AndroidJavaObject("io.bidmachine.SessionAdParams");
            SetSessionDuration(sessionAdParams.SessionDuration);
        }

        public AndroidJavaObject GetJavaObject()
        {
            return javaObject;
        }

        public AndroidSessionAdParams SetSessionDuration(int sessionDuration)
        {
            javaObject.Call<AndroidJavaObject>(
                "setSessionDuration",
                AndroidUtils.GetJavaPrimitiveObject(sessionDuration)
            );
            return this;
        }
    }
}
