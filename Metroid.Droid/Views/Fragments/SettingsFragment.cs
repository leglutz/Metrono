using Android.Graphics;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeTeam.Metroid.Core.ViewModels;
using DiodeTeam.Metroid.Droid.Controls.ColorPicker;
using DiodeTeam.Metroid.Droid.Views.Activities;

namespace DiodeTeam.Metroid.Droid.Views.Fragments
{
    public class SettingsFragment : MvxFragment<SettingsViewModel>
    {
        private ColorPickerPanelView _colorPicker;

        public SettingsFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_settings, null);

            HasOptionsMenu = true;

            _colorPicker = view.FindViewById<ColorPickerPanelView>(Resource.Id.color_picker);
            _colorPicker.Color = Color.Black;
            _colorPicker.Click += (sender, e) => {
                var colorPickerDialogFragment = new ColorPickerDialogFragment(_colorPicker.Color);
                colorPickerDialogFragment.ColorChanged += (o, args) => _colorPicker.Color = args.Color;
                colorPickerDialogFragment.Show(FragmentManager, null);
            };

            return view;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            menu.FindItem (Resource.Id.settings_menu).SetVisible(false);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (true);
            ((MainActivity)Activity).SupportActionBar.Title = "Settings";

            base.OnPrepareOptionsMenu (menu);
        }
    }
}

