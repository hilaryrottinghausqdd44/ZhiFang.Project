/**
 * 预授权设置页面
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.config.Form', {
	extend:'Shell.ux.form.Panel',
    formtype:'add',
	title:'预授权设置页面',
	width:665,
	height:400,
	bodyPadding:'20px 10px',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.lts.operate.basic.DateTime'
	],
	//布局方式
	layout: {type: 'table',columns: 2 },
	//每个组件的默认属性
	defaults:{
		width:220,
		labelWidth:80,
		labelAlign:'right'
	},
	//被选小组id
	SectionID:'',
	//被选小组名称
    SectionCName:'',
    //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
    //获取操作授权
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeById?isPlanish=true',
	//获取操作授权对应小组
    selectUrl2:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateASectionByHQL?isPlanish=true',
    //新增操作权限
	addUrl :'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateAuthorize', 
	//修改操作权限
	editUrl :'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisOperateAuthorizeByField', 
	//新增操作授权对应小组
	addSUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateASection',
	//删除操作授权对应小组关系
	delUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DelLisOperateASection',
	defaultOperateTypeValue:'1',
	AuthorizeType:'1',//默认类型
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	    me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		//自定义按钮功能栏
		me.buttonToolbarItems = ['->','accept',{
			text:'关闭',tooltip:'关闭',iconCls:'button-cancel',
			handler:function(){
				me.close();
			}
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		var items = [{
			hasStyle: true,xtype: 'uxSimpleComboBox',itemId: 'LisOperateAuthorize_OperateTypeID',
			fieldLabel: '授权操作类型',name:'LisOperateAuthorize_OperateTypeID',allowBlank: false,emptyText: '必填项'
		},{
	        xtype: 'radiogroup',
	        fieldLabel: '授权类型',
	        columns: 2,name:'LisOperateAuthorize_AuthorizeType',
	        vertical: true,itemId: 'LisOperateAuthorize_AuthorizeType',
	        listeners:{
				change : function(com,newValue,oldValue,eOpts ){
					//周期，隐藏日期时间组件
    		        var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
					if(newValue.AuthorizeType==info.Id){
						me.isShowTime(true);
					}else{
						me.isShowTime(false);
					}
				}
			}
	    },{
			fieldLabel:'授权人',xtype:'uxCheckTrigger',
			name:'LisOperateAuthorize_AuthorizeUser',itemId:'LisOperateAuthorize_AuthorizeUser',
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001001'},emptyText: '必填项',
            allowBlank: false,colspan: 1,
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},
        {xtype:'textfield',itemId:'LisOperateAuthorize_AuthorizeUserID',name:'LisOperateAuthorize_AuthorizeUserID',fieldLabel:'授权人员ID',hidden:true},
        {
			fieldLabel:'被授权人',xtype:'uxCheckTrigger',
			name:'LisOperateAuthorize_OperateUser',itemId:'LisOperateAuthorize_OperateUser',
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001001'},emptyText: '必填项',
            allowBlank: false,colspan: 1,
             value:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},
		{xtype:'textfield',itemId:'LisOperateAuthorize_OperateUserID',name:'LisOperateAuthorize_OperateUserID',fieldLabel:'授权操作类型',hidden:true,value:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)},
		{
	        xtype: 'checkboxgroup',fieldLabel: '周期',columns: 7,width:me.defaults.width*2,
	        vertical: true,colspan:2,itemId:'Weeks',
	        items: [
	            { boxLabel: '周一', name: 'LisOperateAuthorize_Day1', inputValue: '1'},
	            { boxLabel: '周二', name: 'LisOperateAuthorize_Day2', inputValue: '2' },
	            { boxLabel: '周三', name: 'LisOperateAuthorize_Day3', inputValue: '3' },
	            { boxLabel: '周四', name: 'LisOperateAuthorize_Day4', inputValue: '4' },
	            { boxLabel: '周五', name: 'LisOperateAuthorize_Day5', inputValue: '5' },
	            { boxLabel: '周六', name: 'LisOperateAuthorize_Day6', inputValue: '6' },
	            { boxLabel: '周日', name: 'LisOperateAuthorize_Day0', inputValue: '7' }
	        ]
	    },
		{xtype: 'timefield',width:220,fieldLabel:'时间片段',labelAlign: 'right',format: 'H:i:s',name: 'LisOperateAuthorize_BeginTime',itemId: 'LisOperateAuthorize_BeginTime',allowBlank:false,emptyText:'必填项',colspan:1},
		{xtype: 'timefield',width:100,labelAlign: 'right',fieldLabel:'-',labelWidth:10,format: 'H:i:s',name: 'LisOperateAuthorize_EndTime',itemId: 'LisOperateAuthorize_EndTime',labelSeparator: '',allowBlank:false,emptyText:'必填项',colspan:1},
		{
            xtype: 'fieldcontainer',colspan:2,itemId:'TimeRange',fieldLabel:'时间片段',
			layout: {type: 'table',columns: 2 },
			defaults:{width:110,labelWidth:80,labelAlign:'right'},
            items: [
               {xtype: 'datatimefield',allowBlank:false,width:180,colspan:1,fieldLabel: '',name: 'BeginTime',itemId:'BeginTime'},
               {xtype: 'datatimefield',allowBlank:false,width:180,fieldLabel: '-',labelWidth: 5,name: 'EndTime',itemId:'EndTime'}
            ]
       },{
			fieldLabel:'被选小组',xtype:'uxCheckTrigger',
			name:'SectionCName',itemId:'SectionCName',allowBlank: false,emptyText: '必填项',
			className:'Shell.class.lts.section.role.CheckGrid',
            colspan: 2,width:me.defaults.width*2,
			listeners:{
				check:function(p,records){
					var ids = [],idsName=[];
					if(records){
						for(var i=0;i<records.length;i++){
							ids.push(records[i].data.LBRight_LBSection_Id);
							idsName.push(records[i].data.LBRight_LBSection_CName);
						}
					}
					var idsStrName = idsName.join(',') || "";
					var idsStr = ids.join(',') || "";

					p.setValue(idsStrName);
					p.nextNode().setValue(idsStr);
					p.close();
				},

			}
		},{xtype:'textfield',itemId:'SectionID',name:'SectionID',fieldLabel:'被选小组ID',hidden:true },
		  {xtype:'textfield',itemId:'LisOperateAuthorize_Id',name:'LisOperateAuthorize_Id',fieldLabel:'ID',hidden:true },
		  {xtype:'textfield',itemId:'LisOperateAuthorize_IsUse',name:'LisOperateAuthorize_IsUse',fieldLabel:'IsUse',hidden:true }
        ];
		return items;
	},
   
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//临时 取开始时间和结束时间
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
       if(info.Id == data.LisOperateAuthorize_AuthorizeType){//临时
        	me.isShowTime(true);
        	data.LisOperateAuthorize_BeginTime = data.LisOperateAuthorize_BeginTime ? JShell.Date.toString(data.LisOperateAuthorize_BeginTime) : '';
        	data.LisOperateAuthorize_EndTime = data.LisOperateAuthorize_EndTime ? JShell.Date.toString(data.LisOperateAuthorize_EndTime) : '';
        	me.getComponent('TimeRange').getComponent('BeginTime').setValue(data.LisOperateAuthorize_BeginTime);
            me.getComponent('TimeRange').getComponent('EndTime').setValue(data.LisOperateAuthorize_EndTime);
            data.LisOperateAuthorize_BeginTime="";
            data.LisOperateAuthorize_EndTime="";
        }else{ //周期
        	me.getComponent('LisOperateAuthorize_AuthorizeType').setValue({AuthorizeType:data.LisOperateAuthorize_AuthorizeType});
        	var BeginTime = data.LisOperateAuthorize_BeginTime.split(' ');
        	var EndTime = data.LisOperateAuthorize_EndTime.split(' ');
        	data.LisOperateAuthorize_BeginTime = BeginTime[1];
        	data.LisOperateAuthorize_EndTime = EndTime[1];
        }
        me.getComponent('SectionID').setValue(me.SectionID);
        me.getComponent('SectionCName').setValue(me.SectionCName);
		return data;
	},
    //联动监听
	initFilterListeners : function(){
		var me =  this;
		var AuthorizeTypeList = [];
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新刷新');
    			return;
    		}
			var List=JShell.System.ClassDict.AuthorizeType;
			var AuthorizeType = me.getComponent('LisOperateAuthorize_AuthorizeType');
    		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
			for(var i=0;i<List.length;i++){
				var obj = { boxLabel: List[i].Name, name: 'AuthorizeType', inputValue: List[i].Id};
				if(me.AuthorizeType == List[i].Id)obj.checked=true;
				AuthorizeTypeList.push(obj);
			}
			AuthorizeType.add(AuthorizeTypeList);
	    });
	    JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeOperateType',function(){
			if(!JShell.System.ClassDict.AuthorizeOperateType){
    			JShell.Msg.error('未获取到授权操作类型,请重新刷新');
    			return;
    		}
			var List=JShell.System.ClassDict.AuthorizeOperateType;
			var OperateType = me.getComponent('LisOperateAuthorize_OperateTypeID');
    		if(OperateType.store.data.items.length==0){
			     OperateType.loadData(me.getListData(List));
			     OperateType.setValue(me.defaultOperateTypeValue);
			}
	    });
	},
	//确定保存
	onAcceptClick : function(){
		var me = this,
			values = me.getForm().getValues();	

		if(!me.getForm().isValid()) return;
		//时间大小校验
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
        if(info.Id == values.AuthorizeType){//临时
        	var BeginTime =  me.getComponent('TimeRange').getComponent('BeginTime').getValue();
            var EndTime =  me.getComponent('TimeRange').getComponent('EndTime').getValue();
            if(BeginTime>=EndTime){
            	JShell.Msg.error('开始时间不能小于结束时间!');
            	return;
            }
        }
		if(!me.BUTTON_CAN_CLICK)return;	
		
		if(values.LisOperateAuthorize_Id){//修改
			me.updateSave(values);
		}else{//新增
			var ids = values.SectionID.split(',');
			//新增操作授权
			me.addAuthorize(function(id){
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = ids.length;
	            for(var i=0;i<ids.length;i++){//新增操作授权对应小组
	            	me.addASection(id,ids[i]);
	            }
            });
		}
    },
    	//实体
	getAddParams : function(){
		var me = this,
			values = me.getForm().getValues();
		var  BeginTime ="",EndTime="";
		var OperateType = me.getComponent('LisOperateAuthorize_OperateTypeID');
		var entity = {
			IsOnlyUseTime:1,
			OperateType:OperateType.getRawValue(),
			OperateTypeID:values.LisOperateAuthorize_OperateTypeID,
			AuthorizeUserID:values.LisOperateAuthorize_AuthorizeUserID,
			AuthorizeUser:values.LisOperateAuthorize_AuthorizeUser,
			OperateUserID:values.LisOperateAuthorize_OperateUserID,
			OperateUser:values.LisOperateAuthorize_OperateUser,
			IsUse:1,
			Day1:values.LisOperateAuthorize_Day1 ? 1 : 0,
			Day2:values.LisOperateAuthorize_Day2 ? 1 : 0,
			Day3:values.LisOperateAuthorize_Day3 ? 1 : 0,
			Day4:values.LisOperateAuthorize_Day4 ? 1 : 0,
			Day5:values.LisOperateAuthorize_Day5 ? 1 : 0,
			Day6:values.LisOperateAuthorize_Day6 ? 1 : 0,
			Day0:values.LisOperateAuthorize_Day0 ? 1 : 0,
			AuthorizeType:values.AuthorizeType
		};
		
		//临时 取开始时间和结束时间
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
        if(info.Id == values.AuthorizeType){//临时
        	BeginTime =  me.getComponent('TimeRange').getComponent('BeginTime').getValue();
            EndTime =  me.getComponent('TimeRange').getComponent('EndTime').getValue();
        }else{ //周期
        	var BeginStr = '1900-01-01';
            BeginTime = BeginStr+' '+values.LisOperateAuthorize_BeginTime;
        	EndTime = BeginStr+' '+values.LisOperateAuthorize_EndTime;
        }
        entity.BeginTime=JShell.Date.toServerDate(BeginTime);
        entity.EndTime=JShell.Date.toServerDate(EndTime);
		return {entity:entity};
	},
		/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			if(arr[1])fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.LisOperateAuthorize_Id;
		return entity;
	},
	//修改保存
	updateSave : function(values){
		var me = this;
		//校验被选小组是否已存在数据库中
		me.getOperateASection(values.LisOperateAuthorize_Id,function(list){
			var ids = values.SectionID.split(','); //已选择的小组
			var ids1 = [];//与操作权限有关的小组id
			for(var i=0;i<list.length;i++){ //与操作权限有关的小组
				ids1.push(list[i].LisOperateASection_LBSection_Id);
			}
			//需要从数据库中删除的小组
			var delArr = me.arrayRemove(ids1,ids);
			//需要添加关系的小组
			var addArr = me.arrayRemove(ids,ids1); 
			
			if(delArr.length>0){
				me.delErrorCount = 0;
				me.delCount = 0;
				me.delLength = delArr.length;
	
				me.showMask(me.delText); //显示遮罩层
				for (var i in delArr) {
					var id = delArr[i];
					var ASectionID="";//关系id
					for(var j=0;j<list.length;j++){
						if(list[j].LisOperateASection_LBSection_Id==id){
							ASectionID = list[j].LisOperateASection_Id;
							list.splice(j,1);
							break;
						}
					}
					if(ASectionID)me.delOneById(ASectionID);
				}
			}
			//修改操作授权
			me.addAuthorize(function(id){
				me.saveErrorCount = 0;
				me.saveCount = 0;
				me.saveLength = addArr.length;
				if(me.saveLength==0)me.fireEvent('save',me);
	            for(var i=0;i<addArr.length;i++){//新增操作授权对应小组
	            	me.addASection(id,addArr[i]);
	            }
            });
		});
	},
	/**获取状态列表*/
	getListData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.Id, obj.Name]);
		}
		return data;
	},
	
	isShowTime : function(bo){
		var me = this;
		me.getComponent('TimeRange').setVisible(bo);
		me.getComponent('Weeks').setVisible(!bo);
		me.getComponent('LisOperateAuthorize_BeginTime').setVisible(!bo);
		me.getComponent('LisOperateAuthorize_EndTime').setVisible(!bo);
		if(bo){
			me.getComponent('TimeRange').getComponent('BeginTime').isAllowBlank(false);
			me.getComponent('TimeRange').getComponent('EndTime').isAllowBlank(false);

			me.getComponent('LisOperateAuthorize_BeginTime').allowBlank = true;
			me.getComponent('LisOperateAuthorize_BeginTime').emptyText = "";
	        me.getComponent('LisOperateAuthorize_BeginTime').reset();  
	        me.getComponent('LisOperateAuthorize_EndTime').allowBlank = true;
			me.getComponent('LisOperateAuthorize_EndTime').emptyText = "";
	        me.getComponent('LisOperateAuthorize_EndTime').reset();  
		}else{
			me.getComponent('TimeRange').getComponent('BeginTime').isAllowBlank(true);
			me.getComponent('TimeRange').getComponent('EndTime').isAllowBlank(true);
			
			me.getComponent('LisOperateAuthorize_BeginTime').allowBlank = false;
			me.getComponent('LisOperateAuthorize_BeginTime').emptyText = "必填项";
	        me.getComponent('LisOperateAuthorize_BeginTime').reset();  
	        me.getComponent('LisOperateAuthorize_EndTime').allowBlank = false;
			me.getComponent('LisOperateAuthorize_EndTime').emptyText = "必填项";
	        me.getComponent('LisOperateAuthorize_EndTime').reset();  
		}
	},
	//根据授权操作Id和时间范围内找出已被授权的 操作授权对应小组
	getOperateASection:function(Id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		url += '&fields=LisOperateASection_Id,LisOperateASection_LBSection_CName,LisOperateASection_LBSection_Id';
		url+="&where=lisoperateasection.LisOperateAuthorize.Id="+Id;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//新增操作权限
	addAuthorize : function(callback){
		var me = this;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		me.showMask(me.saveText);//显示遮罩层
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
                callback(id);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//操作授权对应小组Lis_OperateASection
	addASection : function(id,SectionID){
		var me = this;
		var url =  JShell.System.Path.ROOT + me.addSUrl;
		var entity ={
			LisOperateAuthorize:{Id:id,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			LBSection:{Id:SectionID,DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		if(!entity) return;	
		me.showMask(me.saveText);//显示遮罩层
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,Ext.encode({entity:entity}),function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
				JShell.Msg.error(data.msg);
			}
			me.onSaveEnd(data);
		},false);
	},
	onSaveEnd:function(data){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			if (me.saveErrorCount == 0){
				me.fireEvent('save',me);
				
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},

	/**创建数据字段*/
	getStoreFields:function(){
		var me = this;
		var fields = me.callParent(arguments);
		fields.push('LisOperateAuthorize_Day0','LisOperateAuthorize_Day1','LisOperateAuthorize_Day2','LisOperateAuthorize_Day3','LisOperateAuthorize_Day4','LisOperateAuthorize_Day5','LisOperateAuthorize_Day6');
		return fields;
	},
	arrayRemove:function(array1,array2){
		var me = this;
		//临时数组存放
		var tempArray1 = [];//临时数组1
		var tempArray2 = [];//临时数组2
		for (var i = 0; i < array2.length; i++) {
		  tempArray1[array2[i]] = true;//将数array2 中的元素值作为tempArray1 中的键，值为true；
		}
		for (var i = 0; i < array1.length; i++) {
		  if (!tempArray1[array1[i]]) {
		    tempArray2.push(array1[i]);//过滤array1 中与array2 相同的元素；
		  }
		}
		return tempArray2;
	},
	delOneById: function(id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.delCount++;
			} else {
				me.delErrorCount++;
			}
			if (me.delCount + me.delErrorCount == me.delLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.delErrorCount == 0){
				}else{
					JShell.Msg.error(data.msg);
				}
			}
		});
	}
});