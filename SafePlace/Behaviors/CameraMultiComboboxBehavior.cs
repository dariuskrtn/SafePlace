using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Behaviors
{
    //Seems Like WPF doesn't support generics, so in order to use MultiCombobox we need to define new class for each type.
    class CameraMultiComboboxBehavior : MultiComboboxBehavior<Camera>
    {
    }
}
