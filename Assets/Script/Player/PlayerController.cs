using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private Rigidbody2D myRigidbody;

    [SerializeField] private Animator myAnimator;

    [SerializeField] private List<CharacterInteractionTrigger> triggerList;

    [SerializeField] private List<Clothes> myClothes = new List<Clothes>();

    [SerializeField] private Transform monologHolder;
    [SerializeField] private Monolog monologPrefab;

    [Serializable]
    private struct Clothes
    {
        public ProductType Type;
        public SpriteRenderer SpriteOnCharacter;
    }

    private const string horizontalAxisAnimationParameterName = "Horizontal";
    private const string verticalAxisAnimationParameterName = "Vertical";
    private const string speedAnimationParameterName = "Speed";
    private const string directionAnimationParameterName = "Direction";
    private const int initialWalletAmount = 100;

    Vector2 movement;

    private InputDirection lastDirection = InputDirection.Down;
    private UIManager uiManagerRef;
    private Wallet playerWallet;
    private Inventory playerInventory;

    private void Awake()
    {
        playerWallet = new Wallet(initialWalletAmount);
        ServiceLocator.RegisterService<Wallet>(playerWallet);

        playerInventory = new Inventory();
        ServiceLocator.RegisterService<Inventory>(playerInventory);
        RefreshClothesItems();
        playerInventory.RegisterActionToOnEquipChange(RefreshClothesItems);

        ServiceLocator.RegisterService<PlayerController>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<Wallet>();
        ServiceLocator.DeregisterService<Inventory>();
        ServiceLocator.DeregisterService<PlayerController>();
        playerInventory.DeregisterActionToOnEquipChange(RefreshClothesItems);
    }

    private void Start()
    {
        uiManagerRef = ServiceLocator.GetService<UIManager>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        myAnimator.SetFloat(horizontalAxisAnimationParameterName, movement.x);
        myAnimator.SetFloat(verticalAxisAnimationParameterName, movement.y);
        myAnimator.SetFloat(speedAnimationParameterName, movement.magnitude);

        if(movement.magnitude > 0)
        {
            uiManagerRef.CloseUIClosableByMovement();
        }

        SetLastDirectionInput();

        HandleInteraction();
    }

    public (int badassery, int coolness, int cuteness) GetPlayerStats()
    {
        var equipedItems = playerInventory.GetEquipedItems();

        int badassery = 0, coolness = 0, cuteness = 0;
        foreach (var item in equipedItems)
        {
            var product = item.Product;

            if(product == null)
            {
                continue;
            }

            badassery += product.Badassery;
            coolness += product.Coolness;
            cuteness += product.Cuteness;
        }

        return (badassery, coolness, cuteness);
    }

    public void CreateMonolog(string text, float duration)
    {
        var monolog = Instantiate(monologPrefab, monologHolder);
        monolog.SetupMonolog(text, duration);
    }

    private void SetLastDirectionInput()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            lastDirection = InputDirection.Right;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            lastDirection = InputDirection.Left;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            lastDirection = InputDirection.Up;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            lastDirection = InputDirection.Down;
        }

        myAnimator.SetInteger(directionAnimationParameterName, (int)lastDirection);
        UpdateInteractionTrigger();
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var trigger = triggerList.Find(t => t.TriggerDirection == lastDirection);
            trigger.Trigger.TryToInteract();
        }
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + movementSpeed * movement.normalized * Time.fixedDeltaTime);
    }

    private void UpdateInteractionTrigger()
    {
        foreach (var trigger in triggerList)
        {
            trigger.SetActive(trigger.TriggerDirection == lastDirection);
        }
    }

    private void RefreshClothesItems()
    {
        foreach (var piece in myClothes)
        {
            var equipment = playerInventory.GetItemOnEquipedSlot(piece.Type);
            if(equipment.Product == null)
            {
                piece.SpriteOnCharacter.gameObject.SetActive(false);
                continue;
            }

            piece.SpriteOnCharacter.gameObject.SetActive(true);
            piece.SpriteOnCharacter.sprite = equipment.Product.ProductImage;
        }
    }
}
