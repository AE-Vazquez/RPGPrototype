using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layers[] LayerPriorities = {
        Layers.Enemy,
        Layers.Walkable
    };

    float m_distanceToBackground = 100f;
    Camera m_viewCamera;

    RaycastHit m_hit;
    public RaycastHit RayHit
    {
        get { return m_hit; }
    }

    Layers m_layerHit;
    public Layers LayerHit
    {
        get { return m_layerHit; }
    }

    public delegate void OnLayerChange(Layers newLayer);

    public event OnLayerChange OnLayerChangedEvent;

    void Start() // TODO Awake?
    {
        m_viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layers layer in LayerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;

                if (m_layerHit != layer)
                {
                    m_layerHit = layer;

                    if(OnLayerChangedEvent!=null)
                        OnLayerChangedEvent(m_layerHit);
                }
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = m_distanceToBackground;
        m_layerHit = Layers.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layers layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = m_viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, m_distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
