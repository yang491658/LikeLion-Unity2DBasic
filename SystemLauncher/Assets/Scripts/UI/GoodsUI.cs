using TMPro;
using UnityEngine;

public class GoodsUI : MonoBehaviour
{
    public TextMeshProUGUI GoldAmountTxt; // 골드량 텍스트
    public TextMeshProUGUI GemAmountTxt; // 보석량 텍스트

    // 값 설정 함수
    public void SetValues()
    {
        // 유저 재화 데이터 가져오기
        var userGoodData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if (userGoodData == null) // 유저 재화 데이터 없음
        {
            // 오류 로그 출력 : 재화 데이터 없음
            Logger.LogError("No User Goods Data.");
            return; // 종료
        }

        // 골드량 및 보석량 텍스트 설정
        GoldAmountTxt.text = userGoodData.Gold.ToString("N0");
        GemAmountTxt.text = userGoodData.Gem.ToString("N0");
    }
}