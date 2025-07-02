using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public bool IsExistData { get; private set; } // 데이터 유무
    public List<UserData> UserDatas { get; private set; } = new List<UserData>(); // 유저 데이터 배열

    // 초기화 함수 (상속)
    protected override void Init()
    {
        base.Init(); // 초기화 (상속)

        // 유저 데이터 추가
        UserDatas.Add(new UserSettingsData());
        UserDatas.Add(new UserGoodsData());
        UserDatas.Add(new UserInventoryData());
        UserDatas.Add(new UserPlayData());
    }

    // 기본 유저 데이터 설정 함수
    public void SetDefaultUserData()
    {
        for (int i = 0; i < UserDatas.Count; i++) // 유저 데이터 배열
        {
            UserDatas[i].SetDefault(); // 기본값 설정
        }
    }

    // 유저 데이터 불러오기 함수
    public void LoadUserData()
    {
        // 로컬 저장소에서 데이터 유무 가져오기
        IsExistData = PlayerPrefs.GetInt("IsExistData") == 1 ? true : false;

        if (IsExistData) // 데이터 있음
        {
            for (int i = 0; i < UserDatas.Count; i++) // 유저 데이터 배열
            {
                UserDatas[i].LoadData(); // 데이터 불러오기
            }
        }
    }

    // 유저 데이터 저장 함수
    public void SaveUserData()
    {
        bool isErr = false; // 오류 여부

        for (int i = 0; i < UserDatas.Count; i++) // 유저 데이터 배열
        {
            bool isSaveSuccess = UserDatas[i].SaveData(); // 저장 성공 여부

            if (!isSaveSuccess) // 저장 실패
            {
                isErr = true; // 오류 발생
            }
        }

        if (!isErr) // 오류 없음
        {
            IsExistData = true; // 데이터 있음

            // 로컬 저장소에서 데이터 있음 설정 및 저장
            PlayerPrefs.SetInt("IsExistData", 1);
            PlayerPrefs.Save();
        }
    }

    // 유저 데이터 가져오기 함수
    public T GetUserData<T>() where T : class, UserData
    {
        return UserDatas // 유저 데이터 배열
            .OfType<T>() // 타입 변환
            .FirstOrDefault(); // 첫 번째
    }
}