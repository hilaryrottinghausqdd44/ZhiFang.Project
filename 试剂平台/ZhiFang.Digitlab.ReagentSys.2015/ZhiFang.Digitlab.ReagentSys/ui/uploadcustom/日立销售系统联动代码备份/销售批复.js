	
	    
	  //销售批复应用  
	 var rlSalesCList=me.getComponent('RLSalesAList');
     rlSalesCList.on({
        afterOpenShowWin:function(formPanel){ 
            var dataId=formPanel.dataId;//列表的选中行记录id
            formPanel.type='show';
            formPanel.isShow(formPanel.dataId);
        }
     });
	 rlSalesCList.on({
	    afterOpenEditWin:function(formPanel){ 
	        var dataId=formPanel.dataId;//列表的选中行记录id
	        formPanel.type='edit';
            formPanel.isEdit(formPanel.dataId);
            formPanel.listGrid=rlSalesCList;//给弹出表单添加自定义属性
            //批复人姓名   
            var employeeName=Ext.util.Cookies.get('000201');
            var operaterNameCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalManName');
            if(operaterNameCom){
               operaterNameCom.setValue(employeeName);
            } 
            //批复时间
            var auditTimeCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalManTime');
            if(auditTimeCom){
               var val = new Date();
               auditTimeCom.setValue(val);
            }
	        //批复处理(自定义一按钮)
	        //var btnSHTGCom=formPanel.getComponent('PIFUANNIU');
	        //btnSHTGCom.on({
             formPanel.on({
	            default1buttonClick:function(com,e,eOpts){
	            //单选组 批复内容 
	            //[['1','已登记，请继续跟踪'] 登记有效
                //['2','此客户已有公司登记，贵公司不能再登记'] 被拒绝
                //['3','填写内容不全，请重新申请']] 信息不全
	            var approvalContentCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalContent');
                var checked=approvalContentCom.getChecked();
                if(checked.length==0){
                    Ext.Msg.alert('提示','请选择答复内容项');
                    return ;
                }else{
	                var approvalContent='';
	                var inputValue='';
	                var hqlWhere='';
	                Ext.Array.each(checked, function(model) {
	                    approvalContent=model.boxLabel;
	                    inputValue=model.inputValue;
	                });
	                if(inputValue=='1'){
	                    hqlWhere='%27登记有效%25%27';
	                }else if(inputValue=='2'){
	                    hqlWhere='%27被拒绝%25%27';
	                }else if(inputValue=='3'){
	                    hqlWhere='%27信息不全%25%27';
	                }
	                //批复人
	                var approvalManNameCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalManName');
	                var approvalManNameValue=approvalManNameCom.getValue();
	                //批复时间
	                var approvalManTimeCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalManTime');
	                var approvalManTimeValue=approvalManTimeCom.getValue();
	                //批复说明
	                var approvalInfoCom = formPanel.getComponent('HITACHIBusinessProject_ApprovalInfo');
	                var approvalInfoValue=approvalInfoCom.getValue();
	                
	               var selectURL=getRootPath()+'/HITACHIService.svc/ST_UDTO_SearchBBusinessStatusByHQL?isPlanish=true&fields=BBusinessStatus_Name,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp&page=1&start=0&limit=10000';
	               var selectField='&where=bbusinessstatus.Name like '+hqlWhere;
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
				                  
				                   var dataTimeStampCom =formPanel.getComponent('HITACHIBusinessProject_DataTimeStamp');
	                               var dataTimeStamp=dataTimeStampCom.getValue();
	                               var dataTimeStampArr=[];
	                                if(dataTimeStamp&&dataTimeStamp!=undefined){
	                                   dataTimeStampArr=dataTimeStamp.split(',');
	                                }
	                               //日期
	                               var val = new Date();
	                               var myValue=''+Ext.util.Format.date(approvalManTimeValue,'Y-m-d');
	                               var dataTimeVaue=convertJSONDateToJSDateObject(myValue);
	                              
	                               var Id=formPanel.dataId;
				                   var objArr=[];
				                    if(bbsDataTimeStamp&&bbsDataTimeStamp!=undefined){
				                       objArr=bbsDataTimeStamp.split(',');
				                    }
				                   var BBusinessStatus={Id:bbStatusId,DataTimeStamp:objArr};
				                   var  newAdd= {
	                                        Id:Id,
	                                        ApprovalContent:approvalContent,
	                                        ApprovalInfo:approvalInfoValue,
	                                        DataTimeStamp:dataTimeStampArr,
				                            ApprovalManName:approvalManNameValue,
				                            ApprovalManTime:dataTimeVaue,
				                            BBusinessStatus:BBusinessStatus
				                        };
				                    var obj={'entity':newAdd,'fields':'DataTimeStamp,Id,ApprovalManName,ApprovalManTime,BBusinessStatus_Id,BBusinessStatus_DataTimeStamp'};
	                                //
				                    var params = Ext.JSON.encode(obj);
                                    var callbackUpate = function(responseText) {
	                                    var result = Ext.JSON.decode(responseText);
	                                    if(result.success){
	                                        formPanel.listGrid.load('');
	                                        Ext.Msg.alert('提示','批复完成');
	                                        formPanel.close();
	                                    }else{
	                                        Ext.Msg.alert('提示','批复失败');
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
            }
	        });
           
	    } 
	 });

