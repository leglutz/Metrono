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
        }

        public void TrackPage(string pageName)
        {
            #if !DEBUG
            _tracker.SetScreenName (pageName);
            _tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
            #endif
        }

        public void TrackEvent(string eventCategory, string @event)
        {
            #if !DEBUG
            var builder = new HitBuilders.EventBuilder();
            builder.SetCategory(eventCategory);
            builder.SetAction(@event);
            builder.SetLabel("Event");
            _tracker.Send(builder.Build());
            #endif
        }
    }
}

