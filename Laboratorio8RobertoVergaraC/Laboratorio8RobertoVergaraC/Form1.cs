using Laboratorio8RobertoVergaraC.Controllers;
using Laboratorio8RobertoVergaraC.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Laboratorio8RobertoVergaraC
{
    public partial class Form1 : Form
    {
        public delegate bool NewEventHandler(object source, NewEventArgs args);
        public event NewEventHandler NewStoreButtonClicked;
        public event EventHandler<NewEventArgs> LocalChecked;

        List<Panel> stackPanels = new List<Panel>();
        Dictionary<string, Panel> panels = new Dictionary<string, Panel>();

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            panels.Add("NewStorePanel", NewStorePanel);
            panels.Add("CheckStorePanel", CheckStorePanel);
            panels.Add("InicialPanel", InicialPanel);
            stackPanels.Add(panels["InicialPanel"]);
            ShowLastPanel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NewStore_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["NewStorePanel"]);
            ShowLastPanel();
        }

        private void CheckStore_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["CheckStorePanel"]);
            ShowLastPanel();
        }

        private void SeeStores_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Stores store in Controller.Stores)
            {
                string info = "Tipo: " + store.Type + ", Identificador: " + store.Identificador.ToString();
                builder.Append(info).AppendLine();
            }
            System.Windows.MessageBox.Show(builder.ToString());
        }

        private void NewStoreButton_Click(object sender, EventArgs e)
        {
            string tipo = TIPO.Text;
            string nombredueño = NombreDueño.Text;
            string iden = Identificador.Text;
            string horini = horarioatencioninicial.Text;
            string horfin = horariofinatencion.Text;
            string numsalas = numerosalas.Text;
            string cat = categorias.Text;
            string mesex = mesasexclusivas.Text;
            OnNewStoreButtonClicked(tipo, nombredueño, iden, horini, horfin, numsalas, cat, mesex);
        }

        private void OnNewStoreButtonClicked(string type, string nomdue, string iden, string horini, string horfin, string numsalas, string cat, string mesex)
        {
            bool result3 = true;
            Stores store = new Stores("","","","","");
            if (type == "Tienda")
            {
                bool result2 = NewStoreButtonClicked(this, new NewEventArgs() { IdentificadorText = iden, TipoText = type, ExtraText=cat, HorarioFinText=horfin,HorarioInicioText=horini,NombreDueñoText=nomdue });
                result3 = result2;
                store.Type = type; store.DueñoName = nomdue; store.Identificador = iden; store.HorarioInicio = horini; store.HorarioFinal = horfin; store.Extra = cat;
            }
            else if (type == "Cine")
            {
                bool result2 = NewStoreButtonClicked(this, new NewEventArgs() { IdentificadorText = iden, TipoText = type, ExtraText = numsalas, HorarioFinText = horfin, HorarioInicioText = horini, NombreDueñoText = nomdue });
                result3 = result2;
                store.Type = type; store.DueñoName = nomdue; store.Identificador = iden; store.HorarioInicio = horini; store.HorarioFinal = horfin; store.Extra = numsalas;
            }
            else if (type == "Restaurante")
            {
                bool result2 = NewStoreButtonClicked(this, new NewEventArgs() { IdentificadorText = iden, TipoText = type, ExtraText = mesex, HorarioFinText = horfin, HorarioInicioText = horini, NombreDueñoText = nomdue });
                result3 = result2;
                store.Type = type; store.DueñoName = nomdue; store.Identificador = iden; store.HorarioInicio = horini; store.HorarioFinal = horfin; store.Extra = mesex;
            }
            else if (type == "Recreación")
            {
                bool result2 = NewStoreButtonClicked(this, new NewEventArgs() { IdentificadorText = iden, TipoText = type, ExtraText = null, HorarioFinText = horfin, HorarioInicioText = horini, NombreDueñoText = nomdue });
                result3 = result2;
                store.Type = type; store.DueñoName = nomdue; store.Identificador = iden; store.HorarioInicio = horini; store.HorarioFinal = horfin; store.Extra = "";
            }
            foreach (Stores s in Controller.Stores)
            {
                if (s.Identificador == store.Identificador)
                {
                    result3 = false;
                }
            }
            if (!result3)
            {
                System.Windows.MessageBox.Show("El local ya existe, o el número identificador ya fue usado");
            }
            else
            {
                Controller.Stores.Add(store);
                Controller.Serializacion();
                OnLocalChecked(iden);
            }
        }

        private void OnLocalChecked(string iden)
        {
            if (LocalChecked != null)
            {
                LocalChecked(this, new NewEventArgs() { IdentificadorText = iden });
                TIPO.SelectedIndex = -1;
                NombreDueño.ResetText();
                Identificador.ResetText();
                horarioatencioninicial.ResetText();
                horariofinatencion.ResetText();
                numerosalas.ResetText();
                categorias.ResetText();
                mesasexclusivas.ResetText();
                Controller.Serializacion();
                stackPanels.Add(panels["CheckStorePanel"]);
                ShowLastPanel();
            }
        }

        public void setLocal(string type, string nombredueño, string identificador, string horarioinicio, string horariofinal, string extra)
        {
            if (type == "Tienda")
            {
                TipoTextBox.Text += "\r\n"+type;
                CategoriasTextBox.Text += "\r\n" + extra;
                CantidadSalasTextBox.Text += "\r\n";
                MesasExclusivasTextBox.Text += "\r\n";
                HorarioAtencionTextBox.Text += "\r\n" + horarioinicio + " -- " + horariofinal;
                NumeroIdentificadorTextBox.Text += "\r\n"+identificador;
                NombreDueñoTextBox.Text += "\r\n"+nombredueño;
            }
            else if (type == "Cine")
            {
                TipoTextBox.Text += "\r\n" + type;
                CategoriasTextBox.Text += "\r\n";
                CantidadSalasTextBox.Text += "\r\n" + extra;
                MesasExclusivasTextBox.Text += "\r\n";
                HorarioAtencionTextBox.Text += "\r\n" + horarioinicio + " -- " + horariofinal;
                NumeroIdentificadorTextBox.Text += "\r\n" + identificador;
                NombreDueñoTextBox.Text += "\r\n" + nombredueño;
            }
            else if (type == "Restaurante")
            {
                TipoTextBox.Text += "\r\n" + type;
                CategoriasTextBox.Text += "\r\n" ;
                CantidadSalasTextBox.Text += "\r\n";
                MesasExclusivasTextBox.Text += "\r\n" + extra;
                HorarioAtencionTextBox.Text += "\r\n" + horarioinicio + " -- " + horariofinal;
                NumeroIdentificadorTextBox.Text += "\r\n" + identificador;
                NombreDueñoTextBox.Text += "\r\n" + nombredueño;
            }
            else if (type == "Recreación")
            {
                TipoTextBox.Text += "\r\n" + type;
                CategoriasTextBox.Text += "\r\n";
                CantidadSalasTextBox.Text += "\r\n";
                MesasExclusivasTextBox.Text += "\r\n";
                HorarioAtencionTextBox.Text += "\r\n" + horarioinicio + " -- " + horariofinal;
                NumeroIdentificadorTextBox.Text += "\r\n" + identificador;
                NombreDueñoTextBox.Text += "\r\n" + nombredueño;
            }
            Controller.Serializacion();
        }

        private void ShowLastPanel()
        {
            foreach (Panel panel in panels.Values)
            {
                if (panel != stackPanels.Last())
                {
                    panel.Visible = false;
                }
                else
                {
                    panel.Visible = true;
                }
            }
        }
    }
}
