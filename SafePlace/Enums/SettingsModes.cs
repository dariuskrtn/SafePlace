using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Enums
{
    [Flags]
    public enum SettingsModes : byte
    {
        // Left shidting is also an option as well as writing binary or hexadecimal numbers
        Preview = 0,           // None
        //Creating new floor
        CreatingNew = 1 << 0,  // = 0b0001 = 0x1 = 1   
        //editing exsisting floor
        Editing = 1 << 1       // = 0b0010 = 0x2 = 2
    }
}
