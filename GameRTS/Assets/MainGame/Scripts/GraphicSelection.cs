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
    GameObject selectedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            foreach (var result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
                
                Button button = result.gameObject.GetComponent<Button>();
                
                if (button != null)
                {
                    if (button.gameObject.name == "Close Button")
                    {
                        Debug.Log("Close Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        StartCoroutine(WaitForEventSystem());
                    }
                    
                    if (button.gameObject.name == "Create Unit Button")
                    {
                        Debug.Log("Create Unit Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Create Building Button")
                    {
                        Debug.Log("Create Building Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Menu Button")
                    {
                        Debug.Log("Menu Button clicked!");
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
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Castle Button")
                    {
                        Debug.Log("Castle Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Lumbermill Button")
                    {
                        Debug.Log("Lumbermill Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "WoodCutter Button")
                    {
                        Debug.Log("WoodCutter Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Swat Button")
                    {
                        Debug.Log("Swat Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if (button.gameObject.name == "Archer Button")
                    {
                        Debug.Log("Archer Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if(button.gameObject.name == "Plus Button")
                    {
                        Debug.Log("Plus Button clicked!");
                        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                    
                    if(button.gameObject.name == "Minus Button")
                    {
                        Debug.Log("Minus Button clicked!");
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
        Debug.Log("waited 1 second");
    }
}
