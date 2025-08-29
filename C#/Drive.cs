using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using static Google.Apis.Drive.v3.DriveService;
using System.IO;
using System.Text.RegularExpressions;
using static Google.Apis.Requests.BatchRequest;


namespace Sello
{
    public partial class Drive : Form
    {
        private static readonly string[] Scopes = { "https://www.googleapis.com/auth/drive" };
        private static readonly string username = "cickasmurf@gmail.com";
        private static readonly string ApplicationName = "SelloDrive";
        private static readonly string ClientId = null; //Change this (xxxgoogleusercontent.com)
        private static readonly string RedirectUri = "http://127.0.0.1:3301/authorize";
        private static readonly string ClientSecret = null; //Change this (xxx-xxxxxxx)

        private string mainfile = @".\pawd.csv";
        private string mainfilename = "pawd";
        private string mainfileformat = ".csv";

        private static string AccessToken { get; set; }
        private static string RefreshToken { get; set; }

        public Drive(string args)
        {
            InitializeComponent();

            MainFunc(args);
        }


        private async Task MainFunc(string args)
        {
            try
            {
                // Configurar el servidor HTTP

                var listener = new HttpListener();

                listener.Prefixes.Add("http://127.0.0.1:3301/");
                listener.Start();
                AddPrompt("Servidor en ejecución en http://127.0.0.1:3301/authorize/");

                // Redirigir al usuario a la URL de autorización de Google OAuth
                var authorizationUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={ClientId}&redirect_uri={Uri.EscapeUriString(RedirectUri)}&response_type=code&scope={string.Join(" ", Scopes)}";
                AddPrompt($"Redirigiendo a: {authorizationUrl}");

                // Abrir la URL en el navegador predeterminado del usuario
                Process.Start(new ProcessStartInfo(authorizationUrl) { UseShellExecute = true });

                // Esperar la solicitud
                bool web = true;
                while (web)
                {
                    var context = await listener.GetContextAsync();
                    var request = context.Request;

                    // Verificar si es una solicitud de autorización de Google OAuth
                    if (request.Url.AbsoluteUri.StartsWith(RedirectUri))
                    {
                        // Obtener el código de autorización de la consulta
                        var authorizationCode = request.QueryString["code"];
                        AddPrompt("Código de autorización obtenido: " + authorizationCode);

                        // Intercambiar el código de autorización por un token
                        var userCredential = await ExchangeAuthorizationCodeForToken(authorizationCode);
                        AccessToken = userCredential.Token.AccessToken;
                        RefreshToken = userCredential.Token.RefreshToken;


                        AddPrompt("Token obtenido correctamente. AccessToken: " + AccessToken);
                        switch (args)
                        {
                            case "up":

                                try
                                {
                                    string fn = CreateFolder("Cicker Sello", userCredential);
                                    using (var fs = new FileStream(mainfile, FileMode.Open, FileAccess.Read))
                                    {
                                        UploadFile(fs, mainfilename, "text/plain", fn, DateTime.Now.ToString(), userCredential);
                                    }
                                    var lista = ListFiles(fn, userCredential);
                                    AddPrompt("---------------" + " Listado de archivos " + "---------------");
                                    foreach (var f in lista)
                                    {
                                        AddPrompt(f.Name + " - " + f.Id);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    AddPrompt("Error (MFs1), " + ex.Message);
                                    
                                }
                                

                                break;
                            case "down":
                                try
                                {
                                    string fn = CreateFolder("Cicker Sello", userCredential);
                                    var maxFile = GetFileIdWithMaxNumber(ListFiles(fn, userCredential));
                                    DownloadFile(maxFile, userCredential);
                                }
                                catch (Exception ex)
                                {
                                    AddPrompt("Error (MFs2), " + ex.Message);
                                }
                                
                                break;
                        }
                        AddPrompt("cerrando servidor");
                        // Define la página HTML como una cadena de texto en tu código
                        string errorPageContent = @"
                                                <!DOCTYPE html>
                                                <html lang=""es"">
                                                <head>
                                                    <meta charset=""UTF-8"">
                                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                                    <title>Sello</title>
                                                    <script>
                                                        document.addEventListener('DOMContentLoaded', function() {
                                                            var x = document.getElementById('t');
                                                            var i = 10;
                                                            setInterval(function() {
                                                                i--;
                                                                x.innerHTML = i;
                                                                if (i <= 0) {
                                                                    // Placeholder action when countdown reaches 0
                                                                    window.location.href = ""https://www.google.com"";
                                                                }
                                                            }, 1000); // Update every second
                                                        });
                                                    </script>
                                                </head>
                                                <body>
                                                    <h1>Operación realizada con éxito</h1>
                                                    <hr>
                                                    <p>Por favor, cierre esta ventana.</p>
                                                    <p>Redirigiendo en <b id=""t"">10</b> segundos</p>
                                                </body>
                                                </html>
                                            ";


                        context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        byte[] buffer = Encoding.UTF8.GetBytes(errorPageContent);
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.OutputStream.Close();
                        listener.Abort();
                        web = false;
                    }
                }
            }
            catch (Exception ex)
            {
                AddPrompt($"Error: {ex.Message}");
            }
        }
        private void AddPrompt(string prompt)
        {
            richTextBox1.Text += "\n" + prompt;
            richTextBox1.Text += "\n";
        }

        private static async Task<UserCredential> ExchangeAuthorizationCodeForToken(string authorizationCode)
        {

            try
            {
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret
                    },
                    Scopes = Scopes,
                    DataStore = new FileDataStore(username)
                });

