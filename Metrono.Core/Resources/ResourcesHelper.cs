using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DiodeCompany.Metrono.Core.Attributes;

namespace DiodeCompany.Metrono.Core.Resources
{
    public enum ClickKind
    {
        [EnumDescription ("Beep Lo")]
        BeepLo,
        [EnumDescription ("Beep Hi")]
        BeepHi,
        [EnumDescription ("Tick Lo")]
        TickLo,
        [EnumDescription ("Tick Hi")]
        TickHi,
        [EnumDescription ("Clave Lo")]
        ClaveLo,
        [EnumDescription ("Clave Hi")]
        ClaveHi,
        [EnumDescription ("Rimshot Lo")]
        RimshotLo,
        [EnumDescription ("Rimshot Hi")]
        RimshotHi,
        [EnumDescription ("Bell")]
        Bell,
        [EnumDescription ("Tambourine")]
        Tambourine
    }

    public static class ResourcesHelper
    {
        public static Dictionary<int, string> NoteImageSourceMap { get; private set; }
        public static Dictionary<ClickKind, byte[]> ClickSoundMap { get; private set; }

        static ResourcesHelper ()
        {
            CreateNoteImageSourceMap ();
            CreateClickSoundMap ();
        }

        private static void CreateNoteImageSourceMap ()
        {
            NoteImageSourceMap = new Dictionary<int, string> ();
            NoteImageSourceMap [1] = "Whole";
            NoteImageSourceMap [2] = "Half";
            NoteImageSourceMap [4] = "Quarter";
            NoteImageSourceMap [8] = "Eighth";
            NoteImageSourceMap [16] = "Sixteenth";
            NoteImageSourceMap [32] = "ThirtySecond";
        }

        private static void CreateClickSoundMap ()
        {
            var assembly = typeof(ResourcesHelper).GetTypeInfo ().Assembly;
            ClickSoundMap = new Dictionary<ClickKind, byte[]> ();
            ClickSoundMap [ClickKind.BeepLo] = GetClickSound (assembly, ClickKind.BeepLo);
            ClickSoundMap [ClickKind.BeepHi] = GetClickSound (assembly, ClickKind.BeepHi);
            ClickSoundMap [ClickKind.TickLo] = GetClickSound (assembly, ClickKind.TickLo);
            ClickSoundMap [ClickKind.TickHi] = GetClickSound (assembly, ClickKind.TickHi);
            ClickSoundMap [ClickKind.ClaveLo] = GetClickSound (assembly, ClickKind.ClaveLo);
            ClickSoundMap [ClickKind.ClaveHi] = GetClickSound (assembly, ClickKind.ClaveHi);
            ClickSoundMap [ClickKind.RimshotLo] = GetClickSound (assembly, ClickKind.RimshotLo);
            ClickSoundMap [ClickKind.RimshotHi] = GetClickSound (assembly, ClickKind.RimshotHi);
            ClickSoundMap [ClickKind.Bell] = GetClickSound (assembly, ClickKind.Bell);
            ClickSoundMap [ClickKind.Tambourine] = GetClickSound (assembly, ClickKind.Tambourine);

        }

        private static byte[] GetClickSound (Assembly assembly, ClickKind clickKind)
        {
            using (var stream = assembly.GetManifestResourceStream ("DiodeCompany.Metrono.Core.Resources.Sounds.Clicks." + clickKind.ToString () + ".wav"))
            {
                using (var streamReader = new BinaryReader (stream))
                {
                    return streamReader.ReadBytes ((int)stream.Length);
                }
            }
        }
    }
}

