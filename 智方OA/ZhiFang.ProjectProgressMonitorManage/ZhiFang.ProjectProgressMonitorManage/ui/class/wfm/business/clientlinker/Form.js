/**
 * 联系人表单
 * @author liangyl
 * @version 2017-03-20
 */
Ext.define('Shell.class.wfm.business.clientlinker.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '联系人信息',
	width: 460,
	height: 395,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientLinkerById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPClientLinker',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientLinkerByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
    /**客户ID*/
	PClientID: null,
	 /**客户*/
	PClientName:null,
	/**布局方式*/
//	layout: 'anchor',
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 210,
		labelAlign: 'right'
	},
	/**显示成功信息*/
	showSuccessInfo: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var items = [{
			fieldLabel: '姓名',
			emptyText: '必填项',
			allowBlank: false,
			name: 'PClientLinker_Name',
			itemId: 'PClientLinker_Name',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '英文名称',
			name: 'PClientLinker_EName',
			itemId: 'PClientLinker_EName',
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '性别',
			name: 'PClientLinker_Sex',
			itemId: 'PClientLinker_Sex',
			emptyText: "请选择",
			xtype: 'uxSimpleComboBox',
            hasStyle: true,
			data: [
				['1', '男'],
				['2', '女']
			],
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '客户',
			name: 'PClientLinker_PClient',
			xtype: 'uxCheckTrigger',
			itemId: 'PClientLinker_PClient',
			className: 'Shell.class.wfm.client.CheckGrid',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '手机',
			name: 'PClientLinker_PhoneNum',
			itemId: 'PClientLinker_PhoneNum',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '生日',
			name: 'PClientLinker_Brithday',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '籍贯',
			name: 'PClientLinker_Birthplace',
			colspan: 1,
			width: me.defaults.width * 1
		},  {
			fieldLabel: '手机2',
			name: 'PClientLinker_PhoneNum2',
			itemId: 'PClientLinker_PhoneNum2',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '固定电话',
			name: 'PClientLinker_TelPhone',
			itemId: 'PClientLinker_TelPhone',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: 'Email',
			name: 'PClientLinker_Email',
			itemId: 'PClientLinker_Email',
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: 'QQ',
			name: 'PClientLinker_QQ',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: 'WeiXin',
			name: 'PClientLinker_WeiXin',
			itemId: 'PClientLinker_WeiXin',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '职务',
			name: 'PClientLinker_Postion',
			itemId: 'PClientLinker_Postion',
			colspan: 1,width: me.defaults.width * 1
		}, {
			fieldLabel: '是否使用',
			name: 'PClientLinker_IsUse',
			itemId: 'PClientLinker_IsUse',
			xtype:'checkbox',checked:true,
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '次序',
			name: 'PClientLinker_DispOrder',
			itemId: 'PClientLinker_DispOrder',
			xtype:'numberfield',value:0,
			emptyText:'必填项',allowBlank:false,
		    colspan: 2,width: me.defaults.width * 1
		}, {
			fieldLabel: '住址',
			name: 'PClientLinker_Address',
			itemId: 'PClientLinker_Address',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			xtype: 'textarea',
			fieldLabel: '备注',
			minHeight: 40,
			height: 60,
			itemId: 'PClientLinker_Comment',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '主键ID',
			name: 'PClientLinker_Id',
			hidden: true
		}, {
			fieldLabel: '客户ID',
			name: 'PClientLinker_PClient_Id',
			itemId: 'PClientLinker_PClient_Id',
			hidden: true
		}];

		return items;
	},
		/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		data.PClientLinker_Brithday = JShell.Date.getDate(data.PClientLinker_Brithday);

        //客户
		var	PClientID = me.getComponent('PClientLinker_PClient_Id'),
		PClient = me.getComponent('PClientLinker_PClient');
		if(data.PClientLinker_PClient_Id){
	        me.getClientName(data.PClientLinker_PClient_Id,function(data){
	        	if(data.value.list){
	        		PClient.setValue(data.value.list[0].PClient_Name);
	        	}
	        });
		}else{
			PClient.setValue('');
		}
		return data;
	},
	/**获取客户信息*/
	getClientName:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true';
		url += '&fields=PClient_Name&where=pclient.Id='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Name: values.PClientLinker_Name,
			EName: values.PClientLinker_EName,
			PhoneNum: values.PClientLinker_PhoneNum,
			Birthplace: values.PClientLinker_Birthplace,
			Address: values.PClientLinker_Address,
			PhoneNum2: values.PClientLinker_PhoneNum2,
			TelPhone: values.PClientLinker_TelPhone,
			Email: values.PClientLinker_Email,
			QQ: values.PClientLinker_QQ,
			WeiXin: values.PClientLinker_WeiXin,
			Postion: values.PClientLinker_Postion,
			DispOrder: values.PClientLinker_DispOrder,
			Comment:values.PClientLinker_Comment,
			IsUse:values.PClientLinker_IsUse ? true : false
		};
		if(values.PClientLinker_Brithday){
			entity.Brithday=JShell.Date.toServerDate(values.PClientLinker_Brithday);//签署日期
		}
		if(values.PClientLinker_Sex){
			entity.Sex=values.PClientLinker_Sex;
		}
		 //客户
		if(values.PClientLinker_PClient_Id){
			entity.PClient = {
				Id:values.PClientLinker_PClient_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams(),
			fieldsArr = [];
		var fields = ['PClient_Id', 'Name', 'EName','QQ','Postion',
			'Sex', 'PhoneNum', 'Brithday', 'Birthplace','WeiXin',
			'Address', 'PhoneNum2', 'Id','TelPhone','Email','DispOrder',
			'Comment','IsUse'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.PClientLinker_Id;
		return entity;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
		var	PClientID = me.getComponent('PClientLinker_PClient_Id'),
			PClient = me.getComponent('PClientLinker_PClient');
		if(!PClient) return;
		PClient.on({
			check: function(p, record) {
				PClient.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			this.getForm().reset();
			var	PClientID = me.getComponent('PClientLinker_PClient_Id'),
			    PClient = me.getComponent('PClientLinker_PClient');
			if(me.PClientID.toString()!="-1"){
			    PClientID.setValue(me.PClientID);   
			    PClient.setValue(me.PClientName); 
			}
		}else{
			me.getForm().setValues(me.lastData);
		}
	}
	
});