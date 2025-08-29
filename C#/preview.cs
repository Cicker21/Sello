using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sello
{
    public partial class preview : Form
    {
        public preview(string Filepath, string password)
        {
            InitializeComponent();
            mainfile = Filepath;
            seed = password;

            dataGridView1.DataSource = returnW();
        }
        private string seed;
        private DataTable table = new DataTable();
        private string mainfile;
        private DataTable returnW()
        {
            


            string[] content;
            //DataRow workRow;
            try
            {
                if (formatter() != 0)
                {
                    content = File.ReadAllLines(mainfile);
                    try
                    {
                        string[] maincols = { "nun" }; ;
                        string[] cifs = { "non" };
                        for (int i = 0; i < content.Length; i++)
                        {

                            string[] data = content[i].Split(',');
                            switch (i)
                            {
                                case 0:

                                    maincols = data;

                                    DataRow mainRow = table.NewRow();
                                    foreach (string s in data)
                                    {
                                        table.Columns.Add(s);

                                    }
                                    break;
                                case 1:

                                    cifs = data;
                                    DataRow workRow;
                                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                    break;

                                default:

                                    DataRow newRow = table.NewRow();

                                    
                                        for (int j = 0; j < data.Length; j++)
                                        {
                                            string s = data[j];
                                            if (cifs[j] == "cif")
                                            {
                                                s = Cifrado.descifrar(data[j], seed);
                                            }
                                            newRow[maincols[j]] = s;
                                        }
                                        table.Rows.Add(newRow);
                                    

                                    break;
                            }
                        }

                    }

                    catch (System.Exception excpt)
                    {
                        if (excpt.Message.Contains("Padding") == true)
                        {
                            MessageBox.Show("Contraseña incorrecta");
                        }
                        else
                        {
                            MessageBox.Show("Error: " + excpt.Message);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error al leer el archivo: " + e.Message);
            }


            return table;

        }
        private int formatter()
        {
            if (File.Exists(mainfile))
            {
                string[] content = File.ReadAllLines(mainfile);
                int x = 0;
                int output = 0;
                try
                {
                    x = content[0].Count(c => c == ',');
                    foreach (string s in content)
                    {
                        if (s.Count(c => c == ',') != x)
                        {
                            MessageBox.Show("Error con la linea " + s + "\nFormato incorrecto");
                            return 0;
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return 0;
                }
                return x;
            }
            else
            {
                MessageBox.Show("no existe el archivo " + mainfile);
                return 0;
            }


        }
    }
}
