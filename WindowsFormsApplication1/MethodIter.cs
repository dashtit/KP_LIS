using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public class MethodIter
    {
        public static double Count(double[,] c, double f, double[] x, int i)
        {
            double y = f;
            for (int j = 0; j < c.GetLength(0); j++)
                y += c[i, j] * x[j];
            return y;
        }
        public static void Div(ref double[,] a, ref double b, double del, int i) // функция для подсчета метода
        {
            for (int j = 0; j < a.GetLength(0); j++)
                a[i, j] = -a[i, j] / del;
            b /= del;
        }

        

        public static void Solve(double[,] A, double[] B, ref double[] X, double E) // этот метод считает методом простых итераций 
        {
            int n = A.GetLength(0); // мы передаем матрицы введеные в форме 1 и проводим вычисления
            double[] Y = new double[n];
            double N = X[0];
            do
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                        {
                            double Z = A[i, j];
                            A[i, j] = 0;
                            Y[i] = Count(A, B[i], X, i);
                            A[i, j] = Z;
                        }
                    }
                for (int i = 0; i < n; i++) // проверка на продолжение вычислений 
                {
                    if (N > Math.Abs(X[i] - Y[i]))
                        N = Math.Abs(X[i] - Y[i]);
                    X[i] = Y[i];
                }
            }
            while (N > E);
        }
    }
}
