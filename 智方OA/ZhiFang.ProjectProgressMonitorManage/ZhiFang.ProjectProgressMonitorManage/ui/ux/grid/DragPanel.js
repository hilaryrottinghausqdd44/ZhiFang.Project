/**
 * 基础拖动列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.DragPanel',{
    extend:'Shell.ux.grid.Panel',
    alias:'widget.uxGridDragPanel',
    
	initComponent:function(){
		var me = this;
		me.initEvents('afterdrop');
		me.viewConfig = me.viewConfig || {};
		me.viewConfig.plugins = {
			ptype: 'gridviewdragdrop',
			dragText:'{0}条数据正在拖动',
			dragGroup: 'firstGridDDGroup',
			dropGroup: 'firstGridDDGroup'
		};
		me.viewConfig.listeners = {
			drop: function(node, data, dropRec, dropPosition) {
				me.fireEvent('afterdrop',me,node, data, dropRec, dropPosition);
			}
		};
		
		me.callParent(arguments);
	}
});