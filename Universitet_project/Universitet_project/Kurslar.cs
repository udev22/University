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
    public partial class Kurslar : Form
    {
        public Kurslar()
        {
            InitializeComponent();

            GetYonID();
            GetOqID();
            ShowKurslar();
        }
        
        readonly SqlConnection KCon = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        
        private void GetYonID()
        {
            KCon.Open();
            SqlCommand cmd = new SqlCommand("select YonRaqam from YonalishTb1", KCon);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("YonRaqam", typeof(int));
            dt.Load(Rdr);
            KuYonalishIDCB.ValueMember = "YonRaqam";
            KuYonalishIDCB.DataSource = dt;
            KCon.Close();
        }
        private void GetYonNom()
        {
            KCon.Open();
            string Query = "Select * from YonalishTb1 where YonRaqam=" + KuYonalishIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, KCon);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                KuYonalishNomTxB.Text = dr["YonNom"].ToString();
            }
            KCon.Close();
        }
        private void KuYonalishIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetYonNom();
        }
        private void GetOqID()
        {
            KCon.Open();
            SqlCommand cmd = new SqlCommand("select OqRaqam from OqituvchiTb1", KCon);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("OqRaqam", typeof(int));
            dt.Load(Rdr);
            KuOqtuvchiIDCB.ValueMember = "OqRaqam";
            KuOqtuvchiIDCB.DataSource = dt;
            KCon.Close();
        }
        private void GetOqIsm()
        {
            KCon.Open();
            string Query = "Select * from OqituvchiTb1 where OqRaqam=" + KuOqtuvchiIDCB.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, KCon);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                KuOqtuchiIsmTxB.Text = dr["OqIsm"].ToString();
            }
            KCon.Close();
        }
        private void KuOqtuvchiIDCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetOqIsm();
        }

        private void KuSaqlashBtn_Click(object sender, EventArgs e)
        {
            if(KuNomTxB.Text == "" || KuDarsDavomiylikTxB.Text == "" || KuYonalishIDCB.SelectedIndex == -1 || KuYonalishNomTxB.Text == "" || KuOqtuvchiIDCB.SelectedIndex == -1 || KuOqtuchiIsmTxB.Text == "")
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    KCon.Open();
                    SqlCommand cmd = new SqlCommand("Insert into KurslarTb1(KuNom,KuDarsDavomiylig,KuYonID,KuYonNom,KuOqID,KuOqIsm)values(@KN,@KDD,@KYID,@KYN,@KOID,@KOI)", KCon);
                    cmd.Parameters.AddWithValue(@"@KN", KuNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KDD", KuDarsDavomiylikTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KYID", KuYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@KYN", KuYonalishNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KOID", KuOqtuvchiIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@KOI", KuOqtuchiIsmTxB.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kurslar tablitsasiga malumot qo'shilmoqda!!!");
                    KCon.Close();

                    ShowKurslar();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void KuTahrirBtn_Click(object sender, EventArgs e)
        {
            if (KuNomTxB.Text == "" || KuDarsDavomiylikTxB.Text == "" || KuYonalishIDCB.SelectedIndex == -1 || KuYonalishNomTxB.Text == "" || KuOqtuvchiIDCB.SelectedIndex == -1 || KuOqtuchiIsmTxB.Text == "")
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    KCon.Open();
                    SqlCommand cmd = new SqlCommand("Update KurslarTb1 Set KuNom=@KN,KuDarsDavomiylig=@KDD,KuYonID=@KYID,KuYonNom=@KYN,KuOqID=@KOID,KuOqIsm=@KOI where KuRaqam=@KKey", KCon);
                    cmd.Parameters.AddWithValue(@"@KN", KuNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KDD", KuDarsDavomiylikTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KYID", KuYonalishIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@KYN", KuYonalishNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KOID", KuOqtuvchiIDCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue(@"@KOI", KuOqtuchiIsmTxB.Text);
                    cmd.Parameters.AddWithValue(@"@KKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kurslar tablitsasiga malumot tahrirlanmoqda!!!");
                    KCon.Close();

                    ShowKurslar();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void KuOchirishBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                try
                {
                    KCon.Open();
                    SqlCommand cmd = new SqlCommand("Delete from KurslarTb1 where KuRaqam=@KKey", KCon);
                    cmd.Parameters.AddWithValue(@"@KKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kurslar tablitsasiga malumot ochirilmoqda!!!");
                    KCon.Close();

                    ShowKurslar();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShowKurslar()
        {
            KCon.Open();
            string Query = "select * from KurslarTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, KCon);
            var ds = new DataSet();
            sda.Fill(ds);
            KuDGV.DataSource = ds.Tables[0];
            KCon.Close();
        }
        private void Reset()
        {
            KuNomTxB.Text = "";
            KuDarsDavomiylikTxB.Text = "";
            KuYonalishIDCB.SelectedIndex = -1;
            KuYonalishNomTxB.Text = "";
            KuOqtuvchiIDCB.SelectedIndex = -1;
            KuOqtuchiIsmTxB.Text = "";
        }

        int Key = 0;
        private void KuDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KuNomTxB.Text = KuDGV.SelectedRows[0].Cells[1].Value.ToString();
            KuDarsDavomiylikTxB.Text = KuDGV.SelectedRows[0].Cells[2].Value.ToString();
            KuYonalishIDCB.SelectedValue = KuDGV.SelectedRows[0].Cells[3].Value.ToString();
            KuYonalishNomTxB.Text = KuDGV.SelectedRows[0].Cells[4].Value.ToString();
            KuOqtuvchiIDCB.SelectedValue = KuDGV.SelectedRows[0].Cells[5].Value.ToString();
            KuOqtuchiIsmTxB.Text = KuDGV.SelectedRows[0].Cells[6].Value.ToString();
            if(KuNomTxB.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = int.Parse(KuDGV.SelectedRows[0].Cells[0].Value.ToString());
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
        private void TolovlarLB_Click(object sender, EventArgs e)
        {
            Tolovlar tolovlar = new Tolovlar();
            this.Hide();
            tolovlar.Show();
        }
        private void UniversitetLB_Click(object sender, EventArgs e)
        {
            Universitetlar universitetlar = new Universitetlar();
            this.Hide();
            universitetlar.Show();
        }
    }
}
