using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
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

        private void Start()
        {
            callToAction.onClick.AddListener(DispatchClick);
        }

        private NativeAd nativeAd;

        [SerializeField] public Text nativeAdViewTitle;
        [SerializeField] public Text nativeAdViewDescription;
        [SerializeField] public Text nativeAdViewSponsored;
        [SerializeField] public RawImage nativeAdViewIcon;
        [SerializeField] public Text nativeAdViewRatting;
        [SerializeField] public RawImage nativeAdViewImage;
        [SerializeField] public Button callToAction;

        public void OnPointerClick(PointerEventData eventData)
        {
            DispatchClick();
        }

        private void DispatchClick()
        {
            nativeAd?.dispatchClick(nativeAd);
        }

        private void DispatchImpression()
        {
            nativeAd?.dispatchImpression(nativeAd);
        }

        public void setNativeAd(NativeAd ad)
        {
            nativeAd = ad;
            updateNativeAdView();
        }

        public void destroyNativeView()
        {
            nativeAd = null;
            transform.gameObject.SetActive(false);
        }

        private void updateNativeAdView()
        {
            if (nativeAd == null) return;

            if (nativeAdViewTitle)
            {
                nativeAdViewTitle.text = !string.IsNullOrEmpty(nativeAd.getTitle()) ? nativeAd.getTitle() : "";
            }

            if (nativeAdViewDescription)
            {
                nativeAdViewDescription.text =
                    !string.IsNullOrEmpty(nativeAd.getDescription()) ? nativeAd.getDescription() : "";
            }

            if (nativeAdViewDescription)
            {
                nativeAdViewSponsored.text = "Sponsored";
            }

            if (nativeAdViewRatting)
            {
                nativeAdViewRatting.text =
                    !string.IsNullOrEmpty(nativeAd.getRating().ToString("0.0000"))
                        ? nativeAd.getRating().ToString("0.0000")
                        : "";
            }

            if (callToAction.GetComponentInChildren<Text>())
            {
                callToAction.GetComponentInChildren<Text>().text = !string.IsNullOrEmpty(nativeAd.getCallToAction())
                    ? nativeAd.getCallToAction()
                    : "";
            }

            transform.gameObject.SetActive(true);

            if (nativeAdViewIcon)
            {
                StartCoroutine(DownloadImage(nativeAd.getIcon(nativeAd), nativeAdViewIcon));
            }

            if (nativeAdViewImage)
            {
                StartCoroutine(DownloadImage(nativeAd.getImage(nativeAd), nativeAdViewImage));
            }

            DispatchImpression();
        }

        private static IEnumerator DownloadImage(string url, RawImage image)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            #if UNITY_2020_1_OR_NEWER
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            #else 
            if (request.isNetworkError || request.isHttpError)
            #endif
                Debug.Log(request.error);
            else
                image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}