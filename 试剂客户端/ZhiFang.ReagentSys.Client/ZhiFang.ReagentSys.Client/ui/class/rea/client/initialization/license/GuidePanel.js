Ext.define('Shell.class.rea.client.initialization.license.GuidePanel', {
	extend: 'Ext.panel.Panel',
	title: '机构授权向导',
	header: false,
	activeItem: 0,
	layout: 'card',
	defaults: {
		border: false
	},
	OrgObject:{},
	addUrl:'/ReaManageService.svc/ST_UDTO_AddCenOrgInitializeByStep',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//创建挂靠功能栏
		me.createDockedItems();

		me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.items = [{
			id: 'card-module',
			html: '<h1>功能模块信息初始化</h1><p></p>'
		}, {
			id: 'card-cenOrg',
			html: '<h1>机构信息初始化</h1>上一步:【功能模块信息初始化】初始化成功<p></p>'
		}, {
			id: 'card-dept',
			html: '<h1>机构管理部门初始化</h1>上一步:【机构信息初始化】初始化成功<p></p>'
		}, {
			id: 'card-employee',
			html: '<h1>系统管理员初始化</h1><p></p>'
		}, {
			id: 'card-user',
			html: '<h1>系统管理帐号初始化</h1><p></p>'
		}, {
			id: 'card-role',
			html: '<h1>管理角色信息初始化</h1><p></p>'
		}, {
			id: 'card-empRoles',
			html: '<h1>管理员角色权限初始化</h1><p></p>'
		}, {
			id: 'card-rolemodule',
			html: '<h1>角色模块权限初始化</h1><p></p>'
		}, {
			id: 'card-bparameter',
			html: '<h1>系统运行参数初始化</h1><p></p>'
		}, {
			id: 'card-barcodeformat',
			html: '<h1>系统条码规则信息初始化</h1><p></p>'
		}, {
			id: 'card-bdictType',
			html: '<h1>机构字典类型初始化</h1><p></p>'
		}];
	},
	createDockedItems: function() {
		var me = this;
		me.bbar = [{
				id: 'move_prev',
				text: '上一步',
				handler: function(btn) {
					me.onNavhandler(btn.up("panel"), "prev");
				},
				disabled: true
			},
			'->',
			{
				id: 'move_next',
				text: '下一步',
				handler: function(btn) {
					me.onNavhandler(btn.up("panel"), "next");
				}
			},{text:'确定',tooltip:'确定',iconCls:'button-accept',
				handler:function(){
					me.onSaveClick();
				}
			}
		];
	},
	onNavhandler: function(panel, direction) {
		var me = this;
		var layout = panel.getLayout();
		layout[direction]();
		var pre = Ext.getCmp('move_prev');
		var next = Ext.getCmp('move_next');
		var activeId = layout.activeItem.id;
		pre.setDisabled(!layout.getPrev());
		next.setDisabled(!layout.getNext());
	},
	getAddParams:function(){
		var me = this;
		var entity = {
			t1:'1'
		};
		
		var obj={cenOrg:me.OrgObject,entity:entity};
		return obj;
	},
	onSaveClick:function(){
		var me= this;
        var url = me.addUrl ;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params =me.getAddParams();
		
		if(!params) return;
		var obj = Ext.JSON.encode( params);
		me.showMask(JShell.Server.SAVE_TEXT);//显示遮罩层
		JShell.Server.post(url,obj,function(data){
			me.hideMask();
			if(data.success){
				me.fireEvent('save',p);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		me.body.unmask();//隐藏遮罩层
    	
	}
});