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
    public partial class frm_araclar : Form
    {
        public frm_araclar()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_yoneticipanel a = new frm_yoneticipanel();
            a.Show();
        }
        public void temizle()
        {
            konum.SelectedItem=null;
            plaka.Text = "";
            
        }
        public void getir()
        {
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select id,plaka,subesi from tbl_araclar", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
        private void frm_araclar_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Datagridde çift tıkladığınız satırdaki verileri kutucuklara aktarabilirsiniz.");
            //datagride veriler geldi
            getir();
            //comboboxa şubeler alındı
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("select sehir from tbl_subeler", a.baglan());
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                konum.Items.Add(dr[0]);
             
            }
            //Hücre başlıklarına isim verildi
            dataGridView1.Columns[1].HeaderText = "Plaka";
            dataGridView1.Columns[2].HeaderText = "Şube";

            //hücre genişlikleri
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width =100;
            dataGridView1.Columns[2].Width = 125;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //araç ekleme
            if (plaka.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("insert into tbl_araclar (plaka,subesi) values (@p1,@p2)", a.baglan());
                kmt.Parameters.AddWithValue("@p1", plaka.Text.ToUpper());
                kmt.Parameters.AddWithValue("@p2", konum.Text);

                kmt.ExecuteNonQuery();
                //datagrid yenileme
                getir();
                //araçları temizleme
                temizle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //araç silme
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_araclar where plaka=@p1", a.baglan());
            kmt.Parameters.AddWithValue("@p1", plaka.Text);
            kmt.ExecuteNonQuery();
            //datagrid yenileme
            getir();
            //araçları temizleme
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //çift tıklanan satırı araçlara yazma
            int a = dataGridView1.SelectedCells[0].RowIndex;
            plaka.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
            konum.Text = dataGridView1.Rows[a].Cells[2].Value.ToString();          
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox ile arama yapma
            if (textBox1.Text != "")
            {
                //textbox ile arama yapma
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_araclar where subesi like '"+textBox1.Text+"%'", a.baglan());           
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                a.baglan().Close();
            }
            else
            {
                getir();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           
            //araç güncellemeleri
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("update tbl_araclar set plaka=@p1,subesi=@p2 where id=@p6", a.baglan());
            kmt.Parameters.AddWithValue("@p1", plaka.Text.ToUpper());
            kmt.Parameters.AddWithValue("@p2", konum.Text);
            kmt.Parameters.AddWithValue("@p6", id.Text);
            kmt.ExecuteNonQuery();
            a.baglan().Close();
            getir();
            temizle();
           
        }
    }
}
