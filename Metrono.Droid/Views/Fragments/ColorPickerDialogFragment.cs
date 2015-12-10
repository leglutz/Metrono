using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeCompany.Metrono.Core.Models;
using DiodeCompany.Metrono.Core.Services;
using DiodeCompany.Metrono.Droid.Controls.ColorPicker;

namespace DiodeCompany.Metrono.Droid.Views.Fragments
{
    public class ColorPickerDialogFragment : MvxDialogFragment, View.IOnClickListener
    {
        public event ColorChangedEventHandler ColorChanged;

        private readonly Settings _settings;

        private ColorPickerPanelView _oldColor;
        private ColorPickerPanelView _newColor;

        public ColorPickerDialogFragment ()
        { 
            _settings = Mvx.Resolve<ISettingsService>().Settings;

            SetStyle(MvxDialogFragment.StyleNoTitle, Android.Resource.Style.ThemeHoloLightDialogNoActionBar);

            RetainInstance = true;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);

            var view = this.BindingInflate(Resource.Layout.fragment_dialog_color_picker, null);

            var colorPicker = view.FindViewById<ColorPickerView>(Resource.Id.color_picker_view);
            _oldColor = view.FindViewById<ColorPickerPanelView>(Resource.Id.old_color_panel);
            _newColor = view.FindViewById<ColorPickerPanelView>(Resource.Id.new_color_panel);

            ((LinearLayout)_oldColor.Parent).SetPadding(
                (int) Math.Round(colorPicker.DrawingOffset), 
                0,
                (int) Math.Round(colorPicker.DrawingOffset), 
                0
            );

            _oldColor.SetOnClickListener(this);
            _newColor.SetOnClickListener(this);
            colorPicker.ColorChanged += (sender, args) =>
            {
                _newColor.Color = args.Color;
            };

            var oldColor = new Color (_settings.FlashColor);
            _oldColor.Color = oldColor;
            colorPicker.Color = oldColor;

            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetView(view);

            return dialog.Create();
        }

        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.new_color_panel:
                    if (ColorChanged != null)
                    {
                        ColorChanged (this, new ColorChangedEventArgs { Color = _newColor.Color });
                    }
                    break;
                case Resource.Id.old_color_panel:
                    if (ColorChanged != null)
                    {
                        ColorChanged (this, new ColorChangedEventArgs { Color = _oldColor.Color });
                    }
                    break;
            }
            Dismiss();
        }
    }
}

