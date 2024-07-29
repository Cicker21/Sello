using System.Data;
using System.Drawing.Text;


namespace Sello
{
    public partial class Main : Form
    {
        private string seed;
        private System.Windows.Forms.Timer timer;
        private PrivateFontCollection fontCollection;
        public bool[] circulitos_c;

        public Main(string seed_)
        {
            gf = Program.GetCustomFont(Properties.Resources.Circulitos, 12F, FontStyle.Regular);

            seed = seed_;
            InitializeComponent();
            dataGridView1.DataSource = returnW("");
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;

        }

        private DataTable table = new DataTable();
        private string mainfile = @".\pawd.csv";


        private void Timer_Tick(object sender, EventArgs e)
        {
            // Detiene el temporizador
            timer.Stop();

            // Ejecuta la función una vez que el temporizador ha terminado
            dataGridView1.DataSource = returnW(textBox1.Text);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (textBox1.Text.Length > 0)
                {

                    for (int l = 0; l < 3; l++)
                    {
                        string s = dataGridView1.Rows[i].Cells[l].Value.ToString();
                        if (s.Contains(textBox1.Text) == true && textBox1.Text.Length > 0)
                        {
                            dataGridView1.Rows[i].Cells[l].Style.BackColor = Color.Red;
                            dataGridView1.Rows[i].Cells[l].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[l].Style.BackColor = Color.White;
                            dataGridView1.Rows[i].Cells[l].Style.ForeColor = Color.Black;
                        }
                    }

                }

            }
        }

