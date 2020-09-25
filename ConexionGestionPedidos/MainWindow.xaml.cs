using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConexionGestionPedidos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["ConexionGestionPedidos.Properties.Settings.GestionPedidosConnectionString"].ConnectionString;

            miConexionSql = new SqlConnection(miConexion);

            MuestraClientes();
            MuestraTodosPedidos();
        }
        private void MuestraClientes()
        {
            try
            {
                string consulta = "SELECT * FROM CLIENTE";
                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta, miConexionSql);

                using (miAdaptadorSql)
                {
                    DataTable clienteTabla = new DataTable();
                    miAdaptadorSql.Fill(clienteTabla);

                    listaCliente.DisplayMemberPath = "nombre";
                    listaCliente.SelectedValuePath = "Id";
                    listaCliente.ItemsSource = clienteTabla.DefaultView;
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            

        }

        private void MuestraPedidos()
        {

            try
            {
                //Consulta parametrica
                string consulta = "SELECT * FROM Pedido P INNER JOIN Cliente C ON C.Id = P.cCliente WHERE C.Id = @ClienteId";


                SqlCommand sqlComando = new SqlCommand(consulta, miConexionSql);
                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(sqlComando);

                using (miAdaptadorSql)
                {
                    sqlComando.Parameters.AddWithValue("@ClienteId", listaCliente.SelectedValue);

                    DataTable pedidosTabla = new DataTable();
                    miAdaptadorSql.Fill(pedidosTabla);

                    PedidosCliente.DisplayMemberPath = "fechaPedido";
                    PedidosCliente.SelectedValuePath = "Id";
                    PedidosCliente.ItemsSource = pedidosTabla.DefaultView;
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            

        }
        SqlConnection miConexionSql;
        private void MuestraTodosPedidos()
        {
            try
            {
                //Consulta parametrica
                string consulta = "SELECT *, CONCAT(CCLIENTE, ' ', fechaPedido, ' ', formaPago) AS INFOCOMPLETA FROM PEDIDO";

                SqlDataAdapter miAdaptadorSqlT = new SqlDataAdapter(consulta, miConexionSql);

                using (miAdaptadorSqlT)
                {
                    DataTable pedidosTablaT = new DataTable();
                    miAdaptadorSqlT.Fill(pedidosTablaT);
                    TodosPedidos.SelectedValuePath = "Id";
                    TodosPedidos.DisplayMemberPath = "INFOCOMPLETA";
                    TodosPedidos.ItemsSource = pedidosTablaT.DefaultView;
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }
       

       

       /* private void listaCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MuestraPedidos();
        }*/
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Borra pedidos
            string counsulta = "DELETE FROM PEDIDO WHERE ID=@PEDIDOID";
            SqlCommand miSqlCommand = new SqlCommand(counsulta, miConexionSql);
            miConexionSql.Open();

            miSqlCommand.Parameters.AddWithValue("@PEDIDOID", TodosPedidos.SelectedValue);
            miSqlCommand.ExecuteNonQuery();
            miConexionSql.Close();
            //Refresca la tabla
            MuestraTodosPedidos();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Inserta clientes
            string counsulta = "INSERT INTO CLIENTE(nombre) VALUES (@nombre)";
            SqlCommand miSqlCommand = new SqlCommand(counsulta, miConexionSql);
            miConexionSql.Open();

            miSqlCommand.Parameters.AddWithValue("@nombre", insertaCliente.Text);
            miSqlCommand.ExecuteNonQuery();
            miConexionSql.Close();
            //Refresca la tabla
            MuestraClientes();
            insertaCliente.Text = "";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Borra clientes
            string counsulta = "DELETE FROM CLIENTE WHERE ID=@CLIENTEID";
            SqlCommand miSqlCommand = new SqlCommand(counsulta, miConexionSql);
            miConexionSql.Open();

            miSqlCommand.Parameters.AddWithValue("@CLIENTEID", listaCliente.SelectedValue);
            miSqlCommand.ExecuteNonQuery();
            miConexionSql.Close();
            //Refresca la tabla
            MuestraClientes();
        }

        private void listaCliente_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            MuestraClientes();
        }

        private void PedidosCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
