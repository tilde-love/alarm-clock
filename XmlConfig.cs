using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.IO;
using Rug.UI;
using Rug.UI.Util;
using Rug.UI.Skin;

namespace AlarmClock
{
    public enum ClenseProfile { Minimal, Standard, Aggressive } 

    public class AlarmConfig
    {
        #region Imutable properties 
        public string FilePath
        {
            get
            {
                return Application.UserAppDataPath + @"\times.xml";
            }
        }
        #endregion 

        #region Public Fields 

        /// <summary>
        /// Is show the confirm for the delete
        /// </summary>
        public bool ConfirmDelete;

        /// <summary>
        /// Is the alarm armed? 
        /// </summary>
        public bool Armed;

        /// <summary>
        /// Is the panel expanded? 
        /// </summary>
        public bool PanelExpanded;

        /// <summary>
        /// Is the clock shrunk? 
        /// </summary>
        public bool WindowShrunk;

        /// <summary>
        /// Always on top
        /// </summary>
        public bool AlwaysOnTop;

        /// <summary>
        /// The initial location of the alarm window 
        /// </summary>
        public Point Location;

        /// <summary>
        /// The initial state of the alram window
        /// </summary>
        public FormWindowState State;

        /// <summary>
        /// The initial position of the SkinEditor dialog 
        /// </summary>
        public Point SkinEditorDialogLocation;

        /// <summary>
        /// The initial size of the SkinEditor dialog 
        /// </summary>
        public Point SkinEditorDialogSize;

        /// <summary>
        /// The initial position of the color dialog 
        /// </summary>
        public Point ColourDialogLocation;
        
        /// <summary>
        /// The initial mode for the color dialog
        /// </summary>
        public ColorPickerDialog.ColorPickerDialogMode ColourDialogMode = ColorPickerDialog.ColorPickerDialogMode.Mix;

        /// <summary>
        /// Path to image package
        /// </summary>
        public string ImageOverridePath;

        /// <summary>
        /// All the times of the alarms
        /// </summary>
        public List<Time> Times;

        /// <summary>
        /// The path to the audio file path
        /// </summary>
        public string AudioFilePath;

        public ClenseProfile ClenseProfile = ClenseProfile.Standard; 

        #endregion 

        #region Load / Save 