        private DataTable returnW(string diff)
        {
            table = new DataTable(); //reinicia la tabla


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

                                    if (diff.Length > 0)
                                    {
                                        bool valido = false;
                                        for (int j = 0; j < data.Length; j++)
                                        {
                                            string s = data[j];
                                            if (cifs[j] == "true")
                                            {
                                                s = Cifrado.descifrar(data[j], seed);
                                            }
                                            newRow[maincols[j]] = s;
                                            if (s.Contains(diff)) { valido = true; }
                                        }
                                        if (valido)
                                        {
                                            table.Rows.Add(newRow);
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < data.Length; j++)
                                        {
                                            string s = data[j];
                                            if (cifs[j] == "true")
                                            {
                                                s = Cifrado.descifrar(data[j], seed);
                                            }
                                            newRow[maincols[j]] = s;
                                        }
                                        table.Rows.Add(newRow);
                                    }

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
                            MessageBox.Show("Error 164: " + excpt.Message);
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

        private void refresh()
        {
            if (autoup_cb.Checked) { dataGridView1.DataSource = returnW(textBox1.Text); }

        }
        private Font gf;

        private void circulitos(int index, bool update)
        {
            if (update)
            {
                circulitos_c[index] = !circulitos_c[index];
            }
            try
            {
                string[] data = File.ReadAllLines("settings.cfg");
                data[1] = "hiddenCols:" + string.Join(",", circulitos_c);
                File.WriteAllLines("settings.cfg", data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error 188:" + ex.Message);
            }
        }

        private void toast(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    circulitos(e.ColumnIndex, true);
                    dataGridView1.Invalidate();
                }
                else
                {
                    string s = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    Clipboard.SetDataObject(s);
                    new ToolTip().Show("Copiado al portapapeles!", this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y, 1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error toast " + ex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
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
                catch (Exception exx)
                {
                    MessageBox.Show("error 241:" + exx.Message);
                    return 0;
                }
                return x;
            }
            else
            {
                MessageBox.Show("No existe el archivo " + mainfile);
                return 0;
            }


        }
        private void añadir(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(mainfile))
                {
                    string[] lines = File.ReadAllLines(mainfile);
                    string[] cols = lines[0].Split(',');
                    string[] ctypes = lines[1].Split(",");

                    int datepos = Array.IndexOf(ctypes, "date");

                    string formres = CrearFormulario(null, cols, datepos);

                    string result = "";
                    if (formres != null)
                    {
                        string[] adicion = formres.Split(',');
                        for (int i = 0; i < ctypes.Length; i++)
                        {
                            string sum = adicion[i];
                            switch (ctypes[i])
                            {
                                case "true":
                                    result += Cifrado.cifrar(sum, seed) + ",";
                                    break;

                                default:
                                    result += sum + ",";
                                    break;
                            }
                        }
                        result = result.Substring(0, result.Length - 1);
                        using (StreamWriter writer = File.AppendText(mainfile))
                        {
                            writer.WriteLine(result);
                        }
                    }
                }
                else { MessageBox.Show("No existe el archivo " + mainfile); }
            }
            catch (Exception ex) { MessageBox.Show("error 300: " + ex.Message); }
            refresh();
        }
        private string CrearFormulario(string[] input, string[] cols, int date)
        {
            //string[] vals = input != null ? input.Split(',') : new string[cols.Length];Fedit
            string[] vals = { };
            if (input == null) { vals = new string[cols.Length]; }
            else { vals = input; }
            int cantidad = vals.Length;
            TextBox[] cajas = new TextBox[cantidad];

            // Crear una instancia de Form
            Form formulario = new Form();
            formulario.Text = "Cambiar Datos";
            formulario.Width = 250;
            formulario.FormBorderStyle = FormBorderStyle.Sizable;
            formulario.ShowIcon = false;
            // Definir el margen para los elementos interiores
            int margenHorizontal = 20;
            int margenVertical = 20;

            int posX = margenHorizontal;
            int posY = margenVertical;
            int separacion = 30;
            int tamañoEtiqueta = 80; // Ajusta el tamaño de la etiqueta
            int cajaTextoWidth = formulario.Width - tamañoEtiqueta - 3 * margenHorizontal; // Calcula el ancho de la caja de texto


            string output = null;

            // Crear botón 1 al final del formulario
            Button btn1 = new Button();
            btn1.Text = "Cancelar";
            btn1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom; // Anclar a la izquierda y abajo
            btn1.Location = new Point(margenHorizontal, formulario.ClientSize.Height - btn1.Height - margenVertical); // Ajusta la posición del botón

            btn1.Click += (sender, e) => { output = null; formulario.Close(); };
            // Crear botón 2 al final del formulario
            Button btn2 = new Button();
            btn2.Text = "Aceptar";
            btn2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom; // Anclar a la derecha y abajo
            btn2.Location = new Point(formulario.ClientSize.Width - btn2.Width - margenHorizontal, formulario.ClientSize.Height - btn2.Height - margenVertical); // Ajusta la posición del botón
            btn2.Click += (sender, e) =>
            {

                string output_ = "";
                foreach (TextBox tb in cajas)
                {
                    output_ += tb.Text + ",";
                }
                output_ = output_.Substring(0, output_.Length - 1);
                output = output_;
                formulario.Close();
            };

            // Agregar botones al formulario
            formulario.Controls.Add(btn1);
            formulario.Controls.Add(btn2);

            // Crear y agregar etiquetas y cajas de texto al formulario

            for (int i = 0; i < cantidad; i++)
            {
                // Crear etiqueta
                Label etiqueta = new Label();
                etiqueta.Text = cols[i] + ":";
                etiqueta.Location = new Point(posX, posY);
                etiqueta.Size = new Size(tamañoEtiqueta, etiqueta.Size.Height); // Ajusta el tamaño de la etiqueta

                // Crear caja de texto
                TextBox cajaTexto = new TextBox();
                cajaTexto.Location = new Point(posX + tamañoEtiqueta + 10, posY); // Ajusta la posición de la caja de texto
                cajaTexto.Width = 100;

                if (i == date)
                {
                    DateTime DTN = DateTime.Now;
                    string fecha = DTN.ToString("dd/MM/yyyy");

                    cajaTexto.Text = fecha;
                }
                else { cajaTexto.Text = vals[i]; }
                // Agregar controles al formulario
                formulario.Controls.Add(etiqueta);
                formulario.Controls.Add(cajaTexto);

                // Incrementar la posición en Y para el siguiente par de controles
                posY += separacion;

                cajas[i] = cajaTexto;
            }
            int min = posY + cajas[cantidad - 1].Height + margenVertical * 2 + btn2.Height + margenVertical;

            formulario.MinimumSize = new Size(250, min);
            formulario.MaximumSize = new Size(250, 1000);
            formulario.Size = formulario.MinimumSize;
            // Mostrar el formulario
            formulario.StartPosition = FormStartPosition.CenterParent;
            formulario.ShowDialog();

            while (formulario.Visible)
            {

            }
            return output;
        }


