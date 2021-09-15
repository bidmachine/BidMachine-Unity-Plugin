using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable 649

namespace BidMachineAds.Unity.Api
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class NativeAdView : MonoBehaviour, IPointerClickHandler
    {
        private void Awake()
        {
            transform.gameObject.SetActive(false);
        }

        private NativeAd nativeAd;

        [SerializeField] public Text nativeAdViewTitle;
        [SerializeField] public RawImage nativeAdViewIcon;
        [SerializeField] public Text nativeAdViewRatting;
        [SerializeField] public Text nativeAdViewDescription;
        [SerializeField] public RawImage nativeAdViewImage;
        [SerializeField] public Button callToAction;

        public void OnPointerClick(PointerEventData eventData)
        {
           Debug.Log("OnPointerClick - dispatchClick");
        }

        public void setNativeAd(NativeAd ad)
        {
            nativeAd?.destroy();
            nativeAd = ad;
            updateNativeAdView();
        }

        public void destroyNativeView()
        {
            nativeAd?.destroy();
            transform.gameObject.SetActive(false);
        }

        private void updateNativeAdView()
        {
            if (nativeAd == null) return;

            if (nativeAdViewTitle != null)
            {
                nativeAdViewTitle.text = !string.IsNullOrEmpty(nativeAd.getTitle()) ? nativeAd.getTitle() : "";
            }

            if (nativeAdViewDescription != null)
            {
                nativeAdViewDescription.text =
                    !string.IsNullOrEmpty(nativeAd.getDescription()) ? nativeAd.getDescription() : "";
            }

            if (callToAction != null)
            {
                callToAction.GetComponentInChildren<Text>().text = !string.IsNullOrEmpty(nativeAd.getCallToAction())
                    ? nativeAd.getCallToAction()
                    : "";
            }

            if (nativeAdViewRatting != null)
            {
                nativeAdViewRatting.text =
                    !string.IsNullOrEmpty(nativeAd.getRating().ToString("0.0000"))
                        ? nativeAd.getRating().ToString("0.0000")
                        : "";
            }

            if (callToAction != null)
            {
                callToAction.GetComponentInChildren<Text>().text = !string.IsNullOrEmpty(nativeAd.getCallToAction())
                    ? nativeAd.getCallToAction()
                    : "";
            }

            transform.gameObject.SetActive(true);
        }
    }
}