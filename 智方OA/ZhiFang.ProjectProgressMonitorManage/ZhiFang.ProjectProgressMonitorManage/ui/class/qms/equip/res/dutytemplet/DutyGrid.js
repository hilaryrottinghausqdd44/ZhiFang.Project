/**
 * 所有职责列表
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.dutytemplet.DutyGrid', {
	extend: 'Shell.class.qms.equip.res.manage.basic.Grid',
	title: '职责列表',
	/**默认加载数据*/
	defaultLoad: true,
	/**是否启用查询框*/
	hasSearch: true,


	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			dataIndex: 'EResponsibility_DispOrder',text: '次序',
			width: 55,align:'center',type:'int',	defaultRenderer: true
		});
		return columns;
	},
	changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('eresponsibility.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and eresponsibility.IsUse=1';
			}
		}else{
			me.defaultWhere = 'eresponsibility.IsUse=1';
		}
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});