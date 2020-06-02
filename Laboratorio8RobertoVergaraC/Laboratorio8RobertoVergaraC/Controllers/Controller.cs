using Laboratorio8RobertoVergaraC.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio8RobertoVergaraC.Controllers
{
    [Serializable]
    class Controller
    {
        private static List<Stores> stores = new List<Stores>();
        Form1 view;

        public static List<Stores> Stores { get => stores; set => stores = value; }

        public Controller(Form view)
        {
            this.view = view as Form1;
            IniciarSerializacion();
            this.view.NewStoreButtonClicked += OnNewStoreButtonClicked;
            this.view.LocalChecked += OnLocalChecked;
        }

        public bool OnNewStoreButtonClicked(object sender, NewEventArgs e)
        {
            try
            {
                Stores store = new Stores(e.NombreDueñoText, e.IdentificadorText, e.HorarioInicioText, e.HorarioFinText, e.ExtraText);
                return true;
            }
            catch
            {
                return false;
            }       
        }

        public void OnLocalChecked(object sender, NewEventArgs e)
        {
            Stores store = null;
            store = stores.Where(t =>
               t.Identificador.ToString().ToUpper().Contains(e.IdentificadorText.ToUpper())).FirstOrDefault();
            view.setLocal(store.Type, store.DueñoName, store.Identificador.ToString(), store.HorarioInicio, store.HorarioFinal,store.Extra);
        }

        public void IniciarSerializacion()
        {
            IFormatter formatter = new BinaryFormatter();

            string urlAllStores = Directory.GetCurrentDirectory() + "\\AllStores.bin";

            if (File.Exists(urlAllStores))
            { 
                Stream stream = new FileStream("AllStores.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                //try que Desterializa; catch mostrar mensaje; finally cierra archivo
                try
                {
                    List<Stores> des = (List<Stores>)formatter.Deserialize(stream);
                    if (des.Count != 0)
                    {
                        Stores = des;
                    }
                }
                catch
                {
                }
                finally
                {
                    stream.Close();
                    if (Stores.Count() != 0)
                    {
                        foreach (Stores store in Stores)
                        {
                            view.setLocal(store.Type, store.DueñoName, store.Identificador, store.HorarioInicio, store.HorarioFinal, store.Extra);
                        }
                    }
                }
            }
        }

        public static void Serializacion()
        {
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream("AllStores.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Stores);
            stream.Close();
        }
    }
}
