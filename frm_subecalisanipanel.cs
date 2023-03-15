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
    public partial class frm_subecalisanipanel : Form
    {
        public frm_subecalisanipanel()
        {
            InitializeComponent();
        }
        public void temizle_alici()
        {
            alici_ad.Text = "";
            alici_adres.Text = "";
            alici_sehir.SelectedItem = null;
            alici_soyad.Text = "";
            alici_tc.Text = "";
            alici_telefon.Text = "";
        }
        public void temizle_gonderen()
        {
            gonderen_ad.Text = "";
            gonderen_adres.Text = "";
            gonderen_sehir.SelectedItem = null;
            gonderen_soyad.Text = "";
            gonderen_tc.Text = "";
            gonderen_telefon.Text = "";
        }
        public void temizle_hesapla()
        {
            yukseklik.Text = "";
            boy.Text = "";
            en.Text = "";
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (en.Text != "" && boy.Text != "" && yukseklik.Text != "")
            {
                fiyat.Text = (Convert.ToInt32(en.Text) * Convert.ToInt32(boy.Text) * Convert.ToInt32(yukseklik.Text) / 5000).ToString();
                temizle_hesapla();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        if (fiyat.Text != "" && gonderen_ad.Text!="" && gonderen_soyad.Text !="" && gonderen_adres.Text !="" && alici_ad.Text !="" && alici_soyad.Text !=""  && alici_sehir.Text != "" && gonderen_sehir.Text != "" && gonderen_telefon.Text != "" && alici_adres.Text != "" && alici_telefon.Text != "")
            {

                //kargo ekleme
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("insert into tbl_kargolar (gonderenad,gonderensoyad,gonderentelefon,gonderentc,gonderensehir,gonderenadres,aliciad,alicisoyad,alicitelefon,alicitc,alicisehir,aliciadres,gonderifiyat) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13)", a.baglan());
                kmt.Parameters.AddWithValue("@p1", gonderen_ad.Text);
                kmt.Parameters.AddWithValue("@p2", gonderen_soyad.Text);
                kmt.Parameters.AddWithValue("@p3", gonderen_telefon.Text);
                kmt.Parameters.AddWithValue("@p4", gonderen_tc.Text);
                kmt.Parameters.AddWithValue("@p5", gonderen_sehir.Text);
                kmt.Parameters.AddWithValue("@p6", gonderen_adres.Text);
                kmt.Parameters.AddWithValue("@p7", alici_ad.Text);
                kmt.Parameters.AddWithValue("@p8", alici_soyad.Text);
                kmt.Parameters.AddWithValue("@p9", alici_telefon.Text);
                kmt.Parameters.AddWithValue("@p10", alici_tc.Text);
                kmt.Parameters.AddWithValue("@p11", alici_sehir.Text);
                kmt.Parameters.AddWithValue("@p12", alici_adres.Text);
                kmt.Parameters.AddWithValue("@p13", fiyat.Text);
                kmt.ExecuteNonQuery();
                temizle_hesapla();
                temizle_gonderen();
                temizle_alici();
                fiyat.Text = "";
                MessageBox.Show("Kargo teslim alındı.");
            }
            else
                MessageBox.Show("Fiyat hesaplamayı ve bilgilerin tamamını girmeyi unutmayın.Eksik birşeyler var..");
        }
        public string tasisube;
        private void frm_subecalisanipanel_Load(object sender, EventArgs e)
        {
            label12.Text = tasisube;
            //comboboxlara sehirleri taşıma
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("select sehir from tbl_subeler ORDER BY sehir", a.baglan());
            SqlDataReader dr = kmt.ExecuteReader();
            while(dr.Read())
            {
                alici_sehir.Items.Add(dr[0].ToString());
                gonderen_sehir.Items.Add(dr[0].ToString());
            }
            a.baglan().Close();
           
            //duyuruları listboxa taşıma
            SqlCommand kmt3 = new SqlCommand("select * from tbl_duyurular", a.baglan());
            SqlDataReader dr3 = kmt3.ExecuteReader();
            while (dr3.Read())
            {
                duyurular.Text +="→"+ dr3[1].ToString()+"\n-\n";
            }
            a.baglan().Close();
        }

        private void alici_sehir_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frm_giris a = new frm_giris();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_kargoduzenle a = new frm_kargoduzenle();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_araclarayukle a = new frm_araclarayukle();
            a.sehir = label12.Text;
            a.Show();
        }
    }
}
