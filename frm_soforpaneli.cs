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
    public partial class frm_soforpaneli : Form
    {
        public frm_soforpaneli()
        {
            InitializeComponent();
        }

        private void frm_soforpaneli_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Üzerine çift tıkladığınız kargoyu teslim edebilirsiniz.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Araca yüklü kargolar datagride çekildi
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where aracplaka='" + textBox1.Text.ToUpper() + "' ORDER BY alicisehir", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[13].Width = 50;
            dataGridView1.Columns[13].HeaderText ="(TL)";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = dataGridView1.SelectedCells[0].RowIndex;
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[a].Cells[12].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            //kargolar teslim edildiğinde silindi
            fonksiyonlar a = new fonksiyonlar();
            SqlCommand kmt = new SqlCommand("delete from tbl_kargolar where id=@p1", a.baglan());
            kmt.Parameters.AddWithValue("@p1", id.Text);
            kmt.ExecuteNonQuery();
            a.baglan().Close();
            //Araca yüklü kargolar datagride çekildi
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_kargolar where aracplaka='" + textBox1.Text.ToUpper() + "'", a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
