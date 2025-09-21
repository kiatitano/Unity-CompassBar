using UnityEngine;
using UnityEngine.UI;

public class CompassBar : MonoBehaviour
{
    public Transform player;
    public RectTransform compassBackground;
    public RectTransform missionMarker;
    public float compassWidth = 800f;
    public float fovAngle = 90f;

    void Update()
    {
        var mission = MissionManager.Instance?.GetCurrentMission();
        if (mission == null || mission.targetTransform == null || player == null)
        {
            missionMarker.gameObject.SetActive(false);
            return;
        }

        Transform target = mission.targetTransform;
        missionMarker.gameObject.SetActive(true);

        float playerYaw = player.eulerAngles.y;

        Vector3 toTarget = target.position - player.position;
        float targetYaw = Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg;
        float deltaYaw = Mathf.DeltaAngle(playerYaw, targetYaw);

        float normalized = deltaYaw / (fovAngle / 2);
        float xOffset = normalized * (compassWidth / 2);

        xOffset = Mathf.Clamp(xOffset, -compassWidth / 2, compassWidth / 2);
        missionMarker.anchoredPosition = new Vector2(xOffset, missionMarker.anchoredPosition.y);
    }
}
