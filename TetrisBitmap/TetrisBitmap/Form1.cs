using System;
using System.Drawing;
using System.Windows.Forms;
using TetrisBitmap.Tetris;
using TetrisBitmap.Tetris.Figure;

namespace TetrisBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузки формы, задаем необходимые параметры игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ParamsGame.GetParamsGame().PanelGame = panGame;
            ParamsGame.GetParamsGame().CountpartX = 15;
            ParamsGame.GetParamsGame().CountpartY = 20;
            ParamsGame.GetParamsGame().PartFigure = new Size(panGame.Width / 15, panGame.Height / 20);
            ParamsGame.GetParamsGame().StartGame = new Point(140, 0);
            ParamsGame.GetParamsGame().MoveFigure = new ParamsGame.Move { x = 0, y = 1 };
            ControlGame.GetControlGame();
            ControlGame.GetControlGame().Change_Next_Figure += ChangeFigure;
            ControlGame.GetControlGame().Change_Count += ChangeCount;
            //ParamsGame.GetParamsGame().PlayGame += sdf;
        }

        /// <summary>
        /// Событие запуска игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butStartGame_Click(object sender, EventArgs e)
        {
            labCount.Text = "0";
            ParamsGame.GetParamsGame().CanGame = true;
        }

        /// <summary>
        /// Изменение движения объекта(фигуры)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A: //Осуществляется движение направо
                    {
                        ParamsGame.GetParamsGame().MoveFigure = new ParamsGame.Move { x = -1, y = 1 };
                        break;
                    }
                case Keys.D: //Осуществляется движение налево
                    {
                        ParamsGame.GetParamsGame().MoveFigure = new ParamsGame.Move { x = 1, y = 1 };
                        break;
                    }
                default:
                    {
                        break;
                    }
                case Keys.E: //Осуществляет поворот фигуры
                    {
                        ControlGame.GetControlGame().RotateFigure();
                        break;
                    }
            }
        }

        /// <summary>
        /// Событие рисующее на форме следующую фигуру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeFigure(FigureTetris sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(panNextFigure.Width, panNextFigure.Height);
            Graphics g = Graphics.FromImage(bitmap);
            Point sp = new Point(20, 0);
            Pen p = new Pen(Color.Black, 1);
            Size size = new Size(20, 20);
            for(byte i = 0; i<sender.Figure.Count;i++)
            {
                g.DrawRectangle(p,
                    sp.X + sender.Figure[i].X * size.Width,
                    sp.Y + sender.Figure[i].Y * size.Height,
                    size.Width,
                    size.Height);
            }
            panNextFigure.BackgroundImage = bitmap;
        }

        /// <summary>
        /// Вызывается, если счет увеличивается
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCount(object sender, EventArgs e)
        {
            labCount.Text = Convert.ToString(ControlGame.GetControlGame().Count * ParamsGame.GetParamsGame().PartFigure.Width);
        }

        /// <summary>
        /// Этот класс, который подключает нужный стиль для формы и всех его внутренних компонентов от мерцания
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
    }
}
