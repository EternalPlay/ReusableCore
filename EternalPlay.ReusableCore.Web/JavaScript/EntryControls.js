
function AssociateLabelWithTextBox(labelId,textBoxId){
//PURPOSE = Associates a label with a text box
    var label = document.getElementById(labelId);
    if (label) label.htmlFor = textBoxId;
}

function MoveErrorTextAdjacentToLabel(errorTextId,labelId){
//PURPOSE = Move the specified error text next to the specified label
    var errorText = document.getElementById(errorTextId);
    var label = document.getElementById(labelId);
    
    label.parentNode.appendChild(errorText);
}

//TODO:  Consider moving the following function to the cross browser functions javascript file
function IntegerEntry_OnKeyPress(event,filterExpression){
//PURPOSE = Filter keystrokes using the filter expression and the list of exceptions
    var charCode = EventCharCode(event);
    
    //NOTE:  If the keypress event is thrown for these char codes switch to zero so that they are not filtered.
    //NOTE:  IE does not fire the onkeypress event for these keys but FF does
    switch(charCode){
        case 8:     //Backspace
        case 9:     //Tab
        case 13:    //Enter
        case 27:    //Escape
        case 33:    //Page up
        case 34:    //Page down
        case 35:    //End
        case 36:    //Home
        case 37:    //Left arrow
        case 38:    //Down arrow
        case 39:    //Right arrow
        case 40:    //Up arrow
        case 45:    //Insert
        case 46:    //Delete
            charCode=0;
            break;
    }
    
    //NOTE:  Stop the event for non-zero char codes that don't match the regular expression
    if ((charCode)&&(!RegExp(filterExpression).test(String.fromCharCode(charCode)))) {
        StopEvent(event);
        return false;
    }
}
