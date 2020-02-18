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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-G0R49GJ\\SQLEXPRESS01;Initial Catalog=hierbase;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            bool kontrolSonuc = YasakliKelimeKontrol(textBox1.Text + " " + textBox2.Text);

            if (kontrolSonuc)
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Giris where KullaniciAdi =@KullanıcıAdi and Sifre =@Sifre", baglanti);
                cmd.Parameters.Add("@KullanıcıAdi", textBox1.Text);
                cmd.Parameters.Add("@Sifre", textBox2.Text);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    AnaEkran yeni = new AnaEkran();
                    this.Hide();
                    yeni.Show();
                }
                else
                {
                    MessageBox.Show("Hatali Giris");
                }


            }
            else
            {
                MessageBox.Show("Kullanıcı adınızda veya şifrenizde yasaklı kelime tespit edildi.");
            }

        }
        public bool YasakliKelimeKontrol(string kullaniciVerileri)
        {
            string[] strList = kullaniciVerileri.Split(' ');
            string[] yasakliKelimeler = { "where", "select", "from", "delete", "drop", "alter table", "table",
                                             "insert into", "update", "set", "join", "script", "body", "alert","insert","or" };

            for (int i = 0; i < strList.Length; i++)
            {
                for (int j = 0; j < yasakliKelimeler.Length; j++)
                {
                    if (strList[i] == yasakliKelimeler[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
