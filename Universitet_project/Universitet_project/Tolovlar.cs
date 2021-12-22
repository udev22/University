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
    public partial class Tolovlar : Form
    {        
        public Tolovlar()
        {
            InitializeComponent();

            ShowTolovlar();
            GetTaID();
            ShowMaoshlar();
            GetOqID();
        }

        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        
        private void GetTaID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select TaRaqam from TalabaTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TaRaqam", typeof(int));
            dt.Load(Rdr);
            ToTaIDCB.ValueMember = "TaRaqam";
            ToTaIDCB.DataSource = dt;
            Con.Close();
        }
        private void GetTaIsmYon()
        {
            Con.Open();
            string Query = "Select * from TalabaTb1 where TaRaqam=" + ToTaIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ToTaIsmTxB.Text = dr["TaIsm"].ToString();
                ToTaYonTxB.Text = dr["TaYonNom"].ToString();
            }
            Con.Close();
        }
        private void ToTaIDCB_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetTaIsmYon();
        }
        private void GetOqID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select OqRaqam from OqituvchiTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("OqRaqam", typeof(int));
            dt.Load(Rdr);
            MaOqIDCB.ValueMember = "OqRaqam";
            MaOqIDCB.DataSource = dt;
            Con.Close();
        }
        private void GetOqIsm()
        {
            Con.Open();
            string Query = "Select * from OqituvchiTb1 where OqRaqam=" + MaOqIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                MaOqIsmTxB.Text = dr["OqIsm"].ToString();
                MaOqMiqdorTxB.Text = dr["OqMaosh"].ToString();
            }
            Con.Close();
        }
        private void MaOqIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetOqIsm();
        }

        private void ToTaTolashBtn_Click_1(object sender, EventArgs e)
        {
            if (ToTaIDCB.SelectedIndex == -1 || ToTaIsmTxB.Text == "" || ToTaMiqdorTxB.Text == "" || ToTaYonTxB.Text == "" || ToTaDavrCB.SelectedIndex == -1)
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into TolovlarTb1(ToTaID,ToTaIsm,ToTaYonalish,ToTaMiqdori,ToTaDavr,ToTaTolovKun)values(@TTId,@TTI,@TTY,@TTM,@TTD,@TTTK)", Con);
                    cmd.Parameters.AddWithValue(@"@TTId", ToTaIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@TTI", ToTaIsmTxB.Text);
                    cmd.Parameters.AddWithValue(@"@TTY", ToTaYonTxB.Text);
                    cmd.Parameters.AddWithValue(@"@TTM", ToTaMiqdorTxB.Text);
                    cmd.Parameters.AddWithValue(@"@TTD", ToTaDavrCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue(@"@TTTK", DateTime.Today.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tolovlar tablitsasiga malumot qo'shilmoqda!!!");
                    Con.Close();

                    ShowTolovlar();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ToTaTozalashBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ShowTolovlar()
        {
            Con.Open();
            string Query = "select * from TolovlarTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            ToTaDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            ToTaIDCB.SelectedIndex = -1;
            ToTaIsmTxB.Text = "";
            ToTaYonTxB.Text = "";
            ToTaMiqdorTxB.Text = "";
            ToTaTolovKunTxB.Text = "";
            ToTaDavrCB.SelectedIndex = -1;
        }

        int Key = 0;
        private void ToTaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ToTaIDCB.SelectedValue = ToTaDGV.SelectedRows[0].Cells[1].Value.ToString();
            ToTaIsmTxB.Text = ToTaDGV.SelectedRows[0].Cells[2].Value.ToString();
            ToTaYonTxB.Text = ToTaDGV.SelectedRows[0].Cells[3].Value.ToString();
            ToTaMiqdorTxB.Text = ToTaDGV.SelectedRows[0].Cells[4].Value.ToString();
            ToTaDavrCB.SelectedItem = ToTaDGV.SelectedRows[0].Cells[5].Value.ToString();
            ToTaTolovKunTxB.Text = ToTaDGV.SelectedRows[0].Cells[6].Value.ToString();
            if(ToTaIsmTxB.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = int.Parse(ToTaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void MaOqTolashBtn_Click(object sender, EventArgs e)
        {
            if (MaOqMiqdorTxB.Text == "")
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
            }
            else
            {
                string Davri = MaOqDavrDTP.Value.Date.Month.ToString() + "/" + MaOqDavrDTP.Value.Date.Year.ToString();
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into MaoshTb1(MaOqID,MaOqIsm,MaOqMiqdor,MaDavr,MaOqTolovKun)values(@MOId,@MOI,@MOM,@MD,@MOTK)", Con);
                    cmd.Parameters.AddWithValue(@"@MOId", MaOqIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@MOI", MaOqIsmTxB.Text);
                    cmd.Parameters.AddWithValue(@"@MOM", MaOqMiqdorTxB.Text);
                    cmd.Parameters.AddWithValue(@"@MD", Davri);
                    cmd.Parameters.AddWithValue(@"@MOTK", DateTime.Today.Date);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tolovlar tablitsasiga malumot qo'shilmoqda!!!");
                    Con.Close();

                    ShowMaoshlar();
                    Reset2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void MaOqTozalashBtn_Click(object sender, EventArgs e)
        {
            Reset2();
        }
        private void TolovBtn_Click(object sender, EventArgs e)
        {
            MaoshPN.Visible = false;
            TolovPN.Visible = true;
        }
        private void MaoshBtn_Click(object sender, EventArgs e)
        {
            MaoshPN.Visible = true;
            TolovPN.Visible = false;
        }

        int Key2 = 0;
        private void MaOqDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MaOqIDCB.SelectedValue = MaOqDGV.SelectedRows[0].Cells[1].Value.ToString();
            MaOqIsmTxB.Text = MaOqDGV.SelectedRows[0].Cells[2].Value.ToString();
            MaOqMiqdorTxB.Text = MaOqDGV.SelectedRows[0].Cells[3].Value.ToString();
            MaOqDavrDTP.Text = MaOqDGV.SelectedRows[0].Cells[4].Value.ToString();
            MaOqMaoshKunTxB.Text = MaOqDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (MaOqIsmTxB.Text == "")
            {
                Key2 = 0;
            }
            else
            {
                Key2 = int.Parse(MaOqDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void ShowMaoshlar()
        {
            Con.Open();
            string Query = "select * from MaoshTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            MaOqDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset2()
        {
            MaOqIDCB.SelectedIndex = -1;
            MaOqIsmTxB.Text = "";
            MaOqMiqdorTxB.Text = "";
            MaOqMaoshKunTxB.Text = "";
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
        private void UniversitetlarLB_Click(object sender, EventArgs e)
        {
            Universitetlar universitetlar = new Universitetlar();
            this.Hide();
            universitetlar.Show();
        }
    }
}
