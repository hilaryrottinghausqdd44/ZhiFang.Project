/**
 * 指定日期
 * @author liangyl
 * @version 2017-1-5
 */
Ext.define('Shell.class.qms.equip.templet.admin.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'模板删除指定日期',
    width:250,
    height:135,
    bodyPadding:10,
	
	/**显示成功信息*/
	showSuccessInfo:false,
	/**获取数据服务路径*/
    selectUrl:'',
    /**新增服务地址*/
    addUrl:'',
    /**修改服务地址*/
    editUrl:'',
     /**删除模板数据*/
	delUrl:'/QMSReport.svc/QMS_UDTO_DelETempletData',
    /**布局方式*/
	layout: 'anchor',
    /**每个组件的默认属性*/
    defaults:{labelWidth:60,width:210,labelAlign:'right'},
      /**模板ID*/
    ETempletId:null,
    
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
        me.buttonToolbarItems = ['->'];
		me.buttonToolbarItems.push({
			text:'确定',
			iconCls:'button-accept',
			tooltip:'确定',
			handler:function(){
				me.onDelClick();
			}
		});
        me.callParent(arguments);
    },
     /**删除 */
    onDelClick:function(){
    	var me=this;
    	if(!me.getForm().isValid()) return;
			if(!me.ETempletId) return;
		    var	values = me.getForm().getValues();
	        var StartDate = values.StartDate;
	        var EndDate = values.EndDate;
	        
			Ext.MessageBox.show({
				title: '操作确认消息',
				msg: '要删除的质量记录数据无法恢复，确认删除？',
				buttons: Ext.MessageBox.OKCANCEL,
				fn: function(btn) {
					if(btn == 'ok') {
						var	url = me.delUrl;
						url += '?templetID=' + me.ETempletId+'&beginDate='+StartDate+'&endDate='+EndDate;
						url = JShell.System.Path.getRootUrl(url);
						JShell.Server.get(url, function(data) {
							if(data.success) {
								me.fireEvent('save',me);
							} else {
								var msg = data.msg;
								JShell.Msg.error(msg);
							}
						});
					}
				},
				icon: Ext.MessageBox.QUESTION
			});
    },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;

		var items = [{
			fieldLabel:'开始日期',name:'StartDate',
			emptyText:'必填项',allowBlank:false,itemId:'StartDate',
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'结束日期',name:'EndDate',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',itemId:'EndDate',
			colspan:1,width:me.defaults.width * 1	
		}];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			//设置默认值
			var Sysdate = JcallShell.System.Date.getDate();
		    var date1 = JcallShell.Date.toString(Sysdate, true);
			var StartDate = me.getComponent('StartDate');
			var EndDate = me.getComponent('EndDate');
			StartDate.setValue(date1);
			EndDate.setValue(date1);
		} else {
			me.getForm().setValues(me.lastData);
		}
	}

});