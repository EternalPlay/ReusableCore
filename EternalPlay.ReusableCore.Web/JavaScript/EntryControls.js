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
