using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    static T _inst;

    public static T Instance
    {
        get
        {
            // 이미 생성된 _inst가 있는지 확인합니다.
            if (_inst == null)
            {
                // T 타입의 클래스로 생성된 오브젝트를 검색합니다.
                _inst = FindObjectOfType(typeof(T)) as T;
                if (_inst == null)
                {
                    // 인스턴스가 없으므로 새로 생성합니다.
                    GameObject container = new GameObject();
                    _inst = container.AddComponent<T>();

                    container.name = $"[{_inst.GetType().Name}]";
                }
            }
            return _inst;
        }
    }
}
