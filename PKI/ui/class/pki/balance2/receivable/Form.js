/**
 * 设置应收价
 * @author liangyl	
 * @version 2017-11-10
 */
Ext.define('Shell.class.pki.balance2.receivable.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '设置应收价',
	width: 230,
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
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**设置应收价*/
	SaveUrl:'/StatService.svc/Stat_UDTO_EditItemPrice',
	/**ID */
	idList:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	/**创建功能按钮栏
	 
	 * */
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		var msg = '您确定要对选中行设置应收价吗？';
		items.push('->',{
			text:'确定',tooltip:'确定',iconCls:'button-accept',
			handler:function(btn){
				Ext.MessageBox.show({
				title: '操作确认消息',
				msg: msg,
				icon: Ext.Msg.QUESTION,
				buttons: Ext.MessageBox.OKCANCEL,
					fn: function(btn) {
						if(btn == 'ok') {
							me.saveInfo();
						}
					}
				});	
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
			fieldLabel: '应收价',
			name: 'itemPrice',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		});
		return items;
	},
	/**@overwrite 设置应收价*/
	saveInfo: function() {
		var me=this;
		var values = me.getForm().getValues();
		var url = (me.SaveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.SaveUrl;
	    var values = me.getForm().getValues();
	    if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
	    me.showMask(me.loadingText);//显示遮罩层
       var entity={
      	   idList: me.idList,
	       itemPrice: values.itemPrice,
	       itemMemo: ''
       };
	    var params = Ext.JSON.encode(entity);
        var ErrorMsg='';
		JShell.Server.post(url, params, function(data) {
	    	me.hideMask();//隐藏遮罩层
			if (data.success) {
			    me.fireEvent('save',me);
			} else {
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});