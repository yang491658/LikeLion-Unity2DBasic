using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeControler : MonoBehaviour
{
    private static TimeControler instance;

    public static TimeControler Instance { get { return instance; } }

    public float timeScale = 0.3f;
    public float duration = 0.5f; // 지속 시간
    public float timer = 0f; // 타이머

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
        // Post Processing 컴포넌트 가져오기
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
            // 슬로우 모션 시작 시 효과 설정
            timer = 0f;

            vignette.intensity.value = 0.8f; // 비네트 강도 대폭 증가
            colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            colorGrading.saturation.value = -40f; // 채도 더욱 낮게
            colorGrading.temperature.value = -25f; // 매우 차가운 색감
            colorGrading.contrast.value = 20f; // 대비 더 강하게
            colorGrading.postExposure.value = -1.0f; // 전체적으로 더 어둡게
            colorGrading.tint.value = 10f; // 약간의 초록빛 추가
        }
        else
        {
            // 슬로우 모션 종료 시 효과 해제
            vignette.intensity.value = 0f;
            colorGrading.saturation.value = 0f;
            colorGrading.temperature.value = 0f;
            colorGrading.contrast.value = 0f;
            colorGrading.postExposure.value = 0f;
            colorGrading.tint.value = 0f;
        }
    }
}