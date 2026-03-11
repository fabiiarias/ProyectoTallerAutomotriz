namespace proyectKN.Models
{
    public class Inventario
    {
        public int Consecutivo { get; set; } 
        public string Nombre { get; set; } = string .Empty;
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public Decimal PrecioCompra {  get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int Proveedor { get; set; }


    }
}
