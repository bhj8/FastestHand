using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioClip buttonDeleteClip;
    public AudioClip buttonClip;
    public AudioClip bgmClip;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // 创建并配置用于播放背景音乐的 AudioSource
        AudioSource bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true; // 设置为循环播放
        bgmSource.Play();
    }

    public void PlayButtonDeleteSound()
    {
        PlaySound(buttonDeleteClip);
    }

    public void PlayMenuButton()
    {
        PlaySound(buttonClip);
    }

    public void PlaySound(AudioClip clip)
    {
        // 创建一个新的 AudioSource
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();

        // 指定要播放的音频剪辑
        newAudioSource.clip = clip;

        // 播放音频
        newAudioSource.Play();

        // 开始一个协程，以便在音频播放完毕后删除 AudioSource
        StartCoroutine(RemoveAudioSourceWhenFinished(newAudioSource));
    }

    private IEnumerator RemoveAudioSourceWhenFinished(AudioSource source)
    {
        // 等待音频播放完毕
        yield return new WaitForSeconds(source.clip.length);

        // 删除 AudioSource
        Destroy(source);
    }
}
