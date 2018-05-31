using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgeModBuilder
{
    public partial class WebBrowserForm : Form
    {
        public static WebBrowserForm ShowWebBrowser(string website)
        {
            WebBrowserForm form = new WebBrowserForm();
            form.WebBrowser.Navigate(website);
            form.Text = website;
            form.Width = Screen.PrimaryScreen.WorkingArea.Width / 2 + 100;
            form.Height = Screen.PrimaryScreen.WorkingArea.Height / 2 + 100;

            try
            {
                Uri url = new Uri(website);
                WebRequest request = WebRequest.Create("http://" + url.Host + "/favicon.ico");
                Stream response = null;
                try
                {
                    response = request.GetResponse().GetResponseStream();
                }
                catch {}
                finally
                {
                    if(response != null)
                    {
                        Bitmap icon = new Bitmap(32, 32);
                        MemoryStream memStream = new MemoryStream();
                        byte[] buffer = new byte[1024];
                        int byteCount;
                        do
                        {
                            byteCount = response.Read(buffer, 0, buffer.Length);
                            memStream.Write(buffer, 0, byteCount);
                        } while (byteCount > 0);

                        icon = new Bitmap(Image.FromStream(memStream));

                        if (icon != null)
                        {
                            Icon ic = Icon.FromHandle(icon.GetHicon());
                            form.Icon = ic;
                        }
                    }
                }
            }
            catch {}
            
            form.Show();
            return form;
        }

        public WebBrowserForm()
        {
            InitializeComponent();
        }
    }
}
