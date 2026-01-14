using Game.Map;
using GameFramework.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Map
{
    // “입력 → 결과”만 만드는 순수 실행 이기때문에 static으로 ㅁ낟므
    public class TileMapInstaller
    {
        public void OnMapLoaded(MapData data)
        {
            //InstallGround(data.ground);
            //InstallWall(data.wall);
            //InstallUpper(data.upperGround);
        }

        private void InstallGround(string[] data)
        {
            List<int> v = new List<int>(capacity: 100);
            
        }
    }
}