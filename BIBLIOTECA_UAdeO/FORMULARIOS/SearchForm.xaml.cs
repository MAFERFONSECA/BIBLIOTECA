using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BIBLIOTECA_UAdeO.FORMULARIOS
{
    /// <summary>
    /// Lógica de interacción para SearchForm.xaml
    /// </summary>
    public partial class SearchForm : System.Windows.Window
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg.SelectedItem != null) // Verifica si hay un elemento seleccionado
                {
                    var selectedRow = dg.SelectedItem as DataRowView;
                    if (selectedRow != null)
                    {
                        mReturnValue = selectedRow.Row.ItemArray[0].ToString();
                        DialogResult = true;
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

        private string mReturnValue;
        internal string ReturnValue
        {
            get
            {
                return mReturnValue;
            }
        }

        internal int mColNumber;
        internal DataSet mDS;

        private void gridformulario_Loaded(object sender, RoutedEventArgs e)
        {
            cmbMatch.Items.Add("Cualquier parte del Campo");
            cmbMatch.Items.Add("Total del Campo");
            cmbMatch.Items.Add("Inicio del campo");
            try
            {            
                dg.ItemsSource = mDS.Tables["Table"].DefaultView;   
                var collectionView = CollectionViewSource.GetDefaultView(dg.ItemsSource);
                collectionView.Refresh();            
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }


            //DataColumn dc;
            foreach (DataColumn dc in mDS.Tables[0].Columns)
            {
                cmbLookIn.Items.Add(dc.ColumnName);
            }

            DataGridTableStyle ts1 = new DataGridTableStyle();
            ts1.MappingName = "table";
            cmbLookIn.SelectedIndex = 0;
            cmbMatch.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFindWhat.Text))
                {
                    if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex]
                        .DataType.ToString()) == "System.String")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        switch (cmbMatch.SelectedIndex)
                        {
                            case 0: //Any Part of the Field
                                dv.RowFilter = cmbLookIn.Text + " LIKE '%" + txtFindWhat.Text + "%'";
                                break;
                            case 1: //Whole Field
                                dv.RowFilter = cmbLookIn.Text + "='" + txtFindWhat.Text + "'";
                                break;
                            case 2: // Start of the Field
                                dv.RowFilter = cmbLookIn.Text + " LIKE '" + txtFindWhat.Text + "%'";
                                break;
                        }
                        dg.ItemsSource = dv;
                        dg.ItemsSource = mDS.Tables["Table"].DefaultView;
                    }

                    else if
                       ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.DateTime")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=#" + txtFindWhat.Text + "#";
                        dg.ItemsSource = dv;
                    }
                    else
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=" + txtFindWhat.Text;
                        dg.ItemsSource = dv;
                    }
                }
                else
                { 
                    System.Windows.MessageBox.Show("Introduzca el valor a buscar");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void txtFindWhat_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFindWhat.Text))
                {
                    if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.String")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        switch (cmbMatch.SelectedIndex)
                        {
                            case 0: //Cualquier parte del campo
                                dv.RowFilter = cmbLookIn.Text + " LIKE '%" + txtFindWhat.Text + "%'";
                                break;
                            case 1: //Total del campo
                                dv.RowFilter = cmbLookIn.Text + "='" + txtFindWhat.Text + "'";
                                break;
                            case 2: // Inicio del campo
                                dv.RowFilter = cmbLookIn.Text + " LIKE '" + txtFindWhat.Text + "%'";
                                break;
                        }
                        dg.ItemsSource = dv;
                    }
                    else if ((mDS.Tables[0].Columns[cmbLookIn.SelectedIndex].DataType.ToString()) == "System.DateTime")
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=#" + txtFindWhat.Text + "#";
                        dg.ItemsSource = dv;
                    }

                    else if (cmbLookIn.Text == "Año de publicación")  //Hubo error en esa opción del ComboBox al hacer búsqueda avanzada de libro; lo resolví con ayuda de una IA
                    {
                        string Buscar = txtFindWhat.Text;
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = $"CONVERT([{cmbLookIn.Text}], 'System.String') LIKE '%{Buscar}%'";
                        dg.ItemsSource = dv;
                    }

                    else
                    {
                        DataView dv = new DataView(mDS.Tables[0]);
                        dv.RowFilter = cmbLookIn.Text + "=" + txtFindWhat.Text;
                        dg.ItemsSource = dv;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Introduzca el valor a buscar");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = mDS.Tables["Table"].DefaultView;
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

   
    }
}
