using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace SUDOKU
{
    public partial class hra : Form
    {
        //Deklarace a inicializace proměnných
        string obtiznost, level, cesta, sekundaT = "", minutaT = "", hodinaT = "", vybraneCislo = "";
        StreamReader stream;
        int sekunda = 0, minuta = 0, hodina = 0;
        SudokuBt predchoziPole = new SudokuBt(), vybranePole = new SudokuBt();
        char[,] reseni = new char[9, 9], zobrazeni = new char[9, 9];
        SudokuBt[,] tlacitka = new SudokuBt[9, 9];
        //Základní atributy formuláře při načtení
        public hra(string obtiznost, string level)
        {
            this.level = level;
            this.obtiznost = obtiznost;
            //Tvorba cesty k souborům hry
            cesta = "Levely/" + obtiznost + level[6] + ".sudoku";
            InitializeComponent();
        }
        //Kliknutí na sudoku tlačítko
        private void sudokuBt_Click(object sender, EventArgs e)
        {
            //Utvoření nového sudoku tlačítka se stejnými vlastnosti jako sudoku tlačítko, na které bylo kliknuto
            vybranePole = sender as SudokuBt;
            predchoziPole.BackColor = Color.White;
            vybranePole.BackColor = Color.FromArgb(252, 188, 110);
            predchoziPole = vybranePole;
            vybranePole.Text = vybraneCislo;
        }
        //Kliknutí na tlačítko s číslem k vyplnění
        private void sudokuCisla_Click(object sender, EventArgs e)
        {
            //Utvoření nového sudoku tlačítka se stejnými vlastnosti jako sudoku tlačítko, na které bylo kliknuto
            SudokuBt cislo = sender as SudokuBt;
            vybraneCislo = cislo.Text;
            lbVybraneCislo.Text = "Vybrané číslo: " + vybraneCislo;
        }
        private void sudoku_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Úplné vypnutí
            Application.Exit();
        }
        //Zobrazení mřížky s čísly
        private void hra_Load(object sender, EventArgs e)
        {
            lbObtiznost.Text = obtiznost;
            lbUroven.Text = level;
            casovacCas.Enabled = true;
            stream = new StreamReader(cesta);
            for(int i = 0; i < 9; i++)
            {
                string radek = stream.ReadLine();
                for (int j = 0; j < 9; j++)
                {
                    reseni[j, i] = radek[j];

                }
            }
            stream.ReadLine();
            for (int i = 0; i < 9; i++)
            {
                string radek = stream.ReadLine();
                for(int j = 0; j < 9; j++)
                {
                    zobrazeni[j, i] = radek[j];

                }
            }
            stream.Dispose();
            stream.Close();
            foreach(SudokuBt bt in Controls.OfType<SudokuBt>())
            {
                if(bt.X != 10)
                {
                    bt.Text = "";
                    tlacitka[bt.X, bt.Y] = bt;
                }
            }
            for(int i = 0; i < 9; i ++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (zobrazeni[i, j] != '0')
                    {
                        tlacitka[i, j].Text = zobrazeni[i, j].ToString();
                        tlacitka[i, j].Enabled = false;
                    }
                }
            }
        }
        //Kliknutí na Vzdání se hry
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult vzdatSe;
            if (button1.Text == "Zpět do menu")
            {
                menu menu = new menu();
                menu.Show();
                Hide();
            }
            if (button1.Text == "Zobrazit řešení")
            {
                if (button2.Text != "Zpět do menu")
                {
                    vzdatSe = MessageBox.Show("Opravdu se chcete vzdát a zobrazit řešení?", "Vzdát se?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (vzdatSe == DialogResult.Yes)
                    {
                        casovacCas.Enabled = false;
                        MessageBox.Show("Zde máte řešení!", "Řešení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                tlacitka[i, j].Text = reseni[i, j].ToString();
                                tlacitka[i, j].Enabled = false;
                            }
                        }
                        button1.Text = "Zpět do menu";
                    }
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            tlacitka[i, j].Text = reseni[i, j].ToString();
                        }
                    }
                    button1.Visible = false;
                }
            }
            
        }
        //Kliknutí na zkontrolovaní
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Zpět do menu")
            {
                menu menu = new menu();
                menu.Show();
                this.Hide();
            }
            if (button2.Text == "Zkontrolovat")
            {
                DialogResult kontrola = MessageBox.Show("Opravdu chcete řešení zkontrolovat?", "Zkontrolovat?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kontrola == DialogResult.Yes)
                {
                    casovacCas.Enabled = false;
                    vybranePole = new SudokuBt();
                    int chyby = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            tlacitka[i, j].Enabled = false;
                            if (tlacitka[i, j].Text != reseni[i, j].ToString())
                            {
                                tlacitka[i, j].BackColor = Color.Red;
                                chyby++;
                            }
                        }
                    }
                    if (chyby == 0)
                    {
                        MessageBox.Show("Úspěšně jste dokončil tuto úroveň po: " + hodina + "hodinách " + minuta + "minutách a " + sekunda + "sekundách", "Správně!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Neúspěšně jste dokončil tuto úroveň, počet chyb: " + chyby + " po: " + hodina + "hodinách " + minuta + "minutách a " + sekunda + "sekundách", "Špatně!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    button2.Text = "Zpět do menu";
                }
            }
 
        }
        //Kliknutí na opuštení hry
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult zpet;
            zpet = MessageBox.Show("Opravdu se chcete vrátit zpět do hlavního menu?", "Odejít?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(zpet == DialogResult.Yes)
            {
                menu menu = new menu();
                menu.Show();
                Hide();
            }
        }

        //Tvoření času
        private void casovacCas_Tick(object sender, EventArgs e)
        {
            sekunda++;
            if (sekunda - 60 == 0)
            {
                sekunda -= 60;
                minuta++;
            }
            if (minuta - 60 == 0)
            {
                minuta -= 60;
                hodina++;
            }
            if (sekunda >= 10)
            {
                sekundaT = sekunda.ToString();
            }
            else
            {
                sekundaT = "0" + sekunda.ToString();
            }
            if (minuta >= 10)
            {
                minutaT = minuta.ToString();
            }
            else
            {
                minutaT = "0" + minuta.ToString();
            }
            if (hodina >= 10)
            {
                hodinaT = hodina.ToString();
            }
            else
            {
                hodinaT = "0" + hodina.ToString();
            }
            if (hodina != 0)
            {
                lbCas.Text = "Čas: " + hodinaT + ":" + minutaT + ":" + sekundaT;
            }
            else
            {
                lbCas.Text ="Čas: " + minutaT + ":" + sekundaT;
            }
        }
    }
}
