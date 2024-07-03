using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LPTask.Popups
{
    public sealed class ErrorPopup
        : MonoBehaviour, IPopup
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Button button;
        
        public void Show()
        {
            root.SetActive(true);
            button.onClick.AddListener(Close);
        }

        private void Close()
        {
            button.onClick.RemoveListener(Close);
            root.SetActive(false);
        }
    }
}
