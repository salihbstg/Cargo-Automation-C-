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
    public partial class frm_subeler : Form
    {
        public frm_subeler()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_yoneticipanel a = new frm_yoneticipanel();
            a.Show();
        }
        public void getir()
        {
            //datagride veri çektik 
            fonksiyonlar a = new fonksiyonlar();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_subeler",a.baglan());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }
        private void frm_subeler_Load(object sender, EventArgs e)
        {
            getir();
            MessageBox.Show("Datagridde çift tıkladığınız satırdaki verileri kutucuklara aktarabilirsiniz.");
        }
        public void temizle()
        {
            kasa.Text = "";
            sehir.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Şube ekleme
            if (sehir.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("insert into tbl_subeler (sehir,kasa) values (@p1,@p2)", a.baglan());
                kmt.Parameters.AddWithValue("@p1", sehir.Text);
                kmt.Parameters.AddWithValue("@p2", kasa.Text);
                kmt.ExecuteNonQuery();
                a.baglan().Close();
                //tabloyu yenileme
                getir();
                //araçları temizleme
                temizle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //silme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_subeler where sehir=@p1 and id=@p2", a.baglan());
            kmt.Parameters.AddWithValue("@p1", sehir.Text);
            kmt.Parameters.AddWithValue("@p2", id.Text);
            kmt.ExecuteNonQuery();
            //yenileme
            getir();
            //araçları temizleme
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = dataGridView1.SelectedCells[0].RowIndex;
            kasa.Text = dataGridView1.Rows[a].Cells[2].Value.ToString();
            sehir.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            //şubelerde güncelleme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("update tbl_subeler set sehir=@p1,kasa=@p2 where id=@p3", a.baglan());
            kmt.Parameters.AddWithValue("@p1", sehir.Text);
            kmt.Parameters.AddWithValue("@p2", kasa.Text);
            kmt.Parameters.AddWithValue("@p3", id.Text);
            kmt.ExecuteNonQuery();
            //datagridi yenileme
            getir();
            //araçları temizleme
            temizle();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox ile arama yapma
            if (textBox1.Text != "")
            {
                //textbox ile arama yapma
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_subeler where sehir like '" + textBox1.Text + "%'", a.baglan());
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
    }
}