        public void Load()
        {
            if (!File.Exists(FilePath))
                return;

            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(FilePath);

                XmlNode rootNode = doc.SelectSingleNode("root");

                if (rootNode.Attributes["file"] != null)
                    AudioFilePath = rootNode.Attributes["file"].Value;

                if (rootNode.Attributes["confirm-delete"] != null)
                    ConfirmDelete = bool.Parse(rootNode.Attributes["confirm-delete"].Value);                

                if (rootNode.Attributes["armed"] != null)
                    Armed = bool.Parse(rootNode.Attributes["armed"].Value);

                if (rootNode.Attributes["expanded"] != null)
                    PanelExpanded = bool.Parse(rootNode.Attributes["expanded"].Value);

                if (rootNode.Attributes["shrunk"] != null)
                    WindowShrunk = bool.Parse(rootNode.Attributes["shrunk"].Value);

                if (rootNode.Attributes["on-top"] != null)
                    AlwaysOnTop = bool.Parse(rootNode.Attributes["on-top"].Value);                

                if (rootNode.Attributes["location"] != null)
                    Location = Helper.DeserializePoint(rootNode.Attributes["location"].Value);

                if (rootNode.Attributes["state"] != null)
                    State = (FormWindowState)Enum.Parse(typeof(FormWindowState), rootNode.Attributes["state"].Value);

                if (rootNode.Attributes["clense-profile"] != null)
                    ClenseProfile = (ClenseProfile)Enum.Parse(typeof(ClenseProfile), rootNode.Attributes["clense-profile"].Value);

                if (rootNode.Attributes["colorpicker-location"] != null)
                    ColourDialogLocation = Helper.DeserializePoint(rootNode.Attributes["colorpicker-location"].Value);

                if (rootNode.Attributes["colorpicker-mode"] != null)
                    ColourDialogMode = (ColorPickerDialog.ColorPickerDialogMode)Enum.Parse(typeof(ColorPickerDialog.ColorPickerDialogMode), rootNode.Attributes["colorpicker-mode"].Value);

                if (rootNode.Attributes["skineditor-location"] != null)
                    SkinEditorDialogLocation = Helper.DeserializePoint(rootNode.Attributes["skineditor-location"].Value);

                if (rootNode.Attributes["skineditor-size"] != null)
                    SkinEditorDialogSize = Helper.DeserializePoint(rootNode.Attributes["skineditor-size"].Value);

                if (rootNode.Attributes["images-path"] != null)
                    ImageOverridePath = rootNode.Attributes["images-path"].Value;                        

                XmlNode coloursNode = Helper.FindChild(rootNode, "colours");
                SkinHelper.LoadColoursFromNode(coloursNode);

                XmlNode mappingsNode = Helper.FindChild(rootNode, "mappings");
                SkinHelper.LoadMappingsFromNode(mappingsNode);                

                

                Times = new List<Time>();

                foreach (XmlNode node in rootNode.SelectNodes("alarm"))
                {
                    // Parse the time from the string 
                    Time time = Time.Parse(Helper.GetAttributeValue(node, "time", "00:00 / EveryDay"));
                    time.Enabled = bool.Parse(Helper.GetAttributeValue(node, "enabled", "true")); 

                    // add it to the output 
                    Times.Add(time);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading config file");
            }
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootNode = doc.CreateElement("root");

            Helper.AppendAttributeAndValue(rootNode, "file", this.AudioFilePath);
            Helper.AppendAttributeAndValue(rootNode, "confirm-delete", this.ConfirmDelete.ToString());

            Helper.AppendAttributeAndValue(rootNode, "armed", this.Armed.ToString());
            Helper.AppendAttributeAndValue(rootNode, "expanded", this.PanelExpanded.ToString());
            Helper.AppendAttributeAndValue(rootNode, "shrunk", this.WindowShrunk.ToString());
            Helper.AppendAttributeAndValue(rootNode, "on-top", this.AlwaysOnTop.ToString());            

            Helper.AppendAttributeAndValue(rootNode, "location", Helper.SerializePoint(this.Location));
            Helper.AppendAttributeAndValue(rootNode, "state", this.State.ToString());

            Helper.AppendAttributeAndValue(rootNode, "clense-profile", this.ClenseProfile.ToString());            

            Helper.AppendAttributeAndValue(rootNode, "colorpicker-location", Helper.SerializePoint(this.ColourDialogLocation));
            Helper.AppendAttributeAndValue(rootNode, "colorpicker-mode", this.ColourDialogMode.ToString());

            Helper.AppendAttributeAndValue(rootNode, "skineditor-location", Helper.SerializePoint(this.SkinEditorDialogLocation));
            Helper.AppendAttributeAndValue(rootNode, "skineditor-size", Helper.SerializePoint(this.SkinEditorDialogSize));

            Helper.AppendAttributeAndValue(rootNode, "images-path", this.ImageOverridePath);            

            XmlElement coloursNode = Helper.AppendChild(rootNode, "colours");            
            SkinHelper.SaveColoursToNode(coloursNode);

            XmlElement mappingsNode = Helper.AppendChild(rootNode, "mappings");
            SkinHelper.SaveMappingsToNode(mappingsNode);

            foreach (Time time in Times)
            {
                // create node 
                XmlElement node = Helper.AppendChild(rootNode, "alarm");

                Helper.AppendAttributeAndValue(node, "time", time.ToString());
                Helper.AppendAttributeAndValue(node, "enabled", time.Enabled.ToString());
            }

            // add the root to the doc 
            doc.AppendChild(rootNode);

            //doc.Save(@"C:\xmlfile.xml");
            doc.Save(FilePath);
        }
        /*
        private void LoadColoursFromNode(XmlNode coloursNode)
        {

            if (coloursNode != null)
            {
                
                if (coloursNode.Attributes["window-top"] != null)
                    WindowTop = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "window-top", "#cccccc"));
                if (coloursNode.Attributes["window-top-text"] != null)
                    WindowTopText = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "window-top-text", "#000000"));

                if (coloursNode.Attributes["minmize-hover"] != null)
                    MinmizeHover = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "minmize-hover", "#ffffff"));
                if (coloursNode.Attributes["minmize-fore"] != null)
                    MinmizeFore = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "minmize-fore", "#000000"));
                if (coloursNode.Attributes["minmize-back"] != null)
                    MinmizeBack = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "minmize-back", "#cccccc"));

                if (coloursNode.Attributes["menu-text"] != null)
                    MenuText = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "menu-text", "#ffffff"));
                if (coloursNode.Attributes["menu-back"] != null)
                    MenuBack = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "menu-back", "#333333"));
                if (coloursNode.Attributes["menu-separator"] != null)
                    MenuSeparator = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "menu-separator", "#000000"));

                if (coloursNode.Attributes["panel-back"] != null)
                    PanelBackColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "panel-back", "#cccccc"));
                if (coloursNode.Attributes["panel-text"] != null)
                    PanelTextColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "panel-text", "#000000"));
                if (coloursNode.Attributes["panel-border"] != null)
                    PanelBorderColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "panel-border", "#000000"));


                if (coloursNode.Attributes["alarms-back"] != null)
                    AlarmsBackColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "alarms-back", "#ffffff"));
                if (coloursNode.Attributes["alarms-text"] != null)
                    AlarmsTextColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "alarms-text", "#000000"));
                if (coloursNode.Attributes["alarms-border"] != null)
                    AlarmsBorderColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "alarms-border", "#000000"));

                if (coloursNode.Attributes["alarms-selected-back"] != null)
                    AlarmsSelectedColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "alarms-selected-back", "#000000"));
                if (coloursNode.Attributes["alarms-selected-text"] != null)
                    AlarmsSelectedTextColour = Helper.DeserializeColor(Helper.GetAttributeValue(coloursNode, "alarms-selected-text", "#ffffff"));

                XmlNode regularNode = Helper.FindChild(coloursNode, "regular");

                if (regularNode != null)
                    this.Regular.Load(regularNode);

                XmlNode flashingNode = Helper.FindChild(coloursNode, "flashing");

                if (flashingNode != null)
                    this.Flashing.Load(flashingNode);
            }
        }

        private void SaveColoursToNode(XmlElement coloursNode)
        {
            Helper.AppendAttributeAndValue(coloursNode, "window-top", Helper.SerializeColor(WindowTop));
            Helper.AppendAttributeAndValue(coloursNode, "window-top-text", Helper.SerializeColor(WindowTopText));

            Helper.AppendAttributeAndValue(coloursNode, "minmize-hover", Helper.SerializeColor(MinmizeHover));
            Helper.AppendAttributeAndValue(coloursNode, "minmize-fore", Helper.SerializeColor(MinmizeFore));
            Helper.AppendAttributeAndValue(coloursNode, "minmize-back", Helper.SerializeColor(MinmizeBack));

            Helper.AppendAttributeAndValue(coloursNode, "menu-text", Helper.SerializeColor(MenuText));
            Helper.AppendAttributeAndValue(coloursNode, "menu-back", Helper.SerializeColor(MenuBack));
            Helper.AppendAttributeAndValue(coloursNode, "menu-separator", Helper.SerializeColor(MenuSeparator));

            Helper.AppendAttributeAndValue(coloursNode, "panel-back", Helper.SerializeColor(PanelBackColour));
            Helper.AppendAttributeAndValue(coloursNode, "panel-text", Helper.SerializeColor(PanelTextColour));
            Helper.AppendAttributeAndValue(coloursNode, "panel-border", Helper.SerializeColor(PanelBorderColour));

            Helper.AppendAttributeAndValue(coloursNode, "alarms-back", Helper.SerializeColor(AlarmsBackColour));
            Helper.AppendAttributeAndValue(coloursNode, "alarms-text", Helper.SerializeColor(AlarmsTextColour));
            Helper.AppendAttributeAndValue(coloursNode, "alarms-border", Helper.SerializeColor(AlarmsBorderColour));
            Helper.AppendAttributeAndValue(coloursNode, "alarms-selected-text", Helper.SerializeColor(AlarmsSelectedTextColour));
            Helper.AppendAttributeAndValue(coloursNode, "alarms-selected-back", Helper.SerializeColor(AlarmsSelectedColour));

            this.Regular.Save(Helper.AppendChild(coloursNode, "regular"));
            this.Flashing.Save(Helper.AppendChild(coloursNode, "flashing"));
        }
        */ 
        public void LoadColoursFromFile(string path)
        {
            if (!File.Exists(path))
                return;

            try
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(path);

                XmlNode coloursNode = doc.SelectSingleNode("colours");

                SkinHelper.LoadColoursFromNode(coloursNode);
                //LoadColoursFromNode(coloursNode);

            }
            catch (Exception)
            {
                MessageBox.Show("Error loading config file");
            }            
        }

