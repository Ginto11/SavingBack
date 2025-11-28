using SavingBack.Models;

namespace SavingBack.Interfaces
{
    public interface ICorreoService
    {
        Task MensajeAdministradores(InfoMensaje infoMensaje);

        Task MensajeClientes(InfoMensaje infoMensaje, string correo);
    }
}
