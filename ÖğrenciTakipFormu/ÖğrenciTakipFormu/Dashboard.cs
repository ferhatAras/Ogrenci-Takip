using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ÖğrenciTakipFormu
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            string dosyaicerik = IcerikAl();
            string dosyaYolu = DosyaadiAl();
            File.WriteAllText("Öğrenciler/" + dosyaYolu, dosyaicerik);
            MessageBox.Show("Kaydedildi");

            listBox1.Items.Add(txt_ad.Text + "" + txt_soyad.Text);
        }

        private string DosyaadiAl()
        {
            string[] dosyalar = Directory.GetFiles(Directory.GetCurrentDirectory());
            if (dosyalar.Length == 0)
            {
                return "1.txt";
            }
            else
            {
                return (KactaKaldik(dosyalar) + 1) + ".txt";
            }
        }

        private int KactaKaldik(string[] dosyalar)
        {
            int enBuyukSayi = 0;
            foreach (var item in dosyalar)
            {
                string sonuncu = SonuncuyuAl(item); // Örneğin: "2.txt" veya "3.txt"
                sonuncu = sonuncu.Replace(".txt", ""); // Sayısal kısmı al: "2" veya "3"
                if (int.TryParse(sonuncu, out int sonSayi) && sonSayi > enBuyukSayi)
                {
                    enBuyukSayi = sonSayi;
                }
            }
            return enBuyukSayi;
        }

        private string SonuncuyuAl(string yol)
        {
            string[] parcalar = yol.Split('\\');
            return parcalar[parcalar.Length - 1];
        }

        private string IcerikAl()
        {
            string dosyacontent = txt_ad.Text;
            dosyacontent += Environment.NewLine;
            dosyacontent += txt_soyad.Text;
            dosyacontent += Environment.NewLine;
            dosyacontent += cbm_sınıf.Text;
            dosyacontent += Environment.NewLine;
            dosyacontent += DateTime.Today.ToShortDateString();
            return dosyacontent;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            string secilenAdSoyad = (string)listBox1.SelectedItem;
            string[] tumOgrenciDosyaları = Directory.GetFiles("Öğrenciler");
            foreach (var item in tumOgrenciDosyaları)
            {
                string[] satirlar = File.ReadAllLines(item);
                if (satirlar[0] + "" + satirlar[1] == secilenAdSoyad)
                {
                    lbl_ad.Text = satirlar[0];
                    lbl_soyad.Text = satirlar[1];
                    lbl_sınıf.Text = satirlar[2];
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems == null)
            {
                MessageBox.Show("Hata");
            }
            else
            {
                string secilenAdSoyad = (string)listBox1.SelectedItem;
                string[] tumOgrenciDosyaları = Directory.GetFiles("Öğrenciler");
                foreach (var item in tumOgrenciDosyaları)
                {
                    string[] satirlar = File.ReadAllLines(item);
                    if (satirlar[0] + "" + satirlar[1] == secilenAdSoyad)
                    {
                        listBox1.Items.Remove(secilenAdSoyad);  //Öğrenci Siler
                        File.Delete(item);                      // öğrencinin oldugu dosyayı ve txt i de siler
                        break;
                    }
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            lbl_ad.Text = "";
            lbl_soyad.Text = "";
            lbl_sınıf.Text = "";

            string[] tumOgrenciDosyaları = Directory.GetFiles("Öğrenciler");
            foreach (var item in tumOgrenciDosyaları)
            {
                string[] satirlar = File.ReadAllLines(item);
                string ad = satirlar[0];
                string soyad = satirlar[1];
                listBox1.Items.Add(ad + "" + soyad);
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            txt_ad.Clear();
            txt_soyad.Text = "";
            cbm_sınıf.Text = "";
        }
    }
}
