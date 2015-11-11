using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Views.Activities;

namespace DiodeTeam.Metroid.Droid.Views.Fragments
{
    public class SettingsFragment : MvxFragment<SettingsViewModel>
    {
        public SettingsFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            return this.BindingInflate (Resource.Layout.fragment_settings, null);
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            menu.FindItem (Resource.Id.settings_menu).SetVisible(false);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (true);

            base.OnPrepareOptionsMenu (menu);
        }
    }
}

