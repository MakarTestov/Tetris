using System;
using System.Collections.Generic;
using System.Drawing;

namespace TetrisBitmap.Tetris.Figure
{
    /// <summary>
    /// Абстрактный класс для реализаций необходимых функций любых фигур
    /// </summary>
    abstract class FigureTetris
    {
        public delegate void RePoint(object sender, EventArgs e);
        /// <summary>
        /// Событие вызываемое после изменений точек фигуры
        /// </summary>
        abstract public event RePoint ChangePointFigure;
        /// <summary>
        /// Список точек фигур, относительно центральной части
        /// </summary>
        abstract public List<Point> Figure { get; set; }
        /// <summary>
        /// Создать фигуру тетриса
        /// </summary>
        abstract public void CreateFigure();

        /// <summary>
        /// Изменить координаты фигуры по часовой стрелки на 90 градусов (вращение)
        /// </summary>
        abstract public void RotateBy();

        /// <summary>
        /// Изменить координаты фигуры против часовой стрелки на 90 градусов(вращение)
        /// </summary>
        abstract public void RotateCon();

        /// <summary>
        /// Устанавливает координаты фигуры по умолчанию
        /// </summary>
        abstract public void PointDefault();
    }
}
