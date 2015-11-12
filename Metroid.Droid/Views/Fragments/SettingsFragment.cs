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
        private ColorPickerPanelView _panelAlpha;

        public SettingsFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_settings, null);

            HasOptionsMenu = true;

            _panelAlpha = view.FindViewById<ColorPickerPanelView>(Resource.Id.color_picker);
            _panelAlpha.Color = Color.Black;
            _panelAlpha.Click += (sender, e) => {
                using (var colorPickerDialog = new ColorPickerDialog(Activity, _panelAlpha.Color))
                {
                    colorPickerDialog.AlphaSliderVisible = true;
                    colorPickerDialog.ColorChanged += (o, args) => _panelAlpha.Color = args.Color;
                    colorPickerDialog.Show();
                }
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

