﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace CaesarCipher
{


    public partial class Form1 : Form
    {
        const string AL = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";//Алфавит шифруемого языка, в данном случае русского
        string F = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";//Алфавит смещения, можно без него, но тест показал, что так выполняется быстрее
        int key = 0;//ключ шифрования
        string path = "log.txt";
        public Form1()
        {
            InitializeComponent();//Это стандартная штука Windows Forms, к заданию отношения не имеет, просто нужна
        }

        private void button1_Click(object sender, EventArgs e)//Вызывается при нажатии на кнопку
        {
            textOutputField.Text = Process(textInputField.Text);//При нажатии запускается функция Process с текстом с верхнего поля в качестве аргумента
            //Результат работы функции выводится  в нижний 
        }
        void Log(string message)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(message);
            //writer.
            writer.Close();
        }
        string Process(string original)//Основная функция, которая производит смещение
        {
            string NewOne = "";//Сделал строку, в которую мы будем писать
            original = original.ToLower();//Полученную на входе строку преобразуем, чтобы у нас был нижний регистр (маленькие буквы
            for (int i = 0; i < original.Length; i++)//Цикл переберающий все символы строки
            {
                int AlNum = -1;//Номер символа в алфавите
                for(int z = 0; z < 33; z++)//Цикл, сравнивающий символ сравниваемой строки и символы в алфавите
                {
                    if (original[i] == AL[z])//Если совпадение найдено
                    {
                        AlNum = z;//Пишем номер этогосимвола в исходном алфавите
                        break;//Выходим из цикла ради оптимизации, тк мы уже нашли, что искали
                    }
                }
                if (AlNum == -1)//Если AlNum = -1, значит, что совпадения в цикле выше найдено не было
                {
                    NewOne += original[i];//В зашифрованную строку записываем символ без шифровки 
                    //Без шифровки останутся все символы, которые не русские буквы: знаки припенания и буквы других языков
                    //При желании можно переключить на шифрование другого языка, просто поставив его алфавит в строку AL
                }
                else//Совпадения было найдено
                {
                    NewOne += F[AlNum];//В зашифрованную строку записываем символ с шифровкой из алфавита смещения
                    //Алфавит смещения оптимальнее, если шифруется больше символов, чем содержит алфавит
                }
                
            }
            

           
            string message = DateTime.Now+": ";
            message+= "\"" +original+ "\" was converted to \""+NewOne+"\" with key = "+key.ToString();
            Log(message);
           
            return NewOne;//Возвращает
        }

        private void textBox1_Validated(object sender, EventArgs e)//Юзер ввёл новое число в поле ключа
        {
            int L = key;
            try//Исключение тут стоит на случай, если в поле ключа записаны не цифры
            {
                L = Convert.ToInt32(textBox1.Text);//Явное преобразоване
                if (Math.Abs(L) > 32) L = L % 32;//Если юзер ввёл число большее длины алфавита, то ключ будет равен остатку от деления.
                //Если игрок хочет сместить на 34, то результат такой же, как при смещении на 2
                key = L;//Если всё выше сработало, то объявляем число новым ключом
            }
            catch (System.FormatException)//Вызывается, если игрок пытается ввести что-то, что не получится преобразить к int
            {
              
                string message = DateTime.Now + ": ";
                message += " user tryed to get \"" + textBox1.Text + "\" as key, but it commited the error";
                Log(message);
              
            }
            textBox1.Text = key.ToString();//Ставим в поле ключа новый ключ

            //Переписываем алфавит смещения
            F = "";//Пустая строка
            for (int i = 0; i < 33; i++)//Цикл для всех букв алфавита
            {

                int g = i + key;//номер буквы в шфире в исходном алфавите  это номер её до шифровки и ключа.
                if (g > 32) g -= 33; if (g < 0) g += 33;//Если мы ушли за границы алфавита, то нужно вернуться с другого края
                F += AL[g];//Дописываем в алфавит шифрования новую букву
            }
            label3.Text = F;//Выводим алфавит шифрования в соотвутвующее поле
        }
    }
}
