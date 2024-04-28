using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surname_Group_Course_project
{

    class MajorWork
    {
        private System.DateTime TimeBegin;
        private string Data; //вхідні дані
        private string Result; // Поле результату

        public void SetTime() // метод запису часу початку роботи програми
        {
            this.TimeBegin = System.DateTime.Now;
        }// Методи

        public System.DateTime GetTime() // Метод отримання часу завершення програми
        {
            return this.TimeBegin;
        }
            public void Write(string D)// метод запису даних в об'єкт.
        {
            this.Data = D;
        }
        public string Read()
        {
            return this.Result;// метод відображення результату
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
        }
    }
}