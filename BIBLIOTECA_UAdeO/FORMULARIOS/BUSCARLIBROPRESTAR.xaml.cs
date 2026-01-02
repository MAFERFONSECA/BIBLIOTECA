using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIBLIOTECA_UAdeO.FORMULARIOS
{
    /// <summary>
    /// Lógica de interacción para BUSCARLIBROPRESTAR.xaml
    /// </summary>
    public partial class BUSCARLIBROPRESTAR : Window
    {
        public BUSCARLIBROPRESTAR()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DTGRIDLIBROS.ItemsSource = mDS.Tables["Table"].DefaultView;
                var collectionView = CollectionViewSource.GetDefaultView(DTGRIDLIBROS.ItemsSource);
                collectionView.Refresh();
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }


            //DataColumn dc;
            foreach (DataColumn dc in mDS.Tables[0].Columns)
            {
                CBXBUSCARPOR.Items.Add(dc.ColumnName);
            }

            DataGridTableStyle ts1 = new DataGridTableStyle();
            ts1.MappingName = "table";
            CBXBUSCARPOR.SelectedIndex = 0;
        }

        private void BTNACEPTAR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DTGRIDLIBROS.SelectedItem != null) // Verifica si hay un elemento seleccionado
                {
                    var selectedRow = DTGRIDLIBROS.SelectedItem as DataRowView;

                    if (selectedRow != null)
                    {
                        mReturnValue = selectedRow.Row.ItemArray[0].ToString();  //Obtiene ID del libro
                        mReturnCant = TXTCANTIDADLIBROS.Text;  //mReturnCant toma la cantidad de libros

                        int CantidadActual = Convert.ToInt32(selectedRow.Row.ItemArray[5]);
                        int CantidadLlevar = Convert.ToInt32(mReturnCant);

                        if (CantidadActual == 0)
                        {
                            System.Windows.MessageBox.Show("Sin existencias");
                            DTGRIDLIBROS.SelectedItem = null;
                            TXTCANTIDADLIBROS.Text = "1";
                            return;
                        }

                        else if (CantidadLlevar > CantidadActual)
                        {
                            System.Windows.MessageBox.Show("No puede llevar más libros de los que hay disponibles.");
                            DTGRIDLIBROS.SelectedItem = null;
                            TXTCANTIDADLIBROS.Text = "1";
                            return;
                        }

                        else if (CantidadLlevar < 1)
                        {
                            System.Windows.MessageBox.Show("Tiene que llevarse al menos un libro.");
                            DTGRIDLIBROS.SelectedItem = null;
                            TXTCANTIDADLIBROS.Text = "1";
                            return;
                        }

                        else
                        {
                            DialogResult = true;
                        }
                    }
                }

                else
                {
                    System.Windows.MessageBox.Show("Seleccione un dato antes de continuar.");
                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private string mReturnValue;  //cadena para obtener el ID del libro

        internal string ReturnValue
        {
            get
            {
                return mReturnValue;
            }
        }

        private string mReturnCant;  //Cadena creada de forma exclusiva para obtener la cantidad del libro

        internal string ReturnCant
        {
            get
            {
                return mReturnCant;
            }
        }

        internal int mColNumber;
        internal DataSet mDS;

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TXTBUSCAR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty(TXTBUSCAR.Text))
                {
                    if ((mDS.Tables[0].Columns[CBXBUSCARPOR.SelectedIndex].DataType.ToString()) == "System.String")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = CBXBUSCARPOR.Text + " LIKE '%" + TXTBUSCAR.Text + "%'";
                        DTGRIDLIBROS.ItemsSource = dv;
                    }

                    else if (CBXBUSCARPOR.Text == "Año de publicación")
                    {
                        string Buscar = TXTBUSCAR.Text;
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = $"CONVERT([{CBXBUSCARPOR.Text}], 'System.String') LIKE '%{Buscar}%'";
                        DTGRIDLIBROS.ItemsSource = dv;
                    }

                    else
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = CBXBUSCARPOR.Text + " = " + TXTBUSCAR.Text;
                        DTGRIDLIBROS.ItemsSource = dv;
                    }
                }

                else
                {
                    DTGRIDLIBROS.ItemsSource = mDS.Tables["Table"].DefaultView;
                }
            }

            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
