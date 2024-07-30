
namespace Sello
{
    using Newtonsoft.Json.Linq;
    using System;

    public partial class Inicio : Form
    {
        static string mainfile = @".\pawd.csv";

        public Inicio()
        {
            InitializeComponent();

        }
        private async Task lastVersion()
        {
            string output = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");

                string url = $"https://api.github.com/repos/Cicker21/Sello/releases/latest";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject release = JObject.Parse(responseBody);
                string tagName = release["tag_name"].ToString();
                output = tagName;
            }
            if (compV(output, this.Text))
            {

                label2.Text = "Hay una nueva versi�n disponible, " + output;
            }
            else
            {
                label2.Text = "Tienes la version mas reciente, " + output;
            }
        }
        private bool compV(string git, string local)
        {
            git = git.Split(git.Split('.')[0])[1].Substring(1);
            local = local.Split(local.Split('.')[0])[1].Substring(1);

            //MessageBox.Show(git + " " + local);
            int[] gitV = Array.ConvertAll(git.Split('.'), int.Parse);
            int[] localV = Array.ConvertAll(local.Split('.'), int.Parse);

            string log = "";
            // Comparar las versiones
            for (int i = 1; i < gitV.Length; i++)
            {
                if (gitV[i] > localV[i])
                {
                    //MessageBox.Show(gitV[i] + ">" + local[i] + "\n");
                    return true; //hay update
                }
                else if (gitV[i] < localV[i])
                {
                    //MessageBox.Show(gitV[i] + "<" + local[i] + "\n");
                    return false; //no hay update

                }

            }
            return false; //no hay update, son iguales
        }
        public void validar(object sender, EventArgs e)
        {

            Main a = new Main(textBox1.Text);
            this.Hide();
            a.ShowDialog();
            this.Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validar(sender, e);
            }
        }

        private void ocultar(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Main a = new Main(textBox1.Text);
            this.Hide();
            a.ShowDialog();
            this.Close();
        }

        private async void Inicio_Load(object sender, EventArgs e)
        {
            await lastVersion();

            if (!File.Exists(mainfile))
            {


                DialogResult resultado = MessageBox.Show("El archivo no existe.\n�Tienes copia de seguridad en Google Drive?\n\nEn caso de no tener crearemos un nuevo archivo local.", "Recuperar Datos", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    Drive df = new Drive("down");
                    df.ShowDialog();
                }
                else
                {
                    string contrase�aMaestra = "";
                    int longitudMinima = 5;
                    while (contrase�aMaestra.Length < longitudMinima)
                    {
                        string contrase�aInput = Microsoft.VisualBasic.Interaction.InputBox("En el siguiente campo pondr�s una contrase�a\npara asegurar todos tus datos.\n\nDicha contrase�a deber�s recordarla o anotarla en alg�n lugar seguro.\n\nPodr�s cambiarla en cualquier momento, pero si la olvidas, tus datos no se podr�n recuperar.", "Nueva Contrase�a Maestra", "Editame");

                        if (string.IsNullOrEmpty(contrase�aInput))// El usuario cancel� la entrada
                        {
                            MessageBox.Show("La operaci�n ha sido cancelada.");
                            this.Close();
                            return;
                        }

                        if (contrase�aInput.Length < longitudMinima)
                        {
                            MessageBox.Show("La contrase�a debe tener al menos " + longitudMinima + " caracteres.");
                        }
                        else if (contrase�aInput != "Editame")
                        {
                            contrase�aMaestra = contrase�aInput;
                            MessageBox.Show("La contrase�a maestra ha sido establecida correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("Por favor, edita la contrase�a.");
                        }
                    }
                    string[] firstMainFile = { "Web,Usuario,Contrase�a,Fecha", "false,false,true,date" };

                    using (FileStream fs = File.Create(mainfile)) ;
                    File.WriteAllLines(mainfile, firstMainFile);

                    Main mf = new Main(contrase�aMaestra);

                    mf.ShowDialog();

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            tabla.Visible = false;
        }

    }
}