using Android.App;
using Xamarin;

namespace DiodeCompany.Metrono.Droid.Helpers
{
    public class XamarinInsightsHelper
    {
        private const string ApiKey = "5f5da99d136cd7a61641b178fba1ae3c99668fe5";

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

