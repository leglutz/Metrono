using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using DiodeCompany.Metrono.Core.ViewModels;
using DiodeCompany.Metrono.Droid.Controls.ColorPicker;
using DiodeCompany.Metrono.Droid.Views.Activities;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    [Register("diodecompany.metrono.droid.views.fragments.SettingsFragment")]
    public class SettingsFragment : MvxFragment<SettingsViewModel>
    {
        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = Mvx.Resolve<SettingsViewModel>();

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
            
            return view;
        }

        public override void OnPrepareOptionsMenu (IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            menu.FindItem (Resource.Id.menu_settings).SetVisible(false);
            ((MainActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled (true);
            ((MainActivity)Activity).SupportActionBar.Title = GetString(Resource.String.settings);
        }

        public override bool OnOptionsItemSelected (IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // Metronome fragment
                    FragmentManager.PopBackStack();
                    return true;
            }

            return base.OnOptionsItemSelected (item);
        }
    }
}

