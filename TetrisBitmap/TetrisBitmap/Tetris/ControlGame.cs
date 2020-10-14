using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TetrisBitmap.Tetris.Figure;

namespace TetrisBitmap.Tetris
{
    /// <summary>
    /// Класс для управления игрой тетрис
    /// </summary>
    class ControlGame
    {
        #region Поля
        /// <summary>
        /// Таймер игры в тетрис
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Возвращает объект ControlGame, контролирующий игру
        /// </summary>
        private static ControlGame controlGame;

        /// <summary>
        /// Счет игры, измеряет количество удаленных строк на поле
        /// </summary>
        private int count = 0;

        public delegate void ChangeCount(object sender, EventArgs e);

        /// <summary>
        /// Событие возникающее после изменений счета игры
        /// </summary>
        public event ChangeCount Change_Count;

        /// <summary>
        /// Свойство. Счет игры
        /// </summary>
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                Change_Count?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Координаты текущей фигуры
        /// </summary>
        public Point nowFigurePoint;

        private List<FigureTetris> allfigure;

        /// <summary>
        /// Содержит все возможные фигуры тетриса
        /// </summary>
        public List<FigureTetris> Allfigure
        {
            get { return allfigure; }
            set { allfigure = value; }
        }

        /// <summary>
        /// Текущая созданная фигура на поле
        /// </summary>
        private FigureTetris nowfigure;

        public delegate void ChangeFigure(object sender, EventArgs e);

        /// <summary>
        /// Событие вызываемое после изменений фигуры
        /// </summary>
        public event ChangeFigure Change_Figure;

        /// <summary>
        /// Созданая и управляемая фигура
        /// </summary>
        public FigureTetris nowFigure
        {
            get { return nowfigure; }
            set 
            { 
                nowfigure = value;
                Change_Figure?.Invoke(this, new EventArgs());
            }
        }


        public delegate void ChangeNextFigure(FigureTetris sender, EventArgs e);

        /// <summary>
        /// Событие при изменении следующей фигуры игры
        /// </summary>
        public event ChangeNextFigure Change_Next_Figure;

        /// <summary>
        /// Объект следующй фигуры, которая выйдет на поле
        /// </summary>
        private FigureTetris nextFigure;

        /// <summary>
        /// Свойство. Возвращает следующую фигуру игры
        /// </summary>
        public FigureTetris NextFigure
        {
            get { return nextFigure; }
            set
            {
                nextFigure = value;
                Change_Next_Figure?.Invoke(nextFigure, new EventArgs());
            }

        }

        /// <summary>
        /// Поле игры соразмерное с частями по исям X и Y.
        /// true - свободно, false - занято
        /// </summary>
        private bool[,] masGame;

        /// <summary>
        /// Видимое игроком поле
        /// </summary>
        public Bitmap gamepole;

        /// <summary>
        /// Для рисования на gamepole;
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Ручка для рисования фигур
        /// </summary>
        private SolidBrush p;
        #endregion

        /// <summary>
        /// Конструктор для определений свойст таймера и подготовки к игре
        /// </summary>
        private ControlGame()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Tick_Timer;
            ParamsGame.GetParamsGame().PlayGame += StartGame;
            ClearPoleGame();
            p = new SolidBrush(Color.Blue);
            nowFigurePoint = ParamsGame.GetParamsGame().StartGame;
            CreateFigures();
            nextFigure = allfigure[new Random().Next(0, allfigure.Count)];
            CreateRandomFigure();
            ParamsGame.GetParamsGame().Changemove += ChangeMove;
        }

        /// <summary>
        /// Возвращает объект, для котроля игры
        /// </summary>
        /// <returns></returns>
        public static ControlGame GetControlGame()
        {
            if (controlGame == null)
            {
                controlGame = new ControlGame();
            }
            return controlGame;
        }

