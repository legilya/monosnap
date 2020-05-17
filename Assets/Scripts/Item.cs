using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [SerializeField] private int passwordValue;
    private EntryFieldController entryFieldController;

    public int PasswordValue => passwordValue;

    private void Start()
    {
        entryFieldController = FindObjectOfType<EntryFieldController>();
        entryFieldController.Items.Add(this);
    }

    /// <summary>
    /// При начале ввода передаем соответствующие параметры контроллеру поля ввода
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        entryFieldController.AddEntryPasswordItem(transform.position, passwordValue);
    }

    /// <summary>
    /// Если был начат ввод и указатель попал на данный объект
    /// передать значение пароля и позицию точки
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.eligibleForClick)
            entryFieldController.AddEntryPasswordItem(transform.position, passwordValue);
    }
}