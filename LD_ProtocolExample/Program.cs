namespace LD_P
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LD_ProtocolExample());
        }
    }
}