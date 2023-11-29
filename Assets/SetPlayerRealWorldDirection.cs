using TMPro;
using UnityEngine;

public class SetPlayerRealWorldDirection : MonoBehaviour
{
    private TMP_Dropdown _direction;
    private void Start()
    {
        _direction = GetComponent<TMP_Dropdown>();
        if (_direction != null)
        {
            _direction.onValueChanged.AddListener(ChangeDirection);
        }
    }

    private void ChangeDirection(int value)
    {
        PlayerRealWorldDirection.SetDirection((PlayerRealWorldDirection.Direction)value);
        Debug.Log(PlayerRealWorldDirection.CurrentDirection);
    }
    
}
