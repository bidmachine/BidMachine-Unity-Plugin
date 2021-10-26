using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 649

namespace BidMachineAds.Unity.Api
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "InvokeAsExtensionMethod")]
    public class NativeAdView : MonoBehaviour, IPointerClickHandler
    {
        private void Awake()
        {
            transform.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            if (callToAction)
            {
                callToAction.onClick.AddListener(DispatchClick);
            }
            InvokeRepeating(nameof(CheckVisibilityUI), 0.1f, 1);
        }

        private void Start()
        {
            Debug.Log("Start");
        }

        private NativeAd nativeAd;
        private bool isNativeAdVisible;

        [SerializeField] public Text nativeAdViewTitle;
        [SerializeField] public Text nativeAdViewDescription;
        [SerializeField] public Text nativeAdViewSponsored;
        [SerializeField] public RawImage nativeAdViewIcon;
        [SerializeField] public Text nativeAdViewRatting;
        [SerializeField] public RawImage nativeAdViewImage;
        [SerializeField] public Button callToAction;
        [SerializeField] public Camera cam;

        public void CheckVisibilityUI()
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (!rectTransform)
            {
                return;
            }

            if (!rectTransform.gameObject.activeInHierarchy)
            {
                isNativeAdVisible = false;
                return;
            }

            isNativeAdVisible = IsFullyVisibleNativeAd(rectTransform);

            Debug.Log($"IsFullyVisibleNativeAd - {IsFullyVisibleNativeAd(rectTransform)}");
        }

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

            if (!nativeAdViewTitle || !nativeAdViewDescription || !nativeAdViewDescription || !nativeAdViewRatting ||
                !callToAction || !nativeAdViewIcon || !nativeAdViewImage) return;

            nativeAdViewTitle.text = !string.IsNullOrEmpty(nativeAd.getTitle()) ? nativeAd.getTitle() : "";
            nativeAdViewDescription.text =
                !string.IsNullOrEmpty(nativeAd.getDescription()) ? nativeAd.getDescription() : "";
            nativeAdViewSponsored.text = "Sponsored";
            nativeAdViewRatting.text =
                !string.IsNullOrEmpty(nativeAd.getRating().ToString("0.00"))
                    ? nativeAd.getRating().ToString("0.00")
                    : "";

            var callToActionButtonText = callToAction.GetComponentInChildren<Text>();

            if (callToActionButtonText)
            {
                callToAction.GetComponentInChildren<Text>().text = !string.IsNullOrEmpty(nativeAd.getCallToAction())
                    ? nativeAd.getCallToAction()
                    : "";
            }

            gameObject.SetActive(true);

            StartCoroutine(DownloadImage(nativeAd.getIcon(nativeAd), nativeAdViewIcon));
            StartCoroutine(DownloadImage(nativeAd.getImage(nativeAd), nativeAdViewImage));

            DispatchImpression();
        }

        private static IEnumerator DownloadImage(string url, RawImage image)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }

            yield return null;
        }

        private static int CountCornersVisibleFrom(RectTransform rectTransform, Camera camera = null)
        {
            var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
            var objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);
            return objectCorners.Select
                (t => camera != null ? camera.WorldToScreenPoint(t) : t).Count(tempScreenSpaceCorner
                => screenBounds.Contains(tempScreenSpaceCorner));
        }

        private static bool IsFullyVisibleNativeAd(RectTransform rectTransform, Camera camera = null)
        {
            return CountCornersVisibleFrom(rectTransform, camera) == 4;
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            CancelInvoke();
            if (callToAction)
            {
                callToAction.onClick.AddListener(null);
            }
        }
    }
}