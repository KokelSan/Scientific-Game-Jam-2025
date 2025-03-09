using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public CinemachineVirtualCamera CinemachineCamera;
    public NoiseSettings IdleNoiseSettings;
    public NoiseSettings ShakeNoiseSettings;

    private CinemachineBasicMultiChannelPerlin _cameraNoise;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        _cameraNoise = CinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameManagerHandlerData.OnGameStarted += ActivateNormalNoise;
    }

    private void OnDestroy()
    {
        GameManagerHandlerData.OnGameStarted -= ActivateNormalNoise;
    }

    public void DisableAllNoise()
    {
        _cameraNoise.m_NoiseProfile = null;
    }

    public void ActivateNormalNoise()
    {
        _cameraNoise.m_NoiseProfile = IdleNoiseSettings;
    }

    public void ActivateShakeNoise(float amplitude = 1, float frequency = 1)
    {
        _cameraNoise.m_NoiseProfile = ShakeNoiseSettings;
    }
}
