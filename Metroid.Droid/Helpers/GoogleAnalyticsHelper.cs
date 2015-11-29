using Android.App;
using Android.Content;
using Android.Gms.Analytics;

namespace DiodeCompany.Metroid.Droid.Helpers
{
    public class GoogleAnalyticsHelper
    {
        private const string TrackingId = "UA-70767993-1";

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

        public void Initialize(Context context)
        {
            _googleAnalytics = GoogleAnalytics.GetInstance(context);
            _googleAnalytics.SetLocalDispatchPeriod(60);

            _tracker = _googleAnalytics.NewTracker(TrackingId);
            _tracker.EnableExceptionReporting(true);
            _tracker.EnableAutoActivityTracking(true);
        }

        public void TrackPage(string pageName)
        {
            _tracker.SetScreenName (pageName);
            _tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
        }

        public void TrackEvent(string eventCategory, string @event)
        {
            HitBuilders.EventBuilder builder = new HitBuilders.EventBuilder();
            builder.SetCategory(eventCategory);
            builder.SetAction(@event);
            builder.SetLabel("AppEvent");

            _tracker.Send(builder.Build());
        }
    }
}

