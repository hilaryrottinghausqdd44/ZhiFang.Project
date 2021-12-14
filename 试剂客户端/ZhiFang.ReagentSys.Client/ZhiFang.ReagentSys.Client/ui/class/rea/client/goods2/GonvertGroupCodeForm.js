/**
 * 设置相同码
 * @author liangyl	
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.goods2.GonvertGroupCodeForm', {
	extend: 'Shell.ux.form.Panel',
	title: '设置换算组代码',
	width: 240,
	height: 100,
	/**内容周围距离*/
	bodyPadding:'10px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '',
	/**新增服务地址*/
	addUrl: '',
	/**修改服务地址*/
	editUrl: '',
	/**布局方式*/
	layout: 'anchor',
	idList:null,
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**更新相同码*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateGonvertGroupCode',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏
	 * */
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push('->',{
			text:'确定',tooltip:'确定',iconCls:'button-accept',
			handler:function(btn){
				if(!me.getForm().isValid()) {
					me.fireEvent('isValid', me);
					return;
				}
				var msg = '您确定要对选中行设置同一换算组代码吗？';
				Ext.MessageBox.show({
					title: '操作确认消息',
					msg: msg,
					icon: Ext.Msg.QUESTION,
					buttons: Ext.MessageBox.OKCANCEL,
					fn: function(btn) {
						if(btn == 'ok') {
							me.onsaveinfo();
						}
					}
				})
			    
			}
		},{
			text:'取消',tooltip:'取消',iconCls:'button-cancel',
			handler:function(btn){
			    me.close();
			}
		});
		if(items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:false
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push( {
			fieldLabel: '换算组代码',
			name: 'GonvertGroupCode',
			emptyText: '必填项',
			allowBlank: false
		});
		return items;
	},
	onsaveinfo: function() {
		var me=this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
	    var values = me.getForm().getValues();
	
	    me.showMask(me.loadingText);//显示遮罩层
	    var GonvertGroupCode=values.GonvertGroupCode;
	    var where="?Code="+GonvertGroupCode+"&idList="+me.idList;
        url+=where;
	    
	    JShell.Server.get(url,function(data) {
	    	me.hideMask();//隐藏遮罩层
			if (data.success) {
				 me.fireEvent('save',me);
			} else {
				JShell.Msg.error(data.msg);
			}
		},false);
	}
	
});