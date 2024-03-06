using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_Lab_1
{
    public partial class Simulation1 : Form
    {
        public Simulation1()
        {
            InitializeComponent();
        }

        double height;
        double angle;
        double speed;
        double size;
        double weight;
        double step;
        double x;
        double y;
        double vx;
        double vy;
        double cosa;
        double sina;
        double beta;
        double k;
        double max = 0;
        int i = 0;
        int m = 0;

   
        const double g = 9.81;
        const double C = 0.15;
        const double rho = 1.29;

        private void btnStart_Click(object sender, EventArgs e)
        {

            step = (double)edStep.Value; 
            height = (double)edHeight.Value;
            angle = (double)edAngle.Value;
            speed = (double)edSpeed.Value;
            size = (double)edSize.Value;
            weight = (double)edWeight.Value;

            cosa = Math.Cos(angle * Math.PI / 180);
            sina = Math.Sin(angle * Math.PI / 180);

            beta = 0.5 * C * size * rho;
            k = beta/weight;

            x = 0;
            y = height;
            vx = speed * cosa;
            vy = speed * sina;
            
            timer1.Start(); //график
            dataGrid.Rows.Add(chart1.Series[i].Name, "", "", ""); //таблица
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = (int)edInterval.Value;
            
            double root = Math.Sqrt(vx * vx + vy * vy);

            vx = vx - k * vx * root * step;
            vy = vy - (g + k * vy * root) * step;

            x = x + vx * step;
            y = y + vy * step;

            if (y > max) max = y;

            chart1.Series[i].Points.AddXY(x, y);

            if (y <= 0) 
            { 
                timer1.Stop();

                m = dataGrid.Rows.Count;
                dataGrid.Rows[m - 1].Cells[1].Value = x; //расстояние
                dataGrid.Rows[m - 1].Cells[2].Value = max; //максимальная высота
                dataGrid.Rows[m - 1].Cells[3].Value = Math.Sqrt(vx * vx + vy * vy); //конечная скорость

                if (i < 4) 
                { 
                    i += 1; 
                } 
                else 
                { 
                    i = 0; 
                }

                max = 0;
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
