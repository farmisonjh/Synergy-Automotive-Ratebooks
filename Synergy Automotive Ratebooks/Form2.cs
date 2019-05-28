using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Synergy_Automotive_Ratebooks
{
    public partial class Form2 : Form
    {
        public Form2()
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
            backgroundWorker1.RunWorkerAsync();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] queries = new string[] { "DELETE FROM leases WHERE lease_funder_id = 9;", "INSERT INTO leases (`lease_cap_id`, `lease_term`, `lease_mileage_annual`, `lease_monthly_cost`, `lease_maintenance`, `lease_ppm`, `lease_ppmm`, `lease_funder_id`, `lease_vat`, `lease_datetime`) select convert(mid(tl.`test_cap_id`,locate('/',tl.`test_cap_id`)-5,5), signed integer) as test_cap_id,tl.`test_term`, tl.`test_mileage_annual`, round(tl.`test_monthly_cost`*(tl.`test_term`+5)/(tl.`test_term`+2),2), case when tl.`test_maintenance` > 0 then tl.`test_maintenance` else 0 end as test_maintenance, case when tl.`test_maintenance` > 0 then 0 else 0 end as test_ppm, case when tl.`test_maintenance` > 0 then 0 else 0 end as test_ppmm, tl.`test_funder`, 0, now() from test_lex tl WHERE tl.test_funder = 9;", "DROP TABLE IF EXISTS lex5k_10k;", "CREATE TABLE lex5k_10k AS SELECT l.`lease_cap_id`,l.`lease_term`, max(case when l.`lease_mileage_annual` = 5000 then l.`lease_monthly_cost` end) as 5k_price, max(case when l.`lease_mileage_annual` = 10000 then l.`lease_monthly_cost` end) as 10k_price, case when l.`lease_maintenance` > 0 then 1 else 0 end as maintenance_binary, max(case when l.`lease_mileage_annual` = 5000 then l.`lease_maintenance` end) as 5k_main, max(case when l.`lease_mileage_annual` = 10000 then l.`lease_maintenance` end) as 10k_main, l.`lease_ppm`, l.`lease_ppmm` FROM leases l WHERE l.`lease_funder_id` = 9 and (l.`lease_mileage_annual` = 5000 OR l.`lease_mileage_annual` = 10000) GROUP BY l.`lease_cap_id`, l.`lease_term`, `maintenance_binary`;", "INSERT INTO leases (`lease_funder_id`,`lease_cap_id`,`lease_term`, `lease_mileage_annual`, `lease_initial_rental`, `lease_monthly_cost`, `lease_maintenance`, `lease_ppm`, `lease_ppmm`, `lease_datetime`) select 9 as lease_funder_id, l5.`lease_cap_id`, l5.`lease_term`, 8000 as lease_annual_mileage, 3 as lease_initial_rental, round((l5.`10k_price`-l5.`5k_price`)/5000*3000 + l5.`5k_price`,2) as lease_monthly_cost, round((l5.`10k_main`-l5.`5k_main`)/5000*3000 + l5.`5k_main`,2) as lease_maintenance, l5.`lease_ppm`, l5.`lease_ppmm`, NOW() from `lex5k_10k` l5 WHERE l5.`5k_price` IS NOT NULL and l5.`10k_price` IS NOT NULL and l5.`5k_main` IS NOT NULL and l5.`10k_main` IS NOT NULL;", "UPDATE leases SET `lease_funder_id` = 29 WHERE `lease_funder_id` = 9;", "INSERT INTO leases (`lease_funder_id`,`lease_cap_id`,`lease_term`, `lease_mileage_annual`, `lease_initial_rental`, `lease_monthly_cost`, `lease_maintenance`, `lease_ppm`, `lease_ppmm`, `lease_datetime`) select 9, l.`lease_cap_id`, l.`lease_term`, l.`lease_mileage_annual`, l.`lease_initial_rental`, max(l.`lease_monthly_cost`), max(l.`lease_maintenance`), max(l.`lease_ppm`), max(l.`lease_ppmm`), l.`lease_datetime` from leases l where l.`lease_funder_id` = 29 GROUP BY l.`lease_cap_id`, l.`lease_term`, l.`lease_mileage_annual`;", "DELETE FROM leases WHERE lease_funder_id = 29;"};
            
            SynergyUtilities syn = new SynergyUtilities();
            for(int i = 0; i<queries.Length; i++)
            {
                syn.UploadToDatabase(queries[i]);
                backgroundWorker1.ReportProgress(100 * (i + 1) / queries.Length);
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] query_labels = new string[] { "Remove Leases", "Convert Lex Leases", "Set up 8k Tables", "Creating 8k Tables", "Insert 8k Rates", "Update Lex Leases", "Find Best Lex Leases", "Remove Old Leases" };
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
    }
}
