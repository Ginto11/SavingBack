using Asp.Versioning;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using SavingBack.Database;
using SavingBack.Services;

namespace SavingBack.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reportes")]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteService reporteService;
        public ReporteController(ReporteService reporteService)
        {
            this.reporteService = reporteService;
        }

        [HttpGet]
        [Route("exportar-excel/{usuarioId}")]
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

    }
}
