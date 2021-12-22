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
    public partial class Davomat : Form
    {
        public Davomat()
        {
            InitializeComponent();

            GetYonID();
            GetTaID();
            ShowDavomat();
        }

        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");

        private void GetTaID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TaRaqam from TalabaTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TaRaqam", typeof(int));
            dt.Load(Rdr);
            DaTaIDCB.ValueMember = "TaRaqam";
            DaTaIDCB.DataSource = dt;
            Con.Close();
        }
        private void GetTaMalumot()
        {
            Con.Open();
            string Query = "Select * from TalabaTb1 where TaRaqam=" + DaTaIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DaTaIsmTxB.Text = dr["TaIsm"].ToString();
                DaTaJinsTxb.Text = dr["TaJins"].ToString();
                DaTaSemestrTxb.Text = dr["TaSemastr"].ToString();
            }
            Con.Close();
        }
        private void DaTaIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTaMalumot();
        }
        private void GetYonID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select YonRaqam from YonalishTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("YonRaqam", typeof(int));
            dt.Load(Rdr);
            DaTaIDCB.ValueMember = "YonRaqam";
            DaTaIDCB.DataSource = dt;
            Con.Close();
        }
        private void GetYonMalumot()
        {
            Con.Open();
            string Query = "Select * from YonalishTb1 where YonRaqam=" + DaTaYonalishIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DaTaYonalishNomTxB.Text = dr["YonNom"].ToString();
            }
            Con.Close();
        }
        private void DaTaYonalishIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetYonMalumot();
        }

        private void DaSaqlashBtn_Click(object sender, EventArgs e)
        {
            if (DaTaIDCB.SelectedIndex == -1 || DaTaIsmTxB.Text == "" || DaFanCB.SelectedIndex == -1 || DaGuruhCB.SelectedIndex == -1 || DaTaJinsTxb.Text == "" || DavomatCB.SelectedIndex == -1 || DaTaSemestrTxb.Text == "" || DaTaYonalishIDCB.SelectedIndex == -1 || DaTaYonalishNomTxB.Text == "")
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into DavomatTb1(DaTaID,DaTaIsm,DaSana,DaFanNom,DaGuruh,DaTaJins,Davomat,DaTaSemestr,DaYonID,DaYonNom)values(@DTID,@DTI,@DTS,@DFN,@DG,@DTJ,@D,@DTSe,@DYID,@DYN)", Con);
                    cmd.Parameters.AddWithValue(@"@DTID", DaTaIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@DTI", DaTaIsmTxB.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@DTS", DaTaSanaDTP.Value.Date);
                    cmd.Parameters.AddWithValue(@"@DFN", DaFanCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DG", DaGuruhCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DTJ", DaTaJinsTxb.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@D", DavomatCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DTSe", DaTaSemestrTxb.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@DYID", DaTaYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@DYN", DaTaYonalishNomTxB.Text.ToString());
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Davomat tablitsasiga malumot qo'shilmoqda!!!");
                    Con.Close();

                    ShowDavomat();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DaTahrirBtn_Click(object sender, EventArgs e)
        {
            if (DaTaIDCB.SelectedIndex == -1 || DaTaIsmTxB.Text == "" || DaFanCB.SelectedIndex == -1 || DaGuruhCB.SelectedIndex == -1 || DaTaJinsTxb.Text == "" || DavomatCB.SelectedIndex == -1 || DaTaSemestrTxb.Text == "" || DaTaYonalishIDCB.SelectedIndex == -1 || DaTaYonalishNomTxB.Text == "")
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update DavomatTb1 Set DaTaID=@DTID,DaTaIsm=@DTI,DaSana=@DTS,DaFanNom=@DFN,DaGuruh=@DG,DaTaJins=@DTJ,Davomat=@D,DaTaSemestr=@DTSe,DaYonID=@DYID,DaYonNom=@DYN where DaRaqam=@DKey)", Con);
                    cmd.Parameters.AddWithValue(@"@DTID", DaTaIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@DTI", DaTaIsmTxB.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@DTS", DaTaSanaDTP.Value.Date);
                    cmd.Parameters.AddWithValue(@"@DFN", DaFanCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DG", DaGuruhCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DTJ", DaTaJinsTxb.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@D", DavomatCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@DTSe", DaTaSemestrTxb.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@DYID", DaTaYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@DYN", DaTaYonalishNomTxB.Text.ToString());
                    cmd.Parameters.AddWithValue(@"@DKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Davomat tablitsasiga malumot qo'shilmoqda!!!");
                    Con.Close();

                    ShowDavomat();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DaOchirishBtn_Click(object sender, EventArgs e)
        {
            // shart berdim agar (Yonalish nomi,Qabul qilish,Tolovlar)-> textBox ga qiymat kiritilmasa
            if (Key == 0)
            {
                // 3ta textBoxga malumot kiritilmasa (Malumotlar toldirilmadi) degan habarnoma chiqadi.
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DavomatTb1 where DaRaqam=@DKey", Con);
                    cmd.Parameters.AddWithValue(@"DKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Davomat tablitsasida malumot O'chirilmoqda!!!");
                    Con.Close();

                    ShowDavomat();
                    Reset();
                }
                // agar bazaga textBoxdan notog'ri tipli yoki boshqa hato sezilsa exception tashlaydi
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShowDavomat()
        {
            Con.Open();
            string Query = "select * from DavomatTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            DaDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            DaTaIDCB.SelectedIndex = -1;
            DaTaIsmTxB.Text = "";
            DaFanCB.SelectedIndex = -1;
            DaGuruhCB.SelectedIndex = -1;
            DaTaJinsTxb.Text = "";
            DavomatCB.SelectedIndex = -1;
            DaTaSemestrTxb.Text = "";
            DaTaYonalishIDCB.SelectedIndex = -1;
            DaTaYonalishNomTxB.Text = "";
        }

        int Key = 0;
        private void DaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DaTaIDCB.SelectedValue = DaDGV.SelectedRows[0].Cells[1].Value.ToString();
            DaTaIsmTxB.Text = DaDGV.SelectedRows[0].Cells[2].Value.ToString();
            DaTaSanaDTP.Text = DaDGV.SelectedRows[0].Cells[3].Value.ToString();
            DaFanCB.SelectedValue = DaDGV.SelectedRows[0].Cells[4].Value.ToString();
            DaGuruhCB.SelectedValue = DaDGV.SelectedRows[0].Cells[5].Value.ToString();
            DaTaJinsTxb.Text = DaDGV.SelectedRows[0].Cells[6].Value.ToString();
            DavomatCB.SelectedValue = DaDGV.SelectedRows[0].Cells[7].Value.ToString();
            DaTaSemestrTxb.Text = DaDGV.SelectedRows[0].Cells[8].Value.ToString();
            DaTaYonalishIDCB.SelectedValue = DaDGV.SelectedRows[0].Cells[9].Value.ToString();
            DaTaYonalishNomTxB.Text = DaDGV.SelectedRows[0].Cells[10].Value.ToString();
            if (DaTaIsmTxB.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = int.Parse(DaDGV.SelectedRows[0].Cells[0].Value.ToString());
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
