using UnityEngine;
using Zenject;
using Zparta.Interfaces;
using Zparta.Services;

namespace Zparta
{
    public class WaterShaderController : MonoBehaviour, IUpdatable
    {
        [SerializeField] Renderer m_renderer;
        [SerializeField] Vector2 m_offset;

        IUpdateService _updateService;

        [Inject]
        void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        void OnEnable()
        {
            _updateService.AddToList(this);
        }

        void OnDisable()
        {
            _updateService.RemoveFromList(this);
        }

        public void OptimizedUpdate()
        {
            
        }

        public void PhysicsUpdate()
        {
            m_renderer.material.mainTextureOffset += m_offset * Time.fixedDeltaTime / 10;
        }
    }
}
