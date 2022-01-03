using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace SUDOKU
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }
        //Deklarace a inicializace proměnných
        hra sudoku;
        string obtiznost;
        SoundPlayer sound = new SoundPlayer(Properties.Resources.ElevatorMusic);
        //Na kliknutí jakéhokoliv tlačítka v menu
        private void nastaveni_Click(object sender, EventArgs e)
        {
            //Utvoření nového tlačítka se stejnými vlastnosti jako tlačítko, na které bylo kliknuto
            Button tlacitko = sender as Button;
            //Rozdělení úkonů dle toho, co bylo jako text tlačítka
            if (tlacitko.Text == "Nová hra")
            {
                button1.Text = "Lehká";
                button2.Text = "Střední";
                button3.Text = "Těžká";
                button1.BackColor = Color.LightGreen;
                button2.BackColor = Color.Yellow;
                button3.BackColor = Color.Red;
                button4.Visible = true;
            }
            else if (tlacitko.Text == "Nastavení")
            {
                button1.Text = "Zpět";
                button2.Visible = false;
                button3.Visible = false;
                checkBox1.Visible = true;

            }
            else if (tlacitko.Text == "Zpět")
            {
                button1.Text = "Nová hra";
                button2.Visible = true;
                button3.Visible = true;
                label1.Visible = false;
                checkBox1.Visible = false;
                button4.Visible = false;
                button2.Text = "Nastavení";
                button3.Text = "Nápověda";
                button1.BackColor = Color.White;
                button2.BackColor = Color.White;
                button3.BackColor = Color.White;
            }
            else if (tlacitko.Text == "Nápověda")
            {
                button1.Text = "Zpět";
                button2.Visible = false;
                button3.Visible = false;
                label1.Visible = true;
                label1.Text = "Ovládaní: Myší si vyberete číslo," + Environment.NewLine + " které chcete zadávat do mřížky, " + Environment.NewLine + " následným klikáním na políčka, v mřížce, " + Environment.NewLine + " se nastaví hodnota.";
            }
            else if (tlacitko.Text == "Lehká" || tlacitko.Text == "Střední" || tlacitko.Text == "Těžká")
            {
                obtiznost = tlacitko.Text;
                button1.Text = "Level 1";
                button2.Text = "Level 2";
                button3.Text = "Level 3";
            }
            else if (tlacitko.Text == "Level 1" || tlacitko.Text == "Level 2" || tlacitko.Text == "Level 3")
            {
                sudoku = new hra(obtiznost, tlacitko.Text);
                sudoku.Show();
                sound.Stop();
                Hide();
            }
        }

        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Vypnutí
            Application.Exit();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            //hudba
            sound.PlayLooping();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //nastavení hudby
            if(checkBox1.Checked == false)
            {
                sound.Stop();
            }
            else
            {
                sound.PlayLooping();
            }
        }
    }
}
