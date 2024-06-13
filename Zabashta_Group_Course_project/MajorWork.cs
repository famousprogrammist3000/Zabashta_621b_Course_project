﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Buffer = Zabashta_Group_Course_project.Buffer;

namespace Zabashta_Group_Course_project
{
    class MajorWork
    {
        private System.DateTime TimeBegin;
        private string Data; //вхідні дані
        private string Result; // Поле результату
        public bool Modify;
        private int Key; // поле ключа
        private string SaveFileName; // ім’я файлу для запису
        private string OpenFileName; // ім’я файлу для читання

        public void SetTime() // метод запису часу початку роботи програми
        {
            this.TimeBegin = System.DateTime.Now;
        } // Методи

        public System.DateTime GetTime() // Метод отримання часу завершення програми
        {
            return this.TimeBegin;
        }

        public void Write(string D) // метод запису даних в об'єкт.
        {
            this.Data = D;
        }

        public string Read()
        {
            return this.Result; // метод відображення результату
        }

        public void Task() // метод реалізації програмного завдання
        {
            if (this.Data.Length > 5)
            {
                this.Result = Convert.ToString(true);
            }
            else
            {
                this.Result = Convert.ToString(false);
            }
            this.Modify = true; // Дозвіл запису
        }

        public void WriteSaveFileName(string S) // метод запису даних в об'єкт
        {
            this.SaveFileName = S; // запам'ятати ім’я файлу для запису
        }

        public void WriteOpenFileName(string S)
        {
            this.OpenFileName = S; // запам'ятати ім’я файлу для відкриття
        }

        public void SaveToFile() // Запис даних до файлу
        {
            if (!this.Modify)
                return;
            try
            {
                Stream S; // створення потоку
                if (File.Exists(this.SaveFileName)) // існує файл?
                    S = File.Open(this.SaveFileName, FileMode.Append); // Відкриття файлу для збереження
                else
                    S = File.Open(this.SaveFileName, FileMode.Create); // створити файл

                Buffer D = new Buffer(); // створення буферної змінної
                D.Data = this.Data;
                D.Result = Convert.ToString(this.Result);
                D.Key = Key;

                BinaryFormatter BF = new BinaryFormatter(); // створення об'єкта для форматування
                BF.Serialize(S, D);
                S.Flush(); // очищення буфера потоку
                S.Close(); // закриття потоку
                this.Modify = false; // Заборона повторного запису
                D.Key = Key;
                Key++;
            }
            catch
            {
                MessageBox.Show("Помилка роботи з файлом"); // Виведення на екран повідомлення "Помилка роботи з файлом"
            }
        }

        public void ReadFromFile(System.Windows.Forms.DataGridView DG) // зчитування з файлу
        {
            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("Файлу немає"); // Виведення на екран повідомлення "файлу немає"
                    return;
                }

                Stream S; // створення потоку
                S = File.Open(this.OpenFileName, FileMode.Open); // зчитування даних з файлу

                Buffer D; // буферна змінна для контролю формату
                Object O;

                BinaryFormatter BF = new BinaryFormatter(); // створення об'єкта для форматування
                O = BF.Deserialize(S); // десеріалізація
                D = (Buffer)O;

                System.Data.DataTable MT = new System.Data.DataTable();
                System.Data.DataColumn cKey = new System.Data.DataColumn("Ключ"); // формуємо колонку "Ключ"
                System.Data.DataColumn cInput = new System.Data.DataColumn("Вхідні дані"); // формуємо колонку "Вхідні дані"
                System.Data.DataColumn cResult = new System.Data.DataColumn("Результат"); // формуємо колонку "Результат"

                MT.Columns.Add(cKey); // додавання ключа
                MT.Columns.Add(cInput); // додавання вхідних даних
                MT.Columns.Add(cResult); // додавання результату

                while (S.Position < S.Length)
                {
                    D = (Buffer)BF.Deserialize(S); // десеріалізація
                    if (D == null) break;

                    System.Data.DataRow MR;
                    MR = MT.NewRow();
                    MR["Ключ"] = D.Key; // Занесення в таблицю номера
                    MR["Вхідні дані"] = D.Data; // Занесення в таблицю вхідних даних
                    MR["Результат"] = D.Result; // Занесення в таблицю результату

                    MT.Rows.Add(MR);
                }

                DG.DataSource = MT;
                S.Close(); // закриття
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення "Помилка файлу"
            }
        } // ReadFromFile закінчився

        public void Generator() // метод формування ключового поля
        {
            try
            {
                if (!File.Exists(this.SaveFileName)) // існує файл?
                {
                    Key = 1;
                    return;
                }
                Stream S; // створення потоку
                S = File.Open(this.SaveFileName, FileMode.Open); // Відкриття файлу

                object O; // буферна змінна для контролю формату
                BinaryFormatter BF = new BinaryFormatter(); // створення елементу для форматування
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    Buffer D = O as Buffer;
                    if (D == null) break;
                    Key = D.Key;
                }
                Key++;
                S.Close();
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення "Помилка файлу"
            }
        }

        public bool SaveFileNameExists()
        {
            if (this.SaveFileName == null)
                return false;
            else return true;
        }

        public void NewRec() // новий запис
        {
            this.Data = ""; // "" - ознака порожнього рядка
            this.Result = null; // для string - null
            this.Modify = false; // для bool - false
            this.Key = 0; // для int - 0
            this.SaveFileName = null; // для string - null
            this.OpenFileName = null; // для string - null
        }

        public void Find(string Num) // пошук
        {
            int N;
            try
            {
                N = Convert.ToInt16(Num); // перетворення номера рядка в int16 
            }
            catch
            {
                MessageBox.Show("помилка пошукового запиту"); // Виведення на екран повідомлення
                return;
            }

            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("файлу немає"); // Виведення на екран повідомлення
                    return;
                }
                Stream S; // створення потоку
                S = File.Open(this.OpenFileName, FileMode.Open); // відкриття файлу
                Buffer D;
                object O; // буферна змінна для контролю формату
                BinaryFormatter BF = new BinaryFormatter(); // створення об'єкта для форматування

                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    if (D.Key == N) // перевірка дорівнює чи номер пошуку номеру рядка в таблиці
                    {
                        string ST;
                        ST = "Запис містить:" + (char)13 + "No" + Num + "Вхідні дані:" +
                             D.Data + "Результат:" + D.Result;

                        MessageBox.Show(ST, "Запис знайдена"); // Виведення на екран повідомлення
                        S.Close();
                        return;
                    }
                }
                S.Close();
                MessageBox.Show("Запис не знайдена"); // Виведення на екран повідомлення
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення
            }
        } // Find закінчився
    }
}
