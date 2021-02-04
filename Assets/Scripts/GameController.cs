using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _coinAmountText = default;
    private int _coinAmount = 0;

    [SerializeField] private ParticleSystem _feedbackCoinCollectedPrefab = default;

    public void UpdateCoin()
    {
        _coinAmount++;
        _coinAmountText.text = _coinAmount.ToString();
    }

    public void StartVfxCoinCollected(Transform coinTransform)
    {
        ParticleSystem feedbackTemp = Instantiate(_feedbackCoinCollectedPrefab, coinTransform.position, coinTransform.rotation);

        Destroy(feedbackTemp.gameObject, 1.0f);
    }
}
