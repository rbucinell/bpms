using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BPMSpopulator {
    class Program {
        static void Main( string[] args ) {
            // create reader & open file
            TextReader tr = new StreamReader( "C:\\Documents and Settings\\rbucine1\\Desktop\\WORD.LST");
            TextWriter tw = new StreamWriter( "C:\\Documents and Settings\\rbucine1\\Desktop\\output.txt" );
            int i = 0;
            int num = 100;
            string p1, p2, team;
            //<Team id="119" name="s" p1="ss" p2="ss" streak="0" />
            string line;
            while( i < 173528 ){
                line = "<Team id=\"" + num + "\" name=\"" + tr.ReadLine() + "\" p1=\"" + tr.ReadLine() + "\" p2=\"" + tr.ReadLine() + "\" streak=\"0\" />";
                num++;
                i += 3;
                tw.WriteLine( line );
            }
            Console.WriteLine( tr.ReadLine() );

            // close the stream
            tr.Close();
            tw.Close();
        }
    }
}
