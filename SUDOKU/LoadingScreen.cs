using System;
using System.Drawing;
using System.Windows.Forms;

namespace SUDOKU
{
    public partial class loadingScreen : Form
    {
        public loadingScreen()
        {
            InitializeComponent();
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            casocvacTextu.Enabled = true;
        }

        private void LoadingScreen_Paint(object sender, PaintEventArgs e)
        {
            //Vykreslení stringu
            Graphics load = e.Graphics;
            load.DrawString(text + retezec, new Font("Arial", 48, FontStyle.Bold), barva, ClientSize.Width / 2 - 180, ClientSize.Height / 2 - 40);
            retezec = "";
        }
        //deklarace a inicializace proměnných
        string text = "";
        string retezec = " ";
        bool konec = false;
        SolidBrush barva = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
        int minHodnota = 1;
        int alpha = 0;
        bool odecist = true;
        //
        //tick timeru po 0.07s
        private void casocvacTextu_Tick(object sender, EventArgs e)
        {
            //když je proměnná alpha dělitelná 40 beze zbytku
            if (alpha % 40 == 0)
            {
                //Postupné skládaní slova
                switch (minHodnota)
                {
                    case 1:
                            minHodnota++;
                            text += "S ";
                            break;
                    case 2:
                            minHodnota++;
                            text += "U ";
                            break;
                    case 3:
                            minHodnota++;
                            text += "D ";
                            break;
                    case 4:
                            minHodnota++;
                            text += "O ";
                            break;
                    case 5:
                            minHodnota++;
                            text += "K ";
                            break;
                    case 6:
                            minHodnota++;
                            text += "U ";
                            break;
                }
            }
            //
            //Náhodné generování čísel 1 - 9
            Random random = new Random();
            for (int i = minHodnota; i < 7; i++)
            {
                int cislo = random.Next(1, 10);
                retezec += cislo.ToString() + " ";
            }
            //
            //Když se proměnná alpha dostane na hodnotu 255, začné se místo přičítání odečítat
            if (alpha == 255)
            {
                odecist = true;
            }
            //Když je alpha 0, ukončí se načítací obrazovka a zobrazí se menu
            else if (alpha == 0)
            {
                if(konec)
                {
                    menu menu = new menu();
                    menu.Show();
                    Hide();
                    casocvacTextu.Enabled = false;
                }
                konec = true;
                odecist = false;
            }
            if (odecist)
            {
                alpha -= 5;
            }
            else
            {
                alpha += 5;
            }
            //štětec pro string
            barva = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
            Refresh();
        }
    }
}
