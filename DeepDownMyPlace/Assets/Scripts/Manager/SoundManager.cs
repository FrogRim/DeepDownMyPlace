using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // 2개 생성 // Bgm용, Effect용 // 아직은 비어있음, Init()에서 연결
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>(); // caching 역할 // Key : 경로, Value : AudioClip

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound"); // @Sound Object 찾기
        if (root == null) // 못찾았다면
        {
            root = new GameObject { name = "@Sound" }; // @Sound Object 생성
            Object.DontDestroyOnLoad(root); // Scene을 이동할 때 사라지지 않음

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // Define.Sound에서 Sound 타입들을 추출
            for (int i = 0; i < soundNames.Length - 1; i++) // MaxCount 빼주기 위해 -1을 해줌
            {
                GameObject go = new GameObject { name = soundNames[i] }; // 정의해둔 Sound 타입들로 Object 생성
                _audioSources[i] = go.AddComponent<AudioSource>(); // 생성한 Object에 AudioSource Component를 붙이고 _audioSources[i]에 연결
                go.transform.parent = root.transform; // 부모를 root로 설정 // SetParent는 Rect Transform일 때 이용
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; // Bgm의 경우에는 loop를 하도록 설정
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (path.Contains("Sound/") == false) // 입력한 경로에 Sound/ 가 없다면
        {
            path = $"Sound/{path}"; // Sound/ 넣어주기
        }

        if (type == Define.Sound.Bgm) // Sound 타입이 Bgm이면
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path); // 음원을 path에서 불러오기
            if (audioClip == null) // audioClip이 없다면
            {
                Debug.Log($"AudioClip Missing! {path}");
                return; // 그냥 return
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm]; // 간편하게 사용할 수 있도록 audioSource 변수에 연결 // _audioSources[(int)Define.Sound.Bgm].~~~ 처럼 사용할 수 있음
            if (audioSource.isPlaying) // 이미 재생중 이라면
            {
                audioSource.Stop(); // 노래 멈추기
            } // 이 코드가 없어도 작동하지만 명확하게 하기 위해 작성

            audioSource.pitch = pitch; // 피치 설정
            audioSource.clip = audioClip; // 음원 연결
            audioSource.Play(); // loop는 Init()에서 설정함
        }
        else // Bgm 이외의 Sound 타입이면
        {
            AudioClip audioClip = GetOrAddAudioClip(path); // 음원을 _audioClips에서 찾고, 없으면 _audioClips에 추가와 동시에 불러오기
            if (audioClip == null) // audioClip이 없다면
            {
                Debug.Log($"AudioClip Missing! {path}");
                return; // 그냥 return
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // 간편하게 사용할 수 있도록 audioSource 변수에 연결 // _audioSources[(int)Define.Sound.Effect].~~~ 처럼 사용할 수 있음
            audioSource.pitch = pitch; // 피치 설정
            audioSource.PlayOneShot(audioClip); // audioClip 한번 실행
        }
    }

    AudioClip GetOrAddAudioClip(string path) // AudioClip을 _audioClips Dictionary에서 가져오거나 없으면 추가하는 메소드
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false) // _audioClips에 값이 있었다면 가져오고, 만약 없었다면
        {
            audioClip = Managers.Resource.Load<AudioClip>(path); // audioClip에 음원을 경로에서 찾아 넣어주기
            _audioClips.Add(path, audioClip); // _audioClips에 추가하기
        }

        return audioClip;
    }

    public void Clear() // Scene이 바뀔때마다 _audioClips를 초기화 해주는 메소드 // @Managers가 DontDestroyOnLoad이기 때문에 @Managers에서 실행시키는 SoundManager는 해제되지 않아 초기화 해주지 않으면 메모리가 터질 수 있음
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.Stop(); // 노래 멈추기
            audioSource.clip = null; // clip을 null로 초기화
        }
        _audioClips.Clear(); // _audioClips 초기화
    }
}