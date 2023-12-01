// using TMPro;
using UnityEngine;

public class SetPlayerRealWorldDirection : MonoBehaviour
{
    public PlayerRealWorldDirection.Direction direction;
    // private TMP_Dropdown _direction;
    // private void Start()
    // {
    //     _direction = GetComponent<TMP_Dropdown>();
    //     if (_direction != null)
    //     {
    //         _direction.onValueChanged.AddListener(ChangeDirection);
    //     }
    // }

    public void ChangeDirection()
    {
        PlayerRealWorldDirection.SetDirection(direction);
        Debug.Log(PlayerRealWorldDirection.CurrentDirection);
    }
    
}
