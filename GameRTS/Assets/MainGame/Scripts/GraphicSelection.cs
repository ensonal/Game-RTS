using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicSelection : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;
    GameObject selectedObject;
    // take EventSystem from the scene
    [SerializeField] GameObject eventSystemInScene;
    
    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (var result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
                Button button = result.gameObject.GetComponent<Button>();
                
                if (button != null)
                {
                    if (button.gameObject.name == "Upgrade Castle Button")
                    {
                        Debug.Log("Upgrade Castle Button clicked!");
                        eventSystemInScene.GameObject().SetActive(false);
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Close Button")
                    {
                        Debug.Log("Close Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        StartCoroutine(WaitForEventSystem());
                    }
                    
                    if (button.gameObject.name == "Create Building Button")
                    {
                        Debug.Log("Create Building Button clicked!");
                        eventSystemInScene.GameObject().SetActive(false);
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Menu Button")
                    {
                        Debug.Log("Menu Button clicked!");
                        eventSystemInScene.GameObject().SetActive(false);
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Close Menu Button")
                    {
                        Debug.Log("Close Menu Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        StartCoroutine(WaitForEventSystem());
                    }
                    
                    if (button.gameObject.name == "Archeryard Button")
                    {
                        Debug.Log("Archeryard Button clicked!");
                        eventSystemInScene.GameObject().SetActive(false);
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Lumbermill Button")
                    {
                        Debug.Log("Lumbermill Button clicked!");
                        eventSystemInScene.GameObject().SetActive(false);
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                }
            }
        }
    }
    
    private IEnumerator WaitForEventSystem()
    {
        Debug.Log("worked WaitForEventSystem coroutine");
        yield return new WaitForSeconds(1);
        eventSystemInScene.GameObject().SetActive(true);
        Debug.Log("waited 1 second");
    }
}
