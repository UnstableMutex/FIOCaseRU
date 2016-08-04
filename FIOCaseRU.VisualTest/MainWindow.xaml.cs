using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using NDatabase;
using NDatabase.Api;

namespace FIOCaseRU.VisualTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<FIO> coll;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
//   XmlSerializer ser = new XmlSerializer(typeof(List<FIO>));
//            object o = ser.Deserialize(new StreamReader("FIO_samples.xml"));
//            var coll1 = o as IEnumerable<FIO>;
//            SqlConnectionStringBuilder b=new SqlConnectionStringBuilder();
//            b.IntegratedSecurity = true;
//            b.InitialCatalog = "FIO";
//            b.DataSource = @"STORMMAINPC\SQLEXPRESS";
//            string cs = b.ConnectionString;
//            SqlConnection conn = new SqlConnection(cs);
//         string q = @"INSERT INTO [dbo].[FIO]
//           ([Surname]
//           ,[FirstName]
//           ,[Patronymic])
//     VALUES
//           ('{0}'
//           ,'{1}'
//           ,'{2}'
//           )";
//            conn.Open();
//            foreach (var fio in coll1)
//            {
//                var query = string.Format(q, fio.Surname, fio.FirstName, fio.Patronymic);
//                using (var cmd=conn.CreateCommand())
//                {
//                    cmd.CommandText = query;
//                    cmd.ExecuteNonQuery();
//                }

//            }



        }

        private IOdb db;
        IOdb GetDB()
        {
            if (db == null)
                db = OdbFactory.Open("database.ndb");
            return db;
        }
        void LoadToDBFromXML()
        {
            File.Delete("database.ndb");
            XmlSerializer ser = new XmlSerializer(typeof(List<FIO>));
            object o = ser.Deserialize(new StreamReader("FIO_samples.xml"));
            var coll1 = o as IEnumerable<FIO>;
            var f = GetDB();
            foreach (var obj in coll1)
            {
                f.Store(obj);
            }



        }
        void LoadFromDBToWork()
        {
            IEnumerable<FIO> coll1;
            var f = GetDB();
            coll1 = f.QueryAndExecute<FIO>();


            coll = new List<FIO>(coll1);
        }

        private int currentindex = -1;



        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CheckNext();
        }

        private void CheckNext()
        {
            currentindex++;
            FIO f = coll.ElementAt(currentindex);
            FIOCaseRU.FIO fio = new FIOCaseRU.FIO(f.Surname, f.FirstName, f.Patronymic);

            gr.DataContext = fio;
        }

        private void Next_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.Right)
            {
                Next_Click(sender, e);
            }
            e.Handled = true;
        }

        private void Prev_OnClick(object sender, RoutedEventArgs e)
        {
            currentindex--;
            FIO f = coll.ElementAt(currentindex);
            FIOCaseRU.FIO fio = new FIOCaseRU.FIO(f.Surname, f.FirstName, f.Patronymic);

            gr.DataContext = fio;
        }

        private void DeleteFromDB(object sender, RoutedEventArgs e)
        {
            var cur = coll.ElementAt(currentindex);
            coll.RemoveAt(currentindex);
            var f = GetDB();
            f.Delete(cur);
            // f.Delete(cur);


            CheckNext();
        }

        private void loadtodbfromxml(object sender, RoutedEventArgs e)
        {
            LoadToDBFromXML();
        }

        private void loadforcheck(object sender, RoutedEventArgs e)
        {
            LoadFromDBToWork();
            CheckNext();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (db!=null)
            db.Dispose();
        }


    }
}
