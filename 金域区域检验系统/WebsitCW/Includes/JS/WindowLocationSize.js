function openWinPositionSizeF(url,ileft,itop,iwidth,iheight)
{
	//alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
	//var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
	var mywin=window.open(url,"_blank","toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+",height="+iheight+",scrollbars=yes");
}

function openWinPositionSize(url,strPositionSize)
{
	//var strPositionSize=openPositionSize.value;
	var arrPositionSize=strPositionSize.split(",");
	var PositionLeft=window.screen.availWidth * .1;
	var PositionTop=window.screen.availHeight * .1;
	var PositionWidth=window.screen.availWidth * .8;
	var PositionHeight=window.screen.availHeight * .8;
	
	if(arrPositionSize.length==4)
	{
		try
		{
			var iPositionLeft=arrPositionSize[0];
			var iPositionTop=arrPositionSize[1];
			var iPositionWidth=parseFloat(arrPositionSize[2]);
			//alert(Math.round(iPositionWidth));
			var iPositionHeight=parseFloat(arrPositionSize[3]);
			switch(arrPositionSize[0])
			{
				case "左":
					PositionLeft=0;
					break;
				case "中":
					PositionLeft=window.screen.availWidth * (100-iPositionWidth)/200;
					PositionLeft=Math.round(PositionLeft);
					//alert(PositionHeight);
					break;
				case "右":
					PositionLeft=window.screen.availWidth * (100-iPositionWidth)/100;
					PositionLeft=Math.round(PositionLeft);
					break;
				default:
					try
					{
						PositionLeft=parseInt(iPositionLeft);
					}
					catch(e){}
					break;
			}
			switch(arrPositionSize[1])
			{
				case "上":
					PositionTop=0;
					break;
				case "中":
					PositionTop=window.screen.availHeight * (100-iPositionHeight)/200;
					PositionTop=Math.round(PositionTop);
					break;
				case "下":
					PositionTop=window.screen.availHeight * (100-iPositionHeight)/100;
					PositionTop=Math.round(PositionTop);
					break;
				default:
					try
					{
						PositionTop=parseInt(iPositionTop);
					}
					catch(e){}
					break;
			}
			
			switch(arrPositionSize[2])
			{
				default:
					try
					{
						PositionWidth=window.screen.availWidth * iPositionWidth/100;
						PositionWidth=Math.round(PositionWidth);
					}
					catch(e){}
					break;
			}
			
			switch(arrPositionSize[2])
			{
				default:
					try
					{
						PositionHeight=window.screen.availHeight * iPositionHeight/100;
						PositionHeight=Math.round(PositionHeight);
					}
					catch(e){}
					break;
			}
		}
		catch(e)
		{
			alert(e);
		}
	}
	//alert(para);
	//openWin('addOpenTable.aspx?btnid=viewinfo&'+para,window.screen.availWidth,window.screen.availHeight);
	openWinPositionSizeF(url	,PositionLeft,PositionTop,PositionWidth,PositionHeight);
}
function print(BarCodeFormNo, controlobjid, cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType,Total,ady) {
    var bar = BarCodeFormNo;
    var controlobject = document.getElementById(controlobjid);
    if (controlobject != null && bar != null && bar.length > 9) {
        try {
            controlobject.PrintBarCode(bar, cname, Gender, Age, AgeUnit, Department, Time, Item, ClientName, SampleType, Total, ady);
        }
        catch (e) {
            return 0;
        }
//        if (ii == 1)
//            return 1;
//        else
        //            return 2;
        return 1;
    }
    else {
        alert('界面出错,controlobject无法取到条码号');
        return 0;
    }

//            public int PrintBarCode(
//            string barCode,
//            string cname,
//            string Gender,string Age,
//            string AgeUnit, string Department,
//            string Time, string DepartmentStyle,
//            string Item,
//            string ClientName, string SampleType,
//            int Total, string ady
//            )
//    var r = controlobject.PortOpen(5, 19200);
//    //alert('open:' + r);
//    if (r == '0') {
//        var rset = controlobject.SetPrintXY(0, 0);
//        //alert('setprintxy:' + rset);
//        var rp = controlobject.SetPaperSize(0, 100, 30, 2);
//        //alert('SetPaperSize:' + rp);
//        var rc = controlobject.PrintChineseStr(65, 2, 0, 2, 2, 0, cname);
//        //alert('PrintChineseStr:' + rc);
//        var rc2 = controlobject.PrintChineseStr(65, 7, 0, 0, 2, 0, Gender + '/' + Age + AgeUnit);
//        var rc3 = controlobject.PrintChineseStr(65, 11, 0, 0, 2, 0, Department);
//        var rc4 = controlobject.PrintChineseStr(49, 16, 0, 0, 2, 0, Time);
//        var rc5 = controlobject.PrintChineseStr(25, 20, 0, 2, 2, 0, Item);
//        var rc6 = controlobject.PrintChineseStr(25, 25, 0, 2, 2, 0, ClientName);

//        var rb = controlobject.PrintBarcode(25, 2, 0, 2, 3, 6, 100, 1, bar);
//        //alert('PrintBarcode:' + rb);
//        var rp = controlobject.Print(1);
//    }
//    var rclear = controlobject.SetClear();
//    //关闭端口
//    var rclose = controlobject.PortClose();
    //alert(rclear + "," + rclose);
}