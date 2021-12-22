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
namespace Unversitet
{
    public partial class Menyu : Form
    {
        public Menyu()
        {
            InitializeComponent();

            MoliyaHisobi();
            MaoshlarHisobi();
            TalabalarSoni();
            YonalishlarSoni();
            UniversitetlarSoni();
            OqituvchilarSoni();
        }

        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        
        private void MoliyaHisobi()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(ToTaMiqdori) from TolovlarTb1",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeMoliyaLb.Text = "RS " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void MaoshlarHisobi()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(MaOqMiqdor) from MaoshTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeMaoshlarLb.Text = "RS " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void TalabalarSoni()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from TalabaTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeTaLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void YonalishlarSoni()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from YonalishTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeYonLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void UniversitetlarSoni()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from UniversitetTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeUnLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void OqituvchilarSoni()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from OqituvchiTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MeOqituvchiLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void TalabaLB_Click(object sender, EventArgs e)
        {
            Talaba talaba = new Talaba();
            this.Hide();
            talaba.Show();
        }
        private void YonalishLB_Click(object sender, EventArgs e)
        {
            Yonalish yonalish = new Yonalish();
            this.Hide();
            yonalish.Show();
        }
        private void OqituvchiLB_Click(object sender, EventArgs e)
        {
            Oqituvchi oqituvchi = new Oqituvchi();
            this.Hide();
            oqituvchi.Show();
        }
        private void KurslarLB_Click(object sender, EventArgs e)
        {
            Kurslar kurslar = new Kurslar();
            this.Hide();
            kurslar.Show();
        }
        private void TolovlarLB_Click(object sender, EventArgs e)
        {
            Tolovlar tolovlar = new Tolovlar();
            this.Hide();
            tolovlar.Show();
        }
        private void DavomatLB_Click(object sender, EventArgs e)
        {
            Davomat davomat = new Davomat();
            this.Hide();
            davomat.Show();
        }
        private void UniversitetlarLB_Click(object sender, EventArgs e)
        {
            Universitetlar universitetlar = new Universitetlar();
            this.Hide();
            universitetlar.Show();
        }
    }
}
