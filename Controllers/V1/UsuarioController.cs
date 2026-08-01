using Asp.Versioning;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Services;
using SavingBack.Utilities;
using System.Threading.Tasks;

namespace SavingBack.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly Utilidad utilidadService;
        private readonly IConfiguration config;
        private readonly IHostEnvironment environment;
        private readonly AhorroService ahorroService;
        private readonly EgresoService egresoService;
        private readonly IngresoService ingresoService;
        private readonly ReporteService reporteService;
        private readonly MetaAhorroService metaAhorroService;
        private readonly GraficaService graficaService;
        private readonly CorreoService correoService;
        private readonly PdfService pdfService;


        public UsuarioController(PdfService pdfService, CorreoService correoService, UsuarioService usuarioService, Utilidad utilidadService, IConfiguration config, IHostEnvironment environment, AhorroService ahorroService, ReporteService reporteService, EgresoService egresoService, IngresoService ingresoService, MetaAhorroService metaAhorroService, GraficaService graficaService)
        {
            this.utilidadService = utilidadService;
            this.usuarioService = usuarioService;
            this.config = config;
            this.environment = environment;
            this.ahorroService = ahorroService;
            this.reporteService = reporteService;
            this.egresoService = egresoService;
            this.ingresoService = ingresoService;
            this.metaAhorroService = metaAhorroService;
            this.graficaService = graficaService;
            this.correoService = correoService;
            this.pdfService = pdfService;
        }

        #region METODOS BASICOS
        [HttpPost]
        public async Task<ActionResult> Nuevo([FromBody] CrearUsuarioDto usuario)
        {
            try
            {
                usuario.Rol = "Cliente";
                usuario.Contrasena = utilidadService.Encriptar(usuario.Contrasena);

                await usuarioService.Insertar(usuario);

                if (environment.IsProduction())
                    await correoService.MensajeBienvenida(usuario, "https://saving-front.vercel.app/ingresar");

                if (environment.IsDevelopment())
                    await correoService.MensajeBienvenida(usuario, "http://localhost:4200/ingresar");

                return RespuestasService.Created();
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [Authorize]
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult> ObtenerUsuario(int usuarioId)
        {
            try
            {
                var usuario = await usuarioService.BuscarPorId(usuarioId);

                if (usuario is null)
                    return RespuestasService.ErrorModelo(this, "Usuario no encontrado", 404);

                return RespuestasService.Ok(usuario);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                var usuarios = await usuarioService.BuscarTodos();

                return Ok(usuarios);
            } catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{usuarioId}")]
        public async Task<ActionResult> ActualizarUsuario(int usuarioId, [FromForm] UsuarioDto usuario)
        {
            try
            {
                var usuarioExistente = await usuarioService.BuscarEntidadUsuarioPorId(usuarioId);
                if (usuarioExistente is null)
                    return RespuestasService.ErrorModelo(this, "Usuario no encontrado", 404);

                usuarioExistente.PrimerNombre = usuario.PrimerNombre;
                usuarioExistente.PrimerApellido = usuario.PrimerApellido;
                usuarioExistente.Cedula = usuario.Cedula;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
                usuarioExistente.ManejaGastos = usuario.ManejaGastos;

                if (usuario.NuevaFoto is not null)
                {

                    var nombreFoto = $"Foto{usuario.Cedula}{Path.GetExtension(usuario.NuevaFoto!.FileName)}";
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Fotos", nombreFoto);
                    using var stream = new FileStream(ruta, FileMode.Create);
                    usuarioExistente.FotoPerfil = $"/Uploads/Fotos/{nombreFoto}";
                    await usuario.NuevaFoto!.CopyToAsync(stream);

                }

                await usuarioService.Actualizar(usuarioExistente);

                return RespuestasService.Ok("Usuario actualizado exitosamente");
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
        #endregion

        #region METODOS AHORROS
        [HttpGet]
        [Route("{usuarioId}/ahorros")]
        public async Task<ActionResult> ObtenerAhorrosPorUsuario(int usuarioId, int paginaActual, int tamanoPagina, [FromQuery] string? descripcion = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(descripcion))
                {
                    var ahorros = await ahorroService.ObtenerAhorrosPorDescripcionPaginadosPorUsuarioId(usuarioId, paginaActual, tamanoPagina, descripcion);

                    return RespuestasService.Ok(ahorros);
                }

                if (paginaActual == 0)
                    return RespuestasService.ErrorModelo(this, "El numero de pagina debe ser mayor a 0", 409);


                var resultadoPagina = await ahorroService.ObtenerTodosLosAhorrosPaginadosPorUsuarioId(usuarioId, paginaActual, tamanoPagina);

                return RespuestasService.Ok(resultadoPagina);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/ahorros/totales")]
        public async Task<ActionResult> ObtenerTotalesPorUsuario(int usuarioId)
        {
            try
            {

                var cantidades = await ahorroService.ObtenerCantidadesTotalesPorUsuarioId(usuarioId);


                return RespuestasService.Ok(cantidades);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/ahorros/recientes")]
        public async Task<ActionResult> ObtenerUltimosMovimientos(int usuarioId)
        {
            try
            {

                var ultimos = await ahorroService.ObtenerUltimosMovimientosPorUsuarioId(usuarioId);


                return RespuestasService.Ok(ultimos);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/reportes/ahorros/excel")]
        public async Task<ActionResult> ExportarExcel(int usuarioId)
        {
            var datos = await reporteService.ObtenerDatos(usuarioId);

            using (var libroTrabajo = new XLWorkbook())
            {
                var hojaIngresos = libroTrabajo.Worksheets.Add("Ingresos");
                var hojaEgresos = libroTrabajo.Worksheets.Add("Egresos");
                var hojaAhorros = libroTrabajo.Worksheets.Add("Ahorros");

                // ================= INGRESOS =================
                if (datos.ListaIngresosReporteExcel is null || !datos.ListaIngresosReporteExcel.Any())
                {
                    hojaIngresos.Cell(1, 1).Value = "Sin datos";
                }
                else
                {
                    var tabla = hojaIngresos.Cell(1, 1)
                        .InsertTable(datos.ListaIngresosReporteExcel, "Ingresos", true);

                    tabla.Theme = XLTableTheme.TableStyleMedium2;

                    hojaIngresos.SheetView.FreezeRows(1);
                    hojaIngresos.Columns().AdjustToContents();

                    hojaIngresos.Column(4).Style.NumberFormat.Format = "$ #,##0";

                    int fila = datos.ListaIngresosReporteExcel.Count + 2;
                    hojaIngresos.Cell(fila, 1).Value = "TOTAL";
                    hojaIngresos.Cell(fila, 4).FormulaA1 = $"SUM(D2:D{fila - 1})";

                    var rango = hojaIngresos.Range(fila, 1, fila, 4);
                    rango.Style.Font.Bold = true;
                    rango.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                // ================= EGRESOS =================
                if (datos.ListaEgresosReporteExcel is null || !datos.ListaEgresosReporteExcel.Any())
                {
                    hojaEgresos.Cell(1, 1).Value = "Sin datos";
                }
                else
                {
                    var tabla = hojaEgresos.Cell(1, 1)
                        .InsertTable(datos.ListaEgresosReporteExcel, "Egresos", true);

                    tabla.Theme = XLTableTheme.TableStyleMedium3;

                    hojaEgresos.SheetView.FreezeRows(1);
                    hojaEgresos.Columns().AdjustToContents();

                    hojaEgresos.Column(6).Style.NumberFormat.Format = "$ #,##0";

                    int fila = datos.ListaEgresosReporteExcel.Count + 2;
                    hojaEgresos.Cell(fila, 1).Value = "TOTAL";
                    hojaEgresos.Cell(fila, 6).FormulaA1 = $"SUM(F2:F{fila - 1})";

                    var rango = hojaEgresos.Range(fila, 1, fila, 6);
                    rango.Style.Font.Bold = true;
                    rango.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                // ================= AHORROS =================
                if (datos.ListaAhorrosReporteExcel is null || !datos.ListaAhorrosReporteExcel.Any())
                {
                    hojaAhorros.Cell(1, 1).Value = "Sin datos";
                }
                else
                {
                    var tabla = hojaAhorros.Cell(1, 1)
                        .InsertTable(datos.ListaAhorrosReporteExcel, "Ahorros", true);

                    tabla.Theme = XLTableTheme.TableStyleMedium4;

                    hojaAhorros.SheetView.FreezeRows(1);
                    hojaAhorros.Columns().AdjustToContents();

                    hojaAhorros.Column(2).Style.NumberFormat.Format = "$ #,##0";
                    hojaAhorros.Column(8).Style.NumberFormat.Format = "$ #,##0";
                    hojaAhorros.Column(7).Style.NumberFormat.Format = "$ #,##0";

                    int fila = datos.ListaAhorrosReporteExcel.Count + 2;
                    hojaAhorros.Cell(fila, 1).Value = "TOTALES";
                    hojaAhorros.Cell(fila, 2).FormulaA1 = $"SUM(B2:B{fila - 1})";
                    hojaAhorros.Cell(fila, 7).FormulaA1 = $"SUM(G2:G{fila - 1})";
                    hojaAhorros.Cell(fila, 8).FormulaA1 = $"SUM(H2:H{fila - 1})";

                    var rango = hojaAhorros.Range(fila, 1, fila, 9);
                    rango.Style.Font.Bold = true;
                    rango.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                using (var stream = new MemoryStream())
                {
                    libroTrabajo.SaveAs(stream);
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "reporte.xlsx"
                    );
                }
            }
        }
        #endregion

        #region METODOS EGRESOS

        [HttpGet]
        [Route("{usuarioId}/egresos")]
        public async Task<ActionResult> ListarEgresos(int usuarioId)
        {
            try
            {
                var lista = await egresoService.ListaDeEgresos(usuarioId);

                return RespuestasService.Ok(lista);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/egresos/totales")]
        public async Task<ActionResult> ObtenerTotales(int usuarioId)
        {
            try
            {
                var resultado = await egresoService.ObtenerTiposTotalesEgresos(usuarioId);

                return RespuestasService.Ok(resultado);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        #endregion

        #region METODOS INGRESOS

        [HttpGet]
        [Route("{usuarioId}/ingresos")]
        public async Task<ActionResult> ListarIngresos(int usuarioId)
        {
            try
            {
                var lista = await ingresoService.ListaDeIngresos(usuarioId);

                return RespuestasService.Ok(lista);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/ingresos/totales")]
        public async Task<ActionResult> IngresosTotalesPorUsuario(int usuarioId)
        {
            try
            {
                var resultado = await ingresoService.ObtenerTiposTotalesIngresos(usuarioId);

                return RespuestasService.Ok(resultado);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        #endregion

        #region METODOS METAS

        [HttpGet]
        [Route("{usuarioId}/metas")]
        public async Task<ActionResult> BuscarMetaPorNombre(int usuarioId, [FromQuery] string? nombre = null, [FromQuery] string? estado = null)
        {
            try
            {

                if (!string.IsNullOrEmpty(nombre))
                {
                    var metasBuscadas = await metaAhorroService.ObtenerMetaPorNombre(nombre, usuarioId);
                    return RespuestasService.Ok(metasBuscadas);
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    var metasCumplidas = await metaAhorroService.MetasCumplidasPorUsuarioId(usuarioId, estado);
                    return RespuestasService.Ok(metasCumplidas);
                }

                var metas = await metaAhorroService.BuscarTodasLasMetasPorUsuarioId(usuarioId);
                return RespuestasService.Ok(metas);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        [HttpGet]
        [Route("{usuarioId}/metas/progreso")]
        public async Task<ActionResult> ObtenerMetasActivasConProgresoPorUsuarioId(int usuarioId)
        {
            try
            {
                var metas = await metaAhorroService.BuscarMetasActivasConProgresoPorUsuarioId(usuarioId);

                return RespuestasService.Ok(metas);

            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        #endregion

        #region METODOS ESTADISTICA

        [Authorize]
        [HttpGet]
        [Route("{usuarioId}/estadisticas")]
        public async Task<ActionResult> ObtenerData(int usuarioId)
        {
            try
            {
                var ahorros = await graficaService.ObtenerAhorroCompletoPorDia(usuarioId);

                return RespuestasService.Ok(ahorros);
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        #endregion

        #region METODO TRANSFERENCIA
        [HttpPost]
        [Authorize]
        [Route("transferencia")]
        public async Task<ActionResult> TransferirDinero([FromBody] TransferenciaDTO transferencia)
        {

            if(transferencia.TipoDestino == transferencia.TipoActual)
                return RespuestasService.ErrorModelo(this, "Debes elegir un destino diferente.", 500);

            if (transferencia.Monto < transferencia.CostoTransferencia)
                return RespuestasService.ErrorModelo(this, "El monto no puede ser menor al costo de transferencia.", 500);


            try
            {
                int montoPorIngresar = transferencia.Monto - transferencia.CostoTransferencia;
                var ingreso = new IngresoDto { 
                    UsuarioId = transferencia.UsuarioId, 
                    Monto = montoPorIngresar, 
                    MovimientoInterno = true,
                    Tipo = transferencia.TipoDestino
                };

                var egreso = new EgresoDto
                {
                    Monto = transferencia.Monto,
                    Tipo = transferencia.TipoActual,
                    CategoriaGastoId = 13,
                    UsuarioId = transferencia.UsuarioId
                };

                var totalIngreso = await ingresoService.BuscarTotalIngresoEnTipo(egreso.Tipo, egreso.UsuarioId);

                var totalEgreso = await egresoService.BuscarTotalEgresoEnTipo(egreso.Tipo, egreso.UsuarioId);

                if (((totalIngreso - totalEgreso) - egreso.Monto) < 0)
                    return RespuestasService.ErrorModelo(this, $"No puede transferir mas de lo que tiene en ({egreso.Tipo})", 409);

                await ingresoService.Insertar(ingreso);
                await egresoService.Insertar(egreso);

                return RespuestasService.Ok("Transferencia exitosa.");
            }
            catch (Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }
        #endregion

        #region METODO REPORTE PDF
        [HttpGet]
        [Route("metas/{metaAhorroId}/pdf")]
        public async Task<ActionResult> ObtenerDataPdf(int metaAhorroId)
        {
            try
            {
                var obtenerDataPdf = await pdfService.ObtenerDataPdfMetaAhorro(metaAhorroId);

                return RespuestasService.Ok(obtenerDataPdf);

            }catch(Exception error)
            {
                return RespuestasService.ErrorModelo(this, error.Message, 500);
            }
        }

        #endregion
    }
}
