using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Контроллер поля ввода
/// </summary>
public class EntryFieldController : MonoBehaviour
{
    private List<int> enteredPassword;
    private List<Item> items;

    [SerializeField] private LineRenderersController lineRenderersController;
    [SerializeField] private PasswordChecker passwordChecker;
    [SerializeField] private GameObject wrongPasswordPopup;
    [SerializeField] private GameObject rightPasswordPopup;

    /// <summary>
    /// Позиции для точек которые необходимо добавить
    /// </summary>
    private Dictionary<int, InputOrder[]> entryOrder = new Dictionary<int, InputOrder[]>
    {
        {1, new[] {new InputOrder(3, 2), new InputOrder(9, 5), new InputOrder(7, 4)}},
        {2, new[] {new InputOrder(8, 5)}},
        {3, new[] {new InputOrder(1, 2), new InputOrder(7, 5), new InputOrder(9, 6)}},
        {4, new[] {new InputOrder(6, 5)}},
        {5, new InputOrder[] {}},
        {6, new[] {new InputOrder(4, 5)}},
        {7, new[] {new InputOrder(1, 4), new InputOrder(3, 5), new InputOrder(9, 8)}},
        {8, new[] {new InputOrder(2, 5)}},
        {9, new[] {new InputOrder(1, 5), new InputOrder(3, 6), new InputOrder(7, 8)}}
    };
    
    public List<Item> Items => items;

    private void Awake()
    {
        items = new List<Item>();
        enteredPassword = new List<int>();
    }

    private void Update()
    {        
        // после того как пользователь уберет палец с экрана
        // завершить ввод и проверить пароль
        if (Input.GetKeyUp(KeyCode.Mouse0)
        && enteredPassword.Count > 0)
        {
            CompletePasswordEntry();
        }
    }

    /// <summary>
    /// Ввести значение выбранной точки пароля
    /// </summary>
    /// <param name="position"></param>
    /// <param name="passwordValue"></param>
    public void AddEntryPasswordItem(Vector3 position, int passwordValue)
    {
        if (enteredPassword.Contains(passwordValue))
        {
            return;
        }

        if (enteredPassword.Count > 0)
        {
            int lastEnteredValue = enteredPassword[enteredPassword.Count - 1];
            InputOrder[] inputOrders = entryOrder[lastEnteredValue];
            foreach (var inputOrder in inputOrders)
            {
                if (passwordValue == inputOrder.newValue)
                {
                    if (!enteredPassword.Contains(inputOrder.needToAddValue))
                    {
                        foreach (Item item in items)
                        {
                            if (item.PasswordValue == inputOrder.needToAddValue)
                            {
                                enteredPassword.Add(item.PasswordValue);
                                lineRenderersController.AddLinePosition(item.transform.position + Vector3.back);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        lineRenderersController.AddLinePosition(position + Vector3.back);
        enteredPassword.Add(passwordValue);
    }

    /// <summary>
    /// Завершить ввод и проверить пароль
    /// </summary>
    private void CompletePasswordEntry()
    {
        bool correct = passwordChecker.CheckPassword(enteredPassword);
        enteredPassword.Clear();

        wrongPasswordPopup.SetActive(!correct);
        rightPasswordPopup.SetActive(correct);
    }
}
