using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synergy_Automotive_Ratebooks
{
    public partial class Leaseplan : Form
    {
        public Leaseplan()
        {
            InitializeComponent();
        }

        private int x;
        public int Counter
        {
            get { return x; }
            set { x++; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateLbl.Text = "Removing Leases";
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] queries = new string[] { "DELETE FROM leases WHERE lease_funder_id = 3;", "INSERT INTO leases (`lease_cap_id`, `lease_term`, `lease_mileage_annual`, `lease_monthly_cost`, `lease_maintenance`, `lease_ppm`, `lease_ppmm`, `lease_funder_id`, `lease_vat`, `lease_datetime`) SELECT t.`test_cap_id`, t.`test_term`, t.`test_mileage_annual`, t.`test_monthly_cost`, case when t.`test_maintenance` > 0 then t.`test_maintenance` else 0 end as test_maintenance, case when t.`test_maintenance` > 0 then 0 else 0.01*t.`test_ppm` end as test_ppm, case when t.`test_maintenance` > 0 then 0.01*t.`test_ppm` else 0 end as test_ppmm, t.`test_funder`, 0, now() FROM test t WHERE t.`test_cap_id` > 0 and t.test_funder = 3;", "UPDATE leases SET lease_funder_id = 23 WHERE lease_funder_id = 3;", "INSERT INTO leases (`lease_funder_id`,`lease_cap_id`,`lease_term`, `lease_mileage_annual`, `lease_initial_rental`, `lease_monthly_cost`, `lease_maintenance`, `lease_ppm`, `lease_ppmm`, `lease_datetime`) select 3, l.`lease_cap_id`, l.`lease_term`, l.`lease_mileage_annual`, l.`lease_initial_rental`, max(l.`lease_monthly_cost`), max(l.`lease_maintenance`), max(l.`lease_ppm`), max(l.`lease_ppmm`), l.`lease_datetime` from leases l where l.`lease_funder_id` = 23 and l.`lease_monthly_cost` > 0 GROUP BY l.`lease_cap_id`, l.`lease_term`, l.`lease_mileage_annual`;", "DELETE FROM leases WHERE lease_funder_id = 23;" };

            SynergyUtilities syn = new SynergyUtilities();
            for (int i = 0; i < queries.Length; i++)
            {
                syn.UploadToDatabase(queries[i]);
                backgroundWorker1.ReportProgress(100 * (i + 1) / queries.Length);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] query_labels = new string[] { "Convert Leaseplan Leases", "Update Leaseplan Leases", "Find Best Leaseplan Leases", "Remove Old Leases", "Finishing off" };
            updateLbl.Text = query_labels[Counter];
            Console.WriteLine(e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;
            Counter++;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            progressBar1.Hide();
            updateLbl.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
    }
}
