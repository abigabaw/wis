function GetCalDate(containerID) {
    var calDate = '';

    spnContainer = document.getElementById(containerID);
    calDate = spnContainer.value;

    /*
    if (spnContainer != null) {
        inputElems = spnContainer.getElementsByTagName('input');

        for (i = 0; i < inputElems.length; i++) {
            elem = inputElems[i];

            if (elem.type == 'text') {
                calDate = elem.value;
                break;
            }
        }
    }
    */

    return calDate;
}

function Getmessages() {
    var msg = new Array();
    msg[0] = "Are you sure you want to Delete?";
    msg[1] = "<b>confirmation </b>";
    msg[2] = "Deleted Successfully.";

    return msg;
}
 
function GetMonthNumber(monthStr) {
    switch (monthStr) {
        case 'Jan':
            return 1;
            break;
        case 'Feb':
            return 2;
            break;
        case 'Mar':
            return 3;
            break;
        case 'Apr':
            return 4;
            break;
        case 'May':
            return 5;
            break;
        case 'Jun':
            return 6;
            break;
        case 'Jul':
            return 7;
            break;
        case 'Aug':
            return 8;
            break;
        case 'Sep':
            return 9;
            break;
        case 'Oct':
            return 10;
            break;
        case 'Nov':
            return 11;
            break;
        case 'Dec':
            return 12;
            break;
    }
}

function ClearDateField(containerID) {
    spnContainer = document.getElementById(containerID);
    if (spnContainer != null) {
        inputElems = spnContainer.getElementsByTagName('input');

        for (i = 0; i < inputElems.length; i++) {
            elem = inputElems[i];

            if (elem.type == 'text') {
                elem.value = '';
            }
        }
    }
}

function PreventDateFieldEntry(elem) {
    if (elem != null) {
        elem.readOnly = true;
        elem.onkeydown = function Check() {
            if (event != null) {
                PreventKeyDown();
            };
        }

        /*inputElems = spnContainer.getElementsByTagName('input');

        for (i = 0; i < inputElems.length; i++) {
            elem = inputElems[i];

            if (elem.type == 'text') {
                elem.readOnly = true;
                elem.onkeydown = function Check() {
                    if (event != null) {
                        PreventKeyDown();
                    };
                }
            }
        }*/
    }
}

function PreventKeyDown() {
    var keyCode = (event.which) ? event.which : event.keyCode;
    if ((keyCode == 8) || (keyCode == 46))
        event.returnValue = false;
}

function ShowSaveMessage(msg) {
    if (msg == '')
        alert('Data saved successfully');
    else
        alert(msg);
}

function ShowUpdateMessage(msg) {
    if (msg == '')
        alert('Data updated successfully');
    else
        alert(msg);
}

function DeleteRecord() {
    return confirm('Are you sure you want to Delete this Record?');
}

function ConfirmFreezeProject() {
    return confirm('Are you sure you want to Freeze this Project?');
}

function CheckDecimal(e, src) {
    if (e.keyCode == 46) { // Invoke when press Enter Key
        var char = src.value;

        if (char.indexOf(".") == -1) {
            return true;
        }
        else if (char.indexOf(".") > -1) {
            e.keyCode = 0;
            return false;
        }
    }
    else
        return true;
}

function SetUpperCase(fld) {
    if (fld) fld.value = fld.value.toUpperCase();
}