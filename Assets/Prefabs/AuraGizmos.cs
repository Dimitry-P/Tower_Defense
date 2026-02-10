using UnityEngine;

public class AuraGizmos : MonoBehaviour
{
    [SerializeField] public CircleCollider2D AuraCollider;
#if UNITY_EDITOR

    [SerializeField] private SpriteRenderer _auraColliderSprite;

    public void Init()
        {
            if (AuraCollider == null) return;
            if (_auraColliderSprite == null) return;
            if (_auraColliderSprite.sprite == null) return;

        float worldRadius = AuraCollider.radius * AuraCollider.transform.lossyScale.x;
        float worldDiameter = worldRadius * 2f;

        float spriteDiameter = _auraColliderSprite.sprite.bounds.size.x;

        float scale = worldDiameter / spriteDiameter;
        _auraColliderSprite.transform.localScale = Vector3.one * scale;

        _auraColliderSprite.enabled = true;
        }

    public void Disabled()
    {
        _auraColliderSprite.enabled = false;
    }

#endif
}
