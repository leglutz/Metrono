using Android.Graphics;
using Android.OS;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Controls.ColorPicker;
using DiodeCompany.Metrono.Droid.Helpers;
using DiodeCompany.Metrono.Droid.Views.Activities;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    public class SettingsFragment : MvxFragment<SettingsViewModel>
    {
        public SettingsFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_settings, null);

            HasOptionsMenu = true;

            var colorPicker = view.FindViewById<ColorPickerPanelView>(Resource.Id.color_picker);
            colorPicker.Color = new Color(ViewModel.Settings.FlashColor);
            colorPicker.Click += (sender, e) => {
                var colorPickerDialogFragment = new ColorPickerDialogFragment();
                colorPickerDialogFragment.ColorChanged += (o, args) => {
                    colorPicker.Color = args.Color;
                    ViewModel.Settings.FlashColor = colorPicker.Color.ToArgb();
                };
                colorPickerDialogFragment.Show(FragmentManager, null);
            };

            GoogleAnalyticsHelper.Instance.TrackPage("Settings");

            return view;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            menu.FindItem (Resource.Id.menu_settings).SetVisible(false);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (true);
            ((MainActivity)Activity).SupportActionBar.Title = GetString(Resource.String.settings);

            base.OnPrepareOptionsMenu (menu);
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // Metronome fragment
                    FragmentManager.PopBackStack();
                    break;
            }

            return base.OnOptionsItemSelected (item);
        }
    }
}

