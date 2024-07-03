using System;
using System.Collections.Generic;
using System.Linq;
using LPTask.Popups;
using LPTask.Utils;
using Unity.VisualScripting;
using UnityEngine;
using IInitializable = LPTask.Utils.IInitializable;

namespace LPTask.Calculator
{
    public sealed class CalculatorPresenter
        : IInitializable, IDisposable
    {
        public CalculatorPresenter(CalculatorModel model, CalculatorView view, IPopup errorPopup)
        {
            _model = model;
            _view = view;

            _errorPopup = errorPopup;
        }

        private readonly CalculatorModel _model;
        private readonly CalculatorView _view;

        private readonly IPopup _errorPopup;

        private readonly IList<string> _solutions = new List<string>();

        private readonly IDataHolder<string> _equationHolder = new PlayerPrefsDataHolder("Equation");
        private readonly IDataHolder<string> _solutionsHolder = new PlayerPrefsDataHolder("Solutions");

        public void Initialize()
        {
            _view.OnSubmit += HandleSubmit;
            _view.OnRequestClear += HandleClear;

            var hasEquation = _equationHolder.TryGetData(out var equation);
            if (hasEquation)
            {
                _view.CurrentEquation = equation;
            }

            var hasSolutions = _solutionsHolder.TryGetData(out var data);
            if (hasSolutions)
            {
                var solutions = ParseSolutions(data);
                _solutions.AddRange(solutions);

                _solutions.Foreach(value => _view.AddSolution(value));
            }
        }

        public void Dispose()
        {
            _view.OnSubmit -= HandleSubmit;
            _view.OnRequestClear -= HandleClear;
            
            _equationHolder.SetData(_view.CurrentEquation);
            _solutionsHolder.SetData(string.Join("\n", _solutions));
        }

        private IEnumerable<string> ParseSolutions(string solutions)
        {
            var result = solutions.Split("\n");
            return result;
        }

        private void HandleSubmit(string equation)
        {
            var isValidEquation = _model.Calculate(equation, out var result);
            if (isValidEquation)
            {
                AddSolution($"{equation}={result}");
            }
            else
            {
                _errorPopup.Show();
                AddSolution($"{equation}=<color=red>ERROR</color>");
            }
        }

        private void HandleClear()
        {
            _solutions.Clear();
            
            PlayerPrefs.DeleteKey("Solutions");
            _view.ClearSolutions();
        }

        private void AddSolution(string solution)
        {
            _view.AddSolution(solution);
            _solutions.Add(solution);
        }
    }
}