        private void borrar(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];

                int row = selectedCell.RowIndex;
                string[] cellValues = new string[dataGridView1.Rows[row].Cells.Count];

                // Iterar a través de todas las celdas de la fila
                for (int i = 0; i < dataGridView1.Rows[row].Cells.Count; i++)
                {
                    // Acceder al valor de la celda y almacenarlo en el arreglo
                    cellValues[i] = dataGridView1.Rows[row].Cells[i].Value.ToString();
                }
                string dialogMess = string.Join(" ", "¿Quieres borrar:", string.Join(", ", cellValues), "?");

                string dialog = MessageBox.Show(dialogMess, "Borrar Fila", MessageBoxButtons.YesNo).ToString();

                if (dialog == "Yes")
                {
                    try
                    {

                        string[] lines = File.ReadAllLines(mainfile);
                        List<string> linesL = lines.ToList();

                        string[] cifs = lines[1].Split(',');

                        for (int i = 0; i < cifs.Length; i++)
                        {
                            if (cifs[i] == "true")
                            {
                                cellValues[i] = Cifrado.cifrar(cellValues[i], seed);
                            }
                        }

                        string objetivo = string.Join(",", cellValues);
                        int index = Array.IndexOf(lines, objetivo);
                        if (index < 0)
                        {
                            MessageBox.Show("No se encuentra " + objetivo);
                        }
                        else
                        {
                            linesL.RemoveAt(index);

                            File.WriteAllLines(mainfile, linesL);
                            foreach (DataGridViewCell cell in dataGridView1.Rows[row].Cells)
                            {
                                cell.Value = "Fila Eliminada";
                            }
                            refresh();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No rows are selected, handle accordingly");
            }
        }

        private void generador(object sender, EventArgs e)
        {
            contras form = new contras();

            // Suscribir al evento de Form2 para manejar el valor devuelto
            form.ValorDevuelto += (senderForm2, valor) =>
            {
                // Manejar el valor devuelto
                MessageBox.Show("Valor devuelto desde Form2: " + valor);
            };

            // Mostrar Form2
            form.Show();


        }

        private void update(object sender, EventArgs e)
        {
            dataGridView1.DataSource = returnW(textBox1.Text);
        }

        private void edit(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Clicks == 2)
                {
                    if (File.Exists(mainfile))
                    {
                        try
                        {
                            string[] lines = File.ReadAllLines(mainfile);
                            string[] cols = lines[0].Split(',');
                            string[] ctypes = lines[1].Split(",");
                            string[] slices = dataGridView1.Rows[e.RowIndex].Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString()).ToArray();
                            string slices_ = "";
                            for (int i = 0; i < slices.Length; i++)
                            {
                                if (ctypes[i] == "true")
                                {
                                    slices_ += Cifrado.cifrar(slices[i], seed) + ',';
                                }
                                else
                                {
                                    slices_ += slices[i] + ',';
                                }
                            }
                            slices_ = slices_.Substring(0, slices_.Length - 1);

                            int datepos = Array.IndexOf(ctypes, "date");
                            int linepos = Array.IndexOf(lines, slices_);
                            if (linepos < 0)
                            {
                                MessageBox.Show("NO se encuentra:\n" + slices_);
                            }

                            string formres = CrearFormulario(slices, cols, datepos);
                            if (formres != null)
                            {
                                string[] formslice = formres.Split(",");
                                string formres_ = "";
                                for (int i = 0; i < formslice.Length; i++)
                                {
                                    if (ctypes[i] == "true")
                                    {
                                        formres_ += Cifrado.cifrar(formslice[i], seed) + ',';
                                    }
                                    else
                                    {
                                        formres_ += formslice[i] + ",";
                                    }
                                }
                                formres_ = formres_.Substring(0, formres_.Length - 1);

                                lines[linepos] = formres_;
                                File.WriteAllLines(mainfile, lines);
                                foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                                {
                                    cell.Value = formslice[cell.ColumnIndex];
                                }
                                refresh();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("error 600:" + ex.Message); }

                    }
                }
            }
        }

        private void modificar(object sender, EventArgs e)
        {
            Estructura formEst = new Estructura(seed);
            this.Hide();
            formEst.ShowDialog();
            this.Show();
            refresh();
        }

        private void drive(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    DialogResult result = MessageBox.Show("¿Desea guardar los datos en Google Drive?\nSe añadira el nuevo archivo sin modificar o borrar nigún otro.\n\n\n¿Desea continuar?", "Actualizar Backup", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Drive form = new Drive("up");
                        form.ShowDialog();
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DialogResult result = MessageBox.Show("¿Desea descargar los datos en Google Drive?\nSe sustituira el nuevo archivo por uno nuevo, sin eliminar el anterior.\n\n\n¿Desea continuar?", "Restaurar Backup", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Drive form = new Drive("down");
                        form.ShowDialog();
                    }
                }
            }
        }

        private string[] global_settings = { "autoupdate:false", "hiddenCols:false" };
        private void Main_Load(object sender, EventArgs e)
        {
            circulitos_c = new bool[dataGridView1.ColumnCount];
            try
            {
                if (File.Exists("settings.cfg"))
                {
                    global_settings = File.ReadAllLines("settings.cfg");
                    if (global_settings[0].Split(':')[1].ToLower() == "true")
                    {
                        autoup_cb.Checked = true;
                    }
                    string[] pre = global_settings[1].Split(':')[1].Split(',');
                    if (pre.Length == dataGridView1.ColumnCount)
                    {
                        bool[] newcircs = new bool[pre.Length];
                        for (int i = 0; i < pre.Length; i++)
                        {
                            if (pre[i].ToLower() == "true")
                            {
                                newcircs[i] = true;
                            }
                            else
                            {
                                newcircs[i] = false;
                            }
                        }
                        circulitos_c = newcircs;
                        circulitos(0, false);
                    }
                }
                else
                {
                    File.WriteAllLines("settings.cfg", global_settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 663:" + ex.Message);
            }
        }
        private void changeSettings(string name, string value)
        {
            try
            {
                string[] data = File.ReadAllLines("settings.cfg");
                for (int i = 0; i < data.Length; i++)
                {

                    string[] dataspl = data[i].Split(":");
                    if (dataspl[0] == name)
                    {
                        data[i] = dataspl[0] + ":" + value;
                        File.WriteAllLines("settings.cfg", data);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 690:" + ex.Message);
            }

        }
        private void autoup_cb_CheckedChanged(object sender, EventArgs e)
        {
            changeSettings("autoupdate", autoup_cb.Checked.ToString());
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && circulitos_c[e.ColumnIndex])
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center; // Alineación horizontal
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoWrap;

                    e.Handled = true;
                    e.PaintBackground(e.CellBounds, true);
                    e.Graphics.DrawString(e.FormattedValue.ToString(), gf, Brushes.Black, e.CellBounds, sf);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
                }
            }
            catch (Exception ez)
            {
                MessageBox.Show("array:" + string.Join(",", circulitos_c) + "\nindex: " + e.RowIndex + "\narrayL: " + circulitos_c.Length + "\ndatagrid: " + dataGridView1.ColumnCount + "\n\n" + ez.Message);
            }
        }
    }
}
