/**
 * 标准项目维护
 * @author liangyl
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.standard.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '标准项目信息',
	width: 350,
	height: 280,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProject',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectByField',
    /**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 80,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**项目类型*/
	ProjectType:'ProjectType',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		  //项目类型默认查询条件
		  var TypedefaultWhere='',TypeID='';
		  me.getTypeById(function(data){
        	if(data.value.list){
        		TypeID='';
        		for(var i=0;i<data.value.list.length;i++){
        			var id=data.value.list[i].TypeID;
        			if(i>0){
        				TypeID+=","+data.value.list[i].TypeID;
        			}else{
        				TypeID=data.value.list[i].TypeID;
        			}
        		}
        	}
        });
        var TypedefaultWhere="pdict.BDictType.DictTypeCode='" + this.ProjectType+"'";
        if(TypeID){
        	TypedefaultWhere=TypedefaultWhere+" and pdict.Id not in("+TypeID +")";
        }
		
		items.push( {
			fieldLabel: '项目名称',
			name: 'PProject_CName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: "项目类型",
			name: 'PProject_Type',
			itemId: 'PProject_Type',
			emptyText: "项目类型",
			xtype: 'uxCheckTrigger',
			allowBlank: false,
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '项目类型选择',
				defaultWhere: TypedefaultWhere
			}
		}, {
			fieldLabel: '项目类型',
			name: 'PProject_TypeID',
			itemId: 'PProject_TypeID',
			hidden: true
		}, {
			fieldLabel: '标准总工作量',
			name: 'PProject_EstiWorkload',
			itemId: 'PProject_EstiWorkload',
			xtype:'numberfield',value:0,
			emptyText:'必填项',allowBlank:false,
			labelAlign: 'right'
		},{
			fieldLabel: '显示次序',
			name: 'PProject_DispOrder',
			itemId: 'PProject_DispOrder',
			xtype:'numberfield',value:0,
			labelAlign: 'right'
		},{
			height: 85,
			fieldLabel: '备注',
			emptyText: '备注',
			name: 'PProject_Memo',
		    itemId: 'PProject_Memo',
			xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',
			name: 'PProject_Id',
			hidden: true
		});
		return items;
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		//项目类型
		var PProject_Type = me.getComponent('PProject_Type'),
			PProject_TypeID = me.getComponent('PProject_TypeID');

		if(PProject_Type) {
			PProject_Type.on({
				check: function(p, record) {
					PProject_Type.setValue(record ? record.get('BDict_CName') : '');
					PProject_TypeID.setValue(record ? record.get('BDict_Id') : '');
					p.close();
				}
			});
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			EstiWorkload: values.PProject_EstiWorkload,
			CName: values.PProject_CName,
			IsUse:1,
			IsStandard:1,
		};
		if (values.PProject_DispOrder){
			entity.DispOrder = values.PProject_DispOrder;
		}
		
		if(values.PProject_TypeID) {
			entity.TypeID = values.PProject_TypeID;
		}
		entity.CreatEmpIdID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(values.PProject_Memo) {
			entity.Memo = values.PProject_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'EstiWorkload', 'Id', 'CName', 'TypeID','Memo','DispOrder'
		];
		entity.fields = fields.join(',');
		if(values.PProject_Id != '') {
			entity.entity.Id = values.PProject_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		var reg = new RegExp("<br />", "g");
		if(data.Memo) data.Memo = data.Memo.replace(reg, "\r\n");
		
	    	//银行种类
		var PProject_Type = me.getComponent('PProject_Type'),
			PProject_TypeID = me.getComponent('PProject_TypeID');
		
		if(data.PProject_TypeID){
	        me.getProjectType(data.PProject_TypeID,function(data){
	        	if(data.value.list){
	        		PProject_Type.setValue(data.value.list[0].BDict_CName);
	        	}
	        });
		}else{
			PProject_Type.setValue('');
		}
		return data;
	},
	/**获取银行种类信息*/
	getProjectType:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true';
		url += '&fields=BDict_CName,BDict_Id&where=pdict.Id='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		me.isValidProjectType(function(){
				var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
			url = JShell.System.Path.getRootUrl(url);
			
			var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
			
			if(!params) return;
			
			var id = params.entity.Id;
			
			params = Ext.JSON.encode(params);
			
			me.showMask(me.saveText);//显示遮罩层
			me.fireEvent('beforesave',me);
			JShell.Server.post(url,params,function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object'){
						id = id.id;
					}
					if(me.isReturnData){
						me.fireEvent('save',me,me.returnData(id));
					}else{
						me.fireEvent('save',me,id);
					}
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					me.fireEvent('saveerror',me);
					JShell.Msg.error(data.msg);
				}
			});	
		});
	
	},
	/**校验项目类型是否重复*/
	isValidProjectType:function(callback,TypeID){
		var me = this,
			values = me.getForm().getValues(),
			url = '/SingleTableService.svc/ST_UDTO_SearchPProjectByHQL';
		
		url = JShell.System.Path.getRootUrl(url);
		var where = "pproject.IsStandard=1 and pproject.TypeID='" + values.PProject_TypeID + "'";
		//修改时判断合同编号是否重复
		if(me.formtype=='edit'){
			where +=' and pproject.Id!='+values.PProject_Id;
		}
		url += '?fields=Id&where=' + where;
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.count > 0){
					JShell.Msg.error( "的项目类型已经存在，请换一个项目类型！");
				}else{
					callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取还未使用的项目类型*/
	getTypeById:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchPProjectByHQL?isPlanish=false';
		url += '&fields=TypeID&where=pproject.IsStandard=1';
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false,200);
	}
});