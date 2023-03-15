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
    public partial class frm_subecalisanları : Form
    {
        public frm_subecalisanları()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_yoneticipanel a = new frm_yoneticipanel();
            a.Show();
        }
        public void getir() //datagridi yenilemek için fonksiyon
        {
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_subecalisani", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }
        public void temizle ()
        {
            ad.Text = "";
            soyad.Text = "";
            tc.Text = "";
            telefon.Text = "";
            sifre.Text = "";
            kadi.Text = "";
            id.Text = "";
            sehir.Text = "";
        }
        private void frm_subecalisanları_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Datagridde çift tıkladığınız satırdaki verileri kutucuklara aktarabilirsiniz.");
            //comboboxa şehirler çekilti
            fonksiyonlar a = new fonksiyonlar();
           SqlCommand kmt=new SqlCommand("select sehir from tbl_subeler",a.baglan());
           SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                sehir.Items.Add(dr[0]);                
            }
            a.baglan().Close();
            //datagride veri çekildi
           getir();            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridde tıklanan satırı araçlara taşıdık
            int a = dataGridView1.SelectedCells[0].RowIndex;
            ad.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
            soyad.Text = dataGridView1.Rows[a].Cells[2].Value.ToString();
            tc.Text = dataGridView1.Rows[a].Cells[3].Value.ToString();
            telefon.Text = dataGridView1.Rows[a].Cells[4].Value.ToString();
            kadi.Text = dataGridView1.Rows[a].Cells[5].Value.ToString();
            sifre.Text = dataGridView1.Rows[a].Cells[6].Value.ToString();
            sehir.Text = dataGridView1.Rows[a].Cells[7].Value.ToString();
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Personel ekleme
            if (ad.Text != "" && soyad.Text != "" && sifre.Text != "" && kadi.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand komut = new SqlCommand("insert into tbl_subecalisani (ad,soyad,tc,telefon,kadi,sifre,sehir) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", a.baglan());
                komut.Parameters.AddWithValue("@p1", ad.Text);
                komut.Parameters.AddWithValue("@p2", soyad.Text);
                komut.Parameters.AddWithValue("@p3", tc.Text);
                komut.Parameters.AddWithValue("@p4", telefon.Text);
                komut.Parameters.AddWithValue("@p5", kadi.Text);
                komut.Parameters.AddWithValue("@p6", a.sifrele(sifre.Text).ToString());
                komut.Parameters.AddWithValue("@p7", sehir.Text.ToString());
                komut.ExecuteNonQuery();
                //tabloyu yeniledik
                getir();
                //Araçları temizledik
                temizle();
            }
            else
                MessageBox.Show("Ad,Soyad,Kullanıcı Adı,Şifre girilmesi zorunludur");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //personel silme
            fonksiyonlar b = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_subecalisani where id=@p1 and ad=@p2", b.baglan());
            kmt.Parameters.AddWithValue("@p1", id.Text);
            kmt.Parameters.AddWithValue("@p2", ad.Text);
            kmt.ExecuteNonQuery();
            b.baglan().Close();
            //tablo yenilendi
            getir();
            //Araçlar temizlendi
            temizle();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Personel Güncelleme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("update tbl_subecalisani set ad=@p1,soyad=@p2,tc=@p3,telefon=@p4,kadi=@p5,sehir=@p7 where id=@p8", a.baglan());
            kmt.Parameters.AddWithValue("@p1", ad.Text);
            kmt.Parameters.AddWithValue("@p2", soyad.Text);
            kmt.Parameters.AddWithValue("@p3", tc.Text);
            kmt.Parameters.AddWithValue("@p4", telefon.Text);
            kmt.Parameters.AddWithValue("@p5", kadi.Text);          
            kmt.Parameters.AddWithValue("@p7", sehir.Text);
            kmt.Parameters.AddWithValue("@p8", id.Text);
            kmt.ExecuteNonQuery();
            
            //araçlar temizlendi
            temizle();

            //tablo yenilendi
            getir();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox ile arama yapma
            if (textBox1.Text != "")
            {
                //textbox ile arama yapma
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_subecalisani where ad like '"+textBox1.Text+"%' or soyad like '"+textBox1.Text+"%'", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                getir();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
