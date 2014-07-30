#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Eteephonehome
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Eteephonehome(960,715,16))
                game.Run();
        }
    }
#endif
}
