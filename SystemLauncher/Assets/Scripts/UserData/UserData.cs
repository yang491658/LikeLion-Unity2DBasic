public interface UserData
{
    // 기본값 설정 함수
    void SetDefault();

    // 데이터 불러오기 함수
    bool LoadData();

    // 데이터 저장 함수
    bool SaveData();
}