using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public GameObject gaugeObj;
    public Image gauge;
    public Image attackGauge;
    public float attackChargeSpeed = 0.1f;
    public float maxAttackPower = 50f;
    public float attackPower;
    public Transform playerTransform;
    public Vector3 gaugeOffset = new Vector3(0, 2, 0);

    private float currentCharge = 0f;
    private bool isCharging = false;
    private RectTransform canvasRectTransform;
    private Camera mainCamera;

    public static Gauge instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvasRectTransform = gauge.canvas.GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // �÷��̾� ���� ������ ��ġ ������Ʈ
        UpdateGaugePosition();

        if (Input.GetKey(KeyCode.Z))
        {
            isCharging = true;
            gaugeObj.SetActive(true);
            ChargeAttack();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isCharging = false;
            gaugeObj.SetActive(false);
            PerformAttack();
            Player.instance.Attack();
        }

        if (!isCharging && currentCharge > 0)
        {
            currentCharge -= attackChargeSpeed * Time.deltaTime;
            attackGauge.fillAmount = currentCharge;
        }
    }


    void ChargeAttack()
    {
        if (currentCharge < 1f)
        {
            currentCharge += attackChargeSpeed * Time.deltaTime;
            attackGauge.fillAmount = currentCharge;
        }
    }

    void PerformAttack()
    {
        attackPower = Mathf.Lerp(10f, maxAttackPower, currentCharge); // ���ݷ� ���
        Debug.Log("Attack with power: " + attackPower);
        currentCharge = 0f;
        attackGauge.fillAmount = currentCharge;
    }

    void UpdateGaugePosition()
    {
        // �÷��̾� ��ġ + �������� ����Ʈ ��ǥ�� ��ȯ
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(playerTransform.position + gaugeOffset);

        // ����Ʈ ��ǥ�� ĵ���� ��ǥ�� ��ȯ
        Vector2 worldObjectScreenPosition = new Vector2(
            ((viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
            ((viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

        // ������ ��ġ ������Ʈ
        gauge.rectTransform.anchoredPosition = worldObjectScreenPosition;
    }
}
