using Android.App;
using Xamarin;

namespace DiodeCompany.Metroid.Droid.Helpers
{
    public class XamarinInsightsHelper
    {
        private const string ApiKey = "ab6566a0d14adf0748a3d916dca49e4cfe75fbd6";

        private static XamarinInsightsHelper _instance;

        public static XamarinInsightsHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XamarinInsightsHelper ();
                }
                return _instance;
            }
        }

        private XamarinInsightsHelper()
        {}

        public void Initialize(Application application)
        {
            // Initialize Insights
            #if DEBUG
            Xamarin.Insights.Initialize (Insights.DebugModeKey, application.ApplicationContext);
            #else
            Xamarin.Insights.Initialize (ApiKey, application.ApplicationContext);
            #endif
        }
    }
}

