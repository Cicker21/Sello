using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Sello
{

    public partial class contras : Form
    {
        public event EventHandler<string> ValorDevuelto;
        public contras()
        {
            InitializeComponent();
        }

        private void enviar(object sender, EventArgs e)
        {
            ValorDevuelto?.Invoke(this, "Valor desde Form2");
            this.Close();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            generar();
        }
        private string nums = "0123456789";
        private string mins = "abcçdefghijklmnñopqrstuvwxyz";
        private string mays = "ABCÇDEFGHIJKLMNÑOPQRSTUVWXYZ";
        private string espc = @"\!@#$%&/():¨·;_.ºª(){}[]+-<>'";
        private void generar()
        {
            int x = (int)numericUpDown1.Value;
            string maestra = "";
            string output = "";
            if (c_nums.Checked) { maestra += nums; }
            if (c_minus.Checked) { maestra += mins; }
            if (c_mayus.Checked) { maestra += mays; }
            if (c_espec.Checked) { maestra += espc; }

            if (noaleatorio.Checked)
            {
                if (noAleatorio_tb.Text.Length >= 5)
                {
                    notEngh_lbl.Visible = false;
                    string texto_base = noAleatorio_tb.Text;
                    string numerostextuales = "";
                    for (int i = 0; i < texto_base.Length; i++)
                    {
                        char c = texto_base[i];
                        if (nums.Contains(c))
                        {
                            numerostextuales += c;
                        }
                        else if (mins.Contains(c))
                        {
                            numerostextuales += mins.IndexOf(c);
                        }
                        else if (mays.Contains(c))
                        {
                            numerostextuales += mays.IndexOf(c);
                        }
                        else if (espc.Contains(c))
                        {
                            numerostextuales += espc.IndexOf(c);
                        }
                        else
                        {
                            throw new Exception("Caracter de mierda hijo de puta: " + c);
                        }
                    }
                    BigInteger num = BigInteger.Parse(numerostextuales);
                    string semilla = (num * x).ToString();
                    string mezcladito = MezclarCadena(maestra, x);
                    //MessageBox.Show("mezcla: " + mezcladito);
                    output = GenerarTexto(noAleatorio_tb.Text, semilla, x, mezcladito);
                }
                else
                {
                    notEngh_lbl.Visible = true;
                    return;
                }
            }
            else
            {
                notEngh_lbl.Visible = false;
                for (int i = 0; i < x; i++)
                {
                    int r = new Random().Next(0, maestra.Length);
                    char c = maestra[r];
                    output += c;
                }

            }
            t_pwd.Text = output;
        }
        static string GenerarTexto(string texto, string numero, int longitud, string caracteres)
        {
            string resultado = "";
            int indiceTexto = 0;
            int indiceNumero = 0;

            // Iterar hasta que alcancemos la longitud deseada
            for (int i = 0; i < longitud; i++)
            {
                // Calcular el índice en la cadena de caracteres
                int indiceCaracter = (int)(Char.GetNumericValue(numero[indiceNumero]) + texto[indiceTexto]) % caracteres.Length;

                // Agregar el carácter correspondiente al resultado
                resultado += caracteres[indiceCaracter];

                // Avanzar al siguiente carácter en el texto y el número
                indiceTexto = (indiceTexto + 1) % texto.Length;
                indiceNumero = (indiceNumero + 1) % numero.Length;
            }

            return resultado;
        }
        static string MezclarCadena(string cadena, int numero)
        {
            StringBuilder resultado = new StringBuilder();
            Random rnd = new Random(numero); // Usamos el número para inicializar el generador de números aleatorios

            int index = rnd.Next(0, cadena.Length); // Empezamos desde una posición aleatoria en la cadena
            int consecutivosNoAleatorios = 0;

            // Mezclamos la cadena
            for (int i = 0; i < cadena.Length; i++)
            {
                char caracter = cadena[index];

                // Agregamos el carácter a la cadena resultado
                resultado.Append(caracter);

                // Incrementamos el contador de caracteres consecutivos no aleatorios si es necesario
                if (EsCaracterNoAleatorio(caracter, numero))
                {
                    consecutivosNoAleatorios++;
                }
                else
                {
                    consecutivosNoAleatorios = 0;
                }

                // Si hay más de tres caracteres consecutivos no aleatorios, generamos un nuevo índice aleatorio
                if (consecutivosNoAleatorios > 3)
                {
                    index = rnd.Next(0, cadena.Length);
                    consecutivosNoAleatorios = 0;
                }
                else
                {
                    // Avanzamos al siguiente índice, asegurándonos de que esté dentro de los límites de la cadena
                    index = (index + numero) % cadena.Length;
                }
            }

            return resultado.ToString();
        }

        static bool EsCaracterNoAleatorio(char caracter, int numero)
        {
            // Verifica si el carácter está en una posición divisible por el número dado
            int indice = caracter - '0'; // Convertimos el carácter en su índice en la cadena original
            return indice % numero == 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            generar();
        }

        private void noaleatorio_CheckedChanged(object sender, EventArgs e)
        {
            if (noaleatorio.Checked)
            {
                noAleatorio_tb.Enabled = true;
            }
            else
            {
                noAleatorio_tb.Enabled = false;
                notEngh_lbl.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = t_pwd.Text;
                Clipboard.SetDataObject(s);
                new ToolTip().Show("Copiado al portapapeles!", this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y, 1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void noAleatorio_tb_TextChanged(object sender, EventArgs e)
        {
            generar();
        }

        private void alMenosUno(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            bool[] checks = { c_espec.Checked, c_mayus.Checked, c_minus.Checked, c_nums.Checked };
            int casillasMarcadas = 0;

            foreach (bool check in checks)
            {
                if (check)
                {
                    casillasMarcadas++;
                }
            }

            // Si se ha desmarcado una casilla
            if (!cb.Checked)
            {
                if (casillasMarcadas < 1)
                {
                    cb.Checked = true;
                }
            }
        }

        private void noAleatorio_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            string global_chars = nums + espc + mins + mays;
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            else if(!global_chars.Contains(e.KeyChar))
            {
                e.Handled = true;
                try
                {
                    new ToolTip().Show("Caracter invalido '" + e.KeyChar + "'", this, noAleatorio_tb.Location.X + noAleatorio_tb.Size.Width +15, noAleatorio_tb.Location.Y + noAleatorio_tb.Size.Height +10, 3000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }
            }
        }
    }
}
