﻿/*
 * Direct port to Mono for Android of https://github.com/attenzione/android-ColorPickerPreference
 * by Tomasz Cielecki <tomasz@ostebaronen.dk>
 * Whose license is:
 * 
 * Copyright (C) 2010 Daniel Nilsson
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Android.Graphics;

namespace DiodeCompany.Metrono.Droid.Controls.ColorPicker
{
    public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);

    public class ColorChangedEventArgs : EventArgs
    {
        public Color Color { get; set; }
    }
}