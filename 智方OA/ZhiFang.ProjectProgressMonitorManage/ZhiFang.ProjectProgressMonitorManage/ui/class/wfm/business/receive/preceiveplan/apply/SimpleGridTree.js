/**
 * 查询收款计划列表树
 * @author liangyl
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.apply.SimpleGridTree', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',

	title: '简单列表树',
	width: 300,
	height: 500,
    /**合同ID*/
	PContractID: null,
	/**默认加载数据*/
	defaultLoad: true,
	
	//=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.push({
			text: '状态',
			dataIndex: 'Status',
			width: 85,
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('PReceivePlanStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		});
		return columns;
	}
	
});