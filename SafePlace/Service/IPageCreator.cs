﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SafePlace.Service
{
    interface IPageCreator
    {
        Page CreateHomePage();
    }
}