using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI RecordScoreText { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }
}
