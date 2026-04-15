using UnityEngine;

namespace EndlessRunner3d.UI
{
    public class ExitButtonUI : ButtonUI
    {
        protected override void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
