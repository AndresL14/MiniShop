using MiniShop.WFClient;
using MiniShop.WFClient.Forms.Login;

namespace MiniShop.WinFormsClient
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Mostrar login primero
            using (var loginForm = new FrmLogin())
            {
                var result = loginForm.ShowDialog();

                // Si el usuario se autentica correctamente, abre la app
                if (result == DialogResult.OK && !string.IsNullOrEmpty(loginForm.Token))
                {
                    Application.Run(new FrmMain(loginForm.Token));
                }
                else
                {
                    // Si cancela o falla el login, se cierra
                    Application.Exit();
                }
            }
        }
    }
}
