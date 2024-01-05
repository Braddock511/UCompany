namespace Gravitons.UI.Modal
{
    using UnityEngine;

    public class Modals : MonoBehaviour
    {
        private void Start()
        {
            // First project
            if (GameManager.Instance.firstTime)
            {

                ModalManager.Show("First project", "Opis project - ustawić termin rok",
                new[]
                {
                    new ModalButton() { Text = "OK", Callback = () => ProjectManager.Instance.StartProject("First project", GameManager.Instance.date.AddDays(3), 100) },
                });
            }
        }
    }
}