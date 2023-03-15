using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kargo_otomasyonu
{
    public partial class frm_yoneticipanel : Form
    {
        public frm_yoneticipanel()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_yoneticiler a = new frm_yoneticiler();
            a.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_subecalisanları a = new frm_subecalisanları();
            a.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm_subeler a = new frm_subeler();
            a.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm_araclar gstr = new frm_araclar();
            gstr.Show();
            this.Hide();
        }

        private void frm_yoneticipanel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_duyurular a = new frm_duyurular();
            a.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frm_giris a = new frm_giris();
            a.Show();
        }
    }
}
