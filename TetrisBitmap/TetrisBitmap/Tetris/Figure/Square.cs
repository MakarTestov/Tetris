using System;
using System.Collections.Generic;
using System.Drawing;

namespace TetrisBitmap.Tetris.Figure
{
    /// <summary>
    /// Класс, отвечающий за фигуру квадрат
    /// * *
    /// * *
    /// </summary>
    class Square : FigureTetris
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
        /// Конструктор Класса Square, для создания списка фигур из 4 точек
        /// </summary>
        public Square()
        {
            figure = new List<Point>(4);
        }

        /// <summary>
        /// Создать Фигуру Square, путем установки начальных точек фигуры, относительно центральной части
        /// </summary>
        public override void CreateFigure()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(1, 0);
            Point p3 = new Point(0, 1);
            Point p4 = new Point(1, 1);
            figure.AddRange(new List<Point> { p1, p2, p3, p4 });
        }

        /// <summary>
        /// Вернуть точки по-умолчанию
        /// </summary>
        public override void PointDefault()
        {
            figure[0] = new Point(0, 0);
            figure[1] = new Point(1, 0);
            figure[2] = new Point(0, 1);
            figure[3] = new Point(1, 1);
        }

        /// <summary>
        /// Повернуть фигуру по часовой стрелки, 
        /// не реализовано, так как квадрат не имеет смысл поворачивать
        /// </summary>
        public override void RotateBy()
        {
        }

        /// <summary>
        /// Повернуть фигуру против часовойстрелки,
        /// не реализовано, так как квадрат не имеет смысл поворачивать
        /// </summary>
        public override void RotateCon()
        {
        }
    }
}
