	
	    
	  //销售审核应用  
	 var rlSalesCList=me.getComponent('RLSalesCList');
	 rlSalesCList.on({
	    afterOpenEditWin:function(formPanel){ 
            //列表的选中行记录id
	        var dataId=formPanel.dataId;
	        formPanel.type='edit';
            //给弹出表单添加自定义属性
            formPanel.listGrid=rlSalesCList;
            formPanel.isEdit(formPanel.dataId);
            //姓名   
            var employeeName=Ext.util.Cookies.get('000201');
            var operaterNameCom = formPanel.getComponent('HITACHIBusinessProject_AuditMan');
            if(operaterNameCom){
               operaterNameCom.setValue(employeeName);
            } 
            //上报时间
            var auditTimeCom = formPanel.getComponent('HITACHIBusinessProject_AuditTime');
            if(auditTimeCom){
               var val = new Date();
               auditTimeCom.setValue(val);
            }
	        //审核上报处理
	        //var btnSHTGCom=formPanel.getComponent('SHTG');
	        //btnSHTGCom.on({
	            //click:function(com,e,eOpts){
               formPanel.on({
               default1buttonClick:function(com,e,eOpts){    
               var selectURL=getRootPath()+'/HITACHIService.svc/ST_UDTO_SearchBBusinessStatusByHQL?isPlanish=true&fields=BBusinessStatus_Name,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp&page=1&start=0&limit=10000';
               var selectField='&where=bbusinessstatus.Name like %27待批复%27';
               selectURL=selectURL+selectField;
	            Ext.Ajax.defaultPostHeader = 'application/json';
			    Ext.Ajax.request({
			        url:selectURL,
			        method:'GET',
			        success:function(response,opts){
                        var result = Ext.JSON.decode(response.responseText);
                        if(result.success){
                            if(result.ResultDataValue && result.ResultDataValue !=  ''){
                               var r = Ext.JSON.decode(result.ResultDataValue);
                               var bbsDataTimeStamp = ''+r.list[0]['BBusinessStatus_DataTimeStamp'];
                               var bbStatusId = r.list[0]['BBusinessStatus_Id'];
                               var url=getRootPath()+'/HITACHIService.svc/ST_UDTO_UpdateHITACHIBusinessProjectByField';
			                   //审核人
			                   var auditManCom = formPanel.getComponent('HITACHIBusinessProject_AuditMan');
			                   
			                   var dataTimeStampCom =formPanel.getComponent('HITACHIBusinessProject_DataTimeStamp');
                               var dataTimeStamp=dataTimeStampCom.getValue();
                               var dataTimeStampArr=[];
                                if(dataTimeStamp&&dataTimeStamp!=undefined){
                                   dataTimeStampArr=dataTimeStamp.split(',');
                                }
                               //审核日期
                               var val = new Date();
                               var myValue=''+Ext.util.Format.date(val,'Y-m-d');
                               var dataTimeVaue=convertJSONDateToJSDateObject(myValue);
                              
                               var Id=formPanel.dataId;
			                   var objArr=[];
			                    if(bbsDataTimeStamp&&bbsDataTimeStamp!=undefined){
			                       objArr=bbsDataTimeStamp.split(',');
			                    }
			                   var BBusinessStatus={Id:bbStatusId,DataTimeStamp:objArr};
			                   var  newAdd= {
                                        Id:Id,
                                        DataTimeStamp:dataTimeStampArr,
			                            AuditMan:auditManCom.getValue(),
			                            AuditTime:dataTimeVaue,
			                            BBusinessStatus:BBusinessStatus
			                        };
			                    var obj={'entity':newAdd,'fields':'DataTimeStamp,Id,AuditMan,AuditTime,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp'};
			                    var params = Ext.JSON.encode(obj);
                                var callbackUpate = function(responseText) {
	                                var result = Ext.JSON.decode(responseText);
	                                if(result.success){
	                                    formPanel.listGrid.load('');
                                        Ext.Msg.alert('提示','上报成功,等待批复');
                                        formPanel.close();
	                                }else{
                                        Ext.Msg.alert('提示','上报失败，请重新上报');
                                    }
                                };
			                    //util-POST方式与后台交互
                                var defaultPostHeader = 'application/json';
			                    postToServer(url,params,callbackUpate,defaultPostHeader);
                            }
                        }
			        }
			    });
	            }
	        });
	    } 
	 });

