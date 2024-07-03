using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LPTask.Calculator
{
    public sealed class CalculatorView
        : MonoBehaviour
    {
        public event Action<string> OnSubmit = _ => { };
        public event Action OnRequestClear = () => { };

        [SerializeField] private TMP_Text equationText;
        [SerializeField] private Transform solutionsRoot;

        [SerializeField] private Button calculateButton;
        [SerializeField] private Button clearHistoryButton;
        
        [SerializeField] private TMP_Text solutionPrefab;
        
        public string CurrentEquation
        {
            get => equationText.text;
            set => equationText.text = value;
        }
        
        /// <summary>
        /// Добавляет решение в выводимый список
        /// </summary>
        public void AddSolution(string solution)
        {
            var result = Instantiate(solutionPrefab, solutionsRoot);
            result.text = solution;
        }

        public void ClearSolutions()
        {
            for (var i = 0; i < solutionsRoot.childCount; i++)
            {
                Destroy(solutionsRoot.GetChild(i).gameObject);
            }
        }

        private void Submit()
        {
            OnSubmit.Invoke(CurrentEquation);
        }

        private void RequestClear()
        {
            OnRequestClear.Invoke();
        }

        private void OnEnable()
        {
            calculateButton.onClick.AddListener(Submit);
            clearHistoryButton.onClick.AddListener(RequestClear);
        }

        private void OnDisable()
        {
            calculateButton.onClick.RemoveListener(Submit);
            clearHistoryButton.onClick.RemoveListener(RequestClear);
        }
    }
}