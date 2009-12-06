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
PURPOSE:  Provides basic cross-browser javascript functionality

FUNCTION SIGNATURES:
--------------------
function AttachEventHandler(eventSource, eventIEName, eventFFName, eventTarget)
function AttachWindowLoadEventHandler(eventTarget)
function EventCharCode(ev)
function getStyle(el, styleProp)
function FindPosX(el)
function FindPosY(el)
function setStyle(el, styleProp, styleValue)
function StopEvent(ev)
function SourceElement(ev)
function WindowInnerDimensionVertical()
function WindowInnerDimensionHorizontal()
*/


function AttachEventHandler(eventSource, eventIEName, eventFFName, eventTarget){
//PURPOSE = Attach an event handler
    if(eventSource.attachEvent)
        eventSource.attachEvent(eventIEName, eventTarget);
    else
        eventSource.addEventListener(eventFFName, eventTarget, false);    
}

function AttachWindowLoadEventHandler(eventTarget){
//PURPOSE = Attach an event handler to the window onload\load event
    AttachEventHandler(window, "onload", "load", eventTarget);
}

function EventCharCode(ev){
//PURPOSE = Get the character code from the event object.
    return (ev.charCode) ? ev.charCode :
           ((ev.keyCode) ? ev.keyCode :
           ((ev.which) ? ev.which : 0));
}

function getStyle(el, styleProp){
//PURPOSE = Get the value for a given style property - coded for IE and FF
    if (el.currentStyle)
        var styleValue = el.currentStyle[styleProp];
    else if (window.getComputedStyle)
        var styleValue = document.defaultView.getComputedStyle(el, null).getPropertyValue(styleProp);
    
    return styleValue;
}

function FindPosX(el){
//PURPOSE = Find the absolute X co-ordinate of an HTML element, taking into account scrollable regions.
//Param.1.in = HTML element
    var currentLeft = 0;
    if(el.offsetParent)
        while(1){
          currentLeft+= el.offsetLeft - ((el.scrollLeft) ? el.scrollLeft : 0);
          if(!el.offsetParent)
            break;
          el = el.offsetParent;
        }
    else if(el.x)
        currentLeft+= el.x;
    return currentLeft;
}

function FindPosY(el){
//PURPOSE = Find the absolute Y co-ordinate of an HTML element
//Param.1.in = HTML element
    var currentTop = 0;
    if(el.offsetParent)
        while(1){
          currentTop+= el.offsetTop - ((el.scrollTop) ? el.scrollTop : 0);
          if(!el.offsetParent)
            break;
          el = el.offsetParent;
        }
    else if(el.y)
        currentTop+= el.y;
    return currentTop;
}

function setStyle(el, styleProp, styleValue){
//PURPOSE = Get the value for a given style property - coded for IE and FF
    if (el.currentStyle)
        el.style[styleProp] = styleValue;
    else if (window.getComputedStyle)
        el.style.setProperty(styleProp, styleValue, "");
}                

function StopEvent(ev){
//PURPOSE = Disable post-back handling code
    if (ev.stopPropagation){
        ev.preventDefault();
        ev.stopPropagation();          
    }else{
        ev.returnValue = false;
        ev.cancelBubble = true;
    }
}

function SourceElement(ev){
//PURPOSE = Get a reference to the source element
//Param.1.in = Required, event object reference
    return (ev.srcElement) ? ev.srcElement : ev.target;
}

function WindowInnerDimensionVertical(){
//PURPOSE = Determine the vertical inner dimension for the window
  if (typeof(window.innerHeight) == 'number')
    return window.innerHeight;  //Non-IE
  else if (document.documentElement && document.documentElement.clientHeight)
    return document.documentElement.clientHeight;  //IE 6+ in 'standards compliant mode'
  else if (document.body && document.body.clientHeight)
    return document.body.clientHeight;  //IE 4 compatible 
}

function WindowInnerDimensionHorizontal(){
//PURPOSE = Determine the horizontal inner dimension for the window
  if (typeof(window.innerWidth) == 'number')
    return window.innerWidth;  //Non-IE
  else if (document.documentElement && document.documentElement.clientWidth)
    return document.documentElement.clientWidth;  //IE 6+ in 'standards compliant mode'
  else if (document.body && document.body.clientWidth)
    return document.body.clientWidth;  //IE 4 compatible 
}