//------------------------------------------------------------------------------------------------------------- 
//-- author buddy //-- ???? //addr=addr+"/JS/calendardlg.jsp";
//------------------------------------------------------------------------------------------------------------- 
function fPopUpCalendarDlg(ctrlobj,addr) {
 showx = event.screenX - event.offsetX - 4 - 210 ;
 // + deltaX;
 showy = event.screenY - event.offsetY + 18; // + deltaY; 
 newWINwidth = 210 + 4 + 18;
 retval = window.showModalDialog(addr, "", "dialogWidth:197px; dialogHeight:210px; dialogLeft:" + showx + "px; dialogTop:" + showy + "px; status:no; directories:yes;scrollbars:no;Resizable=no; ");
 if (retval != null) { ctrlobj.value = retval; } 
   }