using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DiodeTeam.Metroid.Core.Attributes;

namespace DiodeTeam.Metroid.Core.Resources
{
    public enum ClickKind
    {
        [EnumDescription ("Bell")]
        Bell,
        [EnumDescription ("Tambourine")]
        Tambourine,
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
        RimshotHi
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
            ClickSoundMap [ClickKind.Bell] = GetClickSound (assembly, ClickKind.Bell);
            ClickSoundMap [ClickKind.ClaveHi] = GetClickSound (assembly, ClickKind.ClaveHi);
            ClickSoundMap [ClickKind.ClaveLo] = GetClickSound (assembly, ClickKind.ClaveLo);
            ClickSoundMap [ClickKind.RimshotHi] = GetClickSound (assembly, ClickKind.RimshotHi);
            ClickSoundMap [ClickKind.RimshotLo] = GetClickSound (assembly, ClickKind.RimshotLo);
            ClickSoundMap [ClickKind.Tambourine] = GetClickSound (assembly, ClickKind.Tambourine);
            ClickSoundMap [ClickKind.TickHi] = GetClickSound (assembly, ClickKind.TickHi);
            ClickSoundMap [ClickKind.TickLo] = GetClickSound (assembly, ClickKind.TickLo);
        }

        private static byte[] GetClickSound (Assembly assembly, ClickKind clickKind)
        {
            using (var stream = assembly.GetManifestResourceStream ("DiodeTeam.Metroid.Core.Resources.Sounds.Clicks." + clickKind.ToString () + ".wav"))
            {
                using (var streamReader = new BinaryReader (stream))
                {
                    return streamReader.ReadBytes ((int)stream.Length);
                }
            }
        }
    }
}

