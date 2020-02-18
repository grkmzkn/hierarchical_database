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
namespace hier
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-G0R49GJ\\SQLEXPRESS01;Initial Catalog=hierbase;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("dbo.Listeleme", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, "dbo.Personel");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT DepartmanAd,dbo.departmanOrtalamaMaas(DepartmanId) as 'Ortalama Maas' FROM dbo.Departman";
            SqlCommand cmd = new SqlCommand(query, baglanti);
            cmd.CommandType = CommandType.Text;
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string query = textBox1.Text;
            SqlCommand cmd = new SqlCommand(query, baglanti);
            cmd.CommandType = CommandType.Text;
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "Select MusteriAd,MusteriSoyad,SiparisFiyat From dbo.Siparis join dbo.Musteri on Siparis.MusteriID = Musteri.MusteriID Where SiparisFiyat > (Select avg(SiparisFiyat) From dbo.Siparis)";
            SqlCommand cmd = new SqlCommand(query, baglanti);
            cmd.CommandType = CommandType.Text;
            baglanti.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
