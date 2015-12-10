using Android.App;
using Android.Content;
using Android.Gms.Analytics;

namespace DiodeCompany.Metrono.Droid.Helpers
{
    public class GoogleAnalyticsHelper
    {
        private const string TrackingId = "UA-70767993-3";

        private static GoogleAnalyticsHelper _instance;

        private GoogleAnalytics _googleAnalytics;
        private Tracker _tracker;

        public static GoogleAnalyticsHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GoogleAnalyticsHelper ();
                }
                return _instance;
            }
        }

        private GoogleAnalyticsHelper()
        {}

        public void Initialize(Application application)
        {
            _googleAnalytics = GoogleAnalytics.GetInstance(application.ApplicationContext);
            _tracker = _googleAnalytics.NewTracker(TrackingId);

            #if DEBUG
            _googleAnalytics.SetDryRun(true);
            #endif
        }

        public void TrackPage(string pageName)
        {
            _tracker.SetScreenName (pageName);
            _tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
        }

        public void TrackEvent(string eventCategory, string @event)
        {
            var builder = new HitBuilders.EventBuilder();
            builder.SetCategory(eventCategory);
            builder.SetAction(@event);
            builder.SetLabel("Event");
            _tracker.Send(builder.Build());
        }
    }
}

