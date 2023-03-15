using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kargo_otomasyonu
{
    public partial class frm_giris : Form
    {
        public frm_giris()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frm_giris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fonksiyonlar baglanti = new fonksiyonlar();        
            SqlCommand kmt = new SqlCommand("select * from tbl_yoneticiler where kadi=@p1 and sifre=@p2", baglanti.baglan());
            kmt.Parameters.AddWithValue("@p1", yonetici_kadi.Text);
            kmt.Parameters.AddWithValue("@p2", baglanti.sifrele(yonetici_sifre.Text));         
            SqlDataReader dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Başarıyla Giriş Yaptınız..");
                frm_yoneticipanel a = new frm_yoneticipanel();
                a.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                yonetici_kadi.Text = "";
                yonetici_sifre.Text = "";
            }
            baglanti.baglan().Close();
        }
        frm_araclarayukle tasi = new frm_araclarayukle();
        private void button2_Click(object sender, EventArgs e)
        {

            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("select * from tbl_subecalisani where kadi=@p1 and sifre=@p2",a.baglan());
            kmt.Parameters.AddWithValue("@p1", sube_kadi.Text);
            kmt.Parameters.AddWithValue("@p2", a.sifrele(sube_sifre.Text).ToString());
            SqlDataReader dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                frm_subecalisanipanel c = new frm_subecalisanipanel();
                c.tasisube = dr[7].ToString();
                MessageBox.Show("Başarıyla giriş yaptınız..");                               
                this.Hide();               
                c.Show();
                
            }
            else
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frm_soforpaneli a = new frm_soforpaneli();
            a.Show();
        }
    }
}