                // Obtener el token almacenado para este usuario, si existe
                var tokenResponse = await flow.DataStore.GetAsync<TokenResponse>(username);

                if (tokenResponse == null)
                { }
                // Intercambiar el código de autorización por un token si no hay uno almacenado
                tokenResponse = await flow.ExchangeCodeForTokenAsync(username, authorizationCode, RedirectUri, System.Threading.CancellationToken.None);


                // Crear UserCredential utilizando el flujo de autorización y el token de acceso obtenido
                var userCredential = new UserCredential(flow, username, tokenResponse);
                return userCredential;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intercambiar el código de autorización por token\n Error:" + ex.Message);
            }
        }

        private static DriveService GetService(UserCredential credential)
        {

            var applicationName = ApplicationName;


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            return service;
        }
        public string CreateFolder(string folderName, UserCredential cred)
        {
            var service = GetService(cred);

            // Crear la solicitud para listar archivos
            var listRequest = service.Files.List();

            // Establecer el filtro para buscar la carpeta por su nombre y tipo MIME
            listRequest.Q = $"name = '{folderName}' and mimeType = 'application/vnd.google-apps.folder'";

            // Ejecutar la solicitud y obtener la lista de archivos
            var fileList = listRequest.Execute().Files;

            // Verificar si ya existe una carpeta con el mismo nombre
            var existingFolder = fileList.FirstOrDefault();

            if (existingFolder != null)
            {
                AddPrompt($"Ya existe una carpeta con el nombre '{folderName}'. ID: {existingFolder.Id}");
                return existingFolder.Id;
            }
            else
            {
                // Si no existe, crear la carpeta
                var driveFolder = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var request = service.Files.Create(driveFolder);
                var file = request.Execute();
                AddPrompt($"Carpeta '{folderName}' creada correctamente. ID: {file.Id}");
                return file.Id;
            }
        }


        public string UploadFile(Stream file, string fileName, string fileMime, string folder, string fileDescription, UserCredential cred)
        {
            DriveService service = GetService(cred);


            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + mainfileformat;
            driveFile.Description = fileDescription;
            driveFile.MimeType = fileMime;
            driveFile.Parents = new string[] { folder };


            var request = service.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";

            var response = request.Upload();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                throw response.Exception;
            }
            else
            {
                AddPrompt("Archivo " + driveFile.Name + " subido con éxito, id: " + request.ResponseBody.Id);
            }

            return request.ResponseBody.Id;
        }
        public void DownloadFile(string fileId, UserCredential cred)
        {
            DriveService service = GetService(cred); // Método para obtener el servicio de Google Drive
            string oldfilenewname = "pawd-old_" + DateTime.Now.ToString("yyyyMMddHHmmss") + mainfileformat;
            
            if (System.IO.File.Exists(mainfile))
            {
                try
                {
                    AddPrompt("archivo local " + mainfile + " ya existe, reenombrando a " + oldfilenewname);
                    System.IO.File.Move(mainfile, oldfilenewname);
                    if (System.IO.File.Exists(mainfile))
                    {
                        throw new Exception("(DFe2), mainfile sigue existiendo");
                    }

                }
                catch (Exception ex)
                {
                    AddPrompt("Error (DFe1), " + ex.Message);
                }
            }
            var request = service.Files.Get(fileId);
            using (var stream = new MemoryStream())
            {
                request.Download(stream);
                using (var fileStream = new FileStream(mainfile, FileMode.Create, FileAccess.Write))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        public IEnumerable<Google.Apis.Drive.v3.Data.File> ListFiles(string folder, UserCredential cred)
        {
            var service = GetService(cred);

            var fileList = service.Files.List();
            fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
            fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

            var result = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;
            do
            {
                fileList.PageToken = pageToken;
                var filesResult = fileList.Execute();
                var files = filesResult.Files;
                pageToken = filesResult.NextPageToken;
                result.AddRange(files);
            } while (pageToken != null);

            return result;
        }




        public string GetFileIdWithMaxNumber(IEnumerable<Google.Apis.Drive.v3.Data.File> fileList)
        {
            // Inicializar variables para el seguimiento del archivo con el número máximo
            string maxFileId = null;
            long maxNumber = 0;

            AddPrompt(fileList.Count() + " Archivos");

            foreach (var file in fileList)
            {
                // Acceder al nombre del archivo
                string fileName = file.Name;

                // Extraer el número del nombre del archivo
                string[] parts = fileName.Split("pawd");
                if (parts.Length == 2)
                {
                    string numberStr = parts[1].Split('.')[0];
                    if (long.TryParse(numberStr, out long number))
                    {
                        DateTime date_ = DateTime.ParseExact(number.ToString(), "yyyyMMddHHmmss", null);
                        string fDate_ = date_.ToString("dd/MM/yyyy");
                        AddPrompt("Fecha del archivo: " + fDate_ + " - - " + number + " - - " + maxNumber);
                        if (number > maxNumber)
                        {
                            maxNumber = number;
                            maxFileId = file.Id;
                        }
                    }
                    else
                    {
                        AddPrompt("No se pudo convertir '" + numberStr + "' a un número.");
                    }
                }
                else
                {
                    AddPrompt("El nombre del archivo no tiene el formato esperado.");
                }
            }

            if (maxNumber == 0)
            {
                throw new Exception("No se encontraron números válidos en los nombres de archivo.");
            }
            DateTime date = DateTime.ParseExact(maxNumber.ToString(), "yyyyMMddHHmmss", null);
            string fDate = date.ToString("dd/MM/yyyy");
            AddPrompt("Fecha del archivo final: " + fDate);
            return maxFileId;
        }


        public async Task<UserCredential> RenewAccessToken(UserCredential credential)
        {
            try
            {
                var clientSecrets = new ClientSecrets
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                };

                var tokenResponse = new TokenResponse
                {
                    RefreshToken = credential.Token.RefreshToken
                };

                var credentialWithToken = new UserCredential(new GoogleAuthorizationCodeFlow(
                    new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = clientSecrets
                    }),
                    username,
                    tokenResponse);

                await credentialWithToken.RefreshTokenAsync(CancellationToken.None);

                // Actualiza el token de acceso en el UserCredential original
                credential.Token.AccessToken = credentialWithToken.Token.AccessToken;
                credential.Token.ExpiresInSeconds = credentialWithToken.Token.ExpiresInSeconds;
                credential.Token.IssuedUtc = credentialWithToken.Token.IssuedUtc;
                AddPrompt("Token renovado: " + credential.Token.AccessToken);
                return credential;
            }
            catch (Exception ex)
            {
                AddPrompt("Error al renovar el token de acceso: " + ex.Message);
                return null;
            }
        }

    }
}

