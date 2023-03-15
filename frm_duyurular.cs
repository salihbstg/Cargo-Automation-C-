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
    public partial class frm_duyurular : Form
    {
        public frm_duyurular()
        {
            InitializeComponent();
        }
        public void getir()
        {
            fonksiyonlar a = new fonksiyonlar();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_duyurular",a.baglan());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            a.baglan().Close();
        }
     

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_yoneticipanel a = new frm_yoneticipanel();
            a.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Duyuru ekleme
            if (richTextBox1.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("insert into tbl_duyurular (duyuru) values (@p1)", a.baglan());
                kmt.Parameters.AddWithValue("@p1", richTextBox1.Text);
                kmt.ExecuteNonQuery();
                a.baglan().Close();
                //datagrid yenileme
                getir();
                //temizleme
                richTextBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Duyuru silme                
            if (id.Text != "")
            {
                fonksiyonlar a = new fonksiyonlar();
                SqlCommand kmt = new SqlCommand("delete from tbl_duyurular where id=@p1", a.baglan());
                kmt.Parameters.AddWithValue("@p1", id.Text);
                kmt.ExecuteNonQuery();
                a.baglan().Close();
                //datagrid yenileme
                getir();
                //temizleme
                richTextBox1.Text = "";
                id.Text = "";
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridden verileri araçlara aktarma
            int a = dataGridView1.SelectedCells[0].RowIndex;
            id.Text = dataGridView1.Rows[a].Cells[0].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[a].Cells[1].Value.ToString();
        }

        private void frm_duyurular_Load(object sender, EventArgs e)
        {
            getir();
            MessageBox.Show("Datagridde çift tıkladığınız satırdaki verileri kutucuklara aktarabilirsiniz.");
            dataGridView1.Columns[1].Width =250;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
