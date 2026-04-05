using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TecnoGo.Enumeraciones;
using TecnoGo.Extensions;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Entities.DTO;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.DAL
{
    public class FacturaDAL : IFacturaDAL
    {
        private static readonly ILog _myLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public FacturaEncabezado Save(FacturaEncabezado pFactura)
        {
            FacturaEncabezado oFacturaEncabezado = null;
            string sqlEncabezado = string.Empty;
            string sqlDetalle = string.Empty;
            string sqlProducto = string.Empty;

            SqlCommand cmdEncabezado = new SqlCommand();
            SqlCommand cmdDetalle = null;
            SqlCommand cmdProducto = null;

            List<IDbCommand> listaCommands = new List<IDbCommand>();
            double rows = 0;
            string msg = "";

            // Reenumerar factura
            pFactura.Id = GetCurrentNumeroFactura();
            pFactura.ListaDetalles.ForEach(d => d.IdFactura = pFactura.Id);

            sqlEncabezado = @"
            INSERT INTO FacturaEncabezado
            (IdCliente,IdUsuario,FechaFactura,Subtotal,Impuesto,TotalCRC,TotalUSD,TipoCambio,
             TipoPago,Documento,Banco,TipoTarjeta,FirmaCliente,XmlFactura,Estado)
            VALUES
            (@IdCliente,@IdUsuario,getdate(),@Subtotal,@Impuesto,@TotalCRC,@TotalUSD,@TipoCambio,
             @TipoPago,@Documento,@Banco,@TipoTarjeta,@FirmaCliente,@XmlFactura,@Estado)
        ";

            try
            {
                // Encabezado
                cmdEncabezado.Parameters.AddWithValue("@IdCliente", pFactura.IdCliente);
                cmdEncabezado.Parameters.AddWithValue("@IdUsuario", pFactura.IdUsuario);
                cmdEncabezado.Parameters.AddWithValue("@Subtotal", pFactura.Subtotal);
                cmdEncabezado.Parameters.AddWithValue("@Impuesto", pFactura.Impuesto);
                cmdEncabezado.Parameters.AddWithValue("@TotalCRC", pFactura.TotalCRC);
                cmdEncabezado.Parameters.AddWithValue("@TotalUSD", pFactura.TotalUSD);
                cmdEncabezado.Parameters.AddWithValue("@TipoCambio", pFactura.TipoCambio);
                cmdEncabezado.Parameters.AddWithValue("@TipoPago", pFactura.TipoPago.ToString());
                cmdEncabezado.Parameters.AddWithValue("@Documento", pFactura.Documento);
                cmdEncabezado.Parameters.AddWithValue("@Banco", pFactura.Banco.ToString());
                cmdEncabezado.Parameters.AddWithValue("@TipoTarjeta", pFactura.TipoTarjeta.ToString());
                cmdEncabezado.Parameters.AddWithValue("@FirmaCliente", pFactura.FirmaCliente);
                cmdEncabezado.Parameters.AddWithValue("@XmlFactura", pFactura.XmlFactura.ToString());
                cmdEncabezado.Parameters.AddWithValue("@Estado", pFactura.Estado.ToString());

                cmdEncabezado.CommandText = sqlEncabezado;
                cmdEncabezado.CommandType = CommandType.Text;

                listaCommands.Add(cmdEncabezado);

                // Detalle
                sqlDetalle = @"
                INSERT INTO FacturaDetalle
                (IdFactura,IdProducto,Cantidad,Precio,Subtotal,Impuesto,Total)
                VALUES
                (@IdFactura,@IdProducto,@Cantidad,@Precio,@Subtotal,@Impuesto,@Total)
            ";

                foreach (var det in pFactura.ListaDetalles)
                {
                    cmdDetalle = new SqlCommand();
                    cmdDetalle.Parameters.AddWithValue("@IdFactura", det.IdFactura);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", det.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@Precio", det.Precio);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", det.Subtotal);
                    cmdDetalle.Parameters.AddWithValue("@Impuesto", det.Impuesto);
                    cmdDetalle.Parameters.AddWithValue("@Total", det.Total);
                    cmdDetalle.CommandText = sqlDetalle;
                    cmdDetalle.CommandType = CommandType.Text;

                    listaCommands.Add(cmdDetalle);

                    // Rebajar inventario
                    sqlProducto = @"UPDATE Producto SET Cantidad = Cantidad - @Cantidad WHERE Id = @IdProducto";

                    cmdProducto = new SqlCommand();
                    cmdProducto.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                    cmdProducto.Parameters.AddWithValue("@IdProducto", det.IdProducto);
                    cmdProducto.CommandText = sqlProducto;
                    cmdProducto.CommandType = CommandType.Text;

                    listaCommands.Add(cmdProducto);
                }

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = db.ExecuteNonQuery(listaCommands, IsolationLevel.ReadCommitted);
                }

                if (rows == 0)
                    throw new Exception("No se pudo guardar la factura");

                oFacturaEncabezado = GetFactura(pFactura.Id);

                GetNextNumeroFactura();

                return oFacturaEncabezado;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, cmdEncabezado));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public int GetNextNumeroFactura()
        {
            DataSet ds = null;
            IDbCommand command = new SqlCommand();
            int numero = 0;
            string sql = @"SELECT NEXT VALUE FOR SequenceNoFactura";
            string msg = "";

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }

                numero = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                return numero;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
        }

        public int GetCurrentNumeroFactura()
        {
            DataSet ds = null;
            IDbCommand command = new SqlCommand();
            int numero = 0;
            string sql = @"SELECT current_value FROM sys.sequences WHERE name = 'SequenceNoFactura'";
            string msg = "";

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }

                numero = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                return numero;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
        }

        public double GetTotalFactura(double pIdFactura)
        {
            double total = 0;
            string sql = @"SELECT SUM(Cantidad * Precio + Impuesto) FROM FacturaDetalle WHERE IdFactura = @IdFactura";
            string msg = "";
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdFactura", pIdFactura);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    total = db.ExecuteScalar(command);
                }

                return total;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
        }

        public async Task<IEnumerable<VentasDTO>> GetTotalVentasXFecha(DateTime pFechaInicial, DateTime pFechaFinal)
        {
            SqlCommand command = new SqlCommand();
            List<VentasDTO> lista = new List<VentasDTO>();
            string msg = "";

            try
            {
                string sql = @"
                SELECT YEAR(FE.FechaFactura) AS Anno,
                       SUM(FD.Cantidad * FD.Precio + FD.Impuesto) AS TotalVenta
                FROM FacturaDetalle FD
                INNER JOIN FacturaEncabezado FE ON FD.IdFactura = FE.Id
                WHERE FE.FechaFactura BETWEEN @pFechaInicial AND @pFechaFinal
                GROUP BY YEAR(FE.FechaFactura)
            ";

                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@pFechaInicial", pFechaInicial);
                command.Parameters.AddWithValue("@pFechaFinal", new DateTime(pFechaFinal.Year, pFechaFinal.Month, pFechaFinal.Day, 23, 59, 59));

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    DataSet ds = await Task.Run(() => db.ExecuteReader(command, "T"));
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow row in dt.Rows)
                    {
                        VentasDTO dto = new VentasDTO();
                        dto.Anno = int.Parse(row["Anno"].ToString());
                        dto.TotalVenta = Convert.ToDouble(row["TotalVenta"]);
                        lista.Add(dto);
                    }

                    return lista;
                }
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
        }

        public FacturaEncabezado GetFactura(double pIdFactura)
        {
            string msg = "";
            FacturaEncabezado oFactura = new FacturaEncabezado();
            DataSet ds = null;
            SqlCommand command = new SqlCommand();

            string sql = @"
            SELECT FE.Id, FE.IdCliente, FE.IdUsuario, FE.FechaFactura, FE.Subtotal, FE.Impuesto,
                   FE.TotalCRC, FE.TotalUSD, FE.TipoCambio, FE.TipoPago, FE.Documento, FE.Banco,
                   FE.TipoTarjeta, FE.FirmaCliente, FE.XmlFactura, FE.Estado,
                   FD.Id AS DetId, FD.IdProducto, FD.Cantidad, FD.Precio, FD.Subtotal AS DetSubtotal,
                   FD.Impuesto AS DetImpuesto, FD.Total AS DetTotal
            FROM FacturaEncabezado FE
            INNER JOIN FacturaDetalle FD ON FE.Id = FD.IdFactura
            WHERE FE.Id = @IdFactura
        ";

            try
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdFactura", pIdFactura);

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    oFactura.Id = int.Parse(dr["Id"].ToString());
                    oFactura.IdCliente = dr["IdCliente"].ToString();
                    oFactura.IdUsuario = int.Parse(dr["IdUsuario"].ToString());
                    oFactura.FechaFactura = DateTime.Parse(dr["FechaFactura"].ToString());
                    oFactura.Subtotal = double.Parse(dr["Subtotal"].ToString());
                    oFactura.Impuesto = double.Parse(dr["Impuesto"].ToString());
                    oFactura.TotalCRC = double.Parse(dr["TotalCRC"].ToString());
                    oFactura.TotalUSD = double.Parse(dr["TotalUSD"].ToString());
                    oFactura.TipoCambio = double.Parse(dr["TipoCambio"].ToString());

                    Enum.TryParse(dr["TipoPago"].ToString(), out TipoPago tipoPago);
                    oFactura.TipoPago = tipoPago;

                    oFactura.Documento = dr["Documento"].ToString();

                    Enum.TryParse(dr["Banco"].ToString(), out Banco banco);
                    oFactura.Banco = banco;

                    Enum.TryParse(dr["TipoTarjeta"].ToString(), out TipoTarjeta tipoTarjeta);
                    oFactura.TipoTarjeta = tipoTarjeta;

                    oFactura.FirmaCliente = dr["FirmaCliente"] as byte[];

                    oFactura.XmlFactura = new Xml { DocumentContent = dr["XmlFactura"].ToString() };

                    Enum.TryParse(dr["Estado"].ToString(), out EstadoFactura estado);
                    oFactura.Estado = estado;

                    oFactura.ListaDetalles = new List<FacturaDetalle>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        FacturaDetalle det = new FacturaDetalle();
                        det.Id = int.Parse(row["DetId"].ToString());
                        det.IdFactura = oFactura.Id;
                        det.IdProducto = int.Parse(row["IdProducto"].ToString());
                        det.Cantidad = int.Parse(row["Cantidad"].ToString());
                        det.Precio = double.Parse(row["Precio"].ToString());
                        det.Subtotal = double.Parse(row["DetSubtotal"].ToString());
                        det.Impuesto = double.Parse(row["DetImpuesto"].ToString());
                        det.Total = double.Parse(row["DetTotal"].ToString());

                        oFactura.ListaDetalles.Add(det);
                    }
                }

                return oFactura;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
        }
    }
}
