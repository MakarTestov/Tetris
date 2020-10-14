using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisBitmap.Tetris
{
    /// <summary>
    /// Класс для хранения основных особенностей игры Тетрис
    /// </summary>
    class ParamsGame
    {
        #region ParamsGame
        /// <summary>
        /// Панель на которой осуществляется игра
        /// </summary>
        private  Panel panelGame;

        /// <summary>
        /// Свойство. Объект места игры
        /// </summary>
        public Panel PanelGame
        {
            get { return panelGame; }
            set { panelGame = value; }
        }

        private byte countpartX;
        /// <summary>
        /// Сколько отдельных частей фигуры должны вместить по оси Х
        /// </summary>
        public byte CountpartX
        {
            get { return countpartX; }
            set { countpartX = value; }
        }

        private byte countpartY;
        /// <summary>
        /// Сколько отдельных частей фигуры должны вместить по оси Y
        /// </summary>
        public byte CountpartY
        {
            get { return countpartY; }
            set { countpartY = value; }
        }

        private Size partFigure;
        /// <summary>
        /// Размер отдельной части фигуры
        /// </summary>
        public Size PartFigure
        {
            get { return partFigure; }
            set { partFigure = value; }
        }

        private Point startGame;
        /// <summary>
        /// Координаты начала игры (где появляются новые фигуры)
        /// </summary>
        public Point StartGame
        {
            get { return startGame; }
            set { startGame = value; }
        }

        /// <summary>
        /// Объект структуры Move для определения направления движения фигуры
        /// </summary>
        private Move moveFigure;
        public delegate void ChangeMove(object sender, EventArgs e);

        /// <summary>
        /// Событие вызываемое после изменения направдления движения
        /// </summary>
        public event ChangeMove Changemove;

        /// <summary>
        /// Определяет направление движения фигуры
        /// </summary>
        public Move MoveFigure
        {
            get { return moveFigure; }
            set 
            { 
                moveFigure = value;
                Changemove?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Структура для направления движения фигур
        /// </summary>
        public struct Move
        {
            /// <summary>
            /// Направление движения по оси Х
            /// </summary>
            public sbyte x;
            /// <summary>
            /// Направление движения по оси У
            /// </summary>
            public sbyte y;
        }

        public delegate void Gameing(object sender, EventArgs e);

        /// <summary>
        /// Событие возникающее при окончании или продолжении игры
        /// </summary>
        public event Gameing PlayGame;

        private bool canGame;
        /// <summary>
        /// Может ли продолжаться игра?
        /// </summary>
        public bool CanGame
        {
            get { return canGame; }
            set
            {
                
                canGame = value;
                PlayGame?.Invoke(this, new EventArgs());
            }
        }
        #endregion

        /// <summary>
        /// Закрытый конструктор для единого экземпляра
        /// </summary>
        private ParamsGame()
        {

        }

        /// <summary>
        /// Объект для работы с общими параметрами игры
        /// </summary>
        private static ParamsGame paramsGame;

        /// <summary>
        /// Получить объект класа ParamsGame, для управления основными свойствами игры
        /// </summary>
        /// <returns>Объект ParamsGame</returns>
        public static ParamsGame GetParamsGame()
        {
            if (paramsGame == null)
            {
                paramsGame = new ParamsGame();
            }
            return paramsGame;
        }
    }
}
