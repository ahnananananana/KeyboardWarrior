using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gilsu
{
    public class GameData : singleton<GameData> // 싱글턴이 생성됨.
    {
        public int Level = 0;
        public int m_CurScene = 0;

        private void Awake()
        {
            DontDestroyOnLoad(this); // 해당 함수실행 시 해당 데이타는 씬이 바뀌어 제거되더라도 사라지지 않음.(억지로지우는건 가능.)
        }
    }
}
