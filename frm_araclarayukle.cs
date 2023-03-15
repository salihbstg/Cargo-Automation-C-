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
    public partial class frm_araclarayukle : Form
    {
        public frm_araclarayukle()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            frm_subecalisanipanel a = new frm_subecalisanipanel();
            a.Show();
        }
        public void getir()
        {
            
        }
     
        public string sehir;
        private void frm_araclarayukle_Load(object sender, EventArgs e)
        {
            
            //datagride bulunduğumuz şehirdeki kargolar çekildi
            textBox1.Text = sehir;
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where gonderensehir='"+textBox1.Text+"' ORDER BY alicisehir", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[9].Width = 120;
            dataGridView1.Columns[14].Width =80;
            dataGridView1.Columns[12].Width =125;
            dataGridView1.Columns[13].Width =65;
            dataGridView1.Columns[13].HeaderText = "(TL)";
            //comboboxa şubenin araçları çekildi
            SqlCommand kmt = new SqlCommand("select plaka from tbl_araclar where subesi=@p1", a.baglan());
            kmt.Parameters.AddWithValue("@p1", textBox1.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            a.baglan().Close();
            //şubeler comboboxlara çekildi
            SqlCommand komut = new SqlCommand("select sehir from tbl_subeler ORDER BY sehir", a.baglan());
            SqlDataReader dr2 = komut.ExecuteReader();
            while (dr2.Read())
            {
                comboBox2.Items.Add(dr2[0].ToString());
                comboBox3.Items.Add(dr2[0].ToString());
                comboBox4.Items.Add(dr2[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "")
            {
                //araçların varış noktaları güncellendi
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand komut = new SqlCommand("update tbl_araclar set varis=@p1,varis1=@p2,varis2=@p3 where plaka=@p4", a.baglan());
                komut.Parameters.AddWithValue("@p1", comboBox2.Text);
                komut.Parameters.AddWithValue("@p2", comboBox3.Text);
                komut.Parameters.AddWithValue("@p3", comboBox4.Text);
                komut.Parameters.AddWithValue("@p4", comboBox1.Text);
                komut.ExecuteNonQuery();
                a.baglan().Close();
                //kargolar uygun araçlara taşındı
                SqlCommand komut2 = new SqlCommand("update tbl_kargolar set aracplaka=@p1 where alicisehir=@p2 or alicisehir=@p3 or alicisehir=@p4", a.baglan());
                komut2.Parameters.AddWithValue("@p1", comboBox1.Text);
                komut2.Parameters.AddWithValue("@p2", comboBox2.Text);
                komut2.Parameters.AddWithValue("@p3", comboBox3.Text);
                komut2.Parameters.AddWithValue("@p4", comboBox4.Text);
                komut2.ExecuteNonQuery();
                a.baglan().Close();
                //datagride bulunduğumuz şehirdeki kargolar çekildi
                textBox1.Text = sehir;
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where gonderensehir='" + textBox1.Text + "' ORDER BY alicisehir", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                a.baglan().Close();
                
            }
            else
                MessageBox.Show("Boş yer bırakılamaz!");
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fonksiyonlar a = new fonksiyonlar();
            //seçilen aracın güzergahı temizlendi
            SqlCommand komut = new SqlCommand("update tbl_araclar set varis='',varis1='',varis2='' where plaka=@p1", a.baglan());
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            komut.ExecuteNonQuery();
            a.baglan().Close();
            //seçilen araçtaki kargolar araçtan indirildi
            SqlCommand kmt = new SqlCommand("update tbl_kargolar set aracplaka=@p2 where aracplaka=@p1", a.baglan());
            kmt.Parameters.AddWithValue("@p1", comboBox1.Text);
            kmt.Parameters.AddWithValue("@p2", "");
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            kmt.ExecuteNonQuery();
            a.baglan().Close();
            //datagride bulunduğumuz şehirdeki kargolar çekildi
            textBox1.Text = sehir;
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where gonderensehir='" + textBox1.Text + "' ORDER BY alicisehir", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }
    }
}
