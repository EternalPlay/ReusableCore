/*
PURPOSE:  Provides javascript support for ContentPopUp and ContentPopUpLink server controls
*/


/*--------------------------
--- "Constant" Variables ---
--------------------------*/
var CONTENTPOPUP_AUTO = "Auto";
var CONTENTPOPUP_Bottom = "Bottom", CONTENTPOPUP_Top = "Top";
var CONTENTPOPUP_Right = "Right", CONTENTPOPUP_Left = "Left";
var CONTENTPOPUP_BR = CONTENTPOPUP_Bottom + CONTENTPOPUP_Right, CONTENTPOPUP_BL = CONTENTPOPUP_Bottom + CONTENTPOPUP_Left;
var CONTENTPOPUP_TR = CONTENTPOPUP_Top + CONTENTPOPUP_Right, CONTENTPOPUP_TL = CONTENTPOPUP_Top + CONTENTPOPUP_Left;


/*---------------
--- FUNCTIONS ---
---------------*/
function TogglePopupElement(ev, popUpElementID, isRightToLeft, launchPosition){
//PURPOSE = Display a pop-up UI element
//Param.1.in = Required, event object reference
//Param.2.in = Required, valid element ID for the popup element
//Param.3.in = Specifies language direction, English would have isRightToLeft == false
    var popUpElement = document.getElementById(popUpElementID);
    var displayValue = getStyle(popUpElement, "display");  
    
    //NOTE:  Position and Show Pop-Up
    if (displayValue == "none"){
        //NOTE:  Append popup element under form and position using client X\Y values 
        var srcEl = SourceElement(ev);
        document.forms[0].appendChild(popUpElement);
        
        //NOTE:  Show popup
        setStyle(popUpElement, "display", "block");
        
        //NOTE:  Auto set language appropriate launch position
        if (launchPosition == CONTENTPOPUP_AUTO || launchPosition == undefined)
            launchPosition = (isRightToLeft) ? CONTENTPOPUP_BL : CONTENTPOPUP_BR;    
        
        //NOTE:  Coordinate assignment
        setStyle(popUpElement, "left", fnGetLaunchPositionX(srcEl, popUpElement, isRightToLeft, launchPosition) + "px");
        setStyle(popUpElement, "top", fnGetLaunchPositionY(srcEl, popUpElement, isRightToLeft, launchPosition) + "px");
    }else{
    //NOTE:  Hide Pop-Up
        setStyle(popUpElement, "display", "none");                    
    }
    
    StopEvent(ev);
    return false;
}

function fnGetLaunchPositionX(sourceElement, popupElement, isRightToLeft, launchPosition){
//PURPOSE = Determine the x coordinate for the popup launch target position
//Param.1.in = reference to the event source, popup link
//Param.2.in = reference to the popup element
//Param.3.in = Specifies language direction, English would have isRightToLeft == false
//Param.4.in = Specifies the desired launch direction
    var posX = 0, srcX = FindPosX(sourceElement);
    
    switch(launchPosition){
        case CONTENTPOPUP_BL:
        case CONTENTPOPUP_TL:
            //NOTE:  Launch Left of Source if possible
            if (fnPopupCanFitLeft(srcX, sourceElement, popupElement))
                posX = fnGetLeftX(srcX, sourceElement, popupElement);
            else if (fnPopupCanFitRight(srcX, popupElement))
                posX = fnGetRightX(srcX);
            else
                posX = 0;  
            break;
           
        case CONTENTPOPUP_BR:
        case CONTENTPOPUP_TR:
            //NOTE:  Launch Right of Source if possible
            if (fnPopupCanFitRight(srcX, popupElement))
                posX = fnGetRightX(srcX);
            else if (fnPopupCanFitLeft(srcX, sourceElement, popupElement))
                posX = fnGetLeftX(srcX, sourceElement, popupElement);
            else
                posX = WindowInnerDimensionHorizontal() - popupElement.offsetWidth;             
            break;
    }
    
    return posX;
}

function fnGetLaunchPositionY(sourceElement, popupElement, isRightToLeft, launchPosition){
//PURPOSE = Determine the y coordinate for the popup launch target position
//Param.1.in = reference to the event source, popup link
//Param.2.in = reference to the popup element
//Param.3.in = Specifies language direction, English would have isRightToLeft == false
//Param.4.in = Specifies the desired launch direction
    var posY = 0, srcY = FindPosY(sourceElement);
    
    switch(launchPosition){
        case CONTENTPOPUP_TR:
        case CONTENTPOPUP_TL:
            //NOTE:  Launch Above Source if possible
            if (fnPopupCanFitAbove(srcY, popupElement))
                posY = fnGetAboveY(srcY, popupElement);
            else if (fnPopupCanFitBelow(srcY, sourceElement, popupElement))
                posY = fnGetBelowY(srcY, sourceElement);
            else 
                posY = 0;
            break;
            
        case CONTENTPOPUP_BR:
        case CONTENTPOPUP_BL:
            //NOTE:  Launch Below Source if possible
            if (fnPopupCanFitBelow(srcY, sourceElement, popupElement))
                posY = fnGetBelowY(srcY, sourceElement);
            else if (fnPopupCanFitAbove(srcY, popupElement))
                posY = fnGetAboveY(srcY, popupElement);
            else
                posY = document.body.offsetHeight - popupElement.offsetHeight;            
            break;
    }
    
    return posY;
}

function fnPopupCanFitBelow(srcY, sourceElement, popupElement){
    return (fnGetBelowY(srcY, sourceElement) + popupElement.offsetHeight <= WindowInnerDimensionVertical());
}

function fnPopupCanFitAbove(srcY, popupElement){
    return (fnGetAboveY(srcY, popupElement) >= 0);
}

function fnPopupCanFitLeft(srcX, sourceElement, popupElement){
    return (fnGetLeftX(srcX, sourceElement, popupElement) >=0);
}

function fnPopupCanFitRight(srcX, popupElement){
    return (fnGetRightX(srcX) + popupElement.offsetWidth <= WindowInnerDimensionHorizontal());
}

function fnGetBelowY(srcY, sourceElement){
    return srcY + sourceElement.offsetHeight;
}

function fnGetAboveY(srcY, popupElement){
    return srcY - popupElement.offsetHeight;
}

function fnGetLeftX(srcX, sourceElement, popupElement){
    return srcX + sourceElement.offsetWidth - popupElement.offsetWidth;
}

function fnGetRightX(srcX){
    return srcX;
}