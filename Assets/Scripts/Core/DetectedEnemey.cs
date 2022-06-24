using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectedEnemey : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private Image visionIndicatorImage;
    [SerializeField] private bool showVisionIndicator;

    private HeroCombat _combat;

    private void Start()
    {
        _combat = GetComponent<HeroCombat>();
    }

    private void Update()
    {
        if (_combat.EnableEnemyDetection())
        {
            _combat.enemy = FindNearestEnemy();
        }

        visionIndicatorImage.enabled = showVisionIndicator;
    }

    private GameObject FindNearestEnemy()
    {
        var enemies = Physics.OverlapSphere(transform.position, _combat.character.stats.visionRange, targetLayerMask);
        if (enemies.Length > 0)
        {
            enemies = enemies.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
            return enemies[0].gameObject;
        }

        return null;
    }
}