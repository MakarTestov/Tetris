using System.Drawing;
using TetrisBitmap.Tetris.Figure;

namespace TetrisBitmap.Tetris
{
    /// <summary>
    /// Класс для определения коллизии движений фигур
    /// </summary>
    class Collision
    {
        /// <summary>
        /// Закрытый конструктор для singleton
        /// </summary>
        private Collision()
        {

        }

        /// <summary>
        /// Экземпляр объекта класса Collision для singleton
        /// </summary>
        private static Collision collision;

        /// <summary>
        /// Структура для понимания оси колизии
        /// </summary>
        public struct CollisionXY
        {
            public CollisionXY(bool x, bool y)
            {
                X = x;
                Y = y;
            }
            /// <summary>
            /// Колизия по оси Х (true - коллизия, false - нет колизии)
            /// </summary>
            public bool X;

            /// <summary>
            /// Колизия по оси Y (true - коллизия, false - нет колизии)
            /// </summary>
            public bool Y;
        }
        /// <summary>
        /// Возвращает объект класса для работы с коллизией
        /// </summary>
        /// <returns>Объект класса Collisoin</returns>
        public static Collision GetCollision()
        {
            if(collision == null)
            {
                collision = new Collision();
            }
            return collision;
        }

        /// <summary>
        /// Проверка колизии фигуры тетриса
        /// </summary>
        /// <param name="startfigure">Начальная позиция фигуры</param>
        /// <param name="figure">Фигура, которую требуется проверить</param>
        /// <param name="maspolegame">Игровое поле, на котором требуется проверить колизию</param>
        /// <returns>Возвращает структуру колизии, указывающий по какой оси произошла коллизия</returns>
        public CollisionXY BeCollision(Point startfigure, FigureTetris figure, bool[,] maspolegame)
        {
            CollisionXY collisionXY = new CollisionXY(false, false);
            for (byte i = 0; i < figure.Figure.Count; i++)
            {
                //Получение индекса Х для maspolegame
                int X = (figure.Figure[i].X * ParamsGame.GetParamsGame().PartFigure.Width + startfigure.X) / ParamsGame.GetParamsGame().PartFigure.Width;
                //Получение индекса Y для maspolegame
                int Y = (figure.Figure[i].Y * ParamsGame.GetParamsGame().PartFigure.Height + startfigure.Y) / ParamsGame.GetParamsGame().PartFigure.Height;

                //Проверка колизии по оси Y
                if (Y + 1 > maspolegame.GetLength(0) || //Проверка, что фигура не уйдет за поле снизу
                                                        //Y - 1 < 0 || //Проверка, что фигура не уйдет за поле сверху
                    maspolegame[Y, X] == false) //Проверка, что снизу не занято фигурой
                {
                    collisionXY.Y = true;
                    return collisionXY;
                }
                //Проверка колизии по Оси X
                if (X - 1 < 0 || //Проверка, что фигура не уйдет за поле справа
                    X + 1 >= maspolegame.GetLength(1) || //Проверка, что фигура не уйдет за поле слева)
                    maspolegame[Y,X + ParamsGame.GetParamsGame().MoveFigure.x] == false) //Проверка, свободно ли поле в требуемом направлении по X
                {
                    collisionXY.X = true;
                }

                
            }
            return collisionXY;
        }
    }
}
