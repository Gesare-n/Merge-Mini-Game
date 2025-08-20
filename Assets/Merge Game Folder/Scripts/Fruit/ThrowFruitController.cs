using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowFruitController : MonoBehaviour
{
    public static ThrowFruitController instance;

    public GameObject CurrentFruit { get; set; }
    [SerializeField] private Transform _fruitTransform;
    [SerializeField] private Transform _parentAfterThrow;
    [SerializeField] private FruitSelector _selector;
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private BoxCollider2D _boundaries;

    private float _leftBound;
    private float _rightBound;
    private float _startingLeftBound;
    private float _startingRightBound;

    //private PlayerController _playerController;

    //private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    public Bounds Bounds { get; private set; }

    private Bounds _bounds;

    private const float EXTRA_WIDTH = 0.03f;

    public bool CanThrow { get; set; } = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _bounds = _boundaries.bounds;

        _leftBound = _bounds.min.x;
        _rightBound = _bounds.max.x;

        _startingLeftBound = _leftBound;
        _startingRightBound = _rightBound;
    }

    private void Start()
    {
        //_playerController = GetComponent<PlayerController>();

        SpawnAFruit(_selector.PickRandomFruitForThrow());
    }

    private void Update()
    {
        if (UserInput.hasTouch)
        {
            UserInput.targetX = Mathf.Clamp(UserInput.targetX, _leftBound, _rightBound);

            Vector3 currentPos = _fruitTransform.position;
            float newX = Mathf.MoveTowards(currentPos.x, UserInput.targetX, _moveSpeed * Time.deltaTime);
            _fruitTransform.position = new Vector3(newX, currentPos.y, currentPos.z);

            if (Mathf.Approximately(newX, UserInput.targetX))
            {
                UserInput.hasTouch = false;
                if (CanThrow && !UserInput.hasTouch)
                {
                    SpriteIndex index = CurrentFruit.GetComponent<SpriteIndex>();
                    Quaternion rot = CurrentFruit.transform.rotation;

                    GameObject go = Instantiate(FruitSelector.instance.Fruits[index.Index], CurrentFruit.transform.position, rot);
                    go.transform.SetParent(_parentAfterThrow);

                    Destroy(CurrentFruit);

                    CanThrow = false;
                }
            }
        }
    }

    public void SpawnAFruit(GameObject fruit)
    {
        GameObject go = Instantiate(fruit, _fruitTransform);
        CurrentFruit = go;
        _circleCollider = CurrentFruit.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        ChangeBoundary(EXTRA_WIDTH);
    }

    public void ChangeBoundary(float extraWidth)
    {
        _leftBound = _startingLeftBound;
        _rightBound = _startingRightBound;

        _leftBound += Bounds.extents.x + extraWidth;
        _rightBound -= Bounds.extents.x + extraWidth;
    }
}
