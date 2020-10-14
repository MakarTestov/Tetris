using System;
using System.Collections.Generic;
using System.Drawing;

namespace TetrisBitmap.Tetris.Figure
{
    /// <summary>
    /// Класс, отвечающий за фигуру обратную Г
    /// * *
    ///   *
    ///   *
    /// </summary>
    class RG : FigureTetris
    {
        /// <summary>
        /// Список точек фигуры, то есть где расположеная каждая часть фигуры
        /// </summary>
        private List<Point> figure;
        /// <summary>
        /// Свойство. Список точек фигур, относительно центральной части
        /// </summary>
        public override List<Point> Figure
        {
            get => figure;
            set
            {
                ChangePointFigure?.Invoke(this, new EventArgs());
                figure = value;
            }
        }

        /// <summary>
        /// Событие при изменении точек фигуры
        /// </summary>
        public override event RePoint ChangePointFigure;

        /// <summary>
        /// Конструктор Класса RG, для создания списка фигур из 4 точек
        /// </summary>
        public RG()
        {
            figure = new List<Point>(4);
        }

        /// <summary>
        /// Создать Фигуру RG, путем установки начальных точек фигуры, относительно центральной части
        /// </summary>
        public override void CreateFigure()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(-1, 0);
            Point p3 = new Point(0, 1);
            Point p4 = new Point(0, 2);
            figure.AddRange(new List<Point> { p1, p2, p3, p4 });
        }

        /// <summary>
        /// Вернуть точки по-умолчанию
        /// </summary>
        public override void PointDefault()
        {
            figure[0] = new Point(0, 0);
            figure[1] = new Point(-1, 0);
            figure[2] = new Point(0, 1);
            figure[3] = new Point(0, 2);
        }

        /// <summary>
        /// Повернуть фигуру по часовой стрелки
        /// </summary>
        public override void RotateBy()
        {
            figure[0] = new Point(0, 0);
            figure[1] = new Point(-figure[1].Y,figure[1].X);
            figure[2] = new Point(-figure[2].Y, figure[2].X);
            figure[3] = new Point(-figure[3].Y, figure[3].X);
        }

        /// <summary>
        /// Повернуть фигуру против часовой
        /// </summary>
        public override void RotateCon()
        {
            figure[0] = new Point(0, 0);
            figure[1] = new Point(figure[1].Y, -figure[1].X);
            figure[2] = new Point(figure[2].Y, -figure[2].X);
            figure[3] = new Point(figure[3].Y, -figure[3].X);
        }
    }
}
