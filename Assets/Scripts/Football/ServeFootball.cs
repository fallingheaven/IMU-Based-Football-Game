using UnityEngine;

public class ServeFootball : MonoBehaviour
{
    public float serveTimeGap;
    private float _currentTimeGap;
    public bool serving;
    private CharacterPool _characterPool;

    public Vector3 initPosition;
    public Vector3 serveVelocity;
    public float absoluteVelocity;
    public GameObject player;

    private void Start()
    {
        _characterPool = GetComponent<CharacterPool>();
        player = GameObject.Find("Kick Checker");
        SetServeVelocity();
    }

    private void SetServeVelocity()
    {
        var playerPosition = player.transform.position;
        serveVelocity = new Vector3(playerPosition.x - initPosition.x, 0, playerPosition.z - initPosition.z).normalized;
        serveVelocity *= absoluteVelocity;
        
        var estimatedTime = (playerPosition.x - initPosition.x) / serveVelocity.x;

        serveVelocity.y = 9.81f * estimatedTime / 2 + 0.5f;
        // serveVelocity.x += 0.8f;
    }

    private void FixedUpdate()
    {
        switch (serving)
        {
            case false when _currentTimeGap <= 0:
            {
                _currentTimeGap = serveTimeGap;
                break;
            }
            case true when _currentTimeGap > 0:
            {
                _currentTimeGap -= Time.deltaTime;
                break;
            }
            case true when _currentTimeGap <= 0:
            {
                var football = _characterPool.GetCharacterFromPool();
                if (football == null)
                {
                    break;
                }
                
                football.transform.position = initPosition;
                football.GetComponent<ShootFootball>().hit = false;
                Serve(football);
        
                _currentTimeGap = serveTimeGap;
                
                break;
            }
        }
    }
    

    private void Serve(GameObject football)
    {
        var _rigidbody = football.GetComponent<Rigidbody>();
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = serveVelocity;
        // _rigidbody.velocity = Vector3.zero;
        // _rigidbody.AddForce(serveForce, ForceMode.Impulse);
    }
}
