using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Support.Fragging.Fragments;
using DiodeTeam.Metroid.Droid.Controls.ColorPicker;

namespace DiodeTeam.Metroid.Droid.Views.Fragments
{
    public class ColorPickerDialogFragment : MvxDialogFragment, View.IOnClickListener
    {
        public event ColorChangedEventHandler ColorChanged;

        private readonly Color _baseColor;

        private ColorPickerPanelView _oldColor;
        private ColorPickerPanelView _newColor;

        public ColorPickerDialogFragment (Color baseColor)
        {
            _baseColor = baseColor;

            SetStyle(MvxDialogFragment.StyleNoTitle, 0);
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView (inflater, container, savedInstanceState);
            var view = this.BindingInflate (Resource.Layout.fragment_dialog_color_picker, null);

            var colorPicker = view.FindViewById<ColorPickerView>(Resource.Id.color_picker_view);
            colorPicker.AlphaSliderVisible = true;
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

            _oldColor.Color = _baseColor;
            colorPicker.Color = _baseColor;

            return view;
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

