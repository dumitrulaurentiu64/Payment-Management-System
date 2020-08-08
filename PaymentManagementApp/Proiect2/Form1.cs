using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect2
{
    public partial class Form1 : Form
    {
        private string SqlStringCommand = "";
        private string SqlStringCommand1 = "";
        private string SqlStringCommand2 = "";
        private string SqlStringCommand3 = "";
        private string SqlStringCommand4 = "";

        OracleConnection conn;
        OracleCommand cmd;
        String str;
        OracleDataAdapter da, da2;
        DataSet ds, ds2;
        OracleParameter p0, p1, p2, p3, p4, p5, p6;
        bool parolabool = false;

        string NumeSelectat = "";
        string PrenumeSelectat = "";

        private void aNGAJATIBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.aNGAJATIBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet2);
        }

        private void txtPremiiBrute_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtPremiiBrute.Text, @"^[0-9_.-]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti numere intregi!");
            }
        }

        private void txtSpor_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtSpor.Text, @"^[0-9_.-]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti numere intregi!");
            }
        }

        private void txtRetineri_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtRetineri.Text, @"^[0-9_.-]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti numere intregi!");
            }
        }

        private void txtSalarBaza_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtSalarBaza.Text, @"^[0-9_.-]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti numere intregi!");
            }
        }

        private void txtFunctie_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtFunctie.Text, @"^[a-zA-Z]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti o functie corect!");
            }
        }

        private void txtPrenume_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtPrenume.Text, @"^[a-zA-Z]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti un prenume corect!");
            }
        }

        private void txtNume_TextChanged(object sender, EventArgs e)
        {
            Match mc = Regex.Match(txtNume.Text, @"^[a-zA-Z]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti un nume corect!");
            }
        }

        private void btn_ajutor_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" INTRODUCERE DATE : \n \n -> Actualizare date:" +
               "Se vor actualiza datele unui angajat selectat/cautat \n " +
               "-> Adaugare angajati: Se va adauga un angajat nou \n -> " +
               "Stergere angajati: Se va sterge un angajat selectat/cautat anterior \n" +
               "\n TIPARIRE: \n \n -> Stat de plata: Se listeaza toti angajatii si salariile aferente" +
               "\n -> Fluturasi: Se listeaza fluturasul unui angajat \n \n" +
               "MODIFICARE PROCENTE: \n " +
               "Se pot modifica impozitele, CAS-ul si CASS-ul. Ca masura de securitate avem introducerea unei parole.");
        }

        private void btn_iesire_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esti sigur ca vrei sa inchizi aplicatia?", "Inchidere aplicatie", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Aplicatia se va inchide", "", MessageBoxButtons.OK);
                Application.Exit();
            }
            else
            {
                this.Activate();
            }
        }

        private void txtNrCrt_TextChanged(object sender, EventArgs e)
        {

            Match mc = Regex.Match(txtSpor.Text, @"^[0-9_.-]*$");
            if (!mc.Success)
            {
                MessageBox.Show("Introduceti numere intregi!");
            }
        }

        public Form1()
        {
            InitializeComponent();
            try
            {
                conn = new OracleConnection("DATA SOURCE=localhost:1521/XE;PASSWORD=student;USER ID=STUDENT");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btn_procente.Hide();
        }

        private void aNGAJATIDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //conn.Open();
            DataGridViewRow row = this.aNGAJATIDataGridView.Rows[e.RowIndex];
            NumeSelectat = row.Cells[1].Value.ToString();
            Console.WriteLine(NumeSelectat);
            PrenumeSelectat = row.Cells[2].Value.ToString();
            Console.WriteLine(PrenumeSelectat);
        }

        private void btnCautare_Click(object sender, EventArgs e)
        {
            //try { conn.Open(); }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //refreshTable();
            string filtrare = string.Format("Nume LIKE '{0}*' and Prenume LIKE '{1}*'", txtNume.Text, txtPrenume.Text);
            //MessageBox.Show(variab);
            aNGAJATIBindingSource.Filter = filtrare;

            //try { conn.Close(); }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void txtRapoarte_Click(object sender, EventArgs e)
        {
            Form2 formRapoarte = new Form2();
            formRapoarte.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string parolaa = txtParola.Text;
            conn.Open();
            SqlStringCommand = $"SELECT Parola from Impozit";
            da = new OracleDataAdapter(SqlStringCommand, conn);
            ds = new DataSet();
            ds.Tables["Impozit"]?.Clear();
            da.Fill(ds, "Impozit");
            if (parolaa == ds.Tables["Impozit"].Rows[0]["PAROLA"].ToString())
            {
                parolabool = true;
                btn_procente.Hide();
                btn_procente.Show();
                iMPOZITDataGridView.Show();
            }
            else MessageBox.Show("Parola incorecta");
            conn.Close();
        }

        private void txtCAS_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCASS_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtImpozit_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshTableImpozit()
        {
            str = "SELECT * FROM Impozit";
            da = new OracleDataAdapter(str, conn);
            ds = new DataSet();
            da.Fill(ds, "Impozit");
            iMPOZITDataGridView.DataSource = ds.Tables["Impozit"].DefaultView;
        }

        private void btn_procente_Click(object sender, EventArgs e)
        {
            conn.Open();
            /*Modificare procente*/
            if (txtImpozit.Text != "")
            {
                int impozit = Convert.ToInt16(txtImpozit.Text);
                SqlStringCommand1 = $"UPDATE Impozit set IMPOZIT={impozit}";
                cmd = new OracleCommand(SqlStringCommand1, conn);
                cmd.ExecuteNonQuery();
                refreshTableImpozit();
            }
            if (txtCAS.Text != "")
            {
                int cas = Convert.ToInt16(txtCAS.Text);
                SqlStringCommand2 = $"UPDATE Impozit set CAS={cas}";
                cmd = new OracleCommand(SqlStringCommand2, conn);
                cmd.ExecuteNonQuery();
                refreshTableImpozit();
            }
            if (txtCASS.Text != "")
            {
                int cass = Convert.ToInt16(txtCASS.Text);
                SqlStringCommand3 = $"UPDATE Impozit set CASS={cass}";
                cmd = new OracleCommand(SqlStringCommand3, conn);
                cmd.ExecuteNonQuery();
                refreshTableImpozit();
            }

            if (txtParola.Text != "")
            {
                string parola = txtParola.Text;
                SqlStringCommand4 = $"UPDATE Impozit set Parola={parola}";
                cmd = new OracleCommand(SqlStringCommand4, conn);
                cmd.ExecuteNonQuery();
                refreshTableImpozit();
            }

            refreshTable();

            /*Actualizare tabelAngajati*/
            int nrrow = aNGAJATIDataGridView.RowCount;
            nrrow--;
            while (nrrow > 0)
            {
                nrrow--;
                DataGridViewRow row = aNGAJATIDataGridView.Rows[nrrow];
                SqlStringCommand1 = $"UPDATE Angajati set NUME='{row.Cells[1].Value.ToString()}' where NUME='{row.Cells[1].Value.ToString()}'";
                cmd = new OracleCommand(SqlStringCommand1, conn);
                cmd.ExecuteNonQuery();
            }

            conn.Close();

            /*Reset stringuri sql*/
            SqlStringCommand = "";
            SqlStringCommand1 = "";
            SqlStringCommand2 = "";
            SqlStringCommand3 = "";
            SqlStringCommand4 = "";
        }

        private void aNGAJATIBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.aNGAJATIBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.IMPOZIT' table. You can move, or remove it, as needed.
            this.iMPOZITTableAdapter.Fill(this.dataSet2.IMPOZIT);
            // TODO: This line of code loads data into the 'dataSet2.ANGAJATI' table. You can move, or remove it, as needed.
            this.aNGAJATITableAdapter.Fill(this.dataSet2.ANGAJATI);
            // TODO: This line of code loads data into the 'dataSet1.ANGAJATI' table. You can move, or remove it, as needed.
        }

        private void refreshTable()
        {
            str = "SELECT * FROM Angajati";
            da = new OracleDataAdapter(str, conn);
            ds = new DataSet();
            da.Fill(ds, "Angajati");
            aNGAJATIDataGridView.DataSource = ds.Tables["Angajati"].DefaultView;
        }

        private void btn_adaugare_Click(object sender, EventArgs e)
        {
            try
            {
                p0 = new OracleParameter();
                p1 = new OracleParameter();
                p2 = new OracleParameter();
                p3 = new OracleParameter();
                p4 = new OracleParameter();
                p5 = new OracleParameter();
                p6 = new OracleParameter();

                conn.Open();

                if ((txtNume.Text == "") || (txtPrenume.Text == "") || (txtFunctie.Text == "") ||
                (txtSalarBaza.Text == "") || (txtSpor.Text == "") || (txtPremiiBrute.Text == "")
                || (txtRetineri.Text == ""))
                {
                    MessageBox.Show("Nu lasati niciun camp liber!");
                }
                else
                {
                    //string nume = txtNume.Text;
                    //string prenume = txtPrenume.Text;
                    //string functie = txtFunctie.Text;
                    //int salar = Convert.ToInt16(txtSalarBaza.Text);
                    //int spor = Convert.ToInt16(txtSpor.Text);
                    //int premii = Convert.ToInt16(txtPremiiBrute.Text);
                    //int retineri = Convert.ToInt16(txtRetineri.Text);

                    p0.Value = txtNume.Text;
                    p1.Value = txtPrenume.Text;
                    p2.Value = txtFunctie.Text;
                    p3.Value = Convert.ToInt16(txtSalarBaza.Text);
                    p4.Value = Convert.ToInt16(txtSpor.Text);
                    p5.Value = Convert.ToInt16(txtPremiiBrute.Text);
                    p6.Value = Convert.ToInt16(txtRetineri.Text);

                    str = "insert into Angajati(NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, RETINERI) values(:0,:1, :2, :3, :4, :5, :6)";
                    cmd = new OracleCommand(str, conn);
                    cmd.Parameters.Add(p0);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);

                    cmd.ExecuteNonQuery();
                    refreshTable();
                    conn.Close();
                    MessageBox.Show("Adaugat cu succes!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtNume.Text = "";
                txtPrenume.Text = "";
                txtFunctie.Text = "";
                txtSalarBaza.Text = "";
                txtSpor.Text = "";
                txtPremiiBrute.Text = "";
                txtRetineri.Text = "";

            }
        }

        private void btn_actualizare_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtNrCrt.Text.Equals("")) && (txtNrCrt.Text != null))
                {
                    p1 = new OracleParameter();
                    p1.Value = txtNrCrt.Text;
                    if (!(txtNume.Text.Equals("")) && (txtNume.Text != null))
                    {
                        try { conn.Open(); }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        p2 = new OracleParameter();
                        p2.Value = txtNume.Text;

                        str = "update Angajati set Nume=:1 where Nr_crt=:2";

                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        try { conn.Close(); }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                    if (!txtPrenume.Text.Equals("") && txtPrenume.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtPrenume.Text;

                        str = "update Angajati set Prenume=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtFunctie.Text.Equals("") && txtFunctie.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtFunctie.Text;

                        str = "update Angajati set Functie=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtSalarBaza.Text.Equals("") && txtSalarBaza.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtSalarBaza.Text;

                        str = "update Angajati set Salar_baza=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtRetineri.Text.Equals("") && txtRetineri.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtRetineri.Text;

                        str = "update Angajati set Retineri=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtSpor.Text.Equals("") && txtSpor.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtSpor.Text;

                        str = "update Angajati set Spor=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtPremiiBrute.Text.Equals("") && txtPremiiBrute.Text != null)
                    {
                        conn.Open();
                        //p1 = new OracleParameter();
                        p2 = new OracleParameter();
                        //p1.Value = Convert.ToInt16(txtNrCrt.Text);
                        p2.Value = txtPremiiBrute.Text;

                        str = "update Angajati set Premii_brute=:1 where Nr_crt=:2";
                        cmd = new OracleCommand(str, conn);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p1);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                }
                else if ((NumeSelectat != null && NumeSelectat != "") && (PrenumeSelectat != null && PrenumeSelectat != ""))
                {
                    if (!(txtNume.Text.Equals("")) && (txtNume.Text != null))
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Nume = '{txtNume.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }

                    if (!txtPrenume.Text.Equals("") && txtPrenume.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Prenume = '{txtPrenume.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtFunctie.Text.Equals("") && txtFunctie.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Functie = '{txtFunctie.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtSalarBaza.Text.Equals("") && txtSalarBaza.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Salar_baza = '{txtSalarBaza.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtRetineri.Text.Equals("") && txtRetineri.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Retineri = '{txtRetineri.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtSpor.Text.Equals("") && txtSpor.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Spor = '{txtSpor.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                    if (!txtPremiiBrute.Text.Equals("") && txtPremiiBrute.Text != null)
                    {
                        conn.Open();
                        str = $"UPDATE Angajati set Premii_brute = '{txtPremiiBrute.Text}' where NUME = '{NumeSelectat}' and PRENUME = '{PrenumeSelectat}'";
                        cmd = new OracleCommand(str, conn);
                        cmd.ExecuteNonQuery();
                        refreshTable();
                        conn.Close();

                    }
                }
            }

            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                txtNrCrt.Text = "";
                txtNume.Text = "";
                txtPrenume.Text = "";
                txtFunctie.Text = "";
                txtSalarBaza.Text = "";
                txtSpor.Text = "";
                txtPremiiBrute.Text = "";
                txtRetineri.Text = "";
            }
        }

        private void btn_stergere_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                if (!(txtNume.Text.Equals("")) && (txtNume.Text != null) && !(txtPrenume.Text.Equals("")) && (txtPrenume.Text != null))
                {
                    SqlStringCommand = $"DELETE from Angajati where NUME='{txtNume.Text}' and PRENUME='{txtPrenume.Text}'";
                    if (MessageBox.Show("Angajatul va fi sters. Continuati?", "Stergere", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cmd = new OracleCommand(SqlStringCommand, conn);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Angajatul a fost sters", "Stergere");

                        refreshTable();
                    }
                    else MessageBox.Show("Angajatul nu a fost sters", "Stergere");
                }
                else if ((NumeSelectat != null && NumeSelectat != "") && (PrenumeSelectat != null && PrenumeSelectat != ""))
                {
                    SqlStringCommand = $"DELETE from Angajati where NUME='{NumeSelectat}' and PRENUME='{PrenumeSelectat}'";
                    if (MessageBox.Show("Angajatul va fi sters. Continuati?", "Stergere", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cmd = new OracleCommand(SqlStringCommand, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Angajatul a fost sters", "Stergere");

                        refreshTable();
                    }
                    else MessageBox.Show("Angajatul nu a fost sters", "Stergere");
                }
            }

            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                txtNrCrt.Text = "";
                txtNume.Text = "";
                txtPrenume.Text = "";
                txtFunctie.Text = "";
                txtSalarBaza.Text = "";
                txtSpor.Text = "";
                txtPremiiBrute.Text = "";
                txtRetineri.Text = "";
            }
        }
    }
}