        public void SaveColoursToFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement coloursNode = doc.CreateElement("colours");

            //SaveColoursToNode(coloursNode);
            SkinHelper.SaveColoursToNode(coloursNode);
            doc.AppendChild(coloursNode);

            doc.Save(path); 
        }

        #endregion 

        private AlarmConfig()
        {

        }

        private static AlarmConfig m_Config; 
        public static AlarmConfig Config
        {
            get
            {
                if (m_Config == null)
                    m_Config = new AlarmConfig();
                
                return m_Config; 
            }
        }
    }

    public class AlarmColours
    {
        #region Public Fields
        
        /// <summary>
        /// The lit digit segment colour 
        /// </summary>
        public Color DigitColour = Color.FromArgb(255, Color.FromArgb(0x00ff00)); 

        /// <summary>
        /// The dimmed digit segment colour 
        /// </summary>
        public Color DimDigitColour = Color.FromArgb(255, Color.FromArgb(0x006000)); 

        /// <summary>
        /// The back colour 
        /// </summary>
        public Color BackColour = Color.FromArgb(255, Color.FromArgb(0x001000));

        internal Bitmap Icon = null; 

        #endregion

        #region Load / Save
        /*
        public void Load(XmlNode node)
        {
            if (node.Attributes["digit"] != null)
                DigitColour = Helper.DeserializeColor(Helper.GetAttributeValue(node, "digit", "#00ff00"));
            if (node.Attributes["dim"] != null)
                DimDigitColour = Helper.DeserializeColor(Helper.GetAttributeValue(node, "dim", "#006000"));
            if (node.Attributes["back"] != null)
                BackColour = Helper.DeserializeColor(Helper.GetAttributeValue(node, "back", "#001000"));
        }

        public void Save(XmlElement element)
        {
            Helper.AppendAttributeAndValue(element, "digit", Helper.SerializeColor(DigitColour));
            Helper.AppendAttributeAndValue(element, "dim", Helper.SerializeColor(DimDigitColour));
            Helper.AppendAttributeAndValue(element, "back", Helper.SerializeColor(BackColour));
        }
        */
        #endregion 

    }    
}

