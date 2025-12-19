using SavingBack.Interfaces;
using SavingBack.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SavingBack.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration configuration;

        public CorreoService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task MensajeAdministradores(InfoMensaje infoMensaje)
        {

            try
            {
                string cuerpo = $@"
                <!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body style='font-family: Arial, sans-serif; background-color: #f5f7fa; padding: 20px;'>
                    <div style='max-width: 600px; margin: auto; background: #ffffff; border-radius: 8px; padding: 20px; box-shadow: 0 0 10px rgba(0,0,0,0.08);'>

                        <h2 style='color: #2a6f97; text-align:center;'>¡Nuevo movimiento en tu ahorro!</h2>

                        <p style='font-size: 16px; color: #333;'>
                            Hola <strong>Admin</strong>,
                        </p>

                        <p style='font-size: 16px; color: #333;'>
                            Te informamos que un usuario ha registrado un nuevo aporte a tu ahorro:
                        </p>

                        <div style='background:#e8f4fb; border-left: 4px solid #2a6f97; padding: 12px 16px; margin: 20px 0;'>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                💰 <strong>Monto añadido:</strong> {infoMensaje.MontoAhorro.ToString("#,0.00")}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                🏦 <strong>Descripción ahorro:</strong> {infoMensaje.DescripcionAhorro}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                📅 <strong>Fecha:</strong> {infoMensaje.FechaAhorro:dd/MM/yyyy HH:mm}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                📝 <strong>Nombre meta:</strong> {infoMensaje.NombreMetaAhorro}
                            </p>
                        </div>

                        <p style='font-size: 16px; color:#333;'>
                            ¡Sigue así! Cada aporte te acerca más a tu meta. 🚀
                        </p>

                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #ddd;' />

                        <p style='text-align:center; font-size: 13px; color: #666;'>
                            Este mensaje fue generado automáticamente por la aplicación de ahorro.
                        </p>
                    </div>
                </body>
                </html>";


                var emailEmisor = configuration.GetValue<string>("CONFIGURACION_EMAIL:EMAIL");
                var password = configuration.GetValue<string>("CONFIGURACION_EMAIL:PASSWORD");
                var host = configuration.GetValue<string>("CONFIGURACION_EMAIL:HOST");
                var puerto = configuration.GetValue<int>("CONFIGURACION_EMAIL:PUERTO");

                var smtpCliente = new SmtpClient(host, puerto);
                smtpCliente.EnableSsl = true;
                smtpCliente.UseDefaultCredentials = false;
                smtpCliente.Credentials = new NetworkCredential(emailEmisor, password);

                var mensaje = new MailMessage();
                mensaje.From = new MailAddress(emailEmisor!, "Saving Front");
                mensaje.To.Add("salinitosnelson@gmail.com");
                mensaje.To.Add("Mateoa1044606010@gmail.com");
                mensaje.Subject = "Nuevo depósito";
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = true;

                await smtpCliente.SendMailAsync(mensaje);

            }
            catch (Exception)
            {
                throw;
            }

            

        }

        public Task MensajeClientes(InfoMensaje infoMensaje, string correo)
        {
            try
            {
                string cuerpo = $@"
                <!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body style='font-family: Arial, sans-serif; background-color: #f5f7fa; padding: 20px;'>
                    <div style='max-width: 600px; margin: auto; background: #ffffff; border-radius: 8px; padding: 20px; box-shadow: 0 0 10px rgba(0,0,0,0.08);'>

                        <h2 style='color: #2a6f97; text-align:center;'>¡Nuevo movimiento en tu ahorro!</h2>

                        <p style='font-size: 16px; color: #333;'>
                            Hola <strong> {infoMensaje.NombreUsuariAhorro} </strong>,
                        </p>

                        <p style='font-size: 16px; color: #333;'>
                            Te informamos que has registrado un nuevo aporte a tu ahorro:
                        </p>

                        <div style='background:#e8f4fb; border-left: 4px solid #2a6f97; padding: 12px 16px; margin: 20px 0;'>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                💰 <strong>Monto añadido:</strong> {infoMensaje.MontoAhorro.ToString("#,0.00")}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                🏦 <strong>Descripción ahorro:</strong> {infoMensaje.DescripcionAhorro}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                📅 <strong>Fecha:</strong> {infoMensaje.FechaAhorro:dd/MM/yyyy HH:mm}
                            </p>
                            <p style='margin: 5px 0; font-size: 15px;'>
                                📝 <strong>Nombre meta:</strong> {infoMensaje.NombreMetaAhorro}
                            </p>
                        </div>

                        <p style='font-size: 16px; color:#333;'>
                            ¡Sigue así! Cada aporte te acerca más a tu meta. 🚀
                        </p>

                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #ddd;' />

                        <p style='text-align:center; font-size: 13px; color: #666;'>
                            Este mensaje fue generado automáticamente por la aplicación de ahorro.
                        </p>
                    </div>
                </body>
                </html>";

                var emailEmisor = configuration.GetValue<string>("CONFIGURACION_EMAIL:EMAIL");
                var host = configuration.GetValue<string>("CONFIGURACION_EMAIL:HOST");
                var puerto = configuration.GetValue<int>("CONFIGURACION_EMAIL:PUERTO");
                var password = configuration.GetValue<string>("CONFIGURACION_EMAIL:PASSWORD");

                var smtpCliente = new SmtpClient(host, puerto);
                smtpCliente.EnableSsl = true;
                smtpCliente.UseDefaultCredentials = false;
                smtpCliente.Credentials = new NetworkCredential(emailEmisor, password);

                var mensaje = new MailMessage();
                mensaje.From = new MailAddress(emailEmisor!, "Saving Front");
                mensaje.To.Add(correo);
                mensaje.Subject = "Nuevo depósito";
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = true;

                return smtpCliente.SendMailAsync(mensaje);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
