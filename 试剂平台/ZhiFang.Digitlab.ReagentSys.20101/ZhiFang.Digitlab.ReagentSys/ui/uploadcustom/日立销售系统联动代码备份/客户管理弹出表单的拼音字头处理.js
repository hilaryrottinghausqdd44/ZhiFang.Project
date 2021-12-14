  
  //js代码压缩地址http://tool.css-js.com/compressor.html?b
  
    ///列表的新增按钮弹出表单后拼音字头处理 
    //RLClientMList需要修改列表的itemId
    var rlList=me.getComponent('RLClientMList');
     rlList.on({
        afterOpenAddWin:function(formPanel){
            formPanel.type='add';
            formPanel.isAdd();
		    //BCompany_Name需要修改表单里的中文名称
		    var changeName = formPanel.getComponent('BHITACHIClient_Name');
		    if(changeName){
		        changeName.on({
		            change:function(com,newValue,oldValue,e,eOpts){
		                if(formPanel.isLoadingComplete==true&&(formPanel.type=='edit'||formPanel.type=='add')){
		                    if(newValue!=''&&newValue!=null){
                                newValue=newValue.replace(/ /g,'');
                                newValue=newValue.replace(/\-/g,'');
                                newValue=newValue.replace(/\_/g,'');
                                //util中获取拼音字头的公共方法
                                var callback = function(value){
                                //BCompany_PinYinZiTou需要修改为表单里的拼音字头交互字段
                                var obj = {'BHITACHIClient_PinYinZiTou':value};
                                formPanel.getForm().setValues(obj);
                            };
                             getPinYinZiTouFromServer(newValue,callback);   
                            }
		                }else{
		                    formPanel.isLoadingComplete=true;
		                }
		            }
		        });
		    }
          }
     });
    //列表的编辑按钮弹出表单后拼音字头处理 
    rlList.on({
        afterOpenEditWin:function(formPanel){
            formPanel.type='edit';
            formPanel.isEdit(formPanel.dataId);
            //BCompany_Name需要修改表单里的中文名称
            var changeName = formPanel.getComponent('BHITACHIClient_Name');
            if(changeName){
                changeName.on({
                    change:function(com,newValue,oldValue,e,eOpts){
                    if(formPanel.isLoadingComplete==true&&(formPanel.type=='edit'||formPanel.type=='add')){
                            if(newValue!=''&&newValue!=null){
                                newValue=newValue.replace(/ /g,'');
                                newValue=newValue.replace(/\-/g,'');
                                newValue=newValue.replace(/\_/g,'');
                                //util中获取拼音字头的公共方法
                                var callback = function(value){
                                //BCompany_PinYinZiTou需要修改为表单里的拼音字头交互字段
                                var obj = {'BHITACHIClient_PinYinZiTou':value};
                                formPanel.getForm().setValues(obj);
                            };
                                getPinYinZiTouFromServer(newValue,callback);
                            }
                        }else{
                            formPanel.isLoadingComplete=true;
                        }
                    }
                });
            }
          }
     });     
         
         
    