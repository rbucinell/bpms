using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BPMS
{
    /// <summary>
    /// Enum for current Mode of the System
    /// </summary>
    public enum SystemMode {
        MAIN = 0,
        TOURNEY_SETUP,
        TOURNEY_PLAY,
        SIMPLE //ToDo, eventually make a simple mode
    };

    /// <summary> 
    /// A container to hold and support all the data for BPMS
    /// 
    /// @author:    Ryan Bucinell
    /// @date:      2/2/10
    /// </summary>
    public class SystemData {

        /*Teams data*/
        //All teams in the data
        private List<Team> teams;

        /// <summary>Accessor and Mutator for the teams that are queued
        /// </summary>
        public Queue<Team> QueuedTeams { get; set; }

        public Team[] InTourney { get; set; }

        /// <summary> Stores the data in the Winners field  </summary>
        public Team Winner { get; set; }

        /// <summary> Stores the data in the Challengers field  </summary>
        public Team Challenger { get; set; }

        /// <summary> The current Mode of the system </summary>
        public SystemMode Mode { get; set; }

        /// <summary>Accessor and Mutator for Password (Encrypt before saving to here!) </summary>
        public String Password { get; set; }

        /// <summary>Accessor and Mutator for bool value of if password has been set </summary>
        public bool PassIsSet { get; set; }

        /// <summary> Getter and Setter for save file path </summary>
        public string SaveFile { get; set; }

        /// <summary> Accessor and Mutator for current Theme set
        /// </summary>
        public string Theme { get; set; }

        /// <summary> Getter and Setter for if teams are allowed to queue multiple times </summary>
        public bool MulitQueue { get; set; }

        /// <summary> Getter and Setter if a password is required to remove </summary>
        public bool PassToRemove { get; set; }
        
        public bool RecentSave { get; set; }
        
        #region Constructors
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SystemData()
        {
            setDefaults();
        }

        /// <summary> Overloaded contructor, passing in the save file path
        /// Create a new DataSet and load in data
        /// </summary>
        /// <param name="path">Path of the save file</param>
        public SystemData( string path )
        {
            setDefaults();
            SaveFile = path;
            loadData();
        }

        /// <summary>
        /// Helper method for constructors, to build common materials
        /// </summary>
        private void setDefaults() {
            Winner = null;
            Challenger = null;
            PassToRemove = false;
            MulitQueue = true;
            Theme = "default";
            Password = "";
            PassIsSet = false;
            teams = new List<Team>();
            InTourney = new Team[16];
            QueuedTeams = new Queue<Team>();
            //inTournament = new List<Team>( 16 );
            SaveFile = Environment.GetEnvironmentVariable( "USERPROFILE" ) + "\\My Documents\\BPMS_save.xml";
            RecentSave = false;
        }
        #endregion

        #region Accessors & Mutators
        
        /// <summary>
        /// Accessor for all the teams
        /// </summary>
        public List<Team> AllTeams
        {
            get { return teams; }
        }
        
        /// <summary> Accesor for a Team, searching on the team's Id
        /// </summary>
        /// <param name="id">The Teams ID</param>
        /// <returns>A team from storage</returns>
        public Team getTeam( int id ){
            foreach (Team t in teams) {
                if (t.Id == id) {
                    return t;
                }
            }
            return null;
        }

        /// <summary> Accesor for a Team, searching on the team's team name
        /// </summary>
        /// <param name="name">The Teams Name</param>
        /// <returns>A team from storage</returns>
        public Team getTeam(string name)
        {
            if( name != "" || name != " " )
            {
                foreach( Team t in teams )
                {
                    if( t.TeamName == name )
                    {
                        return t;
                    }
                }
            }
            return null;
        }
        #endregion

        /// <summary> Checks to see if team exists
        /// </summary>
        /// <param name="name">The Teams Name</param>
        /// <returns>true if found</returns>
        public bool hasTeam(string name)
        {
            if( name != "" || name != " " )
            {
                foreach( Team t in teams )
                {
                    if( t.TeamName == name )
                        return true;
                }
            }
            return false;
        }

        /// <summary> Checks to see if team exists
        /// </summary>
        /// <param name="name">The Teams ID</param>
        /// <returns>true if found</returns>
        public bool hasTeam(int id)
        {
            if( id > 10 )
            {
                foreach( Team t in teams )
                {
                    if( t.Id == id )
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Removes a team from the queue
        /// </summary>
        /// <param name="pos">index of the team in queue</param>
        public void removeFromQueue( int pos ) {
            Queue<Team> tempQueue = new Queue<Team>();
            
            int a = QueuedTeams.Count;
            for (int i = 0; i < a; i++) {
                if (i != pos)
                    tempQueue.Enqueue( QueuedTeams.Dequeue() );
                else
                    QueuedTeams.Dequeue();
            }
            QueuedTeams = tempQueue;
        }

        /// <summary> Deletes a team from the data & removes it from any queue or tournament
        /// </summary>
        /// <param name="t">The team that you wish to delete</param>
        /// <returns>String of thee report</returns>
        public string deleteTeam(Team t)
        {
            if( teams.Contains(t) )
            {
                //remove from the queue if its there
                if( QueuedTeams.Contains(t) )
                {
                    Queue<Team> temp = new Queue<Team>();// = inQ;
                    for( int i = 0; i < QueuedTeams.Count; i++ )
                    {
                        Team deq = QueuedTeams.Dequeue();
                        if( deq != t )
                            temp.Enqueue(deq);
                    }
                    QueuedTeams = temp;
                }
                //remove it from the tournament
                for( int i = 0; i < InTourney.Length; i++ )
                {
                    if( InTourney[i] == t )
                        InTourney[i] = null;
                }
                //if its a winner or challenger:
                if( Winner == t )
                {
                    Winner = Challenger;
                    Challenger = (QueuedTeams.Count != 0) ? QueuedTeams.Dequeue() : null;
                }
                else if( Challenger == t )
                {
                    Challenger = (QueuedTeams.Count != 0) ? QueuedTeams.Dequeue() : null;
                }

                //finally remove it for good
                teams.Remove(t);
                return "Removed " + t;
            }
            else
            {
                return "Remove failed! " + t + " Does not exist";
            }
        }

        /// <summary> 
        /// Adds the contra team to data
        /// </summary>
        public void AddContraTeam()
        {
            if( getTeam("Contra") == null )
            {
                Team contra = new Team("Bill", "Lance");
                contra.TeamName = "Contra";
                teams.Add(contra);
            }
        }

        /// <summary>
        /// Inserts a Team into the tournament
        /// </summary>
        /// <param name="t">Some Team to be placed in the tourney</param>
        public bool addTeamToTournament(Team t)
        {
            for( int i = 0; i < InTourney.Length; i++ )
            {
                if( InTourney[i] == t )
                {
                    return false;
                }
            }
            for( int i = 0; i < InTourney.Length; i++ )
            {
                if( InTourney[i] == null )
                {
                    InTourney[i] = t;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds team into a particular seed (Not 0 indexed, 1 = 1st)
        /// </summary>
        /// <param name="t">The team to insert</param>
        /// <param name="seed">the seeding to give the team</param>
        public void seedTeamToTournament(Team t, int seed)
        {
            //We can assume that they will be entering the number they want not the index
            seed -= 1;
            //avoid our index out of bounds
            if( seed >= InTourney.Length )
                seed = InTourney.Length - 1;
            if( InTourney[seed] != null )
            {
                DialogResult result = MessageBox.Show("There is already a Team in the seed. Do you want to replace it?" +
                    " Yes-Replace, No-Push everything down, Cancel-will cancel the action.",
                    "Seed already taken", MessageBoxButtons.YesNoCancel);
                switch( result )
                {
                    case DialogResult.Yes:
                        InTourney[seed] = t;
                        break;
                    case DialogResult.No:
                        Team cur = InTourney[seed], next;
                        InTourney[seed] = t;
                        for( int i = seed + 1; i < InTourney.Length; i++ )
                        {
                            if( i + 1 >= InTourney.Length )
                                break;
                            next = InTourney[i + 1];
                            InTourney[i + 1] = cur;
                            cur = next;
                        }
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                InTourney[seed] = t;
            }
        }

        /// <summary> 
        /// Loads data from a saved XML file
        /// </summary>
        /// <param name="theSaveFile"> path of the XML file</param>
        public void loadData()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                FileStream fsxml = new FileStream(SaveFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                xmlDoc.Load(fsxml);

                XmlNodeList xnl = xmlDoc.GetElementsByTagName("system");
                XmlAttributeCollection xnc = xnl[0].Attributes;

                if( xnc["admin_pass"].Value == "" )
                {
                    Password = "";
                    PassIsSet = false;
                }
                else
                {
                    Password = xnc["admin_pass"].Value;
                    PassIsSet = true;
                }

                Theme = (xnc["theme"].Value != "") ? xnc["theme"].Value : "default";
                MulitQueue = (xnc["multi_queue"].Value == "False") ? false : true;

                PassToRemove = (xnc["removal_type"].Value == "True") ? true : false;

                xnl = xmlDoc.GetElementsByTagName("Team");
                foreach( XmlNode teamNode in xnl )
                {
                    xnc = teamNode.Attributes;
                    Team xTeam = Team.FromXML(Convert.ToInt32(xnc["id"].Value),
                                           xnc["name"].Value,
                                           xnc["p1"].Value,
                                           xnc["p2"].Value,
                                           Convert.ToInt32(xnc["w"].Value),
                                           Convert.ToInt32(xnc["l"].Value),
                                           Convert.ToInt32(xnc["streak"].Value));
                    teams.Add(xTeam);
                }
            }
            catch( Exception )
            {
                PassIsSet = false;
                Password = "";
                MulitQueue = false;
                PassToRemove = true;
            }
        }

        /// <summary> 
        /// Writes all of the game data to the given save file
        /// </summary>
        public void saveData() {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement category;
            XmlAttribute attrib;

            //XML Doc Type
            XmlNode node = xmlDoc.CreateNode( XmlNodeType.XmlDeclaration, "", "" );
            xmlDoc.AppendChild( node );

            //Add Root Node
            category = xmlDoc.CreateElement( "", "BPMS", "" );
            xmlDoc.AppendChild( category );

            //Create's Basic Info
            category = xmlDoc.CreateElement( "system" );

            attrib = xmlDoc.CreateAttribute( "save_date" );
            attrib.Value = DateTime.Now.ToString();
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "theme" );
            attrib.Value = Theme;
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "admin_pass" );
            attrib.Value = (PassIsSet) ? Password : "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "multi_queue" );
            attrib.Value = MulitQueue + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "removal_type" );
            attrib.Value = PassToRemove + "";
            category.SetAttributeNode( attrib );

            xmlDoc.ChildNodes.Item( 1 ).AppendChild( category );
            xmlDoc.DocumentElement.InsertAfter( category,
                                   xmlDoc.DocumentElement.LastChild );

            foreach( Team curTeam in teams )
            {

                if( curTeam.TeamName == "Contra" && curTeam.Player1 == "Bill"
                    && curTeam.Player2 == "Lance" )
                {

                }
                else
                {


                    category = xmlDoc.CreateElement("Team");

                    attrib = xmlDoc.CreateAttribute("id");
                    attrib.Value = curTeam.Id + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("name");
                    attrib.Value = curTeam.TeamName + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("p1");
                    attrib.Value = curTeam.Player1 + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("p2");
                    attrib.Value = curTeam.Player2 + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("w");
                    attrib.Value = curTeam.Wins + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("l");
                    attrib.Value = curTeam.Losses + "";
                    category.SetAttributeNode(attrib);

                    attrib = xmlDoc.CreateAttribute("streak");
                    attrib.Value = curTeam.MaxStreak + "";
                    category.SetAttributeNode(attrib);

                    xmlDoc.DocumentElement.InsertAfter(category, xmlDoc.DocumentElement.LastChild);
                }
            }

            ////////////////////////////////////////////////////
            // Insert winners, challengers and queue into XML //
            //////////////////////////////////////////////////// 
                    
            #region TODO
            /* XmlElement oldCategory;// = category;
            oldCategory = xmlDoc.CreateElement( "", "WINNERS", "" );
            xmlDoc.DocumentElement.InsertAfter( oldCategory, xmlDoc.DocumentElement.LastChild );

            category = xmlDoc.CreateElement( "Team" );
            attrib = xmlDoc.CreateAttribute( "id" );
            attrib.Value = winners.Id + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "name" );
            attrib.Value = winners.teamName + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "p1" );
            attrib.Value = winners.player1 + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "p2" );
            attrib.Value = winners.player2 + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "streak" );
            attrib.Value = winners.longestStreak + "";
            category.SetAttributeNode( attrib );

            oldCategory = xmlDoc.CreateElement( "", "CHALLENGERS", "" );
            xmlDoc.DocumentElement.InsertAfter( oldCategory, xmlDoc.DocumentElement.LastChild );

            category = xmlDoc.CreateElement( "Team" );
            attrib = xmlDoc.CreateAttribute( "id" );
            attrib.Value = challengers.Id + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "name" );
            attrib.Value = challengers.teamName + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "p1" );
            attrib.Value = challengers.player1 + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "p2" );
            attrib.Value = challengers.player2 + "";
            category.SetAttributeNode( attrib );

            attrib = xmlDoc.CreateAttribute( "streak" );
            attrib.Value = challengers.longestStreak + "";
            category.SetAttributeNode( attrib );

            

            //xmlDoc.ChildNodes.Item( 2 ).AppendChild( category );
            ////Save the queue
            //oldCategory.AppendChild( category );
            //xmlDoc.DocumentElement.InsertAfter( category, xmlDoc.DocumentElement.LastChild );

            //foreach (Team t in inQ) {
                
            //} */
            #endregion

            FileStream fsxml = new FileStream(SaveFile, FileMode.Truncate,
                                  FileAccess.Write,
                                  FileShare.ReadWrite );
            xmlDoc.Save( fsxml );
            fsxml.Close();
            RecentSave = true;
        }
    }
}
