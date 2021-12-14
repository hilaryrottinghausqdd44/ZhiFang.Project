<HTML>
<HEAD> <TITLE>Calendar</TITLE></HEAD>

<SCRIPT LANGUAGE="JavaScript">
<!--
// AUTHOR:       Unknown
// MODIFIED BY:  Robert W. Husted
// COMPANY:      Netscape Communications


function setDate() {
    this.dateField   = opener.dateField;
    this.inDate      = dateField.value;

    // SET DAY MONTH AND YEAR TO TODAY'S DATE
    var now   = new Date();
    var day   = now.getDate();
    var month = now.getMonth();
    var year  = now.getFullYear();

    // IF A DATE WAS PASSED IN THEN PARSE THAT DATE
    if (inDate.indexOf('-')) {
        ///var inMonth = inDate.substring(0,inDate.indexOf("/"));
        ///    if (inMonth.substring(0,1) == "0" && inMonth.length > 1)
        ///        inMonth = inMonth.substring(1,inMonth.length);
        ///    inMonth = parseInt(inMonth);
        ///var inDay   = inDate.substring(inDate.indexOf("/") + 1, inDate.lastIndexOf("/"));
        ///    if (inDay.substring(0,1) == "0" && inDay.length > 1)
        ///        inDay = inDay.substring(1,inDay.length);
        ///    inDay = parseInt(inDay);
        ///var inYear  = parseInt(inDate.substring(inDate.lastIndexOf("/") + 1, inDate.length));
        var inMonth   = inDate.substring(inDate.indexOf("-") + 1, inDate.lastIndexOf("-"));
            if (inMonth.substring(0,1) == "0" && inMonth.length > 1)
                inMonth = inMonth.substring(1,inMonth.length);
            inMonth = parseInt(inMonth);
        var inDay  = inDate.substring(inDate.lastIndexOf("-") + 1, inDate.length);
            if (inDay.substring(0,1) == "0" && inDay.length > 1)
                inDay = inDay.substring(1,inDay.length);
            inDay = parseInt(inDay);
        var inYear =parseInt( inDate.substring(0,inDate.indexOf("-")));
        ///
        
        if (inDay) {
            day = inDay;
        }
        if (inMonth) {
            month = inMonth-1;
        }
        if (inYear) {
            year = inYear;
        }
    }
    this.focusDay                           = day;
    document.calControl.month.selectedIndex = month;
    document.calControl.year.value          = year;
    displayCalendar(day, month, year);
}


function setToday() {
    // SET DAY MONTH AND YEAR TO TODAY'S DATE
    var now   = new Date();
    var day   = now.getDate();
    var month = now.getMonth();
    var year  = now.getFullYear();
    this.focusDay                           = day;
    document.calControl.month.selectedIndex = month;
    document.calControl.year.value          = year;
    displayCalendar(day, month, year);
}


function isFourDigitYear(year) {
    if (year.length != 4) {
        alert ("Sorry, the year must be four-digits in length.");
        document.calControl.year.select();
        document.calControl.year.focus();
    }
    else {
        return true;
    }
}


function selectDate() {
    var year  = document.calControl.year.value;
    if (isFourDigitYear(year)) {
        var day   = 0;
        var month = document.calControl.month.selectedIndex;
        displayCalendar(day, month, year);
    }
}


function setPreviousYear() {
    var year  = document.calControl.year.value;
    if (isFourDigitYear(year)) {
        var day   = 0;
        var month = document.calControl.month.selectedIndex;
        year--;
        document.calControl.year.value = year;
        displayCalendar(day, month, year);
    }
}


function setPreviousMonth() {
    var year  = document.calControl.year.value;
    if (isFourDigitYear(year)) {
        var day   = 0;
        var month = document.calControl.month.selectedIndex;
        if (month == 0) {
            month = 11;
            if (year > 1000) {
                year--;
                document.calControl.year.value = year;
            }
        }
        else {
            month--;
        }
        document.calControl.month.selectedIndex = month;
        displayCalendar(day, month, year);
    }
}