        /// <summary>
        /// Изменить скорость игры
        /// </summary>
        /// <param name="slow">На сколько медленно нужно играть</param>
        public void ChangeSpeed(int slow)
        {
            timer.Interval = slow;
        }

        #region Создание фигур
        /// <summary>
        /// Создает случайную фигуру тетриса
        /// </summary>
        public void CreateRandomFigure()
        {
            nowFigure = nextFigure;
            NextFigure = allfigure[new Random().Next(0, allfigure.Count)];
        }

        /// <summary>
        /// Создает все фигуры игры
        /// </summary>
        public void CreateFigures()
        {
            allfigure = new List<FigureTetris>();
            Line linr = new Line();
            linr.CreateFigure();
            allfigure.Add(linr);

            Light light = new Light();
            light.CreateFigure();
            allfigure.Add(light);

            RLight rlight = new RLight();
            rlight.CreateFigure();
            allfigure.Add(rlight);

            G mg = new G();
            mg.CreateFigure();
            allfigure.Add(mg);

            RG rg = new RG();
            rg.CreateFigure();
            allfigure.Add(rg);

            Tank tank = new Tank();
            tank.CreateFigure();
            allfigure.Add(tank);

            Square square = new Square();
            square.CreateFigure();
            allfigure.Add(square);
        }
        #endregion

        #region Чистка игрового поля
        /// <summary>
        /// Чистит поле игры
        /// </summary>
        public void ClearPoleGame()
        {
            ClearMasPole();
            ClearSeenPole();
        }

        /// <summary>
        /// Чистит массив поля занятых и свободны зон
        /// </summary>
        public void ClearMasPole()
        {
            masGame = new bool[ParamsGame.GetParamsGame().CountpartY, 
                ParamsGame.GetParamsGame().CountpartX];
            for (byte i = 0; i < masGame.GetLength(0); i++)
            {
                for (byte j = 0; j < masGame.GetLength(1); j++)
                {
                    masGame[i, j] = true;
                }
            }
        }

        /// <summary>
        /// Чистит видимое игроком поле
        /// </summary>
        public void ClearSeenPole()
        {
            gamepole = new Bitmap(ParamsGame.GetParamsGame().PanelGame.Width, 
                ParamsGame.GetParamsGame().PanelGame.Height);
            g = Graphics.FromImage(gamepole);
        }
        #endregion

        #region Рисование на поле и отрисовка игры
        /// <summary>
        /// Отрисовывает все объекты на поле
        /// </summary>
        public void ShowGamePole()
        {
            ClearSeenPole();
            //Если дальше двигать фигуру нельзя. то останавливаем ее и создаем новую
            if (Collision.GetCollision().BeCollision(nowFigurePoint, nowfigure, masGame).Y)
            {

                ChangeMasPole();
                NewFigureOnGamePole();
            }
            ShowWall();
            Showfigure();
            ParamsGame.GetParamsGame().PanelGame.BackgroundImage = gamepole;
        }

