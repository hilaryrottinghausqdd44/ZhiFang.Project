/**
 * 项目明细报表
 * @author liangyl	
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.report.item.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目明细报表',
    /**消息框消失时间*/
	hideTimes: 500,
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			save: function(p, values) {
			    var arr=[];                  
				if(values.rb) {
					 arr=arr.concat(values.rb);
					for (var i in me.Grid.columns) {
						if(i>0)	me.Grid.columns[i].hide();
						for (var j in arr) {
							if(me.Grid.columns[i].dataIndex==arr[j]){
								me.Grid.columns[i].show();
								break;
							}
						}
					}
				}
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				me.Grid.onSearch();
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.weixin.report.item.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.weixin.report.item.Form', {
			region: 'east',
			width:210,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		return [me.Grid,me.Form];
	}
});