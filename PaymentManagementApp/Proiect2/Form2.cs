using Oracle.ManagedDataAccess.Client;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect2
{
    public partial class Form2 : Form
    {
        private OracleDataAdapter DataAdapter;
        private OracleConnection Connection;
        private OracleCommand OracleCommand;
        private DataSet ds;
        private string SqlStringCommand = "";
        public DateTime Data { get; set; } = DateTime.Now;
        public Form2()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ds = new DataSet2();

            Connection = new OracleConnection("DATA SOURCE=localhost:1521/XE;PASSWORD=student;USER ID=STUDENT");
            try { Connection.Open(); }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SqlStringCommand = $"SELECT * from Angajati";
            DataAdapter = new OracleDataAdapter(SqlStringCommand, Connection); 
            DataAdapter.Fill(ds, "Angajati"); 

            Connection.Close();
            ds.AcceptChanges();

            CrystalReport2 raport1 = new CrystalReport2();
            raport1.SetDataSource(ds);

            crystalReportViewer1.ReportSource = raport1;
        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {
            ds = new DataSet2();

            Connection = new OracleConnection("DATA SOURCE=localhost:1521/XE;PASSWORD=student;USER ID=STUDENT");
            try { Connection.Open(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SqlStringCommand = $"SELECT * from Angajati";
            DataAdapter = new OracleDataAdapter(SqlStringCommand, Connection);
            DataAdapter.Fill(ds, "Angajati");

            Connection.Close();
            ds.AcceptChanges();

            CrystalReport3 raport2 = new CrystalReport3();
            raport2.SetDataSource(ds);

            crystalReportViewer2.ReportSource = raport2;
        }
    }
}
