using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner3d
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonUI : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
    }
}
