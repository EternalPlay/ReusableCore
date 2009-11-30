
/*
PURPOSE:  Provides javascript support for the Expander server control
*/

function ToggleExpander(ev, bodyElementID, expandElementID, collapseElementID){
//PURPOSE = Display a pop-up UI element
//Param.1.in = Required, event object reference
//Param.2.in = Required, valid element ID, Expander body
//Param.3.in = Required, valid element ID, expand element
//Param.4.in = Required, valid element ID, collapse element
    var bodyElement = document.getElementById(bodyElementID);
    var expandElement = document.getElementById(expandElementID);
    var collapseElement = document.getElementById(collapseElementID);
    var displayValue = getStyle(bodyElement, "display");  
    
    //NOTE:  Expand Body
    if (displayValue == "none"){
        //NOTE:  Append popup element under form and position using client X\Y values 
        var srcEl = SourceElement(ev);
        
        setStyle(bodyElement, "display", "block");
        setStyle(expandElement, "display", "none");
        setStyle(collapseElement, "display", "");
    }else{
    //NOTE:  Collapse Body
        setStyle(bodyElement, "display", "none");                    
        setStyle(expandElement, "display", ""); 
        setStyle(collapseElement, "display", "none"); 
    }
    
    StopEvent(ev);
    return false;
}




