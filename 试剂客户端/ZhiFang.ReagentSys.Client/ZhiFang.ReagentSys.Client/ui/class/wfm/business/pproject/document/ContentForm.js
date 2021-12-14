/**
 * 项目文档维护
 * @author liangyl	
 * @version 2017-04-21
 */
Ext.define('Shell.class.wfm.business.pproject.document.ContentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '项目文档',

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectDocumentById?isPlanish=true',
	/**新增服务地址*/
	addUrl:'/SingleTableService.svc/ST_UDTO_AddPProjectDocument',
	/**修改服务地址*/
    editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectDocumentByField',

	width: 1366,
	height: 400,
	formtype: "add",
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**开启加载数据遮罩层*/
	hasLoadMask: false,
	autoScroll: false,
	layout: 'fit',

	bodyPadding: '0px 0px 0px 0px',
	ProjectID:null,
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//解决在线编辑器换行出现滚动条后工具栏会被隐藏,需要手工调整高度,工具栏才不会被隐藏
		setTimeout(function() {
			me.setHeight(me.height - 1);
		}, 100);
	},
	setHeight: function(height) {
		var me = this;
		//if(height) height = height < 120 ? 120 : height;
		return me.setSize(undefined, height);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//me.buttonToolbarItems = ['->', 'save', 'reset'];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight * 0.725;
		height = (height > 395 ? height : 395);

		items = [ {
			name: 'PProjectDocument_Content',
			itemId: 'PProjectDocument_Content',
			//margin: '5px 0px 2px 0px',//上右下左
			xtype: 'ueditor',
			width: '100%',
			height: height,
			autoScroll: true,
			border: false
		},{
			fieldLabel:'主键ID',name:'PProjectDocument_Id',hidden:true
		}];
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var entity={};
		if(values.PProjectDocument_Content) {
			entity.Content = values.PProjectDocument_Content.replace(/\\/g, '&#92');
		}
	   
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = ['Id','Content'];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PProjectDocument_Id;
		return entity;
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});