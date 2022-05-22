using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace denis12._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Глобальные переменные 
        private Point PreviousPoint, point;
        private Bitmap bmp;
        private Pen blackPen;
        private Graphics g;
        // Действия при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // Подготавливаем перо для рисования 
            blackPen = new Pen(Color.Black, 4);
        }
        // Действия при нажатии кнопки загрузки изображения 
        private void button1_Click(object sender, EventArgs e)
        {
            // Описываем объект класса OpenFileDialog 
            OpenFileDialog dialog = new OpenFileDialog();
            // Задаем расширения файлов  
            dialog.Filter = "Image files (*.BMP, *.JPG, " +
            "*.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            // Вызываем диалог и проверяем выбран ли файл 
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Загружаем изображение из выбранного файла 
                Image image = Image.FromFile(dialog.FileName);
                int width = image.Width;
                int height = image.Height;
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                // Создаем и загружаем изображение в формате bmp 
                bmp = new Bitmap(image, width, height);
                // Записываем изображение в pictureBox1 
                pictureBox1.Image = bmp;
                // Подготавливаем объект Graphics для рисования
                g = Graphics.FromImage(pictureBox1.Image);
            }
        }
        // Действия при нажатии мышки в pictureBox1 
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Записываем в предыдущую точку текущие координаты
            PreviousPoint.X = e.X;
            PreviousPoint.Y = e.Y;
        }
        // Действия при перемещении мышки
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Проверяем нажата ли левая кнопка мыши 
            if (e.Button == MouseButtons.Left)
            {
                // Запоминаем текущее положение курсора мыши
                point.X = e.X;
                point.Y = e.Y;
                // Соеденяем линией предыдущую точку с текущей
                g.DrawLine(blackPen, PreviousPoint, point);
                // Текущее положение курсора ‐ в PreviousPoint
                PreviousPoint.X = point.X;
                PreviousPoint.Y = point.Y;
                // Принудительно вызываем перерисовку 
                pictureBox1.Invalidate();
            }
        }
        // Действия при нажатии кнопки сохранения файла
        private void button2_Click(object sender, EventArgs e)
        {
            // Описываем и порождаем объект savedialog
            SaveFileDialog savedialog = new SaveFileDialog();
            // Задаем свойства для savedialog 
            savedialog.Title = "Сохранить картинку как ..."; savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
            "Bitmap File(*.bmp)|*.bmp|" +
            "GIF File(*.gif)|*.gif|" +
            "JPEG File(*.jpg)|*.jpg|" +
            "PNG File(*.png)|*.png";
            // Показываем диалог и проверяем задано ли имя файла 
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                // Убираем из имени расширение файла 
                string strFilExtn = fileName.Remove(0,
                fileName.Length - 3);
                // Сохраняем файл в нужном формате 
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }
        // Действия при нажатии кнопки перевода в градации серого 
        private void button3_Click(object sender, EventArgs e)
        {
            // Циклы для перебора всех пикселей на изображении
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //Разработайте функцию, которая переводит каждый нечетный столбец пикселей(вертикальные линии) в градации серого цвета.
                    if (j % 2 == 1)//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    {
                        // Извлекаем в R значение красного цвета 
                        int R = bmp.GetPixel(i, j).R;
                        // Извлекаем в G значение зеленого цвета 
                        int G = bmp.GetPixel(i, j).G;
                        // Извлекаем в B значение синего цвета 
                        int B = bmp.GetPixel(i, j).B;
                        // Высчитываем среднее арифметическое 
                        int Gray = (R + G + B) / 3;
                        // Переводим число в значение цвета.  
                        // 255 – показывает степень прозрачности. 
                        // Остальные значения одинаковы 
                        Color p = Color.FromArgb(255, Gray, Gray, Gray);
                        // Записываем цвет в текущую точку 
                        bmp.SetPixel(i, j, p);
                    }

                }
            }
            // Вызываем функцию перерисовки окна 
            Refresh();
        }
    }
}
