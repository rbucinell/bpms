using BPMS.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BPMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Team t1 = new Team(new Player("Ryan"), new Player("Ben"), "The Awesomes");
            Debug.WriteLine(t1.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form() );
        }
    }
}
