using BidMachineAds.Unity.Common;

namespace BidMachineAds.Unity.Api
{
    public sealed class AdPlacementConfig
    {
        internal BannerAdSize BannerAdSize { get; private set; }
        internal AdContentType AdContentType { get; private set; }
        internal string PlacementId { get; private set; }
        internal CustomParams CustomParams { get; private set; }
        internal AdPlacementType PlacementType { get; private set; }

        private AdPlacementConfig() { }

        public static BannerPlacementBuilder BannerBuilder(BannerAdSize size)
        {
            return new BannerPlacementBuilder(size);
        }

        public static InterstitialPlacementBuilder InterstitialBuilder(AdContentType contentType)
        {
            return new InterstitialPlacementBuilder(contentType);
        }

        public static RewardedPlacementBuilder RewardedBuilder(AdContentType contentType)
        {
            return new RewardedPlacementBuilder(contentType);
        }

        public class BannerPlacementBuilder
        {
            private readonly AdPlacementConfig config;

            internal BannerPlacementBuilder(BannerAdSize size)
            {
                config = new AdPlacementConfig
                {
                    BannerAdSize = size,
                    PlacementType = AdPlacementType.Banner
                };
            }

            public BannerPlacementBuilder WithPlacementId(string placementId)
            {
                config.PlacementId = placementId;
                return this;
            }

            public BannerPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return config;
            }
        }

        public class InterstitialPlacementBuilder
        {
            private readonly AdPlacementConfig config;

            internal InterstitialPlacementBuilder(AdContentType contentType)
            {
                config = new AdPlacementConfig
                {
                    AdContentType = contentType,
                    PlacementType = AdPlacementType.Interstitial
                };
            }

            public InterstitialPlacementBuilder WithPlacementId(string placementId)
            {
                config.PlacementId = placementId;
                return this;
            }

            public InterstitialPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return config;
            }
        }

        public class RewardedPlacementBuilder
        {
            private readonly AdPlacementConfig config;

            internal RewardedPlacementBuilder(AdContentType contentType)
            {
                config = new AdPlacementConfig
                {
                    AdContentType = contentType,
                    PlacementType = AdPlacementType.Rewarded
                };
            }

            public RewardedPlacementBuilder WithPlacementId(string placementId)
            {
                config.PlacementId = placementId;
                return this;
            }

            public RewardedPlacementBuilder WithCustomParams(CustomParams customParams)
            {
                config.CustomParams = customParams;
                return this;
            }

            public AdPlacementConfig Build()
            {
                return config;
            }
        }
    }

    internal enum AdPlacementType
    {
        Banner,
        Interstitial,
        Rewarded
    }
}
