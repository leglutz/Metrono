using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metrono.Droid.Activities;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    public class TutorialFragment : MvxFragment
    {
        private const string KeyImageResource = "TutorialFragment:ImageResource";
        private const string KeyIsLast = "TutorialFragment:IsLast";

        private int _imageResource;
        private bool _isLast;

        public static TutorialFragment NewInstance(int imageResource, bool isLast)
        {
            var fragment = new TutorialFragment();

            fragment._imageResource = imageResource;
            fragment._isLast= isLast;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null)
            {
                if (savedInstanceState.ContainsKey (KeyImageResource))
                {
                    _imageResource = savedInstanceState.GetInt (KeyImageResource);
                }

                if (savedInstanceState.ContainsKey (KeyIsLast))
                {
                    _isLast = savedInstanceState.GetBoolean (KeyIsLast);
                }
            }
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_tutorial, null);

            // ImageView
            var imageView = view.FindViewById<ImageView> (Resource.Id.tutorial_image_view);
            imageView.SetImageResource (_imageResource);

            // Ok button
            var okButton = view.FindViewById<Button> (Resource.Id.tutorial_button_ok);
            okButton.Visibility = _isLast ? ViewStates.Visible : ViewStates.Gone;
            okButton.Click += (sender, e) => ((TutorialActivity)Activity).ViewModel.OkCommand.Execute ();

            return view;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutInt(KeyImageResource, _imageResource);
            outState.PutBoolean(KeyIsLast, _isLast);
        }
    }
}
