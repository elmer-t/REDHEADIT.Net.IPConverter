using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REDHEADIT.Net.IPConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtIPAddress.GotFocus += txtIPAddress_GotFocus;
            txtLong.GotFocus += txtLong_GotFocus;

            txtIPAddress.LostFocus -= txtIPAddress_GotFocus;
            txtLong.LostFocus -= txtLong_GotFocus;
        }

        private void txtIPAddress_TextChanged(object sender, EventArgs e) {
            IPAddress ip;
            if (IPAddress.TryParse(txtIPAddress.Text, out ip)) {
                txtLong.Text = IP2Long(ip).ToString();
                txtIPAddress.BackColor = Color.White;
            } else {
                txtIPAddress.BackColor = Color.Yellow;
            }
        }

        private void txtLong_TextChanged(object sender, EventArgs e) {
            long value;
            if (long.TryParse(txtLong.Text, out value)) {
                txtIPAddress.Text = Long2IP(value).ToString();
                txtLong.BackColor = Color.White;
            } else {
                txtLong.BackColor = Color.Yellow;
            }
        }

        void txtLong_GotFocus(object sender, EventArgs e)
        {
            txtIPAddress.TextChanged -= txtIPAddress_TextChanged;
            txtLong.TextChanged += txtLong_TextChanged;
        }

        void txtIPAddress_GotFocus(object sender, EventArgs e) {
            txtLong.TextChanged -= txtLong_TextChanged;            
            txtIPAddress.TextChanged += txtIPAddress_TextChanged;
        }

        // ReSharper disable once InconsistentNaming
        public static long IP2Long(IPAddress ip)
        {
            var b = ip.GetAddressBytes();
            Int64 result = 0;
            var len = b.Length - 1;

            for (var i = 0; i <= len-1; i++)
            {
                result += Convert.ToInt64(b[i] * Math.Pow(256, len - i));
            }
            result += b[len];
            return result;
        }

        // ReSharper disable once InconsistentNaming
        public static IPAddress Long2IP(long value)
        {
            var ipString = string.Empty;
            IPAddress result;

            for (var i = 3; i >= 0; i += -1)
            {
                var part = Convert.ToInt64(Math.Floor(value/Math.Pow(256, i)));
                ipString += part + ".";
                value = value - Convert.ToInt64(part * Math.Pow(256, i));
            }

            ipString = ipString.TrimEnd('.');
            return IPAddress.TryParse(ipString, out result) ? result : IPAddress.Parse("0.0.0.0");
        }

        
    }
}