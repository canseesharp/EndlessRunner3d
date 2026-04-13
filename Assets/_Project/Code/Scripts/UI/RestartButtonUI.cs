using UnityEngine.SceneManagement;

namespace EndlessRunner3d
{
    public class RestartButtonUI : ButtonUI
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
