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
    public partial class frm_yoneticiler : Form
    {
        public frm_yoneticiler()
        {
            InitializeComponent();
        }

        private void frm_yoneticiler_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Datagridde çift tıkladığınız satırdaki verileri kutucuklara aktarabilirsiniz.");
            //Form yüklenirken datagride yöneticileri aktardık
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridde tıklanan satırı araçlara taşıdık
            int a;
            a = dataGridView1.SelectedCells[0].RowIndex;
            ad.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
            soyad.Text = dataGridView1.Rows[a].Cells[2].Value.ToString();
            tc.Text = dataGridView1.Rows[a].Cells[3].Value.ToString();
            telefon.Text = dataGridView1.Rows[a].Cells[4].Value.ToString();
            kadi.Text = dataGridView1.Rows[a].Cells[5].Value.ToString();
            sifre.Text = dataGridView1.Rows[a].Cells[6].Value.ToString();
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Yönetici ekleme
            if (kadi.Text != "" && sifre.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("insert into tbl_yoneticiler (ad,soyad,tc,telefon,kadi,sifre) values (@p1,@p2,@p3,@p4,@p5,@p6)", a.baglan());
                kmt.Parameters.AddWithValue("@p1", ad.Text);
                kmt.Parameters.AddWithValue("@p2", soyad.Text);
                kmt.Parameters.AddWithValue("@p3", tc.Text);
                kmt.Parameters.AddWithValue("@p4", telefon.Text);
                kmt.Parameters.AddWithValue("@p5", kadi.Text);
                kmt.Parameters.AddWithValue("@p6", a.sifrele(sifre.Text).ToString());
                kmt.ExecuteNonQuery();
                a.baglan().Close();
                //Araçları temizledik
                ad.Text = "";
                soyad.Text = "";
                tc.Text = "";
                telefon.Text = "";
                sifre.Text = "";
                kadi.Text = "";
                id.Text = "";
                //Tabloyu yeniledik            
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                a.baglan().Close();
            }
            else
                MessageBox.Show("Kullanıcı adı ve şifre belirlemediniz");
        }

        private void ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Yönetici silme işlemi
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_yoneticiler where id=@p1 and ad=@p2", a.baglan());
            kmt.Parameters.AddWithValue("@p1", id.Text);
            kmt.Parameters.AddWithValue("@p2", ad.Text);
            kmt.ExecuteNonQuery();
            a.baglan().Close();
            //Araçları temizledik
            ad.Text = "";
            soyad.Text = "";
            tc.Text = "";
            telefon.Text = "";
            sifre.Text = "";
            kadi.Text = "";
            id.Text = "";
            //Tabloyu yeniledik            
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Geri butonu(Yöneticiler)
            this.Hide();
            frm_yoneticipanel a = new frm_yoneticipanel();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Yöneticileri güncelledik
            fonksiyonlar nesne = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("update tbl_yoneticiler set ad=@p1,soyad=@p2,tc=@p3,telefon=@p4,kadi=@p5 where id=@p7", nesne.baglan());
            kmt.Parameters.AddWithValue("@p1", ad.Text);
            kmt.Parameters.AddWithValue("@p2", soyad.Text);
            kmt.Parameters.AddWithValue("@p3", tc.Text);
            kmt.Parameters.AddWithValue("@p4", telefon.Text);
            kmt.Parameters.AddWithValue("@p5", kadi.Text);
            kmt.Parameters.AddWithValue("@p7", id.Text);
            kmt.ExecuteNonQuery();
            nesne.baglan().Close();
            //Araçları temizledik
            ad.Text = "";
            soyad.Text = "";
            tc.Text = "";
            telefon.Text = "";
            sifre.Text = "";
            kadi.Text = "";
            id.Text = "";
            //Tabloyu yeniledik            
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler", nesne.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            nesne.baglan().Close();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox ile arama yapma
            if (textBox1.Text != "")
            {
                //textbox ile arama yapma
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler where ad like '" + textBox1.Text + "%' or soyad like '"+textBox1.Text+"%'", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_yoneticiler", a.baglan());
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                a.baglan().Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            ad.Text = "";
            soyad.Text = "";
            tc.Text = "";
            telefon.Text = "";
            sifre.Text = "";
            kadi.Text = "";
        }
    }
}