function setNextMonth() {
    var year  = document.calControl.year.value;
    if (isFourDigitYear(year)) {
        var day   = 0;
        var month = document.calControl.month.selectedIndex;
        if (month == 11) {
            month = 0;
            year++;
            document.calControl.year.value = year;
        }
        else {
            month++;
        }
        document.calControl.month.selectedIndex = month;
        displayCalendar(day, month, year);
    }
}


function setNextYear() {
    var year  = document.calControl.year.value;
    if (isFourDigitYear(year)) {
        var day   = 0;
        var month = document.calControl.month.selectedIndex;
        year++;
        document.calControl.year.value = year;
        displayCalendar(day, month, year);
    }
}


function displayCalendar(day, month, year) {       

    day     = parseInt(day);
    month   = parseInt(month);
    year    = parseInt(year);
    var i   = 0;
    var now = new Date();

    if (day == 0) {
        var nowDay = now.getDate();
    }
    else {
        var nowDay = day;
    }
    var days         = getDaysInMonth(month+1,year);
    var firstOfMonth = new Date (year, month, 1);
    var startingPos  = firstOfMonth.getDay();
    days += startingPos;

    // MAKE BEGINNING NON-DATE BUTTONS BLANK
    for (i = 0; i < startingPos; i++) {
        document.calButtons.elements[i].value = "    ";
    }

    // SET VALUES FOR DAYS OF THE MONTH
    for (i = startingPos; i < days; i++)  
    {
		theday = i-startingPos+1
		if (theday < 10 )
        	document.calButtons.elements[i].value   = "  " + theday  ;
		else
			document.calButtons.elements[i].value   = theday  ;
		//document.write( "  " + theday   );
        document.calButtons.elements[i].onClick = "returnDate"
    }

    // MAKE REMAINING NON-DATE BUTTONS BLANK
    for (i=days; i<42; i++)  {
        document.calButtons.elements[i].value = "    ";
    }

    // GIVE FOCUS TO CORRECT DAY
    document.calButtons.elements[focusDay+startingPos-1].focus();
}


// GET NUMBER OF DAYS IN MONTH
function getDaysInMonth(month,year)  {
    var days;
    if (month==1 || month==3 || month==5 || month==7 || month==8 ||
        month==10 || month==12)  days=31;
    else if (month==4 || month==6 || month==9 || month==11) days=30;
    else if (month==2)  {
        if (isLeapYear(year)) {
            days=29;
        }
        else {
            days=28;
        }
    }
    return (days);
}


// CHECK TO SEE IF YEAR IS A LEAP YEAR
function isLeapYear (Year) {
    if (((Year % 4)==0) && ((Year % 100)!=0) || ((Year % 400)==0)) {
        return (true);
    }
    else {
        return (false);
    }
}


// SET FORM FIELD VALUE TO THE DATE SELECTED
function returnDate(inDay)
{
    var day   = inDay;
    var month = (document.calControl.month.selectedIndex)+1;
    var year  = document.calControl.year.value;
	
    if ((""+month).length == 1)
    {
        month="0"+month;
    }
    if (day.length == 3)
    {
		day="0"+day.slice(day.length-1);
        //day="0"+day;
    }
    if (day != "    ") {
        dateField.value = year + "-" + month + "-" +day ; // month + "/" + day + "/" + year;
        window.close()
    }
}


// -->
</SCRIPT>

<BODY BGCOLOR="white" TEXT="#000000" LINK=#004040 ALINK="#002D2D" ONLOAD="setDate()">

