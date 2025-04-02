using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeControler : MonoBehaviour
{
    private static TimeControler instance;

    public static TimeControler Instance { get { return instance; } }

    public float timeScale = 0.3f;
    public float duration = 0.5f; // ���� �ð�
    public float timer = 0f; // Ÿ�̸�

    public bool isSlow { get; private set; }

    [Header("Post Processing")]
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private ColorGrading colorGrading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Post Processing ������Ʈ ��������
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    void Update()
    {
        if (isSlow)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                SetSlowMotion(false);
                timer = 0f;
            }
        }
    }

    public float GetTimeScale()
    {
        return isSlow ? timeScale : 1f;
    }

    public void SetSlowMotion(bool SLOW)
    {
        isSlow = SLOW;
        if (SLOW)
        {
            // ���ο� ��� ���� �� ȿ�� ����
            timer = 0f;

            vignette.intensity.value = 0.8f; // ���Ʈ ���� ���� ����
            colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            colorGrading.saturation.value = -40f; // ä�� ���� ����
            colorGrading.temperature.value = -25f; // �ſ� ������ ����
            colorGrading.contrast.value = 20f; // ��� �� ���ϰ�
            colorGrading.postExposure.value = -1.0f; // ��ü������ �� ��Ӱ�
            colorGrading.tint.value = 10f; // �ణ�� �ʷϺ� �߰�
        }
        else
        {
            // ���ο� ��� ���� �� ȿ�� ����
            vignette.intensity.value = 0f;
            colorGrading.saturation.value = 0f;
            colorGrading.temperature.value = 0f;
            colorGrading.contrast.value = 0f;
            colorGrading.postExposure.value = 0f;
            colorGrading.tint.value = 0f;
        }
    }
}