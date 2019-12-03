using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    public CinemachineVirtualCamera virtualCamera;
    
    private void Awake()
    {
        if (gameMaster == null)
        {
            DontDestroyOnLoad(this);
            gameMaster = this;
            virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        }
        else Destroy(gameMaster); return;       
        
    }

    public IEnumerator ShakeCamera(int shakeIntensity, float shakeDuration)
    {
        Noise(shakeIntensity,1);
        yield return new WaitForSeconds(shakeDuration);
        Noise(0, 0);
    }

    private void Noise(int amplitudeGain, int frequencyGain)
    {
       virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
       virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
    }

}
