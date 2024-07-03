using LPTask.Calculator.Operators;
using LPTask.Popups;
using UnityEngine;

namespace LPTask.Calculator
{
    public sealed class CalculatorBootstrapper
        : MonoBehaviour
    {
        [SerializeField] private EOperator @operator;
            
        [SerializeField] private CalculatorView view;
        [SerializeField] private ErrorPopup errorPopup;
        
        private CalculatorPresenter _presenter;
        
        private void Awake()
        {
            _presenter = new CalculatorPresenter(new CalculatorModel(OperatorsHelper.GetOperator(@operator)), view, errorPopup);
            _presenter.Initialize();
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}