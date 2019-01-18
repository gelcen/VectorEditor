﻿using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Presenter class
    /// </summary>
    public class Presenter : IObserver
    {
        private readonly IView _view;
        private readonly IModel _model;
        private IBaseHandler _currentHandler;
        private List<BaseFigure> _figures;

        private UndoRedoStack _undoRedoStack;

        public Presenter(IView view, IModel model)
        {
            _currentHandler = null;
            _undoRedoStack = new UndoRedoStack();

            _view = view;
            _model = model;

            _model.NewProject();


            _view.ToolPicked += _view_ToolPicked;
            _view.ParametersChanged += _view_ParametersChanged;
            _view.CanvasCleared += _view_CanvasCleared;
            _view.FiguresDeleted += _view_FiguresDeleted;
            _view.FigureCopied += _view_FigureCopied;
            _view.UndoPressed += _view_UndoPressed;
            _view.RedoPressed += _view_RedoPressed;
                            
            _model.RegisterObserver(this);
            _model.RegisterObserver((IObserver)_view);
        }

        private void _view_RedoPressed(object sender, System.EventArgs e)
        {
            _undoRedoStack.Redo();
            _view.Canvas.Refresh();
        }

        private void _view_UndoPressed(object sender, System.EventArgs e)
        {
            _undoRedoStack.Undo();
            _view.Canvas.Refresh();
        }

        private void _view_FigureCopied(object sender, System.EventArgs e)
        {
            if (_currentHandler.GetType() == typeof(CursorHandler))
            {
                CursorHandler handler = _currentHandler as CursorHandler;
                foreach (var figure in handler.SelectedFigures)
                {
                    if (_model.getFigureList().Contains(figure))
                    {
                        _model.CopyFigure(figure);
                    }
                }
            }
        }

        private void _view_FiguresDeleted(object sender, System.EventArgs e)
        {
            if (_currentHandler.GetType() == typeof(CursorHandler))
            {
                CursorHandler handler = _currentHandler as CursorHandler;
                foreach (var figure in handler.SelectedFigures)
                {
                    if (_model.getFigureList().Contains(figure))
                    {
                        _model.DeleteFigure(figure);
                    }
                }
                handler.ClearSelectedFigures();
            }
        }

        private void _view_CanvasCleared(object sender, System.EventArgs e)
        {
            _model.ClearCanvas();
            if (_currentHandler.GetType() == typeof(CursorHandler))
            {
                CursorHandler handler = _currentHandler as CursorHandler;
                handler.ClearSelectedFigures();
            }
            _view.Canvas.Refresh();
        }

        private void _view_ParametersChanged(object sender, View.FigureParameters e)
        {
            //Словарь для хранения состояния до
            Dictionary<int, BaseFigure> beforeState = new Dictionary<int, BaseFigure>();
            if (_currentHandler.GetType() == typeof(CursorHandler))
            {
                CursorHandler handler = _currentHandler as CursorHandler;                
                foreach (var figure in handler.SelectedFigures)
                {
                    if (_model.getFigureList().Contains(figure))
                    {
                        int index =_model.getFigureList().IndexOf(figure);
                        beforeState.Add(index, FigureFactory.CreateCopy(figure));
                    }
                }
            }
            if (beforeState != null)
            {
                ChangeParametersCommand cmd = new ChangeParametersCommand(_model, beforeState, e);
                _undoRedoStack.Do(cmd);
            }            
            
            _view.Canvas.Invalidate();
        }

        private void _view_ToolPicked(object sender, Item e)
        {
            if (e == Item.Line)
            {
                _currentHandler = new LineHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (LineHandler)_currentHandler;
            }
            else if (e == Item.Polyline)
            {
                _currentHandler = new PolylineHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (PolylineHandler)_currentHandler;
            }
            else if (e == Item.Circle)
            {
                _currentHandler = new CircleHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (CircleHandler)_currentHandler;
            }
            else if (e == Item.Ellipse)
            {
                _currentHandler = new EllipseHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (EllipseHandler)_currentHandler;
            }
            else if (e == Item.Polygon)
            {
                _currentHandler = new PolygonHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (PolygonHandler)_currentHandler;
            }
            else if (e == Item.Cursor)
            {
                CursorHandler cursorHandler = new CursorHandler(_view.Canvas, _view.FigureParameters, this);
                cursorHandler.FigureSelected += CursorHandler_FigureSelected;
                cursorHandler.FiguresMoved += CursorHandler_FiguresMoved;
                _currentHandler = cursorHandler;
                _view.CurrentHandler = (CursorHandler)_currentHandler;
            }
        }

        private void CursorHandler_FiguresMoved(object sender, System.EventArgs e)
        {
            
        }

        private void CursorHandler_FigureSelected(object sender, View.FigureParameters e)
        {
            _view.FigureParameters = e;
        }

        private void _currentHandler_FigureCreated(object sender, BaseFigure e)
        {
            AddFigureCommand cmd = new AddFigureCommand(_model, e);
            _undoRedoStack.Do(cmd);
            //_model.AddFigure(e);
        }

        /// <summary>
        /// Обновление состояния представления
        /// </summary>
        /// <param name="figures"></param>
        public void Update(List<BaseFigure> figures)
        {
            _figures = figures;
        }

        public List<BaseFigure> GetFigures()
        {
            return _figures;
        }
    }
}
