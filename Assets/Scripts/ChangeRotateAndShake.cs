using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeRotateAndShake : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCam;
    CinemachineCameraOffset cinemachineCameraOffset;
    public float amplitudeGain;
    public float frequencyGain;
    public float shakeTime;
    public float shakeAttackTime;

    private void Awake()
    {
        cinemachineVirtualCam = GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        cinemachineCameraOffset = GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineCameraOffset>();
    }

    public IEnumerator ShakeEffect(float shakeTime)
    {
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
        yield return new WaitForSeconds(shakeTime);
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

    public IEnumerator shakeAttackEffect(float rotateTime)
    {
        yield return new WaitForSeconds(.5f);
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = .1f;
        yield return new WaitForSeconds(shakeTime);
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
    public IEnumerator BossShakeEffect()
    {
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = .2f;
        yield return new WaitForSeconds(.3f);
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }


}
