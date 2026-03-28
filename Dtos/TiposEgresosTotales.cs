namespace SavingBack.Dtos
{
    public class TiposEgresosTotales
    {
        public int TotalEfectivo { get; set; }

        public int TotalNequi { get; set; }

        public int TotalApp { get; set; }

        public TiposEgresosTotales(int efectivo, int nequi, int app)
        {
            TotalEfectivo = efectivo;
            TotalNequi = nequi;
            TotalApp = app;
        }
    }
}
