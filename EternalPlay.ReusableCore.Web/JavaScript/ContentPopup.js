/*	
Microsoft Reciprocal License (Ms-RL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) Reciprocal Grants- For any file you distribute that contains code from the software (in source code or binary format), you must provide recipients the source code to that file along with a copy of this license, which license will govern that file. You may license other files that are entirely your own work and do not contain code from the software under any terms you choose.
(B) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(C) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(D) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(E) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(F) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
*/


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