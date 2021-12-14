  
  //js代码压缩地址http://tool.css-js.com/compressor.html?b
  
    //拼音字头在复杂代码里处理
    
    
    //RLDistributorMForm需要修改右表单itemId
    var formPanel=me.getComponent('RLDistributorMForm');
    //BCompany_Name需要修改右表单的中文名称
    var changeName = formPanel.getComponent('BCompany_Name');
    if(changeName){
        changeName.on({
            change:function(com,newValue,oldValue,e,eOpts){
                if(formPanel.isLoadingComplete==true&&(formPanel.type=='edit'||formPanel.type=='add')){
                    if(newValue!=''&&newValue!=null){
	                    newValue=newValue.replace(/ /g,'');
	                    newValue=newValue.replace(/-/g,'');
	                    newValue=newValue.replace(/\_/g,'');
                        //util中获取拼音字头的公共方法
                        var callback = function(value){
                        //BCompany_PinYinZiTou需要修改表单里的拼音字头交互字段
                        var obj = {'BCompany_PinYinZiTou':value};
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
    
    