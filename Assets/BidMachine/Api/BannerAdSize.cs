namespace BidMachineAds.Unity.Api
{
    /// <summary>
    /// Represents banner ad size for BidMachine SDK 3.5.0+
    /// </summary>
    public sealed class BannerAdSize
    {
        /// <summary>
        /// Standard 320x50 banner
        /// </summary>
        public static readonly BannerAdSize Banner = new BannerAdSize(320, 50, false);

        /// <summary>
        /// Leaderboard 728x90 banner (tablet-optimized)
        /// </summary>
        public static readonly BannerAdSize Leaderboard = new BannerAdSize(728, 90, false);

        /// <summary>
        /// Medium Rectangle 300x250 banner (MREC)
        /// </summary>
        public static readonly BannerAdSize MediumRectangle = new BannerAdSize(300, 250, false);

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool IsAdaptive { get; private set; }

        private BannerAdSize(int width, int height, bool isAdaptive)
        {
            Width = width;
            Height = height;
            IsAdaptive = isAdaptive;
        }

        /// <summary>
        /// Creates an adaptive banner with specified width and maximum height
        /// </summary>
        /// <param name="width">Banner width in density-independent pixels</param>
        /// <param name="maxHeight">Maximum banner height in density-independent pixels</param>
        /// <returns>Adaptive BannerAdSize instance</returns>
        public static BannerAdSize Adaptive(int width, int maxHeight)
        {
            return new BannerAdSize(width, maxHeight, true);
        }

        /// <summary>
        /// Calculates the maximum expected banner height for a given width
        /// </summary>
        /// <param name="width">Banner width in density-independent pixels</param>
        /// <returns>Maximum adaptive height in density-independent pixels</returns>
        public static int GetMaxAdaptiveHeight(int width)
        {
            // Standard IAB adaptive banner height calculation
            if (width >= 728) return 90;
            if (width >= 468) return 60;
            if (width >= 320) return 50;
            return 50; // Default minimum
        }

        public override string ToString()
        {
            return IsAdaptive ? $"Adaptive_{Width}x{Height}" : $"Size_{Width}x{Height}";
        }

        // For backwards compatibility mapping
#pragma warning disable CS0618
        internal BannerSize ToLegacyBannerSize()
        {
            if (Width == 320 && Height == 50) return BannerSize.Size_320x50;
            if (Width == 300 && Height == 250) return BannerSize.Size_300x250;
            if (Width == 728 && Height == 90) return BannerSize.Size_728x90;
            return BannerSize.Size_320x50; // Default
        }

        internal static BannerAdSize FromLegacyBannerSize(BannerSize bannerSize)
        {
            return bannerSize switch
            {
                BannerSize.Size_320x50 => Banner,
                BannerSize.Size_300x250 => MediumRectangle,
                BannerSize.Size_728x90 => Leaderboard,
                _ => Banner
            };
        }
#pragma warning restore CS0618
    }
}
