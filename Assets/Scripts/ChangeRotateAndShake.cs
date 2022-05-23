using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeRotateAndShake : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCam;
    public float amplitudeGain;
    public float frequencyGain;
    public float shakeTime;
    public float shakeAttackTime;

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
}
