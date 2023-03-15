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
    public partial class frm_kargoduzenle : Form
    {
        public frm_kargoduzenle()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_subecalisanipanel a = new frm_subecalisanipanel();
            a.Show();
        }

        private void frm_kargoduzenle_Load(object sender, EventArgs e)
        {
            getir();
            dataGridView1.ForeColor = Color.Black;
            //comboboxa şehirleri getirme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("select sehir from tbl_subeler", a.baglan());
            SqlDataReader dr = kmt.ExecuteReader();
            while(dr.Read())
            {
                alici_sehir.Items.Add(dr[0]);
                gonderen_sehir.Items.Add(dr[0]);
            }
            
        }
        public void getir()
        {
            //datagride kargolari getirme
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void temizle_araclar()
        {
            gonderen_ad.Text = "";
            gonderen_soyad.Text = "";
            gonderen_adres.Text = "";
            gonderen_tc.Text = "";
            gonderen_telefon.Text = "";
            gonderen_sehir.Text = "";
            alici_ad.Text = "";
            alici_soyad.Text = "";
            alici_telefon.Text = "";
            alici_tc.Text = "";
            alici_sehir.Text = "";
            alici_adres.Text = "";
            boy.Text = "";
            en.Text = "";
            yukseklik.Text = "";
            fiyat.Text = "";
        }
        public void temizle_hesapla()
        {
            en.Text = "";
            boy.Text = "";
            yukseklik.Text = "";
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Fiyat hesaplaması
            if (en.Text != "" && boy.Text != "" && yukseklik.Text != "")
            {
                fiyat.Text = (Convert.ToInt32(en.Text) * Convert.ToInt32(boy.Text) * Convert.ToInt32(yukseklik.Text) / 5000).ToString();
                temizle_hesapla();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //arama yapma
            if (textBox1.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where id like '" + textBox1.Text + "%' or gonderenad like '" + textBox1.Text + "%'", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
                getir();
                       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //datagrid güncelleme
            if (gonderen_ad.Text != "" && gonderen_adres.Text != "" && gonderen_sehir.Text != "" && gonderen_soyad.Text != "" && gonderen_tc.Text != "" && gonderen_telefon.Text != "" && alici_ad.Text != "" && alici_adres.Text != "" && alici_sehir.Text != "" && alici_telefon.Text != "" && fiyat.Text != "")
            {

                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("update tbl_kargolar set gonderenad=@p1,gonderensoyad=@p2,gonderentelefon=@p3,gonderentc=@p4,gonderensehir=@p5,gonderenadres=@p6,aliciad=@p7,alicisoyad=@p8,alicitelefon=@p9,alicitc=@p10,alicisehir=@p11,aliciadres=@p12,gonderifiyat=@p13 where id=@p14", a.baglan());
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
                kmt.Parameters.AddWithValue("@p14", id.Text);
                kmt.ExecuteNonQuery();
                temizle_araclar();
                getir();
                MessageBox.Show("Kargo Güncellendi!");
            }
            else
                MessageBox.Show("Eksik bilgi girdiniz..Fiyat hesaplatmayı unutmayınız!");
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //datagridde tıklananları hücrelere aktardık
            int a = dataGridView1.SelectedCells[0].RowIndex;
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
            gonderen_ad.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
            gonderen_soyad.Text = dataGridView1.Rows[a].Cells[2].Value.ToString();
            gonderen_telefon.Text = dataGridView1.Rows[a].Cells[3].Value.ToString();
            gonderen_tc.Text = dataGridView1.Rows[a].Cells[4].Value.ToString();
            gonderen_sehir.Text = dataGridView1.Rows[a].Cells[5].Value.ToString();
            gonderen_adres.Text = dataGridView1.Rows[a].Cells[6].Value.ToString();
            alici_ad.Text = dataGridView1.Rows[a].Cells[7].Value.ToString();
            alici_soyad.Text = dataGridView1.Rows[a].Cells[8].Value.ToString();
            alici_telefon.Text = dataGridView1.Rows[a].Cells[9].Value.ToString();
            alici_tc.Text = dataGridView1.Rows[a].Cells[10].Value.ToString();
            alici_sehir.Text = dataGridView1.Rows[a].Cells[11].Value.ToString();
            alici_adres.Text = dataGridView1.Rows[a].Cells[12].Value.ToString();
            fiyat.Text = dataGridView1.Rows[a].Cells[13].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Kargo Silme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_kargolar where id=@p1",a.baglan());
            kmt.Parameters.AddWithValue("@p1", id.Text);
            kmt.ExecuteNonQuery();
            getir();
            temizle_araclar();
            a.baglan().Close();
            alici_sehir.SelectedItem = null;
            gonderen_sehir.SelectedItem = null;
        }
    }
}
