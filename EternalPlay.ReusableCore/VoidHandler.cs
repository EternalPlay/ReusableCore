using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EternalPlay.ReusableCore {
    /// <summary>
    /// Reusable event handler delegate that takes no parameters and returns no value.  Useful for large event driven patterns such as MVC and MVP
    /// </summary>
    public delegate void VoidHandler();
}