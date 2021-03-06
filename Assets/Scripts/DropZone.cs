﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool isFull = false;
    CardSpace cardSpace;
    public void OnDrop(PointerEventData eventData){
        if(!isFull){
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d!=null){
            d.parentToReturnTo = this.transform;
        }
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(!isFull){
        if(eventData.pointerDrag == null){
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(this.tag == "spazio_up"){
            if(d.baseRotation == 0){
                d.rotated = false;
            } else{
                d.rotated = true;
            }
        } else{
            if(d.baseRotation == 0){
                d.rotated = true;
            } else{
                d.rotated = false;
            }
        }
        /*
        if(this.tag == "spazio_up"){
            d.rotated = false;
        } else{
            d.rotated = true;
        }
        */ //Funzionante
        d.rotated = !d.rotated;
        d.UpdateRotation();
        if(d!=null){
            d.placeholderParent = this.transform;
        }
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if(eventData.pointerDrag == null){
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        d.rotated = false;
        //d.UpdateRotation();
        if(d!=null && d.placeholderParent==this.transform){
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        cardSpace = GetComponent<CardSpace>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount > 1){
            isFull = true;
        }

        if(this.transform.childCount == 0){
            isFull = false;
        }
    }
}