<CENTER>
<FORM NAME="calControl" onSubmit="return false;">
<TABLE CELLPADDING=0 CELLSPACING=0 BORDER=1 bordercolor="#4682B4">
<TR>
	<TD COLSPAN=5 align="left">
		<SELECT NAME="month" onChange='selectDate()'>		
		   <OPTION>一月
		   <OPTION>二月
		   <OPTION>三月
		   <OPTION>四月
		   <OPTION>五月
		   <OPTION>六月
		   <OPTION>七月
		   <OPTION>八月
		   <OPTION>九月
		   <OPTION>十月
		   <OPTION>十一月
		   <OPTION>十二月
		   <!--<OPTION>January
		   <OPTION>February
		   <OPTION>March
		   <OPTION>April
		   <OPTION>May
		   <OPTION>June
		   <OPTION>July
		   <OPTION>August
		   <OPTION>September
		   <OPTION>October
		   <OPTION>November
		   <OPTION>December-->
		</SELECT>
	</td>
	<td COLSPAN=2 align=right>
		<INPUT NAME="year" TYPE=TEXT SIZE=4 MAXLENGTH=4 onChange="selectDate()">
	</TD>
</TR>

<TR>
	<TD COLSPAN=7>
		<CENTER>
		<INPUT TYPE=BUTTON NAME="previousYear" VALUE="<<"    onClick="setPreviousYear()"  ><INPUT TYPE=BUTTON NAME="previousYear" VALUE="<"   	 onClick="setPreviousMonth()">
		<!--<INPUT TYPE=BUTTON NAME="previousYear" VALUE="Today" onClick="setToday()">-->
		<INPUT TYPE=BUTTON NAME="previousYear" VALUE="今天" onClick="setToday()">
		<INPUT TYPE=BUTTON NAME="previousYear" VALUE=">"   	 onClick="setNextMonth()"><INPUT TYPE=BUTTON NAME="previousYear" VALUE=">>"    onClick="setNextYear()">
		</CENTER>
	</TD>
</TR>
</FORM>

<FORM NAME="calButtons">
<style type="text/css">
	.calButn	{color:navy;background-color:white; font:"Arial"; font-size:8pt; cursor:hand; border:0; backgroud-color:white }
	.calButnH	{color:white; font:"Arial"; font-size:8pt; border:0;backgroud-color;#87CEFA}
	.butn2		{display:"block"; cursor:hand; color:"#341185"; font:"Arial"; font-weight:"bold"; font-size:6pt; line-height:1; line-width:0; size=0; border-color:white; background-color:#EFECC9;}
</style>

<TR HEIGHT=10><TD align="center"></TD></TR>

<TR>
	<!--<TD align="center"><div id=1 class="calButn"><b>Su</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>Mo</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>Tu</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>We</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>Th</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>Fr</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>Sa</b></div></TD>-->
	<TD align="center"><div id=1 class="calButn"><b>日</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>一</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>二</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>三</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>四</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>五</b></div></TD>
	<TD align="center"><div id=1 class="calButn"><b>六</b></div></TD>
</TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but0"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but1"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but2"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but3"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but4"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but5"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but6"  value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but7"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but8"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but9"  value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but10" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but11" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but12" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but13" value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but14" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but15" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but16" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but17" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but18" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but19" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but20" value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but21" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but22" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but23" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but24" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but25" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but26" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but27" value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but28" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but29" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but30" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but31" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but32" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but33" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but34" value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

<TR><TD align="center"><INPUT TYPE="button" NAME="but35" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but36" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but37" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but38" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but39" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but40" value="    " onClick="returnDate(this.value)" class="calButn"></TD>
    <TD align="center"><INPUT TYPE="button" NAME="but41" value="    " onClick="returnDate(this.value)" class="calButn"></TD></TR>

</TABLE>
<br>
<!--<CENTER><INPUT TYPE="button" NAME="butnCancel" value="Cancel" onClick="window.close();" class="butn2"></CENTER>-->
<CENTER><INPUT TYPE="button" NAME="butnCancel" value=" 取 消 " onClick="window.close();" ></CENTER>

</FORM>
</BODY>
</HTML>
