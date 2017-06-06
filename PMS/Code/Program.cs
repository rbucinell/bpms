using System;
using System.Windows.Forms;
using System.IO;

namespace BPMS {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// TODO::
        /// -just added saving teams into an xml file, admin pass and settings dont work
        /// </summary>
        [STAThread]
        static void Main( string[] args ) {

            //Default save path
            string save_path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\My Documents\\BPMS_save.xml";
            
            //If the user give the program a save path to use, use that instead of default
            if (args.Length != 0 && args[0] != ""){
                save_path = args[0];
            }

            //Verify that the save file to be used is propper file type
            if (save_path.Substring( save_path.Length - 3, 3 ) != "xml") {
                MessageBox.Show( "Inputed save file is wrong format, needs to be .xml file. Goodbye.",
                    "Beer Pong Management System Error", MessageBoxButtons.OK );
            } else {
                if (!File.Exists( save_path )) {
                    File.Create( save_path );
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );
                Application.Run( new PMSmain( save_path ) );
            }
        }
    }
}
