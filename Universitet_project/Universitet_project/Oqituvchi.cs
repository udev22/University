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
    public partial class Oqituvchi : Form
    {
        public Oqituvchi()
        {
            InitializeComponent();

            ShowOqtuvchi();
            GetYonID();
        }

        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        
        private void GetYonID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select YonRaqam from YonalishTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("YonRaqam", typeof(int));
            dt.Load(Rdr);
            OqYonalishIDCB.ValueMember = "YonRaqam";
            OqYonalishIDCB.DataSource = dt;
            Con.Close();
        }
        private void GetYonNom()
        {
            Con.Open();
            string Query = "Select * from YonalishTb1 where YonRaqam=" + OqYonalishIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                OqYonalishNomTxB.Text = dr["YonNom"].ToString();
            }
            Con.Close();
        }
        private void OqYonalishIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetYonNom();
        }

        private void OqSaqlashBtn_Click(object sender, EventArgs e)
        {
            if (OqIsmTxB.Text == "" || OqJinsCB.SelectedIndex == -1 || OqManzilTxB.Text == "" || OqToyifaCB.SelectedIndex == -1 || OqYonalishNomTxB.Text == "" || OqMaoshTxB.Text == "" || OqTajribaCB.SelectedIndex == -1)
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into OqituvchiTb1(OqIsm,OqDOB,OqJins,OqManzil,OqToyifa,OqTajriba,OqYonID,OqYonNom,OqMaosh)values(@OI,@ODBO,@OJ,@OMa,@OTo,@OTa,@OYID,@OYN,@OMo)", Con);
                    cmd.Parameters.AddWithValue(@"@OI", OqIsmTxB.Text);
                    cmd.Parameters.AddWithValue(@"@ODBO", OqYiliDTP.Value.Date);
                    cmd.Parameters.AddWithValue(@"@OJ", OqJinsCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OMa", OqManzilTxB.Text);
                    cmd.Parameters.AddWithValue(@"@OTo", OqToyifaCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OTa", OqTajribaCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OYID", OqYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@OYN", OqYonalishNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@OMo", OqMaoshTxB.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yonalish tablitsasiga malumot qo'shilmoqda!!!");
                    Con.Close();

                    ShowOqtuvchi();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void OqTahrirBtn_Click(object sender, EventArgs e)
        {
            if (OqIsmTxB.Text == "" || OqJinsCB.SelectedIndex == -1 || OqManzilTxB.Text == "" || OqToyifaCB.SelectedIndex == -1 || OqYonalishNomTxB.Text == "" || OqMaoshTxB.Text == "" || OqTajribaCB.SelectedIndex == -1)
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update OqituvchiTb1 Set OqIsm=@OI,OqDOB=@ODBO,OqJins=@OJ,OqManzil=@OMa,OqToyifa=@OTo,OqTajriba=@OTa,OqYonID=@OYID,OqYonNom=@OYN,OqMaosh=@OMo where OqRaqam=@OKey", Con);
                    cmd.Parameters.AddWithValue(@"@OI", OqIsmTxB.Text);
                    cmd.Parameters.AddWithValue(@"@ODBO", OqYiliDTP.Value.Date);
                    cmd.Parameters.AddWithValue(@"@OJ", OqJinsCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OMa", OqManzilTxB.Text);
                    cmd.Parameters.AddWithValue(@"@OTo", OqToyifaCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OTa", OqTajribaCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@OYID", OqYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@OYN", OqYonalishNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@OMo", OqMaoshTxB.Text);
                    cmd.Parameters.AddWithValue(@"@OKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tahrirlanmoqda!!!");
                    Con.Close();

                    ShowOqtuvchi();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void OqOchirishBtn_Click(object sender, EventArgs e)
        {
            if (OqIsmTxB.Text == "" || OqJinsCB.SelectedIndex == -1 || OqManzilTxB.Text == "" || OqToyifaCB.SelectedIndex == -1 || OqYonalishNomTxB.Text == "" || OqMaoshTxB.Text == "" || OqTajribaCB.SelectedIndex == -1)
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from OqituvchiTb1 where OqRaqam=@OKey", Con);
                    cmd.Parameters.AddWithValue(@"@OKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ochirilmoqda!!!");
                    Con.Close();

                    ShowOqtuvchi();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShowOqtuvchi()
        {
            Con.Open();
            string Query = "select * from OqituvchiTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            OqDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            OqIsmTxB.Text = "";
            OqJinsCB.SelectedIndex = -1;
            OqManzilTxB.Text = "";
            OqToyifaCB.SelectedIndex = -1;
            OqYonalishIDCB.SelectedIndex = -1;
            OqYonalishNomTxB.Text = "";
            OqMaoshTxB.Text = "";
            OqTajribaCB.SelectedIndex = -1;
        }

        int Key = 0;
        private void OqDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OqIsmTxB.Text = OqDGV.SelectedRows[0].Cells[1].Value.ToString();
            OqYiliDTP.Text = OqDGV.SelectedRows[0].Cells[2].Value.ToString();
            OqJinsCB.SelectedItem = OqDGV.SelectedRows[0].Cells[3].Value.ToString();
            OqManzilTxB.Text = OqDGV.SelectedRows[0].Cells[4].Value.ToString();
            OqToyifaCB.SelectedItem = OqDGV.SelectedRows[0].Cells[5].Value.ToString();
            OqTajribaCB.SelectedItem = OqDGV.SelectedRows[0].Cells[6].Value.ToString();
            OqYonalishIDCB.SelectedValue = OqDGV.SelectedRows[0].Cells[7].Value.ToString();
            OqYonalishNomTxB.Text = OqDGV.SelectedRows[0].Cells[8].Value.ToString();
            OqMaoshTxB.Text = OqDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (OqIsmTxB.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = int.Parse(OqDGV.SelectedRows[0].Cells[0].Value.ToString());
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
        private void UniversitetlarLB_Click(object sender, EventArgs e)
        {
            Universitetlar universitetlar = new Universitetlar();
            this.Hide();
            universitetlar.Show();
        }
    }
}
