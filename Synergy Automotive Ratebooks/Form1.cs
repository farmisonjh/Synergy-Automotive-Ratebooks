using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using Microsoft.VisualBasic.FileIO;


namespace Synergy_Automotive_Ratebooks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetDriveLocation()
        {
            string drive = @"R:\Daily Work Folders\Website Uploads\";
            return drive;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE test;");
            int rates = 0;
            Form1 f1 = new Form1();
            string drive = f1.GetDriveLocation();
            string[] filebox = new string[] { @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 8k nm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 8k wm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 5k nm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 5k wm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 24 nm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 24 wm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 36 nm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 36 wm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 48 nm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD 48 wm.csv", @"R:\Daily Work Folders\Ratebooks\ALD\Current\ALD LCV wm.csv" };
            foreach (string fil in filebox)
            {
                var dt = GetALDTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\ald_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\ald_temp.csv","test");
                    rates += dt.Rows.Count;
                }
            }
            System.Windows.Forms.MessageBox.Show("The Ratebooks were uploaded successfully with " + rates + " lines.");

            this.Hide();
            ALD f2 = new ALD();
            f2.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE test;");
            int rates = 0;
            string[] filebox = new string[] { @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-5K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-8K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-10K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-12K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-15K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-18K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-20K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-25K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-24-30K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-5K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-8K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-10K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-12K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-15K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-18K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-20K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-25K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-36-30K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-5K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-8K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-10K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-12K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-15K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-18K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-20K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-25K.csv", @"R:\Daily Work Folders\Ratebooks\Arval\Current\3-48-30K.csv"};
            foreach (string fil in filebox)
            {
                var dt = GetArvalTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\arval_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\arval_temp.csv","test");
                    rates += dt.Rows.Count;
                }
            }
            System.Windows.Forms.MessageBox.Show("The Ratebooks were uploaded successfully with " + rates + " lines.");

            Arval f2 = new Arval();
            f2.ShowDialog();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE test_lex;");
            string[] filebox = new string[] { @"R:\Daily Work Folders\Ratebooks\Lex\Current\CH 24.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CH 36.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CH 48.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CH 60.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CHNM 24.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CHNM 36.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CHNM 48.csv", @"R:\Daily Work Folders\Ratebooks\Lex\Current\CHNM 60.csv" };
            int rates = 0;
            foreach (string fil in filebox)
            {
                
                var dt = GetLexTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\lex_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\lex_temp.csv","test_lex");
                    rates += dt.Rows.Count;
                }
                label1.Text = rates + " vehicles added.";
            }
            System.Windows.Forms.MessageBox.Show("The Ratebooks were uploaded successfully with " + rates + " lines.");

            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE test;");
            //, @"R:\Daily Work Folders\Ratebooks\Leaseplan\Current\Contract Hire_8k.csv" 
            string[] filebox = new string[] {@"R:\Daily Work Folders\Ratebooks\Leaseplan\Current\Contract Hire.csv", @"R:\Daily Work Folders\Ratebooks\Leaseplan\Current\Contract Hire_8k.csv"};
            int rates = 0;
            foreach (string fil in filebox)
            {
                var dt = GetLeaseplanTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\leaseplan_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\leaseplan_temp.csv", "test");
                    rates += dt.Rows.Count;
                }
            }
            System.Windows.Forms.MessageBox.Show("The Ratebooks were uploaded successfully with " + rates + " lines.");
            this.Hide();
            Leaseplan l1 = new Leaseplan();
            l1.ShowDialog();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE test;");
            string[] single_nm = new string[] { @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-30k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-30k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-8k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-8k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-30k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-30k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-8k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-8k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-12k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-12k-nmV.csv" };
            string[] double_nm = new string[] { @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-1015k-nm.csv",  @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-1015k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-2025k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-2025k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-1015k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-1015k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-2025k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-2025k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-1015k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-1015k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-2025k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-2025k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-1015k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-1015k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-2025k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-2025k-nmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-8k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-30k-nm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-30k-nmV.csv" };
            string[] single_wm = new string[] { @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-30k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-30k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-8k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-8k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-30k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-30k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-8k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-8k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-12k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-12k-wmV.csv" };
            string[] double_wm = new string[] { @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-1015k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-1015k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-2025k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-24-2025k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-1015k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-1015k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-2025k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-36-2025k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-1015k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-1015k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-2025k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-48-2025k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-1015k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-1015k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-2025k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-60-2025k-wmV.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-8k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-30k-wm.csv", @"R:\Daily Work Folders\Ratebooks\Hitachi\Current\Hit-2436-30k-wmV.csv" };
            int rates = 0;
            foreach (string fil in single_nm)
            {
                var dt = GetHitachiTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", "test");
                    rates += dt.Rows.Count;
                }
            }
            

            foreach (string fil in double_nm)
            {
                var dt = GetHitachiTable_2(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", "test");
                    rates += dt.Rows.Count;
                }
            }

            foreach (string fil in single_wm)
            {
                var dt = GetHitachiMaintainedTable(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", "test");
                    rates += dt.Rows.Count;
                }
            }

            foreach (string fil in double_wm)
            {
                var dt = GetHitachiMaintainedTable_2(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\hitachi_temp.csv", "test");
                    rates += dt.Rows.Count;
                }
            }
            System.Windows.Forms.MessageBox.Show("The Ratebooks were uploaded successfully with " + rates + " lines.");
            this.Hide();
            Hitachi f2 = new Hitachi();
            f2.ShowDialog();
            
        }

        private static DataTable GetALDTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[24]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[0]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[1]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[12]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[11]);
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn(colFields[25]);
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[24], fieldData[0], fieldData[1], fieldData[12], fieldData[11], fieldData[25], 4);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetArvalTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[10]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[23]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[19]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[5]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[6]);
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    csvData.Columns.Add("PPM");
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[10], fieldData[23], fieldData[19], fieldData[5], fieldData[6], 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetHitachiTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[21]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[22]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[23]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn();
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn(colFields[28]);
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], fieldData[21], fieldData[22], fieldData[23], 0, 0, 5);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetHitachiTable_2(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[21]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[22]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[23]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn();
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn(colFields[26]);
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], fieldData[21], fieldData[22], fieldData[23], 0, fieldData[26], 5);
                        csvData.Rows.Add(fieldData[0], fieldData[27], fieldData[28], fieldData[29], 0, fieldData[32], 5);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetHitachiMaintainedTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[21]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[22]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[23]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[24]);
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn(colFields[28]);
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], fieldData[21], fieldData[22], fieldData[23], fieldData[24], fieldData[28], 5);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetHitachiMaintainedTable_2(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[21]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[22]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[23]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[24]);
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn(colFields[28]);
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], fieldData[21], fieldData[22], fieldData[23], fieldData[24], fieldData[28], 5);
                        csvData.Rows.Add(fieldData[0], fieldData[29], fieldData[30], fieldData[31], fieldData[32], fieldData[36], 5);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetLeaseplanTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn(colFields[12]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[13]);
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[17]);
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[18]);
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    csvData.Columns.Add("PPM");
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], fieldData[12], fieldData[13], fieldData[17], fieldData[18], 0, 3);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }

        private static DataTable GetLexTable(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    csvReader.TrimWhiteSpace = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[14]);
                    csvData.Columns.Add(cap);
                    cap.DataType = System.Type.GetType("System.String");
                    DataColumn term = new DataColumn(colFields[7]);
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn(colFields[8]);
                    csvData.Columns.Add(miles);
                    DataColumn finance = new DataColumn(colFields[22]);
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn(colFields[23]);
                    csvData.Columns.Add(maintenance);
                    csvData.Columns.Add("PPM");
                    csvData.Columns.Add("Funder");
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[14], fieldData[7], fieldData[8], fieldData[22], fieldData[23], 0, 9);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

        static void MyMySQLConnector(string filepath, string table)
        {
            
            string connStr = "datasource=160.153.129.221;port=3306;UID=farmison_john;password=Boro2902;database=synergy_auto;";
            // MySql Connection Object
            MySqlConnection conn = new MySqlConnection(connStr);

            //  csv file path
            string file = filepath;

            // MySQL BulkLoader
            MySqlBulkLoader bl = new MySqlBulkLoader(conn);
            bl.TableName = table;
            bl.FieldTerminator = ",";
            bl.LineTerminator = "\n";
            bl.NumberOfLinesToSkip = 1;
            bl.FileName = file;

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                // Upload data from file
                int count = bl.Load();
                Console.WriteLine(count + " lines uploaded.");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Done.");
            Console.ReadLine();

        }

        static void MyMySQLConnector2(string filepath, string table)
        {
            
            string connStr = "datasource=160.153.129.221;port=3306;UID=farmison_john;password=Boro2902;database=synergy_auto;";
            // MySql Connection Object
            MySqlConnection conn = new MySqlConnection(connStr);

            //  csv file path
            string file = filepath;

            // MySQL BulkLoader
            MySqlBulkLoader bl = new MySqlBulkLoader(conn);
            bl.TableName = table;
            bl.FieldTerminator = ",";
            bl.LineTerminator = "\n";
            bl.NumberOfLinesToSkip = 1;
            bl.FileName = file;

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                // Upload data from file
                int count = bl.Load();
                Console.WriteLine(count + " lines uploaded.");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Done.");
            Console.ReadLine();

        }

        public static string ToCsv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SynergyUtilities syn = new SynergyUtilities();
            syn.UploadToDatabase("TRUNCATE lives;");
            string[] filebox = new string[] { @"R:\Daily Work Folders\Website Uploads\WebsiteUpload.csv" };
            foreach (string fil in filebox)
            {
                var dt = GetWebsite(fil);
                if (dt.Rows.Count > 0)
                {
                    var MyCsv = ToCsv(dt);
                    System.IO.File.WriteAllText(@"R:\Daily Work Folders\Website Uploads\website_temp.csv", MyCsv);
                    MyMySQLConnector(@"R:\Daily Work Folders\Website Uploads\website_temp.csv", "lives");
                    System.Windows.Forms.MessageBox.Show("The Ratebook " + fil + " was uploaded successfully with " + dt.Rows.Count + " lines.");
                }
            }
        }

        private static DataTable GetWebsite(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    //    foreach (string column in colFields)
                    //    {
                    //        DataColumn datecolumn = new DataColumn(column);
                    //        datecolumn.AllowDBNull = true;
                    //        csvData.Columns.Add(datecolumn);
                    //    }
                    DataColumn cap = new DataColumn(colFields[0]);
                    cap.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(cap);
                    DataColumn term = new DataColumn();
                    term.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(term);
                    DataColumn miles = new DataColumn();
                    miles.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(miles);
                    DataColumn ir = new DataColumn();
                    ir.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(ir);
                    DataColumn finance = new DataColumn();
                    finance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(finance);
                    DataColumn maintenance = new DataColumn();
                    maintenance.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(maintenance);
                    DataColumn ppm = new DataColumn();
                    ppm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppm);
                    DataColumn ppmm = new DataColumn();
                    ppmm.DataType = System.Type.GetType("System.Decimal");
                    csvData.Columns.Add(ppmm);
                    DataColumn bulk = new DataColumn();
                    bulk.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(bulk);
                    DataColumn funder24 = new DataColumn();
                    funder24.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(funder24);
                    DataColumn funder36 = new DataColumn();
                    funder36.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(funder36);
                    DataColumn funder48 = new DataColumn();
                    funder48.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(funder48);
                    DataColumn searchtype = new DataColumn();
                    searchtype.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(searchtype);
                    DataColumn bodytype = new DataColumn();
                    bodytype.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(bodytype);
                    DataColumn bopts = new DataColumn();
                    bopts.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(bopts);
                    DataColumn popts = new DataColumn();
                    popts.DataType = System.Type.GetType("System.Int32");
                    csvData.Columns.Add(popts);
                    DataColumn options = new DataColumn();
                    options.DataType = System.Type.GetType("System.String");
                    csvData.Columns.Add(options);

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            //Console.WriteLine(csvData.Columns[0].ColumnName);
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData[0], 24, 5000, 3, fieldData[11], fieldData[204], 0, 0,fieldData[398],fieldData[122],fieldData[123],fieldData[124],fieldData[111],fieldData[1],fieldData[114],fieldData[115],fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 24, 8000, 3, fieldData[12], fieldData[205], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 24, 10000, 3, fieldData[13], fieldData[206], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 24, 15000, 3, fieldData[14], fieldData[207], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 24, 20000, 3, fieldData[15], fieldData[208], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 36, 5000, 3, fieldData[16], fieldData[213], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 36, 8000, 3, fieldData[17], fieldData[214], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 36, 10000, 3, fieldData[18], fieldData[215], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 36, 15000, 3, fieldData[19], fieldData[217], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 36, 20000, 3, fieldData[20], fieldData[219], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 48, 5000, 3, fieldData[21], fieldData[222], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 48, 8000, 3, fieldData[22], fieldData[223], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 48, 10000, 3, fieldData[23], fieldData[224], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 48, 15000, 3, fieldData[24], fieldData[226], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                        csvData.Rows.Add(fieldData[0], 48, 20000, 3, fieldData[25], fieldData[228], 0, 0, fieldData[398], fieldData[122], fieldData[123], fieldData[124], fieldData[111], fieldData[1], fieldData[114], fieldData[115], fieldData[116]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return csvData;
        }
    }
}
