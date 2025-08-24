using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFruitController : MonoBehaviour
{
    public static ThrowFruitController instance;

    [Header("References")]
    public GameObject CurrentFruit { get; private set; }
    [SerializeField] private Transform _fruitTransform;
    [SerializeField] private Transform _parentAfterThrow;
    [SerializeField] private FruitSelector _selector;  // FruitSelector reference
    [SerializeField] private BoxCollider2D _boundaries;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    private const float EXTRA_WIDTH = 0.03f;

    [Header("Runtime")]
    public bool CanThrow { get; set; } = true;
    private CircleCollider2D _circleCollider;
    private Bounds _bounds;
    private float _leftBound;
    private float _rightBound;
    private float _startingLeftBound;
    private float _startingRightBound;

    public Bounds Bounds { get; private set; }

    // --------------------------------------------------------
    // MonoBehaviour Methods
    // --------------------------------------------------------
    private void Awake()
    {
        if (instance == null)
            instance = this;

        // Auto-assign boundaries if not set in Inspector
        if (_boundaries == null)
            _boundaries = GetComponent<BoxCollider2D>();

        _bounds = _boundaries.bounds;
        _leftBound = _bounds.min.x;
        _rightBound = _bounds.max.x;

        _startingLeftBound = _leftBound;
        _startingRightBound = _rightBound;
    }

    private void Start()
    {
        // Auto-assign FruitSelector if not set in Inspector
        if (_selector == null)
            _selector = FindObjectOfType<FruitSelector>();

        if (_selector == null)
        {
            Debug.LogError("❌ FruitSelector not found in scene! Please add one.");
            return;
        }

        // Spawn the first fruit when the game starts
        SpawnAFruit(_selector.PickRandomFruitForThrow());
    }

    private void Update()
    {
        HandleFruitMovement();
    }

    // --------------------------------------------------------
    // Fruit Handling
    // --------------------------------------------------------
    private void HandleFruitMovement()
    {
        if (!UserInput.hasTouch) return;

        // Clamp touch input to boundaries
        UserInput.targetX = Mathf.Clamp(UserInput.targetX, _leftBound, _rightBound);

        // Smoothly move fruit towards target
        Vector3 currentPos = _fruitTransform.position;
        float newX = Mathf.MoveTowards(currentPos.x, UserInput.targetX, _moveSpeed * Time.deltaTime);
        _fruitTransform.position = new Vector3(newX, currentPos.y, currentPos.z);

        // If fruit reaches target, throw it
        if (Mathf.Approximately(newX, UserInput.targetX))
        {
            UserInput.hasTouch = false;

            if (CanThrow && !UserInput.hasTouch)
            {
                ThrowFruit();
            }
        }
    }

    private void ThrowFruit()
    {
        SpriteIndex index = CurrentFruit.GetComponent<SpriteIndex>();
        Quaternion rot = CurrentFruit.transform.rotation;

        GameObject go = Instantiate(FruitSelector.instance.Fruits[index.Index], CurrentFruit.transform.position, rot);
        go.transform.SetParent(_parentAfterThrow);

        Destroy(CurrentFruit);
        CanThrow = false;
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

    // --------------------------------------------------------
    // Powerup Handling
    // --------------------------------------------------------
    public void UpgradeCurrentFruit()
    {
        if (CurrentFruit == null) return;

        SpriteIndex index = CurrentFruit.GetComponent<SpriteIndex>();
        if (index == null) return;

        int nextIndex = index.Index + 1;

        // Ensure we don't go out of fruit list bounds
        if (nextIndex < FruitSelector.instance.Fruits.Length)
        {
            Vector3 pos = CurrentFruit.transform.position;
            Destroy(CurrentFruit);

            GameObject newFruit = Instantiate(FruitSelector.instance.Fruits[nextIndex], pos, Quaternion.identity, _fruitTransform);
            CurrentFruit = newFruit;

            // Update collider + boundaries
            _circleCollider = CurrentFruit.GetComponent<CircleCollider2D>();
            Bounds = _circleCollider.bounds;
            ChangeBoundary(EXTRA_WIDTH);
        }
    }
}
