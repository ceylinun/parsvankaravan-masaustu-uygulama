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

namespace Karavan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-M2Q7PAPM\\SQLEXPRESS01;Initial Catalog=karavanDb;Integrated Security=True");

        void temizle()
        {
            textId.Text = "";
            textAd.Text = "";
            textSoyad.Text = "";
            maskedTextBox1.Text = "";
            comboBoxŞehir.Text = "";
            comboBoxİlçe.Text = "";
            comboBoxModel.Text = "";
            textKişi.Text = "";
            dateTimePicker1.Value= DateTime.Now;    
            dateTimePicker2.Value= DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textFiyat.Text = "";
            textAd.Focus();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Start();

        }

        private void buttonListele_Click(object sender, EventArgs e)
        {
            // this.tbl_KaravanTableAdapter.Fill(this.karavanDbDataSet.Tbl_Karavan);
           
            string getir = "select * from Tbl_Karavan";
            SqlCommand komut = new SqlCommand(getir, baglanti);
            SqlDataAdapter ad=new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
          
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Karavan (Ad,Soyad,Telefon,Sehir,Ilçe,ModelSeçimi,KişiSayısı,BaşlangıçTarihi,BitişTarihi,Fiyat,Indirim) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", baglanti);
            komut.Parameters.AddWithValue("@p1", textAd.Text);
            komut.Parameters.AddWithValue("@p2", textSoyad.Text);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p4", comboBoxŞehir.Text);
            komut.Parameters.AddWithValue("@p5",comboBoxİlçe.Text);
            komut.Parameters.AddWithValue("@p6", comboBoxModel.Text);
            komut.Parameters.AddWithValue("@p7", textKişi.Text);
            komut.Parameters.AddWithValue("p8",dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@p9",dateTimePicker2.Value);
            komut.Parameters.AddWithValue("p10", textFiyat.Text);
            komut.Parameters.AddWithValue("@p11", label13.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi","Bilgilendirme Mesajı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void comboBoxŞehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxŞehir.Text == "aydın")
            {
                comboBoxİlçe.Items.Clear();
                comboBoxİlçe.Items.Add("kuşadası");
                comboBoxİlçe.Items.Add("güzelçamlı");

            }
            else if (comboBoxŞehir.Text == "izmir")
            {
                comboBoxİlçe.Items.Clear();
                comboBoxİlçe.Items.Add("urla");
                comboBoxİlçe.Items.Add("foça");
            }
            else if (comboBoxŞehir.Text == "muğla")
            {
                comboBoxİlçe.Items.Clear();
                comboBoxİlçe.Items.Add("bodrum");
                comboBoxİlçe.Items.Add("marmaris");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) 
            {
                label13.Text = "True";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label13.Text = "False";
            }

        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); 
            textAd.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textSoyad.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            maskedTextBox1.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            comboBoxŞehir.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            comboBoxİlçe.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            comboBoxModel.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            textKişi.Text= dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            dateTimePicker1.Text= dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            dateTimePicker2.Text= dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            label13.Text= dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            textFiyat.Text= dataGridView1.Rows[secilen].Cells[11].Value.ToString();



        }

        private void label13_TextChanged(object sender, EventArgs e)
        {
            if (label13.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label13.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Karavan where Id=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1", textId.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt silindi.");

        }

        private void buttonGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("update Tbl_Karavan set Ad=@a1,Soyad=@a2,Telefon=@a3,Sehir=@a4,Ilçe=@a5,ModelSeçimi=@a6,KişiSayısı=@a7,BaşlangıçTarihi=@a8,BitişTarihi=@a9,Indirim=@a10,Fiyat=@a11 where Id=@a12", baglanti);
            guncelle.Parameters.AddWithValue("@a1", textAd.Text);
            guncelle.Parameters.AddWithValue("@a2", textSoyad.Text);
            guncelle.Parameters.AddWithValue("@a3", maskedTextBox1.Text);
            guncelle.Parameters.AddWithValue("@a4", comboBoxŞehir.Text);
            guncelle.Parameters.AddWithValue("@a5", comboBoxİlçe.Text);
            guncelle.Parameters.AddWithValue("@a6", comboBoxModel.Text);
            guncelle.Parameters.AddWithValue("@a7", textKişi.Text);
            guncelle.Parameters.AddWithValue("@a8", dateTimePicker1.Value);
            guncelle.Parameters.AddWithValue("@a9", dateTimePicker2.Value);
            guncelle.Parameters.AddWithValue("@a10", label13.Text);
            guncelle.Parameters.AddWithValue("@a11", textFiyat.Text);
            guncelle.Parameters.AddWithValue("@a12", textId.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt güncellendi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string ara = "select * from Tbl_Karavan where Ad=@c1";
            SqlCommand aranan=new SqlCommand(ara,baglanti);
            aranan.Parameters.AddWithValue("@c1", textBox1.Text);
            SqlDataAdapter da=new SqlDataAdapter(aranan);
            DataTable dt =new DataTable();
            da.Fill(dt);    
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();
        }
        int sayac = 0;
        int sayac1 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            label15.Text= sayac.ToString();
            if (sayac == 60)
            {
                sayac1++;
                label16.Text=sayac1.ToString();
                sayac = 0;
                label15.Text=sayac.ToString();
            }
        }
    }
}
