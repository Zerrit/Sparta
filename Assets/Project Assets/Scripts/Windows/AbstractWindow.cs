using UnityEngine;

namespace Zparta.Windows
{
    public abstract class AbstractWindow : MonoBehaviour
    {
       [SerializeField] private GameObject _toggle;

       public virtual void Show()
       {
           _toggle.SetActive(true);
       }
       
       public virtual void Hide()
       {
           _toggle.SetActive(false);
       }
    }
}