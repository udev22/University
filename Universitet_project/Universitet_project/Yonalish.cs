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
    public partial class Yonalish : Form
    {
        public Yonalish()
        {
            InitializeComponent();
            
            ShowYonalish();
        }

        // Sql Data Source yolini korsatib qoydim
        // buni Baza dannix UniversitetBD chap tugmasini ustiga bosib svoystvasidan oldim.
        readonly SqlConnection Con = new SqlConnection(@"Data Source=WIN-HJSU2H4CJ7H;Initial Catalog=UniversitetDB;Integrated Security=True;Pooling=False");
        // Saqlash tugmasini bosganda ichidagi cod bilan bazadaga kiritilgan malumotni tablitsaga sahranit qladi.

        private void YonSaqlashBtn_Click_1(object sender, EventArgs e)
        {
            // shart berdim agar (Yonalish nomi,Qabul qilish,Tolovlar)-> textBox ga qiymat kiritilmasa
            if (YonNomTxB.Text == "" || YonQabulTxB.Text == "" || YonTolovlarTxB.Text == "")
            {
                // 7ta textBoxga malumot kiritilmasa (Malumotlar toldirilmadi) degan habarnoma chiqadi.
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                // agar 7ta textBoxga toliq malumotlar kiritilsa (try catch) oz ishini bajaradi.
                try
                {
                    // Sql Server malumotlar bazasiga kirdik
                    Con.Open();
                    // Sql Server malumotlar bazasidagi YonalishTb1 tablitsaga malumotlar toldirmoqchimiz
                    // Malumotlarni toldirish uchun command obyektidan ozgaruvchi oldik
                    // command konstruktori ichiga (Insert into YonalishTb1(YonNom,YonQabul,YonTolovlar)values(@YN,@YQ,@YT)",Con) yozamiz
                    // Malumot kiritish uchun (Insert int) kalit sozdan foydalanib tablitsani nomini yozib
                    /* konstruktoriga bazadagi tablitsani ustunlarini nomini yozamiz ustunlar soni 3ta
                    va (values) kalit sozi bilan kanstruktoriga (@ozgaruvchi_nomi-> (@YN))yani ozgaruvchiga nom berdik */

                    // So'rov matni va SqlConnection bilan SqlCommand sinfining yangi namunasini ishga tushiradi. 
                    SqlCommand cmd = new SqlCommand("Insert into YonalishTb1(YonNom,YonQabul,YonTolovlar)values(@YN,@YQ,@YT)", Con);
                    cmd.Parameters.AddWithValue(@"@YN", YonNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@YQ", YonQabulTxB.Text);
                    cmd.Parameters.AddWithValue(@"@YT", YonTolovlarTxB.Text);
                    // cmd.ExecuteNonQuery) kamandasi bazadagi tablitsa ustunlariga berilga hamma qiymatlarni tablitsaga toldirib chiqadi
                    cmd.ExecuteNonQuery();
                    // tepada bajarilga ishlarimiz ishlasa (Yonalish qo'shildi) habarnomasi chiqadi 
                    MessageBox.Show("Yonalish tablitsasiga malumot qo'shilmoqda!!!");
                    // Con.Close() - bu Sql server bazada ishlar bajarilgandan keyin SqlConnection tozalab chiqib ketadi
                    Con.Close();

                    ShowYonalish(); // Sqldagi tablitsani olish uchun yaratilgan metod
                    Reset(); // Sqldagi malumotlarni bosh qilib berilga qatori yani eng ohirgi qator
                }
                // agar bazaga textBoxdan notog'ri tipli yoki boshqa hato sezilsa exception tashlaydi
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void YonTahrirBtn_Click(object sender, EventArgs e)
        {
            // shart berdim agar (Yonalish nomi,Qabul qilish,Tolovlar)-> textBox ga qiymat kiritilmasa
            if (YonNomTxB.Text == "" || YonQabulTxB.Text == "" || YonTolovlarTxB.Text == "")
            {
                // 3ta textBoxga malumot kiritilmasa (Malumotlar toldirilmadi) degan habarnoma chiqadi.
                MessageBox.Show("Malumotlar to'ldirilmagan!!!");
                Reset();
            }
            else
            {
                // agar 3ta textBoxga toliq malumotlar kiritilsa (try catch) oz ishini bajaradi.
                try
                {
                    // Sql Server malumotlar bazasiga kirdik
                    Con.Open();
                    // Sql Server malumotlar bazasidagi YonalishTb1 tablitsaga malumotlarni tahrirlamoqchimiz
                    // Malumotlarni tahrirlab toldirish uchun command obyektidan ozgaruvchi oldik
                    // command konstruktori ichiga (Update YonalishTb1 Set YonNom=@YN,YonQabul=@YQ,YonTolovlar=@YT where YonRaqam=@YKey", Con) yozamiz
                    /* Malumotlarni tahrirlash uchun (Update) kalit sozdan foydalanib tablitsani nomini yozib
                    konstruktoriga bazadagi tablitsani ustunlarini nomini yozamiz ustunlar soni 3ta
                    ustunlar nomiga yangi ozgaruvchilarni tenglashtiramiz yani (YonNom=@YN) shu kabi. */
                    // So'rov matni va SqlConnection bilan SqlCommand sinfining yangi namunasini ishga tushiradi. 
                    SqlCommand cmd = new SqlCommand("Update YonalishTb1 Set YonNom=@YN,YonQabul=@YQ,YonTolovlar=@YT where YonRaqam=@YKey", Con);
                    // Bazadagi YonalishTb1 tablitsasidagi har bir ustunga tegishli textBoxlarni qiymatini tahrirlash uchun uzatayabmiz
                    cmd.Parameters.AddWithValue(@"@YN", YonNomTxB.Text);
                    cmd.Parameters.AddWithValue(@"@YQ", YonQabulTxB.Text);
                    cmd.Parameters.AddWithValue(@"@YT", YonTolovlarTxB.Text);
                    cmd.Parameters.AddWithValue(@"YKey", Key);
                    // (cmd.ExecuteNonQuery) kamandasi bazadagi tablitsa ustunlariga berilga hamma qiymatlarni tablitsaga toldirib chiqadi
                    cmd.ExecuteNonQuery();
                    // tepada bajarilga ishlarimiz ishlasa (Yonalish qo'shildi) habarnomasi chiqadi 
                    MessageBox.Show("Talaba tablitsasida malumot tahrirlanmoqda!!!");
                    // Con.Close() - bu Sql server bazada ishlar bajarilgandan keyin SqlConnection tozalab chiqib ketadi
                    Con.Close();

                    ShowYonalish();
                    Reset();
                }
                // agar bazaga textBoxdan notog'ri tipli yoki boshqa hato sezilsa exception tashlaydi
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void YonOchirishBtn_Click(object sender, EventArgs e)
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
                // agar 3ta textBoxga toliq malumotlar kiritilsa (try catch) oz ishini bajaradi.
                try
                {
                    // Sql Server malumotlar bazasiga kirdik
                    Con.Open();
                    // Sql Server malumotlar bazasidagi YonalishTb1 tablitsaga malumotlarni tahrirlamoqchimiz
                    // Malumotlarni tahrirlab toldirish uchun command obyektidan ozgaruvchi oldik
                    // command konstruktori ichiga (Update YonalishTb1 Set YonNom=@YN,YonQabul=@YQ,YonTolovlar=@YT where YonRaqam=@YKey", Con) yozamiz
                    /* Malumotlarni tahrirlash uchun (Update) kalit sozdan foydalanib tablitsani nomini yozib
                    konstruktoriga bazadagi tablitsani ustunlarini nomini yozamiz ustunlar soni 3ta
                    ustunlar nomiga yangi ozgaruvchilarni tenglashtiramiz yani (YonNom=@YN) shu kabi. */
                    // So'rov matni va SqlConnection bilan SqlCommand sinfining yangi namunasini ishga tushiradi. 
                    SqlCommand cmd = new SqlCommand("Delete from YonalishTb1 where YonRaqam=@YKey", Con);
                    // Bazadagi YonalishTb1 tablitsasidagi har bir ustunga tegishli textBoxlarni qiymatini tahrirlash uchun uzatayabmiz
                    cmd.Parameters.AddWithValue(@"YKey", Key);
                    // (cmd.ExecuteNonQuery) kamandasi bazadagi tablitsa ustunlariga berilga hamma qiymatlarni tablitsaga toldirib chiqadi
                    cmd.ExecuteNonQuery();
                    // tepada bajarilga ishlarimiz ishlasa (Yonalish qo'shildi) habarnomasi chiqadi 
                    MessageBox.Show("Talaba tablitsasida malumot O'chirilmoqda!!!");
                    // Con.Close() - bu Sql server bazada ishlar bajarilgandan keyin SqlConnection tozalab chiqib ketadi
                    Con.Close();

                    ShowYonalish();
                    Reset();
                }
                // agar bazaga textBoxdan notog'ri tipli yoki boshqa hato sezilsa exception tashlaydi
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShowYonalish()
        {
            // DGV chiqarib beruvchi method
            Con.Open();
            // Yonalish tablitsasidagi barcha malumotlarni tanlab olayabdi
            string Query = "select * from YonalishTb1";
            /* SQL Server ma'lumotlar bazasini (DataSet) to'ldirish va yangilash uchun foydalaniladigan ma'lumotlar buyruqlari 
            to'plamini va ma'lumotlar bazasi ulanishini ifodalaydi.
            SqlDataAdapter ma'lumotlarni olish va saqlash uchun DataSet va SQL Server o'rtasida ko'prik bo'lib xizmat qiladi.
            SqlDataAdapter ushbu ko'prikni mos keladigan Transact-SQL-dan foydalangan holda ma'lumotlar to'plamidagi 
            ma'lumotlarni ma'lumotlar manbasidagi ma'lumotlarga mos keladigan tarzda o'zgartiradigan Fill va ma'lumotlar 
            to'plamidagi ma'lumotlarga mos keladigan yangilash bilan xaritalash orqali taqdim etadi. ma'lumotlar manbasiga 
            qarshi bayonotlar. Yangilash ketma-ketlik asosida amalga oshiriladi. Har bir kiritilgan, o'zgartirilgan va o'chirilgan 
            qator uchun Yangilash usuli unda amalga oshirilgan o'zgartirish turini aniqlaydi (Qo'shish, Yangilash yoki O'chirish). 
            O'zgartirish turiga qarab, o'zgartirilgan qatorni ma'lumotlar manbasiga tarqatish uchun Qo'shish, Yangilash yoki 
            O'chirish buyruq shablonini bajaradi. SqlDataAdapter ma'lumotlar to'plamini to'ldirganda, agar ular mavjud bo'lmasa, 
            qaytarilgan ma'lumotlar uchun kerakli jadvallar va ustunlarni yaratadi. */
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            // Baza ichidagi YonalishTb1 tablitsani (DataGribVine)ga toldiradi
            YonDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            YonNomTxB.Text = "";
            YonQabulTxB.Text = "";
            YonTolovlarTxB.Text = "";
        }

        int Key = 0;
        private void YonDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // (SelectedRows) bu tablitsadagi 0-qator 1,2,3-ustunlarni tanlaganda yani bosganda textBoxlarga chiqadi
            // 0-qator (1-ustun 2-uston 3-ustun)da tablitsadagi malumot tanlansa aynan 0-qatordagi malumotlar textBoxga uzatiladi
            YonNomTxB.Text = YonDGV.SelectedRows[0].Cells[1].Value.ToString();
            YonQabulTxB.Text = YonDGV.SelectedRows[0].Cells[2].Value.ToString();
            YonTolovlarTxB.Text = YonDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (YonNomTxB.Text == "")
            {
                // tabitsadagi malumotlarni ustiga bosganda tablitsani 1 ustunidagi malumot yoq bolsa textBoxlarga hechnima uzatmaydi
                // yani textBoxlarni bombosh qilib beradi
                Key = 0;
                /*YonNomTxB.Text = "";
                YonQabulTxB.Text = "";
                YonTolovlarTxB.Text = "";*/
            }
            else
            {
                // tablitsada tanlangan elemant 1 ustun dagi YonNom da qiymat bolsa
                // uni 0-qator 0-ustundagi qiymatni yani (YonRaqam) ustunidagi stringli raqamni intga otqazib Keyga tenglaydi.
                Key = int.Parse(YonDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void MenyuLb_Click(object sender, EventArgs e)
        {
            Menyu menyu = new Menyu();
            this.Hide();
            menyu.Show();
        }
        private void TalabaLb_Click(object sender, EventArgs e)
        {
            Talaba talaba = new Talaba();
            this.Hide();
            talaba.Show();
        }
        private void OqituvchiLb_Click(object sender, EventArgs e)
        {
            Oqituvchi oqituvchi = new Oqituvchi();
            this.Hide();
            oqituvchi.Show();
        }
        private void KurslarLb_Click(object sender, EventArgs e)
        {
            Kurslar kurslar = new Kurslar();
            this.Hide();
            kurslar.Show();
        }
        private void TolovlarLb_Click(object sender, EventArgs e)
        {
            Tolovlar tolovlar = new Tolovlar();
            this.Hide();
            tolovlar.Show();
        }
        private void UniversitetLb_Click(object sender, EventArgs e)
        {
            Universitetlar universitetlar = new Universitetlar();
            this.Hide();
            universitetlar.Show();
        }
    }
}
