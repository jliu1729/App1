﻿using System;
using Android.Views;

namespace App1.Droid {
    public class RecyclerClickEventArgs : EventArgs {
        public View View { get; set; }
        public int Position { get; set; }
    }
}

