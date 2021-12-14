function submit()
{

   var url="MenuManage.aspx?"+"nrequestformno="+document .getElementById ('nrequestformno').value+"&&flag=3&&lock="+document .getElementById ('lock').value;
    XMLHttp.sendReq('get',url,'','');
}


