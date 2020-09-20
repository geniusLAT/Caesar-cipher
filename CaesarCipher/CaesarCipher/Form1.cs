using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CaesarCipher
{


    public partial class Form1 : Form
    {
        const string AL = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string F = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        int key = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textOutputField.Text = Process(textInputField.Text);
        }

        string Process(string original)
        {
            string NewOne = "";
            original = original.ToLower();
            for (int i = 0; i < original.Length; i++)
            {
                int AlNum = -1;
                for(int z = 0; z < 33; z++)
                {
                    if (original[i] == AL[z])
                    {
                        AlNum = z;
                        break;
                    }
                }
                if (AlNum == -1)
                {
                    NewOne += original[i];
                }
                else
                {
                    NewOne += F[AlNum];
                }
                
            }
            return NewOne;
        }

        private void textBox1_Validated(object sender, EventArgs e)//Игрок ввёл новое число в поле ключа
        {
            int L = key;
            try
            {
                L = Convert.ToInt32(textBox1.Text);
                if (Math.Abs(L) > 32) L = L % 32;
                key = L;
            }
            catch (System.FormatException)//Вызывается, если игрок пытается ввести что-то, что не получится преобразить к int
            {
                //textBox1.Text = key.ToString();

            }
            textBox1.Text = key.ToString();

            
            F = "";
            for (int i = 0; i < 33; i++)
            {

                int g = i + key;
                if (g > 32) g -= 33; if (g < 0) g += 33;
                F += AL[g];
            }
            label3.Text = F;
        }
    }
}
