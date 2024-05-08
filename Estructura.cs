using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;

namespace Sello
{
    public partial class Estructura : Form
    {
        public Estructura(string contraseñaInput)
        {

            InitializeComponent();
            contraseñaMaestra = contraseñaInput;
            init();

            //dibujarT();
        }
        private string mainfile = @".\pawd.csv";
        string[] content;
        string contraseñaMaestra;

        string lastDataPath = "";

        private void init()
        {
            try
            {
                content = File.ReadAllLines(mainfile);
                string[] maincols = content[0].Split(',');
                string[] cifs = content[1].Split(',');

                dataGridView1.ColumnCount = 2;

                // Agregar las columnas personalizadas
                dataGridView1.Columns[0].Name = "Orden (Arrastrar y Soltar)";
                dataGridView1.Columns[1].Name = "Nuevo Nombre";

                // Agregar CheckBox después de agregar las filas
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "Cifrado";
                dataGridView1.Columns.Add(checkBoxColumn);

                // Agregar ComboBox después de agregar las filas
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.HeaderText = "Contenido";
                string[] opciones = maincols.Concat(new string[] { "(Fecha)", "Nuevo" }).ToArray();
                comboBoxColumn.Items.AddRange(opciones);
                dataGridView1.Columns.Add(comboBoxColumn);


                for (int i = 0; i < maincols.Length; i++)
                {
                    string[] row = new string[4];
                    //row[0] = i.ToString();
                    //row[1] = maincols[i];

                    //if (cifs[i] == "true")
                    //{
                    //    row[2] = "true";
                    //}
                    //else
                    //{
                    //    row[2] = "false";
                    //}
                    //row[3] = maincols[i];
                    dataGridView1.Rows.Add(row);
                }
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.Columns[0].ReadOnly = true;

            }
            catch (Exception excpt) { MessageBox.Show("error: " + excpt.Message); }

        }



        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int csize = dataGridView1.RowCount;
            int dsize = 0;

