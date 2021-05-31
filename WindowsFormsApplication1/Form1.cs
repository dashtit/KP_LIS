using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form // это код 1 формы
    {
        private int n; // это кол-во неизвестных
        private double E; // это точность
        public Form1()
        {
            InitializeComponent();  // конструктор
        }
        private void Button4_Click(object sender, EventArgs e) // кнопка очистки
        {
            UpDate(dataGridView1);
            UpDate(dataGridView2);
            UpDate(dataGridView3);
            textBox1.Clear();
        }
        private void button5_Click(object sender, EventArgs e) // загрузка матрицы с ПК
        {
            string fileContent;
            string[] fileContentArr = { };

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Получаем путь к указанному файлу 
                    string filePath = openFileDialog.FileName;


                    fileContentArr = File.ReadAllLines(filePath);

                }
            }

            UpDate(dataGridView1);
            UpDate(dataGridView2);

            int counter = 0;
            int nums = 0;
            dataGridView2.Columns.Add("", "");
            int emptyLines = 0;
            foreach (string line in fileContentArr)
            {
                if (nums == 0)
                {
                    if (int.TryParse(line, out nums))
                    {
                        n = nums;
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("Не валидный файл");
                        return;
                    }
                }

                if (line == "")
                {
                    emptyLines++;
                    continue;
                }

                while (counter < nums)
                {
                    dataGridView1.Columns.Add("", "");
                    counter++;
                }

                while (emptyLines == 1)
                {
                    string[] str = line.Split(' ');
                    dataGridView1.Rows.Add(str);
                    break;
                }

                while (emptyLines == 2)
                {
                    dataGridView2.Rows.Add(line);
                    break;
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e) // при нажатии на кнопку создается матрица в datagridview такого размера, какой был задан в кол-ве неизвестных
        {
            try
            {
                n = int.Parse(textBox1.Text); // берем значение кол-ва неизвестных, введенного в текстбокс
                E = double.Parse(textBox2.Text); // то же самое для точности
                if (n > 0 && n < 40)
                {
                    UpDate(dataGridView1);
                    UpDate(dataGridView2); 
                    dataGridView2.Columns.Add("", "");
                    for (int i = 0; i < n; i++) //цикл добавляет по ячейке на каждую итерацию, итерация = кол-во незвестных
                    {
                        dataGridView1.Columns.Add("", "");
                        dataGridView1.Rows.Add();
                        dataGridView2.Rows.Add();
                    }
                }
                else
                {
                    textBox1.Clear();
                    MessageBox.Show("Количество неизвестных некорректно!");
                }
            }
            catch (Exception ex)
            {
                textBox1.Clear();
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2(); //кнопка помощи - открывает вторую форму
            form2.Show();

        }
        private void Button3_Click(object sender, EventArgs e)
        {
            double[,] A = new double[n, n];
            double[] B = new double[n];
            double[] X = new double[n];
            E = double.Parse(textBox2.Text);
            bool FLAG = true;
            if (E != 0) ///// вот эту строчку
            { ///// вот эту скобку
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                    for (int column = 0; column < dataGridView1.Columns.Count; column++) // циклы на проверку пустых значений или букв

                        try   //проходит по каждой ячейке в датагриде и смотрит число это или нет
                        {
                            if (Convert.ToString(dataGridView1.Rows[row].Cells[column].Value) != "") // проверка ячейки на пустоту
                            {
                                A[row, column] = Convert.ToInt32(dataGridView1.Rows[row].Cells[column].Value);
                            }
                            else // если в ячейке не число, выкидываем мэсседжбокс
                            {
                                MessageBox.Show("Введите все значения", "Ячейки с значениями пусты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                UpDate(dataGridView1);
                                UpDate(dataGridView2);
                                UpDate(dataGridView3);
                                FLAG = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            dataGridView1.Rows[row].Cells[column].Value = "";

                            FLAG = false;
                        }

                for (int row = 0; row < dataGridView2.Rows.Count; row++) // та же самая проверка, но для матрицы из datagridview2 (там, где свободные члены)
                    try
                    {
                        if (Convert.ToString(dataGridView2.Rows[row].Cells[0].Value) != "")
                        {
                            B[row] = Convert.ToInt32(dataGridView2.Rows[row].Cells[0].Value);
                        }
                        else
                        {
                            MessageBox.Show("Введите все значения", "Ячейки с значениями пусты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            UpDate(dataGridView1);
                            UpDate(dataGridView2);
                            UpDate(dataGridView3);
                            FLAG = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dataGridView2.Rows[row].Cells[0].Value = "";

                        FLAG = false;
                    }
                bool flag = false;
                int counter1 = 0;
                int counter2 = 0;
                for (int i = 0; i < n; i++)
                {
                    if (A[n - 1, n - 1] == 0)
                        counter1++;
                }
                if (counter1 == n)
                {
                    flag = true;
                }
                bool flag2 = false;
                if (flag == true)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (B[n - 1] == 0)
                            counter2++;
                    }
                }
                if (counter2 == n)
                {
                    flag2 = true;
                }

                if (flag == true && flag2 == false && FLAG == true)
                {
                    MessageBox.Show("Данная матрица решений не имеет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpDate(dataGridView3);

                    dataGridView3.Columns.Add("", "");

                    int accuracy = 0;
                    while (E < 1)
                    {
                        E *= 10;
                        accuracy++;
                    }

                    for (int row = 0; row < X.Length; row++) // выводим результаты в третий датагрид 
                    {

                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[row].Cells[0].Value = Math.Round(X[row], accuracy);

                    }
                }
                else if (flag == true && flag2 == true && FLAG == true)
                {
                    MessageBox.Show("Данная матрица имеет бесконечно много решений", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpDate(dataGridView3);
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                            if (i == j)
                            {
                                MethodIter.Div(ref A, ref B[i], A[i, j], i); // если прошло проверку, отправляем считаться в MethodIter
                                X[i] = B[i];
                            }
                    }
                    MethodIter.Solve(A, B, ref X, E); // считаем
                    UpDate(dataGridView3);

                    dataGridView3.Columns.Add("", "");

                    int accuracy = 0;
                    while (E < 1)
                    {
                        E *= 10;
                        accuracy++;
                    }
                    int number;
                    bool result = true; ////// флаг для трайпарс
                    bool LETTER = false; ///// флаг для буквы
                    for (int row = 0; row < dataGridView1.Rows.Count; row++)
                    {
                        for (int column = 0; column < dataGridView1.Columns.Count; column++)
                        {
                            if (result != int.TryParse((string)dataGridView1.Rows[row].Cells[column].Value, out number)) // проверяем число ли было введено если нет 
                            {
                                LETTER = true; // то ставим флаг что это буква
                            }
                        }
                    }
                    for (int row = 0; row < dataGridView2.Rows.Count; row++)  // ТО же самое для матрицы В
                    {
                        if (result != int.TryParse((string)dataGridView2.Rows[row].Cells[0].Value, out number))
                        {
                            LETTER = true;
                        }
                    }
                    if (LETTER == true) // если буква выкидываем мессэджбокс
                    {
                        MessageBox.Show("Строка имела неверный формат", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else // если не буква то
                    {

                        for (int row = 0; row < X.Length; row++) //  выводим результаты в третий датагрид 
                        {

                            dataGridView3.Rows.Add();
                            dataGridView3.Rows[row].Cells[0].Value = Math.Round(X[row], accuracy);

                        }
                    }
                }
            } //// вот эту скобку
            else///// и вот отсюда
            {
                MessageBox.Show("Некорректное значение точности", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }/////// досюда
        }
            private void UpDate(DataGridView dataGridView) // ф-ция, которую будем вызывать при нажатии на копнку "Начать сначала"
            {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            }
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e) // сохранение полученного результата матрицы
        {
            using (SaveFileDialog openFileDialog = new SaveFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;

                string filename = openFileDialog.FileName;

                List<string> lines = new List<string>();

                for(int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    lines.Add(dataGridView3.Rows[i].Cells[0].Value.ToString());
                }

                 File.WriteAllLines(filename, lines);

            }
        }
    }

}
