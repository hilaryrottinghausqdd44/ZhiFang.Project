
/***
 * 设置数据过滤条件应用
 */
Ext.ns('Ext.manage');
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.manage.datafilters.setForm', getRootPath() + '/ui/manage/class/datafilters/setForm.js');
Ext.Loader.setPath('Ext.manage.datafilters.roleChooseLists', getRootPath() + '/ui/manage/class/datafilters/roleChooseLists.js');
Ext.define('Ext.manage.datafilters.setDatafiltersApp', {
    extend:'Ext.form.Panel',
    alias:'widget.setDatafiltersApp',
    title:'',
    cls:'bg-white',
    bodyCls:'bg-white',
    setformTitle:'',
    /***
     * 外部传入
     * 行过滤条件角色树
     * @type 
     */
    filtersTree:null,
    /**
     * 数据过滤条件对象的时间戳，用于修改保存时使用
     * @type String
     */
    DataTimeStamp:'',
    DataUpdateTime:'',
    DataAddTime:'',
    /***
     * 行过滤条件是否保存成功
     * @type Boolean
     */
    isRBACRowFilter:true,
    /***
     * 行过滤条件及模块操作关系是否更新成功
     * @type Boolean
     */
    ismoduleOperUpdate:true,
    /***
     * 行过滤条件及角色权限关系是否保存或者更新成功
     * @type Boolean
     */
    isroleRightUpdate:true,
    /***
     * 外部传入
     * 数据过滤条件行记录的Id
     * @type 
     */
    appId:-1,
    /***
     * 外部传入
     * 模块操作的默认数据过滤条件的Id
     * @type 
     */
    defaultRowFilterId:'',
    /***
     * 编辑时已存在的角色列表
     * 需要封装好传入
     * @type 
     */
    roleLists:[],
    /***
     * 外部传入
     * 模块操作id
     * @type String
     */
    moduleOperId:'',
    /***
     * 外部传入
     * 模块操作列表选中行
     * @type String
     */
    moduleOperSelect:'',
    /***
     * 外部传入
     * objectName数据对象
     * @type String
     */
    objectName:'',
	/***
	 * 外部传入
	 * @type String
	 */
    objectCName:'数据对象',
    appType:'add',
    
    /***
     * 行过滤查询服务
     * @type 
     */
    getRBACRowFilterServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_SearchRBACRowFilterByHQL',
    /***
     * 行过滤查询服务字段,需要更新,不完整
     * @type 
     */
    fieldsRBACRowFilter:'RBACRowFilter_Id,RBACRowFilter_DataTimeStamp,RBACRowFilter_LabID,RBACRowFilter_CName,RBACRowFilter_EName,RBACRowFilter_SName,RBACRowFilter_RowFilterCondition,RBACRowFilter_StandCode,RBACRowFilter_DeveCode,RBACRowFilter_PinYinZiTou,RBACRowFilter_Shortcode,RBACRowFilter_Comment,RBACRowFilter_IsUse,RBACRowFilter_DispOrder,RBACRowFilter_DataAddTime,RBACRowFilter_DataUpdateTime',
    /***
     * 行过滤更新服务
     * @type String
     */
    editRBACRowFilterServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_UpdateRBACRowFilterByField',
    //editRBACRowFilterServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_UpdateRBACRowFilter',
    /***
     * 行过滤更新字段
     * @type String
     */
    editRBACRowFilterFields:'CName,RowFilterCondition,RowFilterConstruction,Id',//DataUpdateTime
    /***
     * 行过滤新增服务
     * @type String
     */
    addRBACRowFilterServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_AddRBACRowFilter',
    
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    /***
     * 角色权限关系表数据查询服务
     * 查询角色id,角色名称,角色时间戳
     * 查询数据过滤条件id,数据过滤条件名称,数据过滤条件时间戳
     * @type String
     */
    getRoleRightServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL?isPlanish=true',
    /***
     * 角色权限关系表数据查询服务的查询字段
     * @type 
     */
    fieldsRoleRight:'RBACRoleRight_RBACRowFilter_RowFilterConstruction,RBACRoleRight_Id,RBACRoleRight_DataTimeStamp,RBACRoleRight_RBACRole_Id,RBACRoleRight_RBACRole_DataTimeStamp,RBACRoleRight_RBACRowFilter_Id,RBACRoleRight_RBACRowFilter_DataTimeStamp,RBACRoleRight_RBACRole_CName,RBACRoleRight_RBACRowFilter_CName,RBACRoleRight_RBACModuleOper_Id,RBACRoleRight_RBACModuleOper_DataTimeStamp',
       
    /***
     * 角色权限新增服务
     * @type 
     */
    addRoleRightServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_AddRBACRoleRight',
    /***
     * 角色权限更新服务
     * 更新行过滤条件关系
     * @type 
     */
    editRoleRightServerUrl:getRootPath() +'/RBACService.svc/RBAC_UDTO_UpdateRBACRoleRightByField',
    /***
     * 更新角色权限关系
     * @type String
     */
    editfieldsRoleRight:'Id,RBACRowFilter',
    
    /***
     * 行过滤查询条件的字段
     * @type 
     */
    selectFieldsRowFilter:'RBACRowFilter_RowFilterCondition,RBACRowFilter_DataUpdateTime,RBACRowFilter_RowFilterConstruction,RBACRowFilter_CName,RBACRowFilter_EName,RBACRowFilter_SName,RBACRowFilter_RowFilterCondition,RBACRowFilter_StandCode,RBACRowFilter_Id,RBACRowFilter_LabID,RBACRowFilter_DataAddTime,RBACRowFilter_DataTimeStamp',
    
    /**
     * 初始化
     */
    initComponent:function(){
        
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.ux',getRootPath()+'/ui/extjs/ux');
        Ext.Loader.setPath('Ext.manage.datafilters.ComboBoxTree', getRootPath() + '/ui/manage/class/datafilters/ComboBoxTree.js');
        Ext.Loader.setPath('Ext.manage.datafilters.setForm', getRootPath() + '/ui/manage/class/datafilters/setForm.js');
        Ext.Loader.setPath('Ext.manage.datafilters.roleChooseLists', getRootPath() + '/ui/manage/class/datafilters/roleChooseLists.js');
        
        //边距
        me.bodyPadding = 1;
        //布局方式
        //表单
        var north = me.createNorth();
        //数据过滤行
        var center = me.createCenter();
        //角色选择列表
        var east = me.createEast();
        //功能模块ItemId
        north.itemId = "setForm";
        center.itemId = "datafiltersGrid";
        east.itemId = "roleGrid";
        
        center.autoScroll=true;
        center.fit =true;
        //功能块位置
        center.region = "center";
        
        north.region = "north";
        east.region = "east";
        east.fit =true;
        //功能块大小
        east.width = 210;
        
        //功能块收缩属性
        east.split = true;
        east.collapsible = true;
        //功能块收缩属性
        center.split = true;
        center.collapsible = true;
        //功能块收缩属性
        north.split = true;
        north.collapsible = true;
        
        me.items = [north,center,east];
        me.dockedItems=[{
                xtype:'toolbar',
                dock:'bottom',//bottom
                itemId:'dockedItemsbuttons', 
                items:[
	            {text:'　保存　',xtype:'button',iconCls:'build-button-save',
	            width:80,height:22,
	            itemId:'save',handler:function(button){
	                    me.fireEvent('saveClick');
                        button.setDisabled(true);
	                    me.save(true,button);
	                }
	            },
	            {text:'　返回　',xtype:'button',iconCls:'build-button-refresh',
	                width:80,height:22,
	                itemId:'comeBack',handler:function(button){
	                me.fireEvent('comeBackClick');
	            }
	            }
	           ]
            }];
        me.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.addEvents('saveClick');//
        me.addEvents('updateModuleOperClick');//
        me.addEvents('saveRoleRightClick');//
        me.addEvents('comeBackClick');//
        me.initLink();
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
        if(me.appId!=-1){//编辑修改
            //模块操作行过滤依据对象
            var moduleOperRowFilterBase='';
             //模块操作数据对象选中行记录
	        var selectRecord=me.moduleOperSelect;
	        var moDataTimeStampArr=[];
	        var moduleOperId='';
            //是否有默认的数据权限复选框还原
            var useRowFilter='';
	        if(selectRecord&&selectRecord!=null){
	            moduleOperRowFilterBase=selectRecord.get('RBACModuleOper_BTDAppComponentsOperate_RowFilterBase');
                moduleOperId=selectRecord.get('RBACModuleOper_Id');
	            var moDataTimeStamp=''+selectRecord.get('RBACModuleOper_DataTimeStamp');
	            
	            if (moDataTimeStamp && moDataTimeStamp != undefined) {
	                moDataTimeStampArr = moDataTimeStamp.split(',');
	            }else {
	                Ext.Msg.alert('提示', '模块操作数据对象获取不到');
	                return;
	            }
                
                //是否有默认的数据权限复选框还原
                useRowFilter=''+selectRecord.get('RBACModuleOper_RBACRowFilter_Id');
                
	        }
            
            //还原代码
            var callback=function(responseText){
                var result = Ext.JSON.decode(responseText);
                if(result.success){
                    var ResultDataValue = {count:0,list:[]};
	                if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
	                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
	                }
	                var count = ResultDataValue['count'];
                    var list=ResultDataValue['list'];
                    
                    //数据过滤条件的过滤结构还原
                    var appParams = Ext.JSON.decode(list[0]['RBACRoleRight_RBACRowFilter_RowFilterConstruction']);
                    if(appParams&&appParams!=''){
	                    var designCode=appParams['DesignCode'];
	                    //赋值
	                    me.setSouthRecordByArray(designCode);//数据项列表赋值
                    }
                    
                    var roleGrid=me.getroleGrid();
                    //数据过滤条件时间戳还原
                    me.DataTimeStamp=list[0]['RBACRoleRight_RBACRowFilter_DataTimeStamp'];
                    //数据过滤条件中文名称还原
                    var cname=list[0]['RBACRoleRight_RBACRowFilter_CName'];
                    var setForm=me.getsetForm();
                    var cnameCom=setForm.getrowFilterCName();
                    cnameCom.setValue(cname);
                    
                    //给角色选择列表赋默认行记录值
	                for (var i = 0; i <count; i++) { 
                        var roleId=''+list[i]['RBACRoleRight_RBACRole_Id'];
                        var rowId=''+list[i]['RBACRoleRight_RBACRowFilter_Id'];
                        if (useRowFilter==rowId) {
		                   var setForm=me.getsetForm();
		                   if(setForm){
		                       setForm.setdefaultCondition(true);
		                   }
		                }
                        if(roleId!=''&&roleId!=null){
		                    var obj={
	                            'RBACRoleRight_Id':list[i]['RBACRoleRight_Id'], 
	                            'RBACRoleRight_DataTimeStamp':list[i]['RBACRoleRight_DataTimeStamp'], 
	                            'RBACRole_Id':roleId, 
	                            'RBACRole_CName':list[i]['RBACRoleRight_RBACRole_CName'], 
	                            'RBACRole_DataTimeStamp':list[i]['RBACRoleRight_RBACRole_DataTimeStamp'],
	                            'checkBoxColumn':false //默认为false,不是新增选择角色的数据,保存时只对色权限关系更新
	                        };
	                        roleGrid.store.add(obj);
                        }
	               }
                }
            }
            
            var hqlWhere='rbacroleright.RBACRowFilter.Id='+me.appId;//+" and rbacroleright.RBACRole.Id!=null";
            var url=me.getRoleRightServerUrl+'&fields='+me.fieldsRoleRight+'&where='+hqlWhere;
            //查询数据过滤条件行记录
            getToServer(url,callback);
        }
    },
    /**
     * 打开预定义可选属性设置页面
     * @private
     * @param {} appType
     * @param {} id
     */
    openAppWin:function(){
        var me = this;
        if(me.objectName&&me.objectName!=null&&me.objectName!=""){
	        var title ='';
	        var panel = 'Ext.manage.datafilters.predefinedAttributesTree';
	        var roleLists=[];
	        var moduleOperId='';
	        var maxWidth = document.body.clientWidth*0.46;
	        var maxHeight = document.body.clientHeight*0.96;
	        var win = Ext.create(panel,{
	            title:title,
	            width:maxWidth,
	            height:maxHeight,
	            maxWidth:maxWidth,
	            maxHeight:maxHeight,
	            moduleOperId:me.moduleOperId,
	            /***
				 * 下拉列表框树的所需的数据对象名称
				 * @type String
				 */
				objectName:me.objectName,
				/***
				 * 下拉列表框树的所需的数据对象中文名
				 * @type String
				 */
				objectCName:me.objectCName,
	            layout:'border',
	            modal:true,//模态
	            resizable:false,//可变大小
	            floating:true,//漂浮
	            closable:true,//有关闭按钮
	            draggable:true//可移动
	        }).show();
	        //保存监听
	        win.on({
	            //更新模块操作列表后事件,text为响应信息,arrTreeJson为已经选择的树格式数据
	            okClick:function(but,e,text,arrTreeJson){
	               if(text!=null){
                        var result = Ext.JSON.decode(text);
	                    if (result.success) {
	                        Ext.Msg.alert('提示','预定义可选属性设置成功');
	                    } else {
	                        alertError(result.ErrorInfo);
	                    }
                   }
	            },
	            cancelClick:function(){
	                win.close();
	            },
	            close:function(){
	                
	            }
	        });
        }else{
            Ext.Msg.alert('提示','获取不到模块操作的数据对象');
        }
    },
    initLink:function(panel) {
        var me = this;
        var setForm=me.getsetForm();
        //预定义可选属性
        var predefinedAttributes=setForm.getComponent('btnPredefinedAttributes');
        if(predefinedAttributes){
            predefinedAttributes.on({
                click:function(com, e, eOpts ){
                    Ext.Loader.setConfig({enabled:true});
                    Ext.Loader.setPath('Ext.manage.datafilters.predefinedAttributesTree', getRootPath() + '/ui/manage/class/datafilters/predefinedAttributesTree.js');    
                    me.openAppWin();
                }
            });
        }
        setForm.on({
            //添加行记录
            addRecordClick:function(){
                var setForm=me.getsetForm();
 
                //交互字段值 获得当前选中的提交值
                var submitValue=''+setForm.getinteractionFieldValue();
                if(submitValue==null||submitValue==""){
                    Ext.Msg.alert('提示','请选择属性字段');
                    return ;
                }
                //交互字段值 获得当前选中的显示值
                var displayValue=''+setForm.getDisplayValue();
                
                //运算关系下拉列表值
                var operationValue=setForm.getoperationTypeValue();
                if(operationValue==null||operationValue==""){
                    Ext.Msg.alert('提示','请选择运算关系符');
                    return ;
                }
                var operationNameValue=setForm.getoperationNameValue();
                //值类型值
                var typeValue=setForm.getcolumnTypeListValue();
                if(typeValue==null||typeValue==""){
                    Ext.Msg.alert('提示','请选择值类型');
                    return ;
                }
                //日期选择框值
                var datefieldValue=setForm.getdatefieldComValue();
                
                //宏结果选择框值
                var cbomacrosValue=setForm.getcbomacrosListValue();
                //宏结果选择框二值
                var cbomacrosTwoValue=setForm.getcbomacrosListTwoValue();
                
                //字符型结果录入值
                var txtString=''+setForm.gettxtStringValue();
                
                //布尔勾选录入值
                var booleanComValue=setForm.getbooleanComValue();
                
                //数值型结果录入值
                var txtNumberfieldValue=setForm.gettxtNumberfieldValue();
                
                //关联类型的值选择
                var txtResultHidden=setForm.gettxtResultHiddenValue();
                //数值型结果录入值二
                var txtNumberfieldTwoValue=setForm.gettxtNumberfieldTwoValue();
                //字符型结果录入值二
                var txtStringTwoValue=setForm.gettxtStringTwoValue();
                //日期选择框值
                var datefieldComTwoValue=setForm.getdatefieldComTwoValue();
                
                //对比属性交互字段值 获得当前选中的显示值
                var cTreeDisplayValue=''+setForm.getcTreeDisplayValue();
                //对比属性交互字段值 获得当前选中的提交值
                var contrastTreeValue=''+setForm.getcontrastTreeValue();
                
                var content='';
                var contentTwo='';
                if(typeValue==='string'){//字符型结果录入值
                    if(txtString==null||txtString.toString()==""){
                        Ext.Msg.alert('提示','结果值不能为空');
                        return ;
                    }
                    content=''+txtString;
                    contentTwo=''+txtStringTwoValue;
                    
                }else if(typeValue==='date'){//值类型为日期
                    if(datefieldValue==null||datefieldValue.toString()==""){
	                    Ext.Msg.alert('提示','日期选择值不能为空');
	                    return ;
                    }
                    datefieldValue=''+Ext.util.Format.date(datefieldValue,'Y-m-d');
                    content=''+datefieldValue;
                    if(datefieldComTwoValue!=''){
	                    datefieldComTwoValue=''+Ext.util.Format.date(datefieldComTwoValue,'Y-m-d');
	                    contentTwo=''+datefieldComTwoValue;
                    }
                }else if(typeValue==='macros'){//值类型为宏
                    if(cbomacrosValue==null||cbomacrosValue.toString()==""){
                        Ext.Msg.alert('提示','宏命令不能为空');
                        return ;
                    }
                    if(operationValue=='>= and <='||operationValue=='<= or >='||operationValue=='> and <'||operationValue=='< or >'){
                        if(cbomacrosTwoValue==null||cbomacrosTwoValue.toString()==""){
	                        Ext.Msg.alert('提示','宏命令不能为空');
	                        return ;
                        }
                    }
                    content=''+cbomacrosValue;
                    contentTwo=''+cbomacrosTwoValue;
                }else if(typeValue==='boolean'){//值类型为布尔勾选
                    content=''+booleanComValue;
                    contentTwo='';
                }else if(typeValue==='number'){//数值型结果
                    if(txtNumberfieldValue.toString()==null||txtNumberfieldValue.toString()==""){
                        Ext.Msg.alert('提示','数值不能为空');
                        return ;
                    }
                    content=''+txtNumberfieldValue;
                    contentTwo=''+txtNumberfieldTwoValue;
                    
                }else if(typeValue==='relation'){//关联类型结果
                    if(txtResultHidden==null||txtResultHidden.toString()==""){
                        Ext.Msg.alert('提示','关联对象的结果值不能为空');
                        return ;
                    }
                    content=''+txtResultHidden;
                    contentTwo='';
                }else if(typeValue==='contrast'){//对比属性
                    if(contrastTreeValue==null||contrastTreeValue.toString()==""){
                        Ext.Msg.alert('提示','对比属性的属性二名称不能为空');
                        return ;
                    }
                    content='';
                    contentTwo='';
                }
                
                if((content==null||content.toString()=="")&&(typeValue!='contrast')){
                        Ext.Msg.alert('提示','结果值不能为空');
                        return ;
                    }
                var listGrid=me.getdatafiltersGrid();
                if(submitValue.toString()!=""){
                    submitValue=transformHqlStr(submitValue);
                    //contentTwo='';
                }
                //对比属性二
                if(contrastTreeValue!=''){
                    contrastTreeValue=transformHqlStr(contrastTreeValue);
                     //contentTwo='';
                }
               
                var obj={
	               'InteractionField':submitValue,
                   'CName':displayValue,
                   'InteractionFieldTwo':contrastTreeValue,
                   'CNameTwo':cTreeDisplayValue,
	               'LogicalType':'and',
	               'ColumnTypeList':''+typeValue,
	               'OperationType':''+operationValue,
                   'OperationName':''+operationNameValue,
	               'Content':content,
                   'ContentTwo':''+contentTwo
                };
                listGrid.store.add(obj);
                //清空表单的结果值
                setForm.setFormValue();
            },
            //添加或关系
            addOrClick:function(){
                var listGrid=me.getdatafiltersGrid();
                var count=listGrid.store.getCount( );
                if(count==0){
                    Ext.Msg.alert('提示','或关系不能添加到第一行');
                    return false;
                }else{
	                var obj={
	                   'InteractionField':'',
	                   'CName':'或关系',
	                   'LogicalType':'or',
	                   'ColumnTypeList':'',
	                   'OperationType':'',
                       'OperationName':'或关系',
	                   'Content':'或关系',
	                   'ContentTwo':'或关系'
	                };
	                listGrid.store.add(obj); 
                }
            },
            //增加查看全部数据
            addAllClick:function(){
                
                var listGrid=me.getdatafiltersGrid();
                var count=listGrid.store.getCount( );
                if(count>0){
                    Ext.Msg.alert('提示','查看全部数据只能独立添加');
                    return false;
                }else if(count==0){
                    var setForm=me.getsetForm();
                    var buttonCom=setForm.getComponent('btnAddRecord');
                    var buttonAllCom=setForm.getComponent('btnAddAll');
                    buttonCom.setDisabled(true);
                    buttonAllCom.setDisabled(true);
                    var obj={
                       'InteractionField':'',
                       'CName':'查看全部数据',
                       'LogicalType':'all',
                       'ColumnTypeList':'',
                       'OperationType':'',
                       'OperationName':'',
                       'Content':'1=1',
                       'ContentTwo':''
                    };
                    listGrid.store.add(obj); 
                }
            },
            btnSelect:function(){
                //数据过滤条件
                var hqlWhere='';
                hqlWhere=me.transRecordsHqlStr();
                Ext.Msg.show({ 
					title:"查看数据过滤条件", 
					msg:''+hqlWhere, 
					width:600, 
                    height:380, 
					closable:true, 
					modal:true, 
					//buttons:Ext.Msg.OKCANCEL, 
					icon:Ext.Msg.INFO //QUESTION 
				}); 
                //Ext.Msg.alert('查看','数据过滤条件:'+hqlWhere);
            },
            //分配角色
            assignRolesClick:function(){
                //过滤已经选择的角色
                //只能查询该模块操作下的角色,且该角色没有数据过滤条件关系存在
                var moduleOperId=me.moduleOperSelect.get('RBACModuleOper_Id');
                var obj={moduleOperId:moduleOperId,rowId:null,roleId:null};
                //返回的是某一模块操作的行过滤条件为空且角色不为空的数据
                var lists=me.getRoleRightLists(obj,'moduleOperId');
                var defaultWhere='',roleId='',hqlWhere='',objTwo=null;
                var listsTwo=[];
                if(lists.length>0){
                   var rowId="";var i=0;
                   Ext.Array.each(lists,function(model){
	                    rowId=model['RBACRoleRight_RBACRowFilter_Id'];
	                    if(rowId==null||rowId==""){
		                    roleId=''+model['RBACRoleRight_RBACRole_Id'];
                            if(roleId!=""){
                                if(defaultWhere.indexOf(roleId)==0){//已经存在字符串中
                                
	                            }else{
	                                objTwo={moduleOperId:moduleOperId,rowId:null,roleId:roleId};
	                                   //需要作二次判断,当listsTwo为空时才可以选择该角色
	                                  listsTwo=me.getRoleRightLists(objTwo,'moduleOperIdTwo');
	                                  if(listsTwo.length==0){
	                                      defaultWhere=defaultWhere+ roleId+',';
	                                  }
                                }
                            }
	                    }
	                });
	                defaultWhere=defaultWhere.substring(0,defaultWhere.length-1);
	                
                    if(defaultWhere.length>0){
                        hqlWhere="rbacrole.Id in ("+defaultWhere+")";
                    }
                    
                    if(hqlWhere==""||hqlWhere==null){
                        Ext.Msg.alert('提示','当前模块操作相关的角色已经分配了数据过滤条件<br/>或者当前模块操作没有分配角色<br/>请给模块分配角色后再选择角色');
                        return;
                    }else{
                        //已经选择的角色不能再选择
	                    var tempStr=me.getRoleGridIdList();
	                    if(tempStr.length>0){
	                        if(hqlWhere.length>0){
	                            hqlWhere=hqlWhere+(' and rbacrole.Id not in ('+tempStr+')');
	                        }else{
	                           hqlWhere='rbacrole.Id not in ('+tempStr+')';
	                        }
	                    }else{
	                        tempStr='';
	                    }
                        me.openAppShowWin(hqlWhere);
                    }
	                
	            }else{
                    Ext.Msg.alert('提示','当前模块操作相关的角色已经分配了数据过滤条件<br/>或者当前模块操作没有分配角色<br/>请给模块分配角色后再选择角色');
                    return;
                }
            }
        });
 
    },
    /**
     * 将JSON对象转化成字符串
     * @private
     * @param {} obj
     * @return {}
     */
    JsonToStr:function(obj){
        var str = Ext.JSON.encode(obj);
        str = str.replace(/\\/g,"\\\\");
        str = str.replace(/\"/g,"\\\"");
        return str;
    },
    /***
     * 数据过滤行列表的HQLwhere串处理
     * @return {String}
     */
    transRecordsHqlStr:function(){
        var me=this;
        var gridCom=me.getdatafiltersGrid();
        var store = gridCom.store;
        var hqlAllStr='';
        var hqlPartStr='';
        //判断or关系是否同时两行是重复的
        var isOrCount=0;
        var orStr='';
        var counts=0;
        var records=[];
        records=me.getdatafiltersGridRecords();
        if(records.length==0){
            return '';
        }else if(records.length>0){

        var tempRecord=records[records.length-1];
        if(tempRecord.get('LogicalType')=='or'){
            Ext.Msg.alert('提示','数据过滤条件最后的行记录不能为或关系,请先删除再操作!');
            return '';
        }else{
	        store.each(function(record){
	            //逻辑运算符 and or(如果是or关系,需要将后面的用小括号括起来)
	            var lType=''+record.get('LogicalType');
	            counts=counts+1;
		         //交互字段
		        var fiedValue=''+record.get('InteractionField');
                //交互字段二(对比属性)
                var fiedTwoValue=''+record.get('InteractionFieldTwo');
		        //值类型
		        var ctype=''+record.get('ColumnTypeList');
		        //关系运算符
		        var otype=''+record.get('OperationType');
		        //结果值
		        var content=''+record.get('Content');
	            //结果值二
	            var contentTwo=''+record.get('ContentTwo');
                //逻辑关系为all时,行数据过滤条件为1=1
                if(lType=='all'){
                    hqlAllStr=' 1=1 ';
                    return hqlAllStr;
                }
	            //或关系(or)的处理
	           if(counts==1&&lType=='or'){
	           //第一行为or关系时
	            isOrCount=0;
	            orStr='';
	           }else if(lType=='or'&&hqlAllStr.length>0){
	                isOrCount=isOrCount+1;
	                if(isOrCount==1){
		                //前部分先括起来
	                    if(counts==store.getCount()){//最后一行为or关系
	                        hqlAllStr='('+hqlAllStr+')'+' ';
	                    }else{
	                        var strTemp=''+hqlAllStr.substring(hqlAllStr.length-3);
					        if(strTemp=='or'){
	                            //如果该where串后面的字符已经是or,不作处理,防止后面的几行都是or关系
					        }else{
			                   hqlAllStr='('+hqlAllStr+')'+' or ';
	                        }
		                    orStr='or';
	                    }
	                }else{
	                    //如果同时几行的逻辑关系都是or时,不作处理,isOrCount归零
	                    isOrCount=0;
	                }
	           }else if(lType=='and'){
		           //与关系(and)的处理
	            
	                hqlPartStr='';
	                //如果hqlAllStr最后的逻辑运算符已经是or,本次的行记录逻辑运算符为空,以防出现or  and同时存在
	                if(orStr=='or'){
	                    lType='';
	                }
	                switch(ctype)
					{
					   case 'relation'://关联 (in或者 not in)
	                        hqlPartStr=' '+fiedValue+' '+otype+' ('+content+')';
					    break;
					   case 'boolean'://布尔勾选(=)
					        hqlPartStr=' '+fiedValue+' '+otype+content;
					    break;
		               case 'number'://数字
	                   
		                    if(otype=='='||otype=='<>'||otype=='>'||otype=='<'||otype=='<='||otype=='>='){
	                            hqlPartStr=' '+fiedValue+otype+content;
	                        }else if(otype=='>= and <='){//区间(包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'>='+content+' and '+fiedValue+'<='+contentTwo+')';
	                        }else if(otype=='<= or >='){//区间外(包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'<='+content+' or '+fiedValue+'>='+contentTwo+')';
	                        }else if(otype=='> and <'){//区间(不包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'>'+content+' and '+fiedValue+'<'+contentTwo+')';
	                        }else if(otype=='< or >'){//区间外(不包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'<'+content+' or '+fiedValue+'>'+contentTwo+')';
	                        }
		                break;
		               case 'macros'://宏命令
		                      
                            if(otype=='='||otype=='<>'||otype=='>'||otype=='<'||otype=='<='||otype=='>='){
                                hqlPartStr=' '+fiedValue+' '+otype+content;
                            }else if(otype=='>= and <='){//区间(包含边界)(输入2项)
                                hqlPartStr=' '+'('+fiedValue+'>='+content+' and '+fiedValue+'<='+contentTwo+')';
                            }else if(otype=='<= or >='){//区间外(包含边界)(输入2项)
                                hqlPartStr=' '+'('+fiedValue+'<='+content+' or '+fiedValue+'>='+contentTwo+')';
                            }else if(otype=='> and <'){//区间(不包含边界)(输入2项)
                                hqlPartStr=' '+'('+fiedValue+'>'+content+' and '+fiedValue+'<'+contentTwo+')';
                            }else if(otype=='< or >'){//区间外(不包含边界)(输入2项)
                                hqlPartStr=' '+'('+fiedValue+'<'+content+' or '+fiedValue+'>'+contentTwo+')';
                            }
		               break;
		               case 'string':
		                   if(otype=='='||otype=='<>'){
	                            hqlPartStr=' '+fiedValue+otype+"'"+content+"'";
	                        }else if(otype=='like A%'){//开始于(A*)
	                            hqlPartStr=' '+fiedValue+" like '"+content+"%'";
	                        }else if(otype=='like %A'){//结束于(*A)
	                            hqlPartStr=' '+fiedValue+" like %'"+content+"'";
	                        }else if(otype=='like A%B'){//字符之间(A*B) (输入2项)
	                           hqlPartStr=' '+fiedValue+" like "+"'"+content+'%'+contentTwo+"'";
	                        }else if(otype=='= or = or ='){//等于其中一项(输入多项)
	                           var arr=content.split(',');
                               if(arr&&arr.length>0){
                                var str='';
                              
                               Ext.each(arr,function(item,index,itemAll){
	                                if(index<(arr.length-1)){
							            str=str+(fiedValue+"="+"'"+item+"'"+" or ");
	                                }else if(index==(arr.length-1)){
                                        str=str+(fiedValue+"="+"'"+item+"'");
                                    }
						        });
                                hqlPartStr=' ('+str+") ";
                               }
	                        }else if(otype=='not (= or = or =)'){//不等于任何一项(输入多项)
	                           var arr=content.split(',');
                               if(arr&&arr.length>0){
                                var str='';
                              
                               Ext.each(arr,function(item,index,itemAll){
                                    if(index<(arr.length-1)){
                                        str=str+(fiedValue+"!="+"'"+item+"'"+" and ");
                                    }else if(index==(arr.length-1)){
                                        str=str+(fiedValue+"!="+"'"+item+"'");
                                    }
                                });
                                hqlPartStr=' ('+str+") ";
                               }
	                        }else if(otype=='like %A% or like %B%'){//包含(可输入多个)
	                           var arr=content.split(',');
                               if(arr&&arr.length>0){
                                var str='';
                              
                               Ext.each(arr,function(item,index,itemAll){
                                    if(index<(arr.length-1)){
                                        str=str+(fiedValue+" like "+"'%"+item+"%'"+" or ");
                                    }else if(index==(arr.length-1)){
                                        str=str+(fiedValue+" like "+"'%"+item+"%'");
                                    }
                                });
                                hqlPartStr=' ('+str+") ";
                               }
	                        }else if(otype=='not (like %A% or like %B%)'){//不包含(可输入多个)
	                           var arr=content.split(',');
                               if(arr&&arr.length>0){
                                var str='';
                              
                               Ext.each(arr,function(item,index,itemAll){
                                    if(index<(arr.length-1)){
                                        //2014-07-23 bug修改
                                        str=str+(fiedValue+" not like "+"'%"+item+"%'"+" and ");
                                    }else if(index==(arr.length-1)){
                                        str=str+(fiedValue+" not like "+"'%"+item+"%'");
                                    }
                                });
                                hqlPartStr=' ('+str+") ";
                               }
	                        }
		               break;
	                   case 'date'://日期类型
	                        if(otype=='='||otype=='<>'||otype=='>'||otype=='<'||otype=='<='||otype=='>='){
	                            hqlPartStr=' '+fiedValue+otype+"'"+content+"'";
	                        }else if(otype=='>= and <='){//区间(包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'>='+"'"+content+"'"+' and '+fiedValue+'<='+"'"+contentTwo+"'"+')';
	                        }else if(otype=='<= or >='){//区间外(包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'<='+"'"+content+"'"+' or '+fiedValue+'>='+"'"+contentTwo+"'"+')';
	                        }else if(otype=='> and <'){//区间(不包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'>'+"'"+content+"'"+' and '+fiedValue+'<'+"'"+contentTwo+"'"+')';
	                        }else if(otype=='< or >'){//区间外(不包含边界)(输入2项)
	                            hqlPartStr=' '+'('+fiedValue+'<'+"'"+content+"'"+' or '+fiedValue+'>'+"'"+contentTwo+"'"+')';
	                        }
	                   break;
                       case 'contrast'://对比属性
                            if(otype=='='||otype=='<>'||otype=='>'||otype=='>='||otype=='<'||otype=='<='){
                                hqlPartStr=' '+fiedValue+otype+fiedTwoValue;
                            }
                       break;
					   default:
	                   hqlPartStr='';
					   break;
					}
	                if(counts==1){
	                    hqlAllStr=hqlAllStr+''+hqlPartStr+' ';//+' 1=1 '+lType+' '
	                }else{
	                    if(orStr=='or'){
	                        hqlAllStr=hqlAllStr+' '+lType+' ('+hqlPartStr+') ';
	                    }else{
	                        hqlAllStr=hqlAllStr+' '+lType+' '+hqlPartStr+' ';
	                    }
	                }
	                hqlPartStr='';
	                orStr='';
	           }
	        });
	        var strTemp=''+hqlAllStr.substring(0,4);
	        if(strTemp=='and'){
	            //如果where串开头为and,需要截掉开头为and的字符串
	            hqlAllStr=hqlAllStr.substring(4);
	        }
        }      
       }  
        return hqlAllStr; 
    },
    save:function(bo,button){
        var me=this;
        //过滤结构设计代码（还原代码）
        var appParams = me.getAppParams();
        var id = bo ? me.appId : -1;
        
        var setForm=me.getsetForm(); 
        //默认条件复选框值
        var defaultConditionValue=setForm.getdefaultConditionValue();
        
        var cname=''+setForm.getrowFilterCNameValue();
        if(cname==""||cname==null){
            button.setDisabled(false);
            Ext.Msg.alert('提示','名称不能为空!');
            return false;
        }else{
        //模块操作数据对象选中行记录
        var selectRecord=me.moduleOperSelect;
        var moduleOperDataTimeStampArr=[];
        var moduleOperId='';
        if(selectRecord&&selectRecord!=null){
            moduleOperId=selectRecord.get('RBACModuleOper_Id');
	        var moduleOperDataTimeStamp=''+selectRecord.get('RBACModuleOper_DataTimeStamp');
	        
	        if (moduleOperDataTimeStamp && moduleOperDataTimeStamp != '') {
	            moduleOperDataTimeStampArr = moduleOperDataTimeStamp.split(',');
	        }else {
	            Ext.Msg.alert('提示', '不能保存数据,模块操作数据对象获取不到');
	            return;
            }
        }
        //模块操作数据对象
        var RBACModuleOper={"UseRowFilter":''+defaultConditionValue,"Id":moduleOperId,"DataTimeStamp":moduleOperDataTimeStampArr};
        //过滤条件
        var hqlWhere="";
        hqlWhere=""+me.transRecordsHqlStr();
        
        var RBACRowFilter={
            "Id":id,
            "LabID":0,
            "IsUse":true,//是否使用
            "CName":""+cname,
            "RowFilterCondition":""+hqlWhere,//Ext.encode(),//过滤条件
            "RowFilterConstruction":""+me.JsonToStr(appParams)//过滤结构,设计代码
        };
        var callback = function(response){
            var result = Ext.JSON.decode(response);
                //行过滤条件保存(新增或修改)成功
                if (result.success) {
                    me.isRBACRowFilter=true;
                    me.fireEvent('saveClick',me.isRBACRowFilter);
                    //新增成功后的处理
                    if(me.appType==='add'){
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            var appInfo = '';
                            appInfo = Ext.JSON.decode(result.ResultDataValue);
                            me.appId=appInfo.id;    
                        }
                     }
                    
                    //查询保存成功的行过滤数据信息
                    var c = function(ResultDataValue){
		                if(me.appId !=-1){
                        if (ResultDataValue && ResultDataValue != '') {
                            
                            var list=ResultDataValue.list;
                            me.DataTimeStamp=list[0]['RBACRowFilter_DataTimeStamp'];
                            me.DataAddTime=list[0]['RBACRowFilter_DataAddTime'];
                            me.appId=list[0]['RBACRowFilter_Id'];
                            
                            var rowDataTimeStampArr=[];
	                        
                            //新增数据过滤条件成功后的处理
	                        if(me.appType==='add'){
	                            //新增角色权限的数据过滤条件空行的关系(只有模块操作ID,行过滤条件ID,角色ID为空)
                                rowDataTimeStampArr=[];
                                if (me.DataTimeStamp && me.DataTimeStamp != '') {
                                    rowDataTimeStampArr = me.DataTimeStamp.split(',');
                                }
                                var addRBACRowFilter={
                                    "Id":me.appId,
                                    "DataTimeStamp":rowDataTimeStampArr
                                };
                                var objAdd={
                                "Id":-1,
                                'LabID':0,
                                "RBACRowFilter":addRBACRowFilter,
                                'RBACModuleOper':RBACModuleOper
                                };
                                me.addRoleRight(objAdd);
	                         }
		                    //1.执行更新模块操作数据对象的行过滤条件
                            
		                    var RBACRowFilter={};
                                RBACRowFilter={
                                    Id:me.appId
                                };
                            //模块操作更新默认数据过滤条件
                            var objModuleOper={}
                            var type='';
                            if(defaultConditionValue==true){
			                    objModuleOper={
			                        "Id":moduleOperId,
			                        //数据过滤条件子对象
			                        "RBACRowFilter":RBACRowFilter
			                    };
                                type='add';
                                me.updateModuleOper(objModuleOper,type);
                            }else if(defaultConditionValue==false){
                                objModuleOper={
                                    "Id":moduleOperId
                                };
                                type='empty';
                                //只有模块操作的默认数据过滤条件的Id等于应用id才更新清空
                                if(me.defaultRowFilterId==me.appId){
                                  me.updateModuleOper(objModuleOper,type);
                                }
                            }
                            me.fireEvent('updateModuleOperClick',me.ismoduleOperUpdate);
                            
			                //2.执行角色权限关系表的行过滤条件的更新
			                var roleGrid=me.getroleGrid();
			                var store=roleGrid.getStore();
			                if(store&&store.data.length>0){
			                    var id='';
			                    var dataTimeStampRole='';
			                    var dataTimeStampRoleArr=[];
			                    var checkBoxColumn=true;
			                    store.each(function(record) {
			                        id=record.get('RBACRole_Id');
			                        dataTimeStampRole=record.get('RBACRole_DataTimeStamp');
			                        checkBoxColumn=record.get('checkBoxColumn');
			                        if (dataTimeStampRole && dataTimeStampRole != '') {
			                            dataTimeStampRoleArr = dataTimeStampRole.split(',');
			                        }
                                    
			                        if(checkBoxColumn){
                                        //角色选择列表的复选列值为ture,新增角色处理
                                        
                                        //1.在该模块操作下,完全的新增角色权限及行过滤条件关系
                                        if(me.appType==='add'){
	                                        var addRBACRowFilter={
		                                        "Id":me.appId,
	                                            "DataTimeStamp":rowDataTimeStampArr
		                                    };
				                            var objAdd={
				                            "Id":-1,
	                                        'LabID':0,
				                            "RBACRowFilter":addRBACRowFilter,
				                            'RBACRole':{'Id':''+id,'DataTimeStamp':dataTimeStampRoleArr},
	                                        'RBACModuleOper':RBACModuleOper
				                            };
				                            me.addRoleRight(objAdd);
                                        }
                                        
			                        }else if(checkBoxColumn==false){
                                       
                                        //2.在该模块操作下,角色已经存在,更新行过滤条件关系
                                        var roleRightId=record.get('RBACRoleRight_Id');
                                        var type='add';
                                        if(roleRightId!=''){
                                            
                                            var updateRBACRowFilter={
                                                "Id":me.appId
                                            };
                                            var objUpdate={
                                                "Id":roleRightId,
                                                "RBACRowFilter":updateRBACRowFilter
                                            };
                                            me.updateRoleRight(objUpdate,type);
                                        }
                                    }
			                    });
                                
                                me.fireEvent('saveRoleRightClick',me.isroleRightUpdate);
                                //行过滤条件界面处理编辑状态
                                me.appType='edit';
                                button.setDisabled(false);
                                Ext.Msg.alert('提示','数据过滤条件保存成功');
                                
			                }else if(store&&store.data.length==0){
                                //行过滤条件界面处理编辑状态
                                me.appType='edit';
                                button.setDisabled(false);
                                me.isroleRightUpdate=true;
                                me.fireEvent('saveRoleRightClick',true);
                                Ext.Msg.alert('提示','数据过滤条件保存成功');
                            }
                        }
		            }
                  };
                me.appId = (me.appId == -1 ? appId : me.appId);
                //从后台获取新增或者修改行过滤条件成功信息
                me.getAppInfoFromServer(me.appId,c);
                } else {
                    me.isRBACRowFilter=false;
                    var ErrorInfo='';
                    if(me.appId!=-1){
                        ErrorInfo='修改数据过滤条件失败！错误信息:' + result.ErrorInfo + '</b>';
                    }else{
                        ErrorInfo='新增数据过滤条件失败！错误信息:' + result.ErrorInfo + '</b>';
                    }
                    Ext.Msg.alert('提示',ErrorInfo);
                    
                }
        };
        var obj={};
        var params ="";
        //POST方式与后台交互
        var url='';
        if(me.appId != -1){
            url = me.editRBACRowFilterServerUrl;//修改
            obj={'entity':RBACRowFilter,'fields':me.editRBACRowFilterFields};
            params = Ext.JSON.encode(obj);
        }else{
            url = me.addRBACRowFilterServerUrl;//新增
            obj={'entity':RBACRowFilter};
            params = Ext.JSON.encode(obj);
        }
        //保存或者修改行过滤条件的操作
        var defaultPostHeader='application/json';
        postToServer(url,params,callback,defaultPostHeader);
       }   
    },
    /***
     * 更新角色权限数据对象
     */
    updateRoleRight:function(obj,type){
        var me=this;
        var editServerUrl=me.editRoleRightServerUrl;
        //清空传的字段
        var editfields='Id,RBACRowFilter';
        if(type=='add'){
            editfields='Id,RBACRowFilter_Id';
        }
        if(type=='empty'){
            editfields="Id,RBACRowFilter";
        }
        var obj2={'entity':obj,'fields':editfields};
        var params = Ext.JSON.encode(obj2);
        //util-POST方式与后台交互
        var defaultPostHeader='application/json';
        postToServer(editServerUrl,params,null,defaultPostHeader);
    },
    /***
     * 新增角色权限数据对象关系
     */
    addRoleRight:function(obj){
        var me=this;
        var addServerUrl=me.addRoleRightServerUrl;
        var obj2={'entity':obj};
        var params = Ext.JSON.encode(obj2);
        //util-POST方式与后台交互
        var defaultPostHeader='application/json';
        postToServer(addServerUrl,params,null,defaultPostHeader);
    },
    /***
     * 更新模块操作数据对象
     */
    updateModuleOper:function(obj,type){
        var me=this;
        var editServerUrl=getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField';
        
        var editfields='Id,RBACRowFilter';
        if(type=='empty'){
            editfields='Id,RBACRowFilter';
        }else if(type=='add'){
            editfields='Id,RBACRowFilter_Id';
        }
        var obj2={'entity':obj,'fields':editfields};
        var params = Ext.JSON.encode(obj2);
        //util-POST方式与后台交互
        var defaultPostHeader='application/json';
        postToServer(editServerUrl,params,null,defaultPostHeader);
    },
    /***
     * 已经选择的角色
     * @return {}
     */
    getRoleGridIdList:function(){
        var me=this;
        var roleGrid=me.getroleGrid();
        var store=roleGrid.getStore();
        var idStr='';
        if(store&&store.data.length>0){
	        store.each(function(record) {
	            idStr=idStr+ record.get('RBACRole_Id')+',';
	        });
            idStr=idStr.substring(0,idStr.length-1);
        }
        return idStr;
    },
    /**
     * 打开分配角色应用效果窗口
     * @private
     * @param {} title
     * @param {} ClassCode
     * @param {} id
     */
    openAppShowWin:function(hqlWhere){
        var me = this;
        var panel = 'Ext.manage.datafilters.roleChooseLists';
        var maxHeight = document.body.clientHeight*0.78;
        var maxWidth = document.body.clientWidth*0.88;
        var win = Ext.create(panel,{
            id:-1,
            internalWhere:'',
            externalWhere:hqlWhere,
            maxWidth:maxWidth,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            resizable:true,//可变大小
            draggable:true//可移动
        });
        
        if(win.height > maxHeight){
            win.height = maxHeight;
        }
        //解决chrome浏览器的滚动条问题
        var callback = function(){
            win.hide();
            win.show();
        }
        win.show(null,callback);
        win.load(hqlWhere);
        //选择角色后监听
        win.on({
            saveClick:function(){
                //新增角色选择列表行记录
                var roleGrid=me.getroleGrid();
                var arr=win.getAllChangedRecords();
                if(arr&&arr!=null){
		            var result=Ext.isArray(arr);
                    //为数组时才处理
		            if(result){
                        var moduleOperId=me.moduleOperSelect.get('RBACModuleOper_Id');
		                Ext.Array.each(arr,function(model){
		                    var roleId=model['RBACRole_Id'];
                            var obj={moduleOperId:moduleOperId,rowId:null,roleId:roleId};
                            
		                    var record = roleGrid.store.findRecord('RBACRole_Id',roleId);
		                    if (record==null||record==-1){
                                model['checkBoxColumn']=false;//false为该角色已经角色权限关系中
                                //查询该角色是否已经存在角色权限关系表中
                                
                                var lists=me.getRoleRightLists(obj,'select');
                                if(lists.length>0){
                                    var id=lists[0]['RBACRoleRight_Id'];
                                    var dataTimeStamp=lists[0]['RBACRoleRight_DataTimeStamp'];
                                    model['RBACRoleRight_Id']=id;
                                    model['RBACRoleRight_DataTimeStamp']=dataTimeStamp;
                                    model['checkBoxColumn']=false;//false为该角色已经角色权限关系中
                                }
                                roleGrid.store.add(model);
		                    }
		                });
		            }
		        }
                win.close();
                
            }
        });
    },

   /***
    * 数据过滤条件列表
    * @return {}
    */
    createCenterGrid:function() {
        var me=this;''
        var store=Ext.create('Ext.data.Store', {
            fields:['InteractionField','CName','InteractionFieldTwo','CNameTwo','LogicalType','ColumnTypeList','OperationType','OperationName','Content','ContentTwo'],
            data:[],
            autoLoad:true
        });
        var grid=Ext.create('Ext.grid.Panel', {
            store:store,
            itemId:'datafiltersGrid',
            name:'datafiltersGrid',
            autoScroll:true,
            border:false ,
            viewConfig:{
	            forceFit:true,
	            getRowClass : 
	            function(record,rowIndex,rowParams,store){  
	                if(record.data.LogicalType=="or")
                    {                    
                        return 'gridrowRed';//红色  
                    }                
                    else{                    
                        return '';              
                    }
	             }
            },
            columnLines:true,//在行上增加分割线
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
            columns: [
                {text:'逻辑关系',dataIndex:'LogicalType',sortable:false,hidden:true,width:20
                 ,renderer:function(value, cellmeta, record, rowIndex, columnIndex, store){
                        if(value=='or'){
                           cellmeta.css="gridrowRed";// 设置样式
                        }
                    }
                },
                {text:'数据项类型',dataIndex:'ColumnTypeList',sortable:false,width:10,hidden:true},
                { text: '交互字段',hidden:true,sortable:false,dataIndex: 'InteractionField' },
                { text: '属性名称',hidden:false,sortable:false,width:135,dataIndex: 'CName'},
                { text: '关系',hidden:false, sortable:false,width:110,dataIndex: 'OperationName'},
                {text:'输入内容',dataIndex:'Content',width:220,sortable:false
                    ,editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var grid = me.getdatafiltersGrid();
                                var store = grid.store;
                                store.sync();//与后台数据同步
                            }
                        }
                    }
                },
                {text:'运算符',dataIndex:'OperationType',width:10,sortable:false,hidden:true}, 
                
                { text: '交互字段二',hidden:true,sortable:false,dataIndex: 'InteractionFieldTwo' },
                { text: '对比值二',hidden:false,sortable:false,width:115,dataIndex: 'CNameTwo'},
                {text:'输入内容二',dataIndex:'ContentTwo',width:140,sortable:false
                    ,editor:{
                        allowBlank:false,
                        listeners:{
                            change:function(com,newValue){
                                var grid = me.getdatafiltersGrid();
                                var store = grid.store;
                                store.sync();//与后台数据同步
                            }
                        }
                    }
                },
                {
                    xtype:"actioncolumn",
                    sortable:false,
                    text:"操作",
                    width:54,
                    align:"center",
                    itemId:"Action",
                    items:[ {
                        iconCls:"build-button-delete hand",
                        tooltip:"删除",
                        handler:function(grid,rowIndex,colIndex,item,e,record){
                            me.fireEvent('deleteClick');
                            if(record){
                                //行记录被删除
                                var datafiltersGrid=me.getdatafiltersGrid();
                                datafiltersGrid.store.remove(record);
                                if(record.get('LogicalType')=='all'){
	                                var setForm=me.getsetForm();
				                    var buttonCom=setForm.getComponent('btnAddRecord');
                                    var buttonAllCom=setForm.getComponent('btnAddAll');
				                    buttonCom.setDisabled(false);
                                    buttonAllCom.setDisabled(false);
                                }
                            }
                        }  
                   } ]
                 }
            ]
        });
        return grid;
    },
    /***
     * 选择角色列表
     * @return {}
     */
    createRightGrid:function() {
        var me=this;
        var store=Ext.create('Ext.data.Store', {
		    fields:['RBACRoleRight_Id','RBACRoleRight_DataTimeStamp','RBACRole_Id', 'RBACRole_CName', 'RBACRole_DataTimeStamp','checkBoxColumn'],
		    data:[],
            autoLoad:false
		});
        var grid=Ext.create('Ext.grid.Panel', {
		    store:store,
            itemId:'roleGrid',
            name:'roleGrid',
		    columns: [
                //checkBoxColumn,复选框值,为true是新增
                { text: '复选框',hidden:true, sortable:false,dataIndex: 'checkBoxColumn' },
                { text: '角色权限Id',hidden:true,sortable:false, dataIndex: 'RBACRoleRight_Id' },//选择角色时赋值(如果该角色已经角色权限关系表中)
                { text: '角色权限时间戳',hidden:true,sortable:false, dataIndex: 'RBACRoleRight_DataTimeStamp' },//选择角色时赋值
                
		        { text: 'Id',hidden:true,sortable:false, dataIndex: 'RBACRole_Id' },
		        { text: '角色', width:140,sortable:false,dataIndex: 'RBACRole_CName' },
		        { text: '时间戳', dataIndex: 'RBACRole_DataTimeStamp',hidden:true },
                {
			        xtype:"actioncolumn",
                    sortable:false,
			        text:"操作",
			        width:55,
			        align:"center",
			        itemId:"Action",
			        items:[ 
                        {
			            iconCls:"build-button-delete hand",
			            tooltip:"删除",
                        handler:function(grid,rowIndex,colIndex,item,e,record){
                            me.fireEvent('deleteRoleClick');
                            if(record){
                                var checked=record.get('checkBoxColumn');
                                //新增的角色被删除
                                if(checked==false){
                                    //被删除的角色原来已经存在,需要更新后台角色权限的行过滤id为空
                                    if(me.appId!=-1){
                                        var roleId =record.get('RBACRole_Id');
                                        var moduleOperId=me.moduleOperSelect.get('RBACModuleOper_Id');
                                        var obj={moduleOperId:moduleOperId,rowId:me.appId,roleId:roleId};
                                        //返回角色权限id集
                                        var list=me.getRoleRightLists(obj,'edit');
                                        var type='empty';
			                            Ext.Array.each(list,function(model) {
			                                var id=model['RBACRoleRight_Id'];
			                                var obj={
			                                    Id:id
			                                }
			                             me.updateRoleRight(obj,type);
			                            });

                                    }
                                }
                                var roleGrid=me.getroleGrid();
                                roleGrid.store.remove(record);
                            }
                        }
                        }
                       ]
                 }
		    ]
		});
        return grid;
    },
    /***
     * 获取角色权限数据集
     */
    getRoleRightLists:function(obj,type){
        var me=this;
        var roleRightLists=[];
        var hqlWhere='';
        if(type=='delete'){
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+ ' and rbacroleright.RBACRowFilter.Id='+obj.rowId;
        }else if(type=='edit'){
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+ ' and rbacroleright.RBACRowFilter.Id='+obj.rowId;
            hqlWhere=hqlWhere+(' and rbacroleright.RBACRole.Id='+obj.roleId);
        }else if(type=='select'){
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+(' and rbacroleright.RBACRole.Id='+obj.roleId);
        }else if(type=='moduleOperId'){
            //查询该模块操作下的角色数据集,且该角色的数据过滤条件为空
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+(' and rbacroleright.RBACRowFilter.Id is null');
        }else if(type=='moduleOperIdTwo'){
            //二次判断查询该模块操作下的角色数据集,且该角色的数据过滤条件不为空
            hqlWhere='rbacroleright.RBACModuleOper.Id='+obj.moduleOperId+(' and rbacroleright.RBACRowFilter.Id is not null');
            hqlWhere=hqlWhere+" and rbacroleright.RBACRole.Id="+obj.roleId;
        }
        
        var myUrl=me.getRoleRightServerUrl+'&fields='+me.fieldsRoleRight+'&where='+hqlWhere;
        //查询数据过滤条件行记录
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            method:'GET',
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                    roleRightLists=ResultDataValue.list;
                }
                var count = ResultDataValue['count'];
                }else{
                    roleRightLists=[];
                }
            },
            failure : function(response,options){
                 roleRightLists=[];
            }
        });
        return roleRightLists;
    },
    /***
     * 查询行过滤条件数据信息
     * @param {} id
     * @param {} callback
     */
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        
        if(id && id != -1){
            var fields=me.selectFieldsRowFilter;
            var url = me.getRBACRowFilterServerUrl + "?isPlanish=true&where=rbacrowfilter.Id=" + id+'&fields='+fields;
            //回调函数
            var c = function(text){
                var result = Ext.JSON.decode(text);
                if(result.success){
                    var appInfo = "";
                    if(result.ResultDataValue && result.ResultDataValue != ""){
                        result.ResultDataValue =result.ResultDataValue.replace(/\n/g,"\\u000a");
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if(Ext.typeOf(callback) == "function"){
                        if(appInfo == ""){
                            Ext.Msg.alert('提示','没有获取到应用信息！');
                        }else{
                            callback(appInfo);//回调函数
                        }
                    }
                }else{
                    Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
                }
            };
            //util-POST方式交互
            getToServer(url,c);
        }
    },
    /**
     * 表单
     */
    createNorth:function(){
        var me = this;
        var setForm=Ext.create('Ext.manage.datafilters.setForm',{
            header:false,
            border:false,
            name:'setForm',
		    /***
		     * 外部传入
		     * 模块操作id
		     * @type String
		     */
		    moduleOperId:me.moduleOperId,
		    /***
		     * 外部传入
		     * 模块操作列表选中行
		     * @type String
		     */
		    moduleOperSelect:me.moduleOperSelect,
		    /***
		     * 外部传入
		     * objectName数据对象
		     * @type String
		     */
		    objectName:me.objectName,
		    /***
		     * 外部传入
		     * @type String
		     */
		    objectCName:me.objectCName,
            isShowPredefinedAttributes:me.isShowPredefinedAttributes,
            itemId:'setForm',
            title:''
        });
        var com = {
            xtype:'form',
            header:true,
            border:false,
            moduleOperId:me.moduleOperId,
            moduleOperSelect:me.moduleOperSelect,
            objectName:me.objectName,
            objectCName:me.objectCName,
            title:''+me.setformTitle,
            isShowPredefinedAttributes:me.isShowPredefinedAttributes,
            name:'setForm',
            itemId:'setForm',
            autoScroll:true,
            items:[setForm]
        };
        return com;
    },
    isShowPredefinedAttributes:false,
    /***
     * 数据条件
     * @return {}
     */
    createCenter:function(){
        var me = this;
        
        var item={
            xtype:me.createCenterGrid(),
            header:false,
            name:'datafiltersGrid',
            itemId:'datafiltersGrid',
            title:'',
            autoScroll:true,
            fit:true,
            border:false   
        };
        var com = {
            xtype:'form',
            title:'',
            header:false,
            name:'datafiltersGrid',
            itemId:'datafiltersGrid',
            autoScroll:true,
            border:true,
            items:[item]
        };
        return com;
    },
    createEast:function(){
        var me = this;
        
        var item={
            xtype:me.createRightGrid(),
            name:'roleGrid',
            itemId:'roleGrid',
            header:true,
            title:'',
            border:true
        };
        var com = {
            xtype:'form',
            title:'角色选择',
            itemId:'roleGrid',
            name:'roleGrid',
            autoScroll:true,
            items:[item]
        };
        return com;
    },
    /**
     * 给组件记录列表赋值
     * @private
     * @param {} array
     */
    setSouthRecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addSouthValueByRecord(rec);//添加组件记录
        });
    },
    /**
     * 添加组件属性记录
     * @private
     * @param {} record
     */
    addSouthValueByRecord:function(record){
        var me = this;
        var list = me.getdatafiltersGrid();//列属性列表
        var store = list.store;
        store.add(record);
    },
    /**
     * 给组件记录列表赋值
     * @private
     * @param {} array
     */
    setRoleRecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addRoleValueByRecord(rec);//添加组件记录
        });
    },
    /**
     * 添加组件属性记录
     * @private
     * @param {} record
     */
    addRoleValueByRecord:function(record){
        var me = this;
        var list = me.getroleGrid();//角色选择列表
        var store = list.store;
        store.add(record);
    },
    /***
     * 过滤结构设计代码
     * @param {} bo
     */
    getAppParams:function(bo){
        var me=this;
        var DesignCode = me.getRocordInfoArray();
        var roleLists = me.getRoleListsArray();
        var appParams = {
            DesignCode:DesignCode,
            roleLists:roleLists
        };
        return appParams;
    },
    /**
     * 获取所有组件属性信息（简单对象数组）
     * @private
     * @return {}
     */
    getRoleListsArray:function(){
        var me = this;
        var records = me.getRoleGridRecords();
        var fields = me.getRoleStoreFields();
        
        var arr = [];
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                obj[field.name] = record.get(field.name);
            });
            return obj;
        };
        //组装简单对象数组
        Ext.Array.each(records,function(record){
            var obj = getObjByRecord(record);
            arr.push(obj);
        });
        
        return arr;
    },
    /**
     * 获取所有组件属性信息（简单对象数组）
     * @private
     * @return {}
     */
    getRocordInfoArray:function(){
        var me = this;
        var records = me.getdatafiltersGridRecords();
        var fields = me.getDatafiltersStoreFields();
        
        var arr = [];
        //model转化成简单对象
        var getObjByRecord = function(record){
            var obj = {};
            Ext.Array.each(fields,function(field){
                obj[field.name] = record.get(field.name);
            });
            return obj;
        };
        //组装简单对象数组
        Ext.Array.each(records,function(record){
            var obj = getObjByRecord(record);
            arr.push(obj);
        });
        
        return arr;
    },
    /**
     * 获取组件属性列表Fields
     * @private
     * @return {}
     */
    getRoleStoreFields:function(){
        var me = this;
        var fields = [
            {name:'RBACRoleRight_Id',type:'string'},//
            {name:'RBACRoleRight_DataTimeStamp',type:'string'},//
            {name:'RBACRole_Id',type:'string'},//交互字段
            {name:'RBACRole_CName',type:'string'},//
            {name:'RBACRole_DataTimeStamp',type:'string'},//
            {name:'checkBoxColumn',type:'bool'}
        ];
        return fields;
    },
    /**
     * 获取组件属性列表Fields
     * @private
     * @return {}
     */
    getDatafiltersStoreFields:function(){
        var me = this;
        var fields = [
            {name:'InteractionField',type:'string'},//交互字段
            {name:'InteractionFieldTwo',type:'string'},//交互字段
            {name:'OperationName',type:'string'},//
            {name:'CName',type:'string'},//中文名称
            {name:'CNameTwo',type:'string'},//中文名称
            {name:'LogicalType',type:'string'},//
            {name:'ColumnTypeList',type:'string'},//
            {name:'OperationType',type:'string'},//
            {name:'Content',type:'string'},
            {name:'ContentTwo',type:'string'}
        ];
        return fields;
    },
    /**
     * 获取所有组件属性信息
     * @private
     * @return {}
     */
    getdatafiltersGridRecords:function(){
        var me = this;
        var south = me.getdatafiltersGrid();
        var store = south.store;
        var records = [];
        
        store.each(function(record){
            records.push(record);
        });
        
        return records;
    },
    /**
     * 获取所有组件属性信息
     * @private
     * @return {}
     */
    getRoleGridRecords:function(){
        var me = this;
        var south = me.getroleGrid();
        var store = south.store;
        var records = [];
        store.each(function(record){
            var obj=record
            //obj.checkBoxColumn=false;
            records.push(obj);
        });
        
        return records;
    },
    /***
     * 表单
     * @return {}
     */
    getsetForm:function(){
        var me=this;
        var setForm=me.getComponent('setForm');
        var com=setForm.getComponent('setForm');
        return com;
    },
    /***
     * 数据过滤列表
     * @return {}
     */
    getdatafiltersGrid:function(){
        var me=this;
        var com=null;
        com=me.getComponent('datafiltersGrid').getComponent('datafiltersGrid');
        return com;
    },
    /***
     * 角色列表
     * @return {}
     */
    getroleGrid:function(){
        var me=this;
        var com=null;
        com=me.getComponent('roleGrid').getComponent('roleGrid');
        return com;
    }
    
});