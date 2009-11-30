
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