/*
 * 

        public List<Time> GetXml(out string filePath, out bool armed, out bool expanded, ref Point location, ref FormWindowState state)
        {
            filePath = null;
            armed = true;
            expanded = true;

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(FilePath);
            }
            catch (Exception)
            {
                MessageBox.Show("No file!");
                filePath = null;

                return new List<Time>();
            }

            XmlNode rootNode = doc.SelectSingleNode("root");
            List<Time> times = new List<Time>();

            if (rootNode.Attributes["file"] != null)
                filePath = rootNode.Attributes["file"].Value;

            if (rootNode.Attributes["armed"] != null)
                armed = bool.Parse(rootNode.Attributes["armed"].Value);

            if (rootNode.Attributes["expanded"] != null)
                expanded = bool.Parse(rootNode.Attributes["expanded"].Value);

            if (rootNode.Attributes["location"] != null)
                location = DeserializePoint(rootNode.Attributes["location"].Value);

            if (rootNode.Attributes["state"] != null)
                state = (FormWindowState)Enum.Parse(typeof(FormWindowState), rootNode.Attributes["state"].Value);


            foreach (XmlNode node in rootNode.SelectNodes("alarm"))
            {
                // Parse the time from the string 
                Time time = Time.Parse(node.Attributes["time"].Value);

                // add it to the output 
                times.Add(time);
            }

            return times;
        }
 * 
 *  public void SetXml(List<Time> times, string filePath, bool armed, bool expanded, Point location, FormWindowState state)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("root");

            rootNode.Attributes.Append(doc.CreateAttribute("file"));
            rootNode.Attributes["file"].Value = filePath;

            rootNode.Attributes.Append(doc.CreateAttribute("armed"));
            rootNode.Attributes["armed"].Value = armed.ToString();

            rootNode.Attributes.Append(doc.CreateAttribute("expanded"));
            rootNode.Attributes["expanded"].Value = expanded.ToString();

            rootNode.Attributes.Append(doc.CreateAttribute("location"));
            rootNode.Attributes["location"].Value = SerializePoint(location);

            rootNode.Attributes.Append(doc.CreateAttribute("state"));
            rootNode.Attributes["state"].Value = state.ToString();


            foreach (Time time in times)
            {
                // create node 
                XmlNode node = doc.CreateElement("alarm");

                node.Attributes.Append(doc.CreateAttribute("time"));
                node.Attributes["time"].Value = time.ToString();

                // add it to the root node
                rootNode.AppendChild(node);
            }

            // add the root to the doc 
            doc.AppendChild(rootNode);

            //doc.Save(@"C:\xmlfile.xml");
            doc.Save(FilePath);
        }
 * 
 * 
<root file="[path]" > 
    <thing name="thing 1" time="10:40" /> 
    <thing name="thing 2" time="10:30" /> 
    <thing name="thing 3" time="10:50" /> 
    <thing name="thing 4" time="10:42" /> 
</root>

*/
