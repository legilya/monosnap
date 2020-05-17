using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер отрисовки линий на экране
/// </summary>
public class LineRenderersController : MonoBehaviour
{
    // LineRenderer для отрисовки линии двигающейся за пальцем(мышью)
    [SerializeField] private LineRenderer dynamicLineRenderer;
    // LineRendere для отрисовки введенных точек
    [SerializeField] private LineRenderer enteredLinesLineRenderer;
    private List<Vector3> linePositions;
    private float zPosition;

    private void Awake()
    {
        linePositions = new List<Vector3>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)
        && dynamicLineRenderer.positionCount > 0)
        {
            DrawDynamicLine();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)
        && linePositions.Count > 0)
        {
            dynamicLineRenderer.positionCount = 0;
            enteredLinesLineRenderer.positionCount = 0;
            linePositions.Clear();
        }
    }

    /// <summary>
    /// Добавить точку для отрисовки линии
    /// </summary>
    /// <param name="position"></param>
    public void AddLinePosition(Vector3 position)
    {
        linePositions.Add(position);
        enteredLinesLineRenderer.positionCount = linePositions.Count;
        enteredLinesLineRenderer.SetPositions(linePositions.ToArray());

        if (dynamicLineRenderer.positionCount == 0)
        {
            dynamicLineRenderer.positionCount = 2;
        }
        dynamicLineRenderer.SetPosition(0, position);
        zPosition = position.z;
    }
    
    /// <summary>
    /// Нарисовать линию от последней точки,
    /// которая следует за курсором(положением пальца на экране)
    /// </summary>
    private void DrawDynamicLine()
    { 
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = new Vector3(
            position.x,
            position.y,
            zPosition);

        dynamicLineRenderer.SetPosition(1, currentPosition);
    }
}
