using UnityEngine;

namespace EndlessRunner3d
{
    public class ExitButtonUI : ButtonUI
    {
        protected override void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
