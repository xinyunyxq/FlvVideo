using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using http;
//Download by http://www.codefans.net
using System.Runtime.InteropServices;
namespace Playflv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string xmlPath = "";
        string [] flv=new string[3];
        FileInfo fi;
        string strg;
        AxShockwaveFlashObjects.AxShockwaveFlash ax;
        httpquest httpquest1 = new httpquest();

        private void Form1_Load(object sender, EventArgs e)
        {
            AddFlash();
            this.Height = 307;
            strg = Application.StartupPath.ToString();
            strg = strg.Substring(0, strg.LastIndexOf("\\"));
            strg = strg.Substring(0, strg.LastIndexOf("\\"));
            strg += @"\FLVPlayer";
            strg += @"\FLVplayer.swf";
            ax.Movie = strg;
            string xml;
            xml = http.httpquest.GetHtml("http://api.flvxz.com/url/aHR0cDojI3d3dy50dWRvdS5jb20vYWxidW1wbGF5LzlKd2pjX3lRdmdFLmh0bWw=");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNodeList nodeList = xmlDoc.SelectNodes("root/video/files/file"); //取得节点名为row的XmlNode集合
            int i = 0;
            foreach (XmlNode xxNode in nodeList)
            {
                i++;
                XmlNodeList filesList = xxNode.ChildNodes; //取得row下的子节点集合
                foreach (XmlNode xxNode1 in filesList)
                {
                    
                    switch(xxNode1.Name)
                    {
                        case "furl":
                            string url = xxNode1.InnerText;
                            flv[1] = url;
                            break;
                        case "ftype":
                            string type = xxNode1.InnerText;
                            flv[0] = type + i;
                            break;
                        case "size":
                            string size = xxNode1.InnerText;
                            flv[2] = size;
                            break;
                        case "seconds":
                            string second = xxNode1.InnerText;
                            break;
                        case "time":
                            string time = xxNode1.InnerText;
                            ListViewItem lvi = new ListViewItem(flv);
                            listView1.Items.Add(lvi);
//                            http.httpquest.DownloadFile(flv[1], "d:\\flvvideo.flv", null);
                            break;
                    }
                    

                }
            }
        }

        private void AddFlash()
        {
            ax = new AxShockwaveFlashObjects.AxShockwaveFlash();
            panel1.Controls.Add(ax);
            ax.Dock = DockStyle.Fill;
            ax.ScaleMode = 1;
        }

        private void ChangeFlv(string path)
        {
            xmlPath = Application.StartupPath.ToString();
            xmlPath = xmlPath.Substring(0, xmlPath.LastIndexOf("\\"));
            xmlPath = xmlPath.Substring(0, xmlPath.LastIndexOf("\\"));
            xmlPath += @"\FLVPlayer";
            xmlPath += @"\list.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNode nodePath = doc.SelectSingleNode("flvLists/item");
            XmlElement xe = (XmlElement)nodePath;
            //设置元素的属性
            xe.SetAttribute("title", path);
            doc.Save(xmlPath);
        }

        private void playFLV(string path)
        {
            FileInfo fi2 = new FileInfo(path);
            if (fi2.Exists)
            {
                Directory.CreateDirectory("c:\\flvVidio");
                string newPath="c:\\flvVidio\\"+DateTime.Now.Year+DateTime.Now.Second+".flv";
                File.Copy(path, newPath);
                ChangeFlv(newPath);
                this.Text = listView1.SelectedItems[0].SubItems[0].Text;
                ax.Dispose();
                AddFlash();
                ax.Movie = strg;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string path = listView1.SelectedItems[0].SubItems[1].Text;
                    playFLV(path);
                }
            }
            catch { }
        }

        private void 清空列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                string[] files = openFileDialog1.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    string flvPath = files[i];
                    string flvName = flvPath.Substring(flvPath.LastIndexOf("\\") + 1, flvPath.Length - flvPath.LastIndexOf("\\") - 1);
                    fi = new FileInfo(flvPath);
                    flv[0] = flvName;
                    flv[1] = flvPath;
                    flv[2] = Convert.ToString(fi.Length / 1024) + "KB";
                    ListViewItem lvi = new ListViewItem(flv);
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                ChangeFlv("");
                ax.Dispose();
                Scripting.FileSystemObject fso = new Scripting.FileSystemObject();
                fso.DeleteFolder("c:\\flvVidio", true);
            }
            catch{}
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (this.Height <= 307)
            {
                this.Height = 448;
                panel2.BackgroundImage = (Image)Properties.Resources.up;
            }
            else
            {
                this.Height = 307;
                panel2.BackgroundImage = (Image)Properties.Resources.down;
            }
        }

        private void 删除选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < files.Length; i++)
                {
                    string flvPath = files[i];
                    string flvName = flvPath.Substring(flvPath.LastIndexOf("\\") + 1, flvPath.Length - flvPath.LastIndexOf("\\") - 1);
                    string fileType = flvName.Substring(flvName.LastIndexOf(".") + 1, flvName.Length - 1 - flvName.LastIndexOf("."));
                    if (fileType.ToLower() == "flv")
                    {
                        fi = new FileInfo(flvPath);
                        flv[0] = flvName;
                        flv[1] = flvPath;
                        flv[2] = Convert.ToString(fi.Length / 1024) + "KB";
                        ListViewItem lvi = new ListViewItem(flv);
                        listView1.Items.Add(lvi);
                    }
                }
            }
        }
    }
}
