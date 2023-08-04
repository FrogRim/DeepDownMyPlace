using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // 2�� ���� // Bgm��, Effect�� // ������ �������, Init()���� ����
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>(); // caching ���� // Key : ���, Value : AudioClip

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound"); // @Sound Object ã��
        if (root == null) // ��ã�Ҵٸ�
        {
            root = new GameObject { name = "@Sound" }; // @Sound Object ����
            Object.DontDestroyOnLoad(root); // Scene�� �̵��� �� ������� ����

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // Define.Sound���� Sound Ÿ�Ե��� ����
            for (int i = 0; i < soundNames.Length - 1; i++) // MaxCount ���ֱ� ���� -1�� ����
            {
                GameObject go = new GameObject { name = soundNames[i] }; // �����ص� Sound Ÿ�Ե�� Object ����
                _audioSources[i] = go.AddComponent<AudioSource>(); // ������ Object�� AudioSource Component�� ���̰� _audioSources[i]�� ����
                go.transform.parent = root.transform; // �θ� root�� ���� // SetParent�� Rect Transform�� �� �̿�
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; // Bgm�� ��쿡�� loop�� �ϵ��� ����
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (path.Contains("Sound/") == false) // �Է��� ��ο� Sound/ �� ���ٸ�
        {
            path = $"Sound/{path}"; // Sound/ �־��ֱ�
        }

        if (type == Define.Sound.Bgm) // Sound Ÿ���� Bgm�̸�
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path); // ������ path���� �ҷ�����
            if (audioClip == null) // audioClip�� ���ٸ�
            {
                Debug.Log($"AudioClip Missing! {path}");
                return; // �׳� return
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm]; // �����ϰ� ����� �� �ֵ��� audioSource ������ ���� // _audioSources[(int)Define.Sound.Bgm].~~~ ó�� ����� �� ����
            if (audioSource.isPlaying) // �̹� ����� �̶��
            {
                audioSource.Stop(); // �뷡 ���߱�
            } // �� �ڵ尡 ��� �۵������� ��Ȯ�ϰ� �ϱ� ���� �ۼ�

            audioSource.pitch = pitch; // ��ġ ����
            audioSource.clip = audioClip; // ���� ����
            audioSource.Play(); // loop�� Init()���� ������
        }
        else // Bgm �̿��� Sound Ÿ���̸�
        {
            AudioClip audioClip = GetOrAddAudioClip(path); // ������ _audioClips���� ã��, ������ _audioClips�� �߰��� ���ÿ� �ҷ�����
            if (audioClip == null) // audioClip�� ���ٸ�
            {
                Debug.Log($"AudioClip Missing! {path}");
                return; // �׳� return
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect]; // �����ϰ� ����� �� �ֵ��� audioSource ������ ���� // _audioSources[(int)Define.Sound.Effect].~~~ ó�� ����� �� ����
            audioSource.pitch = pitch; // ��ġ ����
            audioSource.PlayOneShot(audioClip); // audioClip �ѹ� ����
        }
    }

    AudioClip GetOrAddAudioClip(string path) // AudioClip�� _audioClips Dictionary���� �������ų� ������ �߰��ϴ� �޼ҵ�
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip) == false) // _audioClips�� ���� �־��ٸ� ��������, ���� �����ٸ�
        {
            audioClip = Managers.Resource.Load<AudioClip>(path); // audioClip�� ������ ��ο��� ã�� �־��ֱ�
            _audioClips.Add(path, audioClip); // _audioClips�� �߰��ϱ�
        }

        return audioClip;
    }

    public void Clear() // Scene�� �ٲ𶧸��� _audioClips�� �ʱ�ȭ ���ִ� �޼ҵ� // @Managers�� DontDestroyOnLoad�̱� ������ @Managers���� �����Ű�� SoundManager�� �������� �ʾ� �ʱ�ȭ ������ ������ �޸𸮰� ���� �� ����
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.Stop(); // �뷡 ���߱�
            audioSource.clip = null; // clip�� null�� �ʱ�ȭ
        }
        _audioClips.Clear(); // _audioClips �ʱ�ȭ
    }
}