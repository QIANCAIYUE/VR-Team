using UnityEngine;

// 수업에서는 프로젝트와 같은 Inspector 항목 이름을 사용한다.
// 课堂使用与项目相同的Inspector字段名称。
public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource ambientPlayer; // Scene의 AMBIENT · Scene中的AMBIENT
    public AudioSource sfxPlayer; // Scene의 SFX · Scene中的SFX

    [Header("Master Volume")]
    [Range(0f, 1f)] public float masterVolumeSFX = 1f;
    [Range(0f, 1f)] public float masterVolumeAmbient = 0.2f;

    [Header("Audio Library")]
    public AudioClip[] ambientAudioClips; // 배경음 목록 · 背景声音列表
    public AudioClip[] sfxAudioClips; // 효과음 목록 · 音效列表

    private void Awake()
    {
        ConfigurePlayer(ambientPlayer, true);
        ConfigurePlayer(sfxPlayer, false);
        ApplyVolumes();
    }

    private void Start()
    {
        // 배경음 목록의 0번을 시작부터 반복 재생한다.
        // 启动时循环播放背景声音列表的第0项。
        PlayAmbient(0);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            ApplyVolumes();
    }

    private void ConfigurePlayer(AudioSource player, bool loop)
    {
        if (player == null)
            return;

        player.playOnAwake = false;
        player.loop = loop;
        // 위치·3D·거리 설정은 Audio Source에서 정한다.
        // 位置、3D与距离设置在Audio Source中调整。
    }

    private void ApplyVolumes()
    {
        if (ambientPlayer != null)
            ambientPlayer.volume = masterVolumeAmbient;
        if (sfxPlayer != null)
            sfxPlayer.volume = masterVolumeSFX;
    }

    public bool PlayAmbient(int index)
    {
        return PlayClip(ambientPlayer, ambientAudioClips, index, true);
    }

    // 행동 스크립트가 효과음 목록의 번호를 요청한다.
    // 操作脚本请求播放音效列表中指定编号的声音。
    public bool PlaySFX(int index = 0)
    {
        return PlayClip(sfxPlayer, sfxAudioClips, index, false);
    }

    // 잡은 위치 또는 놓는 위치에서 효과음을 재생한다.
    // 在抓取位置或放开位置播放音效。
    public bool PlaySFX(int index, Vector3 position)
    {
        if (sfxPlayer == null)
            return false;

        sfxPlayer.transform.position = position;
        return PlaySFX(index);
    }

    private bool PlayClip(AudioSource player, AudioClip[] clips, int index, bool loop)
    {
        if (player == null || !player.isActiveAndEnabled ||
            clips == null || index < 0 || index >= clips.Length || clips[index] == null)
            return false;

        // 이미 재생 중인 같은 배경음은 처음부터 다시 시작하지 않는다.
        // 同一背景声音已在播放时不重新开始。
        if (loop && player.clip == clips[index] && player.isPlaying)
            return true;

        player.clip = clips[index];
        player.loop = loop;
        player.Play();
        return true;
    }
}