        /// <summary>
        /// Отрисовывает стены игры ( остановившиеся фигуры)
        /// </summary>
        public void ShowWall()
        {
            for (byte i = 0; i < masGame.GetLength(0); i++)
            {
                for (byte j = 0; j < masGame.GetLength(1); j++)
                {
                    if (masGame[i, j] == false)
                    {
                        g.FillRectangle(p,
                            j * ParamsGame.GetParamsGame().PartFigure.Height,
                            i * ParamsGame.GetParamsGame().PartFigure.Width,
                            ParamsGame.GetParamsGame().PartFigure.Width,
                            ParamsGame.GetParamsGame().PartFigure.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Отрисовывает фигуру
        /// </summary>
        public void Showfigure()
        {
            for (byte i = 0; i < nowfigure.Figure.Count; i++)
            {
                g.FillRectangle(p,
                    nowFigurePoint.X + nowfigure.Figure[i].X * ParamsGame.GetParamsGame().PartFigure.Width,
                    nowFigurePoint.Y + nowfigure.Figure[i].Y * ParamsGame.GetParamsGame().PartFigure.Height,
                    ParamsGame.GetParamsGame().PartFigure.Width,
                    ParamsGame.GetParamsGame().PartFigure.Height);
            }
        }
        #endregion

        /// <summary>
        /// Изменяет цвет создаваемой фигуры
        /// </summary>
        /// <param name="c"></param>
        public void ChangeColorFigure(Color c)
        {
            p = new SolidBrush(c);
        }

        #region События
        /// <summary>
        /// Запускает или останавливает игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(object sender, EventArgs e)
        {
            if (ParamsGame.GetParamsGame().CanGame)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
                ClearPoleGame();
            }
        }

        /// <summary>
        /// Событие при каждом тике таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_Timer(object sender, EventArgs e)
        {
            ShowGamePole();
            nowFigurePoint = new Point(nowFigurePoint.X, 
                nowFigurePoint.Y + ParamsGame.GetParamsGame().PartFigure.Height);
            CheckMasPole();
            //Проверка, что самая верхнее поле не имеет фигур, иниче проигрываем
            for(byte i = 0; i < masGame.GetLength(1);i++)
            {
                if(masGame[0,i]==false)
                {
                    ParamsGame.GetParamsGame().CanGame = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Изменение начальных координат, при изменении направления движения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeMove(object sender, EventArgs e)
        {
            if (Collision.GetCollision().BeCollision(nowFigurePoint, nowfigure, masGame).X == false)
            {
                nowFigurePoint = new Point(
                  nowFigurePoint.X + ParamsGame.GetParamsGame().PartFigure.Width * ParamsGame.GetParamsGame().MoveFigure.x,
                  nowFigurePoint.Y);
            }
        }
        #endregion

        /// <summary>
        /// Создает новую фигуру, задает возвращает в начальное значение положение фигуры
        /// </summary>
        public void NewFigureOnGamePole()
        {
            nowfigure.PointDefault();
            CreateRandomFigure();
            nowFigurePoint = ParamsGame.GetParamsGame().StartGame;
        }

        /// <summary>
        /// Изменить поле, при остановке фигуры
        /// </summary>
        public void ChangeMasPole()
        {
            for (byte i = 0; i < nowfigure.Figure.Count; i++)
            {
                try
                {
                    int X = (nowfigure.Figure[i].X * 
                        ParamsGame.GetParamsGame().PartFigure.Width + nowFigurePoint.X) 
                        / ParamsGame.GetParamsGame().PartFigure.Width;
                    int Y = (nowfigure.Figure[i].Y * 
                        ParamsGame.GetParamsGame().PartFigure.Height + nowFigurePoint.Y) 
                        / ParamsGame.GetParamsGame().PartFigure.Height;
                    masGame[Y - 1, X] = false;
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Повернуть текущую фигуру с учетом колизии
        /// </summary>
        public void RotateFigure()
        {
            nowfigure.RotateBy();
            Collision.CollisionXY collisionXY = 
                Collision.GetCollision().BeCollision(nowFigurePoint, nowfigure, masGame);
            if (collisionXY.X || collisionXY.Y)
            {
                nowfigure.RotateCon();
            }
        }

        /// <summary>
        /// Проверить, есть ли занятые строки, и удалить их, сдвинув все вниз
        /// </summary>
        public void CheckMasPole()
        {
            for (byte i = Convert.ToByte(masGame.GetLength(0) - 1); i > 0; i--)
            {
                byte c = 0;
                for (byte j = 0; j < masGame.GetLength(1); j++)
                {
                    if (masGame[i, j] == false)
                    {
                        c++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (c == masGame.GetLength(1))
                {
                    Count++;
                    for (byte f = i; f > 0; f--)
                        for (byte j = 0; j < masGame.GetLength(1); j++)
                        {
                            masGame[f, j] = masGame[f - 1, j];
                        }
                }
            }
        }
    }
}
