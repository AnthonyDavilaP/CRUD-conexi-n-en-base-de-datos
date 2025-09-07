using System;
using System.Windows.Forms;
using System.Linq;
using WinFormsEF6Demo.Data;
using WinFormsEF6Demo.Models;
using WinFormsEF6Demo.Utils;

namespace WinFormsEF6Demo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar/sembrar BD (demo)
            using (var db = new AppDb())
            {
                var c = db.Database.Connection;
                if (c.State == System.Data.ConnectionState.Closed) c.Open();
                MessageBox.Show($"Servidor: {c.DataSource}\nBD: {c.Database}\nUsuarios: {db.Usuarios.Count()}");
            
                db.Database.Initialize(false);
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                if (!db.Usuarios.Any())
                {
                    db.Usuarios.Add(new Usuario
                    {
                        Login = "admin",
                        PasswordHash = PasswordHasher.Hash("123"),
                        Estado = "A"
                    });
                    db.SaveChanges();
                }
            }
            Application.Run(new Forms.FormLogin());
        }
    }
}