            try
            {

                using (StreamReader sr = new StreamReader(mainfile))
                {
                    // Lee la primera línea del archivo
                    dsize = sr.ReadLine().Split(',').Length;
                }
                DataGridViewRow row = dataGridView1.Rows[csize - 1];
                if (csize > dsize)
                {
                    row.Cells[0].Value = csize - 1;
                    row.Cells[1].Value = "Introduzca Nombre";
                    row.Cells[3].Value = "Nuevo";

                }
                else
                {
                    int index = csize - 1;
                    string[] valor = content[0].Split(",");
                    string[] valor2 = content[1].Split(",");
                    row.Cells[0].Value = index;
                    row.Cells[1].Value = valor[index];
                    if (valor2[index] == "true")
                    {
                        row.Cells[2].Value = true;
                    }
                    else
                    {
                        row.Cells[2].Value = false;
                    }

                    row.Cells[3].Value = valor[index];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(csize + "\n" + dsize + "\n" + ex.Message);
            }
        }
        private bool VerificarRepetidos(DataGridViewColumn columna, out string valorRepetido)
        {
            valorRepetido = "";
            // Obtener los valores de la columna especificada
            var valoresColumna = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Select(row => row.Cells[columna.Index].Value.ToString())
                .ToList();

            // Verificar si hay valores repetidos
            var repetidos = valoresColumna.GroupBy(x => x).Where(g => g.Count() > 1).FirstOrDefault();
            if (repetidos != null)
            {
                valorRepetido = repetidos.Key;
                return true;
            }
            return false;
        }
        private bool VerificarValoresVacios(DataGridViewColumn columna)
        {
            // Verificar si hay valores vacíos en la columna especificada
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[columna.Index].Value == null || string.IsNullOrWhiteSpace(row.Cells[columna.Index].Value.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        private string ReorganizarArchivo(string[] texto, string[] numeros)
        {
            string[] texto_ = { "a", "b", "c", "d", "e", "f" };
            int[] numeros_ = { 0, 2, 3, 5, 4, 1 };

            // Ordenar el array de texto según el array de números
            string resultado = string.Join(",", numeros
            .Zip(texto, (num, txt) => new { Num = num, Txt = txt }) // Combinar los dos arrays
            .OrderBy(pair => pair.Num) // Ordenar por los números
            .Select(pair => pair.Txt) // Seleccionar solo el texto
            .ToArray()); // Convertir de nuevo a un array

            return resultado;
        }

        private void refactorizar(string primera, string segunda, string[] contenidoCols)
        {

        }

        static bool EsCaracterBase64(char c)
        {
            return (c >= 'A' && c <= 'Z') ||
                   (c >= 'a' && c <= 'z') ||
                   (c >= '0' && c <= '9') ||
                   c == '+' || c == '/' || c == '=';
        }

        static bool EsBase64(string cadena)
        {
            try
            {
                if (cadena.Length < 1)
                {
                    return false;
                }

                // Verifica si la cadena es null o tiene una longitud que no es múltiplo de 4
                if (cadena == null || cadena.Length % 4 != 0)
                {
                    return false;
                }

                // Verifica si la cadena contiene sólo caracteres válidos para Base64
                foreach (char c in cadena)
                {
                    if (!EsCaracterBase64(c))
                    {
                        return false;
                    }
                }

                // Verifica si la cadena termina con uno o dos caracteres de relleno "="
                int longitud = cadena.Length;
                if (cadena[longitud - 1] != '=')
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("omitiendo '" + cadena + "'\n" + ex.Message);
                return true;
            }
        }

        private void crear(object sender, EventArgs e)
        {
            if (File.Exists(mainfile))
            {

                List<string> DGVList = new List<string>();
                string newCols = "";
                string newCifs = "";
                string newContents = "";

                string[] originalDoc = File.ReadAllLines(mainfile);
                string[] oldCols = originalDoc[0].Split(','); // col1,col2,col3
                bool[] missingCols = new bool[oldCols.Length];

                foreach (DataGridViewRow row in dataGridView1.Rows) // Sacar datos necesarios de la tabla y factorizarlos
                {
                    if (!row.IsNewRow)
                    {
                        string orden = row.Cells[0].Value.ToString();
                        string nuevoNombre = row.Cells[1].Value.ToString();
                        string cifrado = "";
                        if (row.Cells[2].Value == null) { cifrado = "false"; } // Evitamos excepciones
                        else { cifrado = row.Cells[2].Value.ToString().ToLower(); }
                        string contenido = row.Cells[3].Value.ToString();

                        string final = string.Join(",", orden, nuevoNombre, cifrado, contenido);
                        DGVList.Add(final);

                        newCols += nuevoNombre + ",";
                        newContents += contenido + ",";
                        newCifs += cifrado + ",";

                        int index = Array.IndexOf(oldCols, contenido);
                        if (index >= 0) { missingCols[index] = true; }
                    }

                }
                string lostCols = "Los siguientes datos no fueron incluidos\n y se perderán en el nuevo documento:\n";
                if (missingCols.Contains(false))
                {
                    for(int i = 0;i<missingCols.Length;i++)
                    {
                        if (missingCols[i] == false)
                        {
                             lostCols += "\n\t"+oldCols[i];
                        }
                    }
                    MessageBox.Show(lostCols,"AVISO");
                }
                
                newCols = newCols.Substring(0,newCols.Length-1);
                newContents = newContents.Substring(0,newContents.Length-1);
                newCifs = newCifs.Substring(0,newCifs.Length-1);

                string[] newDoc = new string[originalDoc.Length];
                string[] newCifs_ = newCifs.Split(',');
                string[] newContent = newContents.Split(",");
                string[] newCols_ = newCols.Split(",");

                //MessageBox.Show("newCifs:\n" + newCifs + "\nnewCols:\n" + newCols);

                for (int i = 0; i < originalDoc.Length; i++)
                {
                    string linea = originalDoc[i].ToString();
                    string[] datos = linea.Split(',');
                    switch (i)
                    {
                        case 0:
                            newDoc[i] = newCols;
                            break;
                        case 1:
                            newDoc[i] = newCifs;
                            break;

                        default:
                            string lineaFac = "";
                            for (int j = 0; j < newCols_.Length; j++)
                            {
                                if (Array.IndexOf(oldCols, newContent[j])<0) //Es dato nuevo
                                {
                                    switch (newContent[j])
                                    {
                                        case "(Fecha)":
                                            DateTime DTN = DateTime.Now;
                                            string fecha = DTN.ToString("dd/MM/yyyy");
                                            lineaFac += fecha + ',';

                                            break;
                                        case "Nuevo":
                                            lineaFac += ',';
                                            break;
                                        default:
                                            MessageBox.Show("que coño es " + newContent[j]+ "\n"+ string.Join(',',oldCols) + " no contiene " + newCols_[j]);
                                            break;
                                    }
                                }
                                else {
                                    int index = Array.IndexOf(oldCols, newContent[j]);
                                    
                                    if (newCifs_[j] == "true") // Tiene que estar cifrado
                                    {
                                        if (EsBase64(datos[index]))
                                        {
                                            lineaFac += datos[index]+',';
                                        }
                                        else
                                        {
                                            lineaFac += Cifrado.cifrar(datos[index], contraseñaMaestra)+',';
                                        }
                                    }
                                    else //No tiene que estar cifrado
                                    {
                                        if (EsBase64(datos[index]))
                                        {
                                            lineaFac += Cifrado.descifrar(datos[index], contraseñaMaestra) + ',';
                                        }
                                        else
                                        {
                                            lineaFac += datos[index] + ',';
                                        }
                                    }
                                }
                            }
                            lineaFac = lineaFac.Substring(0, lineaFac.Length - 1);
                            newDoc[i] = lineaFac;
                            break;
                    }

                }
                Random random = new Random();
                random.Next(10000, 99999);
                string archivoTemp = Path.Combine(Path.GetTempPath(), "cicker_"+ random.Next(10000, 99999) +".txt");
                File.WriteAllLines(archivoTemp, newDoc);
                lastDataPath = archivoTemp;
                preview pw = new preview(lastDataPath, contraseñaMaestra);
                //Process.Start("notepad.exe", archivoTemp);
                pw.ShowDialog();
                confirm_b.Enabled = validar();

            }
        }

        private bool validar()
        {
            if (VerificarValoresVacios(dataGridView1.Columns[0]))
            {
                MessageBox.Show("Error, celdas vacías en el orden\nTrate de ordenarlas pulsando en [Orden] para buscar dicha celda");
                return false;
            }
            if (VerificarValoresVacios(dataGridView1.Columns[1]))
            {
                MessageBox.Show("Error, celdas vacías en los nombres\nTrate de ordenarlas pulsando en [Nombre] para buscar dicha celda");
                return false;
            }

            string valorRepetido = "";
            if (VerificarRepetidos(dataGridView1.Columns[0], out valorRepetido))
            {
                MessageBox.Show("Error, orden inválido, numeros repetidos\nValor repetido: " + valorRepetido);
                return false;
            }
            if (VerificarRepetidos(dataGridView1.Columns[1], out valorRepetido))
            {
                MessageBox.Show("Error, nombres inválidos, nombres repetidos\nValor repetido: " + valorRepetido);
                return false;
            }
            return true;
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1) // Índice de la columna 2 (Columna2)
            {
                string valor = e.FormattedValue.ToString();
                // Expresión regular para permitir solo letras mayúsculas, letras minúsculas, números, espacios y letras con acentos
                Regex regex = new Regex(@"^[\p{L}0-9 ]+$");

                if (!regex.IsMatch(valor))
                {
                    e.Cancel = true; // Cancela la edición si el valor no cumple con la expresión regular
                    MessageBox.Show("El valor no puede contener símbolos. Solo se permiten letras y números.");
                }
            }
        }

        private void reset(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            init();
            confirm_b.Enabled = false;
        }

        private void nuevaFila(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            confirm_b.Enabled = false;
        }

        private void borrarFila(object sender, EventArgs e)
        {
            confirm_b.Enabled = false;
            // Obtener el DataTable del DataGridView
            if (dataGridView1.Rows.Count > 0)
            {
                // Obtener el índice de la última fila
                int lastIndex = dataGridView1.Rows.Count - 1;

                // Remover la última fila del DataGridView
                dataGridView1.Rows.RemoveAt(lastIndex);

            }
        }

        private void confirmar(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(lastDataPath))
                {
                    DialogResult resultado = MessageBox.Show("¿Quieres continuar?\nTodos tus viejos datos serán borrados y reemplazados con los anteriormente mostrados.", "Confirmación", MessageBoxButtons.OKCancel);
                    if (resultado == DialogResult.OK)
                    {

                        string nuevoContenido = File.ReadAllText(lastDataPath);

                        // Reemplazar el contenido del archivo
                        File.WriteAllText(mainfile, nuevoContenido);
                        reset(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("No existe archivo de datos, por favor cree otro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Verificar si se hizo clic en el encabezado de la primera columna (índice 0)
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Obtener el valor de la celda en la columna 0
                    object valorCelda = row.Cells[0].Value;

                    // Verificar si el valor de la celda se puede convertir a int
                    if (valorCelda != null && Int32.TryParse(valorCelda.ToString(), out int valorEntero))
                    {
                        // Si se puede convertir, asignar el valor como un entero
                        row.Cells[0].Value = valorEntero;
                    }
                }

                // Ordenar la columna alfabéticamente
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
        }
        private int cellIn = -1; // Variable para almacenar el índice de la fila donde se hizo clic

        private void cellUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                int cellUpIndex = e.RowIndex; // Almacena el índice de la fila donde se soltó el clic

                if (cellIn != -1 && cellIn != cellUpIndex)
                {
                    IntercambiarFilas(cellIn, cellUpIndex);
                    cellIn = -1; // Reinicia la variable para evitar intercambios involuntarios
                }
            }
        }

        private void cellDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                cellIn = e.RowIndex; // Almacena el índice de la fila donde se hizo clic
            }
        }

        private void IntercambiarFilas(int rowIndex1, int rowIndex2)
        {
            if (rowIndex1 == rowIndex2) return; // No hacer nada si se intenta intercambiar con la misma fila

            DataGridViewRow row1 = dataGridView1.Rows[rowIndex1];
            DataGridViewRow row2 = dataGridView1.Rows[rowIndex2];

            // Copiar la fila 1 en una fila temporal
            DataGridViewRow tempRow = (DataGridViewRow)row1.Clone();
            for (int i = 0; i < row1.Cells.Count; i++)
            {
                tempRow.Cells[i].Value = row1.Cells[i].Value;
            }

            // Copiar la fila 2 en la fila 1
            for (int i = 0; i < row2.Cells.Count; i++)
            {
                row1.Cells[i].Value = row2.Cells[i].Value;
            }

            // Copiar la fila temporal en la fila 2
            for (int i = 0; i < tempRow.Cells.Count; i++)
            {
                row2.Cells[i].Value = tempRow.Cells[i].Value;
            }
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                dgr.Cells[0].Value = dgr.Index;
            }
        }
    }
}
