using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMS.Code {
    class TreeManager {

        public Control Root { get; set; }
        public TreeManager( TourneyMatchUp[] entries ) {
            Root = new TeamTextBox();
            Root.Visible = false;
            int totalSize = entries.Length;
            int depth = (int)Math.Pow( totalSize, (1.0/3.0) );
            //int curSize = 1;

            



        }
    }

    public class PMSTreeNode {
        public Control Data{get;set;}
        public PMSTreeNode Parent{get;set;}
        public PMSTreeNode LeftChild{get;set;}
        public PMSTreeNode RightChild{get;set;}
        public Int32 Depth { get; set; }
        public Int32 IndexFromLeft { get; set; }


        public PMSTreeNode( Control data ) {
            Data = data;
            Parent = null;
            LeftChild = null;
            RightChild = null;
            Depth = -1;
            IndexFromLeft = -1;
        }

    }
}
