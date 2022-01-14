using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MS_4._2_Biciclette
{
    public partial class Form1 : Form
    {
        public static IEnumerable<T> OfType<T>(IEnumerable e) where T : class
        {
            foreach (object cur in e)
            {
                T val = cur as T;
                if (val != null)
                {
                    yield return val;
                }
            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public class Categoria : IDisposable
        {
            private static List<string> eleCC = new List<string>();
            public Categoria(string i, string d)
            {
                if (eleCC.Contains(i))
                    throw new Exception();

                this._IDC = i;
                this.Description = d;
                eleCC.Add(i);
            }
            private string _IDC;
            public string IDC
            {
                get { return _IDC; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    if (value.Length != 4)
                        throw new Exception();
                    this._IDC = value;
                }
            }
            private string _Description;
            public string Description
            {
                get { return _Description; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    this._Description = value;
                }
            }
            public override string ToString()
            {
                return $"{this.IDC} - {this.Description}";
            }
            private bool disposedValue;
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: eliminare lo stato gestito (oggetti gestiti)
                    }

                    // TODO: liberare risorse non gestite (oggetti non gestiti) ed eseguire l'override del finalizzatore
                    // TODO: impostare campi di grandi dimensioni su Null
                    eleCC.Remove(IDC);
                    disposedValue = true;
                }
            }
            // // TODO: eseguire l'override del finalizzatore solo se 'Dispose(bool disposing)' contiene codice per liberare risorse non gestite
            ~Categoria()
            {
                // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
                Dispose(disposing: false);
            }

            public void Dispose()
            {
                // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
        private class Bike : IDisposable
        {
            private static List<string> eleM = new List<string>();
            public Bike(string m, string ma, string mo, Categoria c, string t, int r, decimal p)
            {
                if (eleM.Contains(m))
                    throw new Exception();
                if (string.IsNullOrWhiteSpace(m))
                    throw new Exception();

                this._IDCode = m;
                this.Brand = ma;
                this.Model = mo;
                this.Category = c;
                this.Frame = t;
                this.Size = r;
                this.Price = p;
                eleM.Add(m);
            }
            private string _IDCode;
            public string IDCode
            {
                get { return _IDCode; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    if (value.Length != 4)
                        throw new Exception();
                    this._IDCode = value;
                }
            }
            private string _Brand;
            public string Brand
            {
                get { return _Brand; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    this._Brand = value;
                }
            }
            private string _Model;
            public string Model
            {
                get { return _Model; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    this._Model = value;
                }
            }
            private Categoria _Category;
            public Categoria Category
            {
                get { return _Category; }
                set
                {
                    if (value == null)
                        throw new Exception();
                    this._Category = value;
                }
            }
            private string _Frame;
            public string Frame
            {
                get { return _Frame; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception();
                    this._Frame = value;
                }
            }
            private int _Size;
            public int Size
            {
                get { return _Size; }
                set
                {
                    if (value < 0)
                        throw new Exception();
                    this._Size = value;
                }
            }
            private decimal _Price;
            public decimal Price
            {
                get { return _Price; }
                set
                {
                    if (value < 0)
                        throw new Exception();
                    this._Price = value;
                }
            }
            private bool disposedValue;
            public override string ToString()
            {
                return $"{this.IDCode} - {this.Brand} - {this.Model} - {this.Price} - {this.Size} - {this.Frame}";
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: eliminare lo stato gestito (oggetti gestiti)
                    }

                    // TODO: liberare risorse non gestite (oggetti non gestiti) ed eseguire l'override del finalizzatore
                    // TODO: impostare campi di grandi dimensioni su Null
                    eleM.Remove(IDCode);
                    disposedValue = true;
                }
            }
            // // TODO: eseguire l'override del finalizzatore solo se 'Dispose(bool disposing)' contiene codice per liberare risorse non gestite
            ~Bike()
            {
                //     // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
                Dispose(disposing: false);
            }
            public void Dispose()
            {
                // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private List<Bike> eleB = new List<Bike>();
        private List<Categoria> eleCate = new List<Categoria>();

        private void BTNADD_Click(object sender, EventArgs e)
        {
            var Pattern = "(^[A-Z]{2})([0-9]{2}$)";

            if (string.IsNullOrEmpty(TXTMARCA.Text) || string.IsNullOrEmpty(TXTMOD.Text) || CMBCATE.SelectedItem == null || CMBTELAIO.SelectedItem == null || TRBRUOTE.Value < 0)
            {
                MessageBox.Show("Write missing informations!", "System error");
                return;
            }
            if (Regex.IsMatch(TXTMAT.Text, Pattern))
            {
                Bike newBike = new Bike(TXTMAT.Text, TXTMARCA.Text, TXTMOD.Text, CMBCATE.SelectedItem as Categoria,
                                        CMBTELAIO.Text, TRBRUOTE.Value, decimal.Parse(TXTPREZZO.Text));
                eleB.Add(newBike);

                dataGridView1.DataSource = eleB.ToList();
            }
            else
            {
                MessageBox.Show("ID Code must be written in the following format:   AA11", "System error");
                return;
            }
        }
        private void BTNCANC_Click(object sender, EventArgs e)
        {
            var Canc = dataGridView1.SelectedRows[0].DataBoundItem as Bike;
            Canc.Dispose();
            eleB.Remove(Canc);

            dataGridView1.DataSource = eleB.ToList();
        }
        private void DROPDOWN(object sender, EventArgs e)
        {
            CMBCATE.Items.Clear();
            CMBCCHAR.Items.Clear();
            CMBCATEGRIDVEW.Items.Clear();

            foreach (var i in eleCate)
            {
                CMBCATE.Items.Add(i);
                CMBCCHAR.Items.Add(i);
                CMBCATEGRIDVEW.Items.Add(i);
            }
        }
        private void SELEZIONA(object sender, DataGridViewCellEventArgs e)
        {
            TXTMAT.Enabled = false;

            var SelectedBike = dataGridView1.SelectedRows[0].DataBoundItem as Bike;

            TXTMARCA.Text = SelectedBike.Brand;
            TXTMOD.Text = SelectedBike.Model;
            TXTPREZZO.Text = SelectedBike.Price.ToString();
            CMBCATE.Text = SelectedBike.Category.ToString();
            CMBTELAIO.Text = SelectedBike.Frame.ToString();
            TRBRUOTE.Value = SelectedBike.Size;
        }
        private void BTNMODI_Click(object sender, EventArgs e)
        {
            TXTMAT.Enabled = false;

            if (string.IsNullOrEmpty(TXTMARCA.Text) || string.IsNullOrEmpty(TXTMOD.Text) || CMBCATE.SelectedItem == null || CMBTELAIO.SelectedItem == null || TRBRUOTE.Value < 0)
            {
                MessageBox.Show("Write missing informations!, System error");
                return;
            }

            var ModiB = dataGridView1.SelectedRows[0].DataBoundItem as Bike;

            ModiB.Brand = TXTMARCA.Text;
            ModiB.Model = TXTMOD.Text;
            ModiB.Price = decimal.Parse(TXTPREZZO.Text);
            ModiB.Category = CMBCATE.SelectedItem as Categoria;
            ModiB.Frame = CMBTELAIO.SelectedItem.ToString();
            ModiB.Size = TRBRUOTE.Value;

            dataGridView1.DataSource = eleB.ToList();
        }
        private void BTNCADD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TXTCID.Text) || CMBCDESC.SelectedItem == null)
            {
                MessageBox.Show("Write missing informations!, System error");
                return;
            }
            else
            {
                var newCate = new Categoria(TXTCID.Text, CMBCDESC.SelectedItem.ToString());
                eleCate.Add(newCate);

                TXTCID.Clear();
                CMBCDESC.Refresh();

                dataGridView2.DataSource = eleCate.ToList();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            eleCate.AddRange(new Categoria[]
            {
                new Categoria("AABB", "Bici da Corsa"),
                new Categoria("CCDD", "Mountain Bike"),
                new Categoria("EEFF", "City Bike"),
                });

            eleB.AddRange(new Bike[]
            {
                new Bike("BG34", "Uno", "Sette", eleCate[0], "Acciaio", 22, 120),
                new Bike("BS56", "Due", "Otto", eleCate[2],"Titanio", 26, 235),
                new Bike("MI98", "Tre", "Nove", eleCate[1], "Carbonio", 24, 180),
                new Bike("CA91", "Quattro", "Dieci", eleCate[2], "Acciaio", 20, 115),
                new Bike("BL64", "Cinque", "Undici", eleCate[2], "Titanio", 22, 155),
                new Bike("LK05", "Sei", "Dodici", eleCate[0], "Carbonio", 22, 80),
                new Bike("BG35", "Tredici", "Diciannove", eleCate[0], "Acciaio", 26, 125),
                new Bike("BS55", "Quattordici", "Venti", eleCate[2],"Titanio", 24, 230),
                new Bike("MI99", "Quindici", "Ventuno", eleCate[1], "Carbonio", 22, 185),
                new Bike("CA92", "Sedici", "Ventidue", eleCate[2], "Acciaio", 22, 110),
                new Bike("BL65", "Diciassette", "Ventitre", eleCate[2], "Titanio", 20, 160),
                new Bike("LK04", "Diciotto", "Ventiquattro", eleCate[0], "Carbonio", 24, 75),
                });

            dataGridView1.DataSource = eleB.ToList();
            dataGridView2.DataSource = eleCate.ToList();
        }
        private void SELEZIONETELAIO(object sender, EventArgs e)
        {
            var eleTelaio = eleB
            .Where(k => k.Frame == CMBSELECTTELAIO.Text)
            .Select(k =>
            new
            {
                ID = k.IDCode,
                Brand = k.Brand,
                Model = k.Model,
                Category = k.Category,
                Frame = k.Frame,
                Size = k.Size,
                Price = k.Price
            });

            eleTelaio = eleTelaio.OrderBy(k => k.Model);
            dataGridView1.DataSource = eleTelaio.ToList();
        }
        private void CATESCELTA(object sender, EventArgs e)
        {
            List<Bike> eleTemp = new List<Bike>();
            eleTemp.Clear();

            var eleCateScelta = eleB.Where(k => k.Category == CMBCATEGRIDVEW.SelectedItem as Categoria);
            Bike Min = eleCateScelta.OrderBy(k => k.Price).First();
            Bike Max = eleCateScelta.OrderBy(k => k.Price).Last();
            eleTemp.Add(Min);
            eleTemp.Add(Max);

            dataGridView1.DataSource = eleTemp.ToList();
        }
        private void SETCHAR(object sender, EventArgs e)
        {
            WheelChar.Series.Clear();

            var eleChar = eleB.Where(c => c.Category == CMBCCHAR.SelectedItem as Categoria);
            var R1 = eleChar.Where(r => r.Size == 20).Count();
            var R2 = eleChar.Where(r => r.Size == 22).Count();
            var R3 = eleChar.Where(r => r.Size == 24).Count();
            var R4 = eleChar.Where(r => r.Size == 26).Count();

            if (CMBCCHAR.SelectedItem == null)
            {
                MessageBox.Show("Select a Category!");
                return;
            }
            else
            {
                TXT20.Text = $"Size 20: {R1}";
                TXT22.Text = $"Size 22: {R2}";
                TXT24.Text = $"Size 24: {R3}";
                TXT26.Text = $"Size 26: {R4}";

                WheelChar.Series["20"].Points.AddXY($"{CMBCCHAR.SelectedItem.ToString()}", R1);
                WheelChar.Series["22"].Points.AddXY($"{CMBCCHAR.SelectedItem.ToString()}", R2);
                WheelChar.Series["24"].Points.AddXY($"{CMBCCHAR.SelectedItem.ToString()}", R3);
                WheelChar.Series["26"].Points.AddXY($"{CMBCCHAR.SelectedItem.ToString()}", R4);
            }
        }
        private void RESET(object sender, EventArgs e)
        {
            TXTCID.Clear();
            TXTMARCA.Clear();
            TXTMAT.Clear();
            TXTMOD.Clear();
            TXTPREZZO.Clear();
            CMBCDESC.Refresh();
            CMBSELECTTELAIO.Refresh();
            CMBTELAIO.Refresh();
            CMBCATE.Refresh();
            CMBCCHAR.Refresh();
            CMBCATEGRIDVEW.Refresh();
            TRBRUOTE.Refresh();
            TXT20.Clear();
            TXT22.Clear();
            TXT24.Clear();
            TXT26.Clear();
            WheelChar.Series.Clear();

            dataGridView1.DataSource = eleB.ToList();
        }
        private void INFORMATIONS(object sender, EventArgs e)
        {
            MessageBox.Show("ID Code of every item must be written with format AA11\nTo Delete/Modify an item, select it directly from the DatGridView\n\nWARNING: It is not possible to modify the ID Code of an item!", "Bikes Archive | Information Box", MessageBoxButtons.OK);
        }
        private void CLOSEFORM(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}