/**
 * 质量记录参数
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.eparameter.Form', {
	extend: 'Shell.ux.form.Panel',
	bodyPadding:10,
    layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 200,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
//	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEParameterById?isPlanish=true',
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEParameterByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/QMS_UDTO_AddEParameter',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/QMS_UDTO_AddEParameter',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	title: "质量记录参数信息",
	formtype: 'edit',
	/**数据主键*/
	PK:'',
	ParaObj:{},
	header:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
	    me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		return [{
			fieldLabel: '参数名称',name: 'EParameter_CName',
			itemId: 'EParameter_CName',hidden:true,
			colspan :1,width : me.defaults.width * 1
		}, {
			fieldLabel: '参数类型',name: 'EParameter_ParaType',
			itemId: 'EParameter_ParaType',hidden:true,
			colspan :1,width : me.defaults.width * 1
		}, {
			fieldLabel: '参数编号',name: 'EParameter_ParaNo',
			itemId: 'EParameter_ParaNo',hidden:true,
			colspan :1,width : me.defaults.width * 1
		},  {
			fieldLabel: '参数值',name: 'EParameter_ParaValue',
			itemId: 'EParameter_ParaValue',height:160,
			colspan :2,
			width : me.defaults.width * 2,
			xtype:'textarea'
		}, {
			fieldLabel: '参数说明',name: 'EParameter_ParaDesc',
			itemId: 'EParameter_ParaDesc',height:100,
			colspan :2,
			width : me.defaults.width * 2,
			xtype:'textarea'
		},{
			fieldLabel: '显示次序',name: 'EParameter_DispOrder',
			itemId: 'EParameter_DispOrder',xtype:'numberfield',
			colspan :2,width : me.defaults.width *2
		},  {
			xtype: "checkbox",boxLabel: "",inputValue: "true",
			uncheckedValue: "false",checked: true,
			labelWidth: 115,itemId: "EParameter_IsUse",name: "EParameter_IsUse",
			fieldLabel: "是否使用"
			,colspan :1,hidden:true,
			width : me.defaults.width * 1
		},{
			xtype: "checkbox",boxLabel: "",inputValue: "true",
			uncheckedValue: "false",checked: true,
			labelWidth: 115,itemId: "EParameter_IsUserSet",name: "EParameter_IsUserSet",
			fieldLabel: "是否允许用户设置",
			colspan :1,hidden:true,
			width : me.defaults.width * 1
		},{
			fieldLabel: 'id',name: 'EParameter_Id',	hidden:true,itemId: 'EParameter_Id'
		}];
	},
	/**根据主键ID加载数据*/
	load:function(id){
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		if(!id) return;
		
		me.ParaObj.PK = id;//面板主键
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}

    	me.showMask(me.loadingText);//显示遮罩层
    	url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
    	url += (url.indexOf('?') == -1 ? "?" : "&" ) + "where=eparameter.ParaNo='" + id+"'";
    	url += '&fields=' + me.getStoreFields().join(',');
    	
    	JShell.Server.get(url,function(data){
    		me.hideMask();//隐藏遮罩层
    		if(data.success){
    			if(data.value){
    				data.value = JShell.Server.Mapping(data.value);
    				me.lastData = me.changeResult(data.value);
                    me.getComponent('EParameter_ParaValue').setValue(me.lastData.EParameter_ParaValue);
					me.getComponent('EParameter_ParaDesc').setValue(me.lastData.EParameter_ParaDesc);
					me.getComponent('EParameter_IsUse').setValue(me.lastData.EParameter_IsUse);
					me.getComponent('EParameter_IsUserSet').setValue(me.lastData.EParameter_IsUserSet);
					me.getComponent('EParameter_DispOrder').setValue(me.lastData.EParameter_DispOrder);
                }else{
                	me.clearData();
                	me.isAdd(me.ParaObj.PK);
                }
    		}else{
    			JShell.Msg.error(data.msg);
    		}
    		me.fireEvent('load',me,data);
    	});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		
		if( data && data.list.length>0){
			result.EParameter_IsUse=data.list[0].EParameter_IsUse =='true'?true:false;
			result.EParameter_IsUserSet=data.list[0].EParameter_IsUserSet=='true'?true:false;
			result.EParameter_ParaValue=data.list[0].EParameter_ParaValue;
			result.EParameter_ParaDesc=data.list[0].EParameter_ParaDesc;
			result.EParameter_DispOrder=data.list[0].EParameter_DispOrder;
		}else{
			me.clearData();
			me.isAdd(me.ParaObj.PK);
		}
	
		return result;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsUserSet: values.EParameter_IsUserSet.toString() == "true" ? 1 : 0,
			IsUse:values.EParameter_IsUse.toString() == "true" ? 1 : 0
		};
		//参数值
		if(values.EParameter_ParaValue) {
			entity.ParaValue = values.EParameter_ParaValue.replace(/\\/g, '&#92');
			entity.ParaValue = entity.ParaValue.replace(/[\r\n]/g, '<br />');
		}
		//概要
		if(values.EParameter_ParaDesc) {
			entity.ParaDesc = values.EParameter_ParaDesc.replace(/\\/g, '&#92');
			entity.ParaDesc = entity.ParaDesc.replace(/[\r\n]/g, '<br />');
		}
		//类型
		if(me.ParaObj.ParaType) {
			entity.ParaType = me.ParaObj.ParaType;
		}
        //名称
		if(me.ParaObj.ParaName) {
			entity.CName = me.ParaObj.ParaName;
		}
		//名称
		if(values.EParameter_DispOrder) {
			entity.DispOrder = values.EParameter_DispOrder;
		}
		//ParaNo
		if(me.ParaObj.PK) {
			entity.ParaNo = me.ParaObj.PK;
		}
		return {entity:entity};
	},
	onSaveClick:function(){
		var me = this;
		var params =me.getAddParams();
		if(!params) return;
		params = Ext.JSON.encode(params);
		var url = JShell.System.Path.getRootUrl(me.editUrl);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
			    JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 2000);
				me.IsTrue = true;
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	
	/**更改标题*/
	changeTitle: function() {}
});