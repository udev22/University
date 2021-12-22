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
    public partial class Universitetlar : Form
    {
        public Universitetlar()
        {
            InitializeComponent();

            ShowUniversitet();
        }
        
        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        
        private void UnSaqlashBtn_Click(object sender, EventArgs e)
        {
            if(UnNomTxB.Text == "" || UnShaharTxB.Text == "" || UnAsosiyTxB.Text == "")
            {
                MessageBox.Show("Malumotlar toliq toldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into UniversitetTb1(UnNom,UnShahar,UnData,UnAsosiy)values(@UN,@USh,@UY,@UA)",Con);
                    cmd.Parameters.AddWithValue(@"@UN", UnNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@USh", UnShaharTxB.Text);
                    cmd.Parameters.AddWithValue(@"@UY", UnYilDTV.Value.Date);
                    cmd.Parameters.AddWithValue(@"@UA", UnAsosiyTxB.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Universitet tablitsasiga malumot qoshildi!!!");
                    Con.Close();

                    ShowUniversitet();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void UnTahrirBtn_Click(object sender, EventArgs e)
        {
            if (UnNomTxB.Text == "" || UnShaharTxB.Text == "" || UnAsosiyTxB.Text == "")
            {
                MessageBox.Show("Malumotlar toliq toldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update UniversitetTb1 Set UnNom=@UN,UnShahar=@USh,UnData=@UY,UnAsosiy=@UA where UnRaqam=@UKey",Con);
                    cmd.Parameters.AddWithValue(@"@UN", UnNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@USh", UnShaharTxB.Text);
                    cmd.Parameters.AddWithValue(@"@UY", UnYilDTV.Value.Date);
                    cmd.Parameters.AddWithValue(@"@UA", UnAsosiyTxB.Text);
                    cmd.Parameters.AddWithValue(@"@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Universitet tablitsasiga malumot tahrirlandi!!!");
                    Con.Close();

                    ShowUniversitet();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void UnOchirishBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Malumotlar toliq toldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from UniversitetTb1 where UnRaqam=@UKey", Con);
                    cmd.Parameters.AddWithValue(@"@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Universitet tablitsasiga malumot o'chirilmoqda!!!");
                    Con.Close();

                    ShowUniversitet();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void ShowUniversitet()
        {
            Con.Open();
            string Query = "select * from UniversitetTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var dt = new DataSet();
            sda.Fill(dt);
            UnDGV.DataSource = dt.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            UnNomTxB.Text = "";
            UnShaharTxB.Text = "";
            UnAsosiyTxB.Text = "";
        }

        int Key = 0;
        private void UnDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnNomTxB.Text = UnDGV.SelectedRows[0].Cells[1].Value.ToString();
            UnShaharTxB.Text = UnDGV.SelectedRows[0].Cells[2].Value.ToString();
            UnYilDTV.Text = UnDGV.SelectedRows[0].Cells[3].Value.ToString();
            UnAsosiyTxB.Text = UnDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(UnNomTxB.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = int.Parse(UnDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void MenyuLB_Click(object sender, EventArgs e)
        {
            Menyu menyu = new Menyu();
            this.Hide();
            menyu.Show();
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
        private void DavomatLB_Click(object sender, EventArgs e)
        {
            Davomat davomat = new Davomat();
            this.Hide();
            davomat.Show();
        }
        private void TolovlarLB_Click(object sender, EventArgs e)
        {
            Tolovlar tolovlar = new Tolovlar();
            this.Hide();
            tolovlar.Show();
        }
    }
